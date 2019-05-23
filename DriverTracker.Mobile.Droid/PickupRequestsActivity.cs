
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

namespace DriverTracker.Mobile.Droid
{

    [Activity(Label = "PickupRequestsActivity")]
    public class PickupRequestsActivity : Activity
    {

        static readonly List<DriverTracker.Mobile.PickupRequest> pickupRequests = new List<DriverTracker.Mobile.PickupRequest>();
        static readonly IServerConnectionStore connectionStore = new AndroidServerConnectionStore();
        static readonly string pickupRequestsPath = "/api/pickuprequestapi";

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PickupRequests);

            ListView listView = FindViewById<ListView>(Resource.Id.PickupRequestListView);
            ArrayAdapter<PickupRequest> adapter = new ArrayAdapter<PickupRequest>(this, Resource.Id.PickupRequestListView, pickupRequests);
            listView.Adapter = adapter;

            ServerConnection connection = connectionStore.CurrentConnection;
            string host = connection.Host;

            // retrieve list
#if DEBUG
            using (HttpClientHandler handler = new BypassSslValidationClientHandler())
#else
            using (HttpClientHandler handler = new HttpClientHandler()) 
#endif
            using (HttpClient client = new HttpClient(handler))
            {
                if (connection.IsAuthenticated)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", connection.Jwt);

                await AttemptRetrievePickupRequests(host, client);
            }
        }

        private async Task AttemptRetrievePickupRequests(string host, HttpClient client)
        {
            if (!connectionStore.CurrentConnection.IsAuthenticated)
            {
                AuthenticateUser();
                return;
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
                            alert.SetMessage("Could not connect to server: " + ex.Message);
                            _ = alert.SetPositiveButton("Try Again", async (senderAlert, args) => await AttemptRetrievePickupRequests(host, client));
                            _ = alert.SetNegativeButton("Cancel", (senderAlert, args) => { });

                            Dialog dialog = alert.Create();
                            dialog.Show();
                        }
                    }
                    else throw ex;
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

        static readonly int AUTHENTICATE_REQUEST = 1;
        static readonly int RESULT_OK = 0;

        /// <summary>
        /// Authenticates the user via the AuthenticateActivity.
        /// </summary>
        /// <returns>The task</returns>
        private void AuthenticateUser()
        {
            Intent intent = new Intent(this, typeof(AuthenticateActivity));
            StartActivityForResult(intent, AUTHENTICATE_REQUEST);

        }

        protected async void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (requestCode == AUTHENTICATE_REQUEST)
            {
                if (resultCode == RESULT_OK)
                {

                    ServerConnection connection = connectionStore.CurrentConnection;

                    await connection.Authenticate(data.GetStringExtra("user"), data.GetStringExtra("pass"), new AndroidAuthenticationService());

#if DEBUG
                    using (HttpClientHandler handler = new BypassSslValidationClientHandler())
#else
                    using (HttpClientHandler handler = new HttpClientHandler()) 
#endif
                    using (HttpClient client = new HttpClient(handler))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", connection.Jwt);
                        await AttemptRetrievePickupRequests(connection.Host, client);
                    }
                }
            }
        }
    }
}
