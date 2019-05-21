
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
using Javax.Net.Ssl;
using Xamarin.Android.Net;
using Android.Net;

namespace DriverTracker.Mobile.Droid
{
#if DEBUG
    // from https://softwareproduction.eu/2017/08/26/xamarin-on-android-bypass-ssl-verification-with-httpclient/
    internal class BypassHostnameVerifier : Java.Lang.Object, IHostnameVerifier
    {
        public bool Verify(string hostname, ISSLSession session)
        {
            return true;
        }
    }

    internal class BypassSslValidationClientHandler : AndroidClientHandler
    {
        protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
        {
            return SSLCertificateSocketFactory.GetInsecure(1000, null);
        }

        protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
        {
            return new BypassHostnameVerifier();
        }
    }
#endif

    [Activity(Label = "PickupRequestsActivity")]
    public class PickupRequestsActivity : Activity
    {

        static readonly List<DriverTracker.Mobile.PickupRequest> pickupRequests = new List<DriverTracker.Mobile.PickupRequest>();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PickupRequests);

            string host = GetString(Resource.String.host);
            string pickupRequestsPath = "/api/pickuprequestapi";
            ListView listView = FindViewById<ListView>(Resource.Id.PickupRequestListView);
            ArrayAdapter<PickupRequest> adapter = new ArrayAdapter<PickupRequest>(this, Resource.Id.PickupRequestListView, pickupRequests);
            listView.Adapter = adapter;

            // retrieve list
#if DEBUG
            using (HttpClientHandler handler = new BypassSslValidationClientHandler())
#else
            using (HttpClientHandler handler = new HttpClientHandler()) 
#endif
                using (HttpClient client = new HttpClient(handler))
                {
                    bool successfulOrCancelledRequest = false;
                    int tryCount = 0;
                    while (!successfulOrCancelledRequest)
                    {
                        try
                        {
                            string responseBody = await client.GetStringAsync("https://" + host + pickupRequestsPath);

                            var newRequestsObj = JsonConvert.DeserializeAnonymousType(responseBody, new { PickupRequests = new List<PickupRequest>() });
                            pickupRequests.Clear();
                            pickupRequests.AddRange(newRequestsObj.PickupRequests);
                            successfulOrCancelledRequest = true;
                        }
                        catch (Exception ex)
                        {
                            if (ex is System.Net.WebException || ex is System.Net.Http.HttpRequestException)
                            {
                                tryCount++;

                                // try 3 times before showing alert
                                if (tryCount == 3)
                                {
                                    // show alert dialog
                                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                                    alert.SetTitle("Cannot Connect");
                                    alert.SetMessage("Could not connect to server: " + ex.Message);
                                    alert.SetPositiveButton("Try Again", (senderAlert, args) => { tryCount = 0; });
                                    alert.SetNegativeButton("Cancel", (senderAlert, args) => { successfulOrCancelledRequest = true; });

                                    Dialog dialog = alert.Create();
                                    dialog.Show();
                                }
                            }
                            else throw ex;
                        }
                    }
                }

        }
    }
}
