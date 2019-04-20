using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace DriverTracker.Mobile.Droid
{
    [Activity(Label = "DriverTracker", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        static int NumOfUnansweredPickupRequests = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            Button pickupRequestsButton = FindViewById<Button>(Resource.Id.PickupRequestsButton);

            pickupRequestsButton.Click +=  (sender, e) => {
                Intent intent = new Intent(this, typeof(PickupRequestsActivity));
                StartActivity(intent);
            };
        }
    }
}

