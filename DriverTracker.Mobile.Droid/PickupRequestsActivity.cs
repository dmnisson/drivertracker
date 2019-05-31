
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DriverTracker.Mobile;
using Newtonsoft.Json;

using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Android.Util;
using Autofac;

namespace DriverTracker.Mobile.Droid
{

    [Activity(Label = "PickupRequestsActivity")]
    public class PickupRequestsActivity : Activity
    {

        static readonly List<PickupRequest> pickupRequests = new List<PickupRequest>();
        static IServerConnectionStore connectionStore;
        static readonly string pickupRequestsPath = "/api/pickuprequestapi";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // obtain the connection store object
            if (connectionStore == null)
            {
                using (ILifetimeScope scope = DriverTrackerApp.Container.BeginLifetimeScope())
                {
                    IServerConnectionStoreFactory factory = scope.Resolve<IServerConnectionStoreFactory>(
                        new NamedParameter("AppContext", this));
                    connectionStore = factory.CreateConnectionStore();
                }
            }

            SetContentView(Resource.Layout.PickupRequests);

            // build gui
            ListView listView = FindViewById<ListView>(Resource.Id.PickupRequestListView);
            ArrayAdapter<PickupRequest> adapter = new ArrayAdapter<PickupRequest>(this, Resource.Id.PickupRequestListView, pickupRequests);
            listView.Adapter = adapter;

            // try to retrieve list
            string app_name = Resources.GetString(Resource.String.app_name);
            AttemptRetrievePickupRequests().FireAndForgetSafeAsync(new LogErrorHandler(app_name));
        }

        private async Task AttemptRetrievePickupRequests()
        {
            ServerConnection connection = connectionStore.CurrentConnection;

            if (connection == null)
            {
                InputServerConnection();
                return;
            }

            string host = connection.Host;

#if DEBUG
            using (HttpClientHandler handler = new BypassSslValidationClientHandler())
#else
            using (HttpClientHandler handler = new HttpClientHandler()) 
#endif
            using (HttpClient client = new HttpClient(handler))
            {
                if (!connection.IsAuthenticated)
                {
                    AuthenticateUser();
                    return;
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", connection.Jwt);

                // make sure token is refreshed before it expires
                IAuthenticationService authenticationService = new AndroidAuthenticationService
                {
                    Host = connectionStore.CurrentConnection.Host
                };
                IDictionary<string, object> payload = JWT.JsonWebToken.DecodeToObject(connection.Jwt, "", false) as IDictionary<string, object>;
                if (payload.ContainsKey("exp") && payload["exp"] != null)
                {
                    int exp = Convert.ToInt32(payload["exp"]);
                    var secondsSinceEpoch = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
                    await authenticationService.SetRefreshInterval(connection,
                        (exp - (int)secondsSinceEpoch) / 2);
                }

                bool successfulOrCancelledRequest = false;
                int tryCount = 0;
                while (!successfulOrCancelledRequest)
                {
                    try
                    {
                        successfulOrCancelledRequest = await RetrievePickupRequests(host, client);
                    }
                    catch (Exception ex)
                    {
                        if (ex is System.Net.WebException || ex is System.Net.Http.HttpRequestException)
                        {
                            tryCount++;

                            // try 3 times before showing alert
                            if (tryCount == 3)
                            {
                                // cancel request until user responds
                                successfulOrCancelledRequest = true;

                                // show alert dialog
                                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                                alert.SetTitle("Cannot Connect");
#if DEBUG
                                alert.SetMessage("Could not connect to server: " + ex.Message);
#else
                                alert.SetMessage("Could not connect to server");
#endif
                                string app_name = Resources.GetString(Resource.String.app_name);
                                _ = alert.SetPositiveButton("Try Again", (senderAlert, args) => 
                                    AttemptRetrievePickupRequests().FireAndForgetSafeAsync(new LogErrorHandler(app_name)));
                                _ = alert.SetNegativeButton("Cancel", (senderAlert, args) => { });

                                Dialog dialog = alert.Create();
                                dialog.Show();
                            }
                        }
                    }
                }
            }
        }

        private static async Task<bool> RetrievePickupRequests(string host, HttpClient client)
        {
            string responseBody = await client.GetStringAsync("https://" + host + pickupRequestsPath);

            var newRequestsObj = JsonConvert.DeserializeAnonymousType(responseBody, new { PickupRequests = new List<PickupRequest>() });
            pickupRequests.Clear();
            pickupRequests.AddRange(newRequestsObj.PickupRequests);
            bool successfulOrCancelledRequest = true;
            return successfulOrCancelledRequest;
        }

        const int AUTHENTICATE_REQUEST = 1;
        const int SERVERCONNECTION_REQUEST = 2;

        /// <summary>
        /// Authenticates the user via the AuthenticateActivity.
        /// </summary>
        private void AuthenticateUser()
        {
            Intent intent = new Intent(this, typeof(AuthenticateActivity));
            intent.PutExtra("hostname", connectionStore.CurrentConnection.Host);
            StartActivityForResult(intent, AUTHENTICATE_REQUEST);

        }

        /// <summary>
        /// Starts the ChooseStoredServer activity to obtain connection information.
        /// </summary>
        private void InputServerConnection()
        {
            Intent intent = new Intent(this, typeof(ChooseStoredServerActivity));
            StartActivityForResult(intent, SERVERCONNECTION_REQUEST);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            string app_name = Resources.GetString(Resource.String.app_name);

            if (requestCode == AUTHENTICATE_REQUEST)
            {
                {
                    if (resultCode == Result.Ok)
                    {
                        connectionStore.CurrentConnection.Jwt = data.GetStringExtra("token");

                        AttemptRetrievePickupRequests().FireAndForgetSafeAsync(new LogErrorHandler(app_name));
                    }
                }
            }
            else if (requestCode == SERVERCONNECTION_REQUEST)
            {
                {
                    if (resultCode == Result.Ok)
                    {

                        AttemptRetrievePickupRequests().FireAndForgetSafeAsync(new LogErrorHandler(app_name));
                    }
                }
            }
        }

    }
}
