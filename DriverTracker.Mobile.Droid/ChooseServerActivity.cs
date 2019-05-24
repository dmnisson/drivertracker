
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

namespace DriverTracker.Mobile.Droid
{
    [Activity(Label = "ChooseServerActivity")]
    public class ChooseServerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ChooseServer);

            Button continueChooseServerButton = FindViewById<Button>(Resource.Id.continueChooseServerButton);
            EditText companyNameField = FindViewById<EditText>(Resource.Id.companyNameField);
            EditText hostnameField = FindViewById<EditText>(Resource.Id.hostnameField);

            // populate field values if data sent to this activity
            companyNameField.Text = Intent.GetStringExtra("companyName");
            hostnameField.Text = Intent.GetStringExtra("hostname");

            continueChooseServerButton.Click += (sender, e) => {
                Intent intent = new Intent();
                intent.PutExtra("companyName", companyNameField.Text);
                intent.PutExtra("hostname", hostnameField.Text);
                SetResult(Result.Ok, intent);
                Finish();
            };
        }
    }
}
