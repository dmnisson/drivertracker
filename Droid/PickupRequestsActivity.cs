
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Newtonsoft.Json;

namespace DriverTracker.Mobile.Droid
{
    [Activity(Label = "PickupRequestsActivity")]
    public class PickupRequestsActivity : Activity
    {

        static readonly List<DriverTracker.Mobile.PickupRequest> pickupRequests = new List<DriverTracker.Mobile.PickupRequest>();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PickupRequests);

            string host = GetString(Resource.String.host);
            string pickupRequestsPath = "/api/pickuprequestsapi";
            ListView listView = FindViewById<ListView>(Resource.Id.PickupRequestListView);
            ArrayAdapter<PickupRequest> adapter = new ArrayAdapter<PickupRequest>(this, Resource.Id.PickupRequestListView, pickupRequests);
            listView.Adapter = adapter;

            // retrieve list
            using (HttpClient client = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler()))
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
                    catch (HttpRequestException ex)
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
                }
            }

        }
    }
}
