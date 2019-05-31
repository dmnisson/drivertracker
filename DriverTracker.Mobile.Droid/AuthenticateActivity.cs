
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;

namespace DriverTracker.Mobile.Droid
{


    /// <summary>
    /// Activity for authenticating users.
    /// </summary>
    [Activity(Label = "AuthenticateActivity")]
    public class AuthenticateActivity : Activity
    {
        private static IAuthenticationService authenticationService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Authenticate);

            // layout GUI
            Button continueButton = FindViewById<Button>(Resource.Id.continueButton);
            EditText emailAddressField = FindViewById<EditText>(Resource.Id.emailAddressField);
            EditText passwordField = FindViewById<EditText>(Resource.Id.passwordField);

            // resolve authentication service
            using (ILifetimeScope scope = DriverTrackerApp.Container.BeginLifetimeScope())
            {
                authenticationService = scope.Resolve<IAuthenticationService>(
                    new NamedParameter("Host", Intent.GetStringExtra("hostname")),
                    new NamedParameter("RefreshContext", this));
            }

            continueButton.Click += (sender, e) =>
            {
                string app_name = Resources.GetString(Resource.String.app_name);
                AttemptAuthentication(emailAddressField, passwordField)
                    .FireAndForgetSafeAsync(new LogErrorHandler(app_name));
            };
        }

        private async Task AttemptAuthentication(
            EditText emailAddressField, EditText passwordField)
        {
            string token = null;
            try
            {
                token = await authenticationService.MakeToken(emailAddressField.Text, passwordField.Text);
            }
            catch (Exception ex)
            {
                if (ex is System.Net.WebException || ex is System.Net.Http.HttpRequestException)
                {
                    // show alert dialog
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Error");
#if DEBUG
                    alert.SetMessage("Authentication failure:" + ex.Message);
#else
                    alert.SetMessage("Authentication failure");
#endif

                    Dialog dialog = alert.Create();
                    dialog.Show();
                }
                else
                {
                    throw ex;
                }
            }

            if (token != null)
            {
                // pass token back to caller
                Intent intent = new Intent();
                intent.PutExtra("token", token);

                SetResult(Result.Ok, intent);
                Finish();
            }
            else
            {
                // alert that username or password is invalid
                // show alert dialog
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Error");

                alert.SetMessage("Invalid username or password");

                Dialog dialog = alert.Create();
                dialog.Show();
            }
        }
    }
}
