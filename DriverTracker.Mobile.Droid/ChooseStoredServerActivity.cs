
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

namespace DriverTracker.Mobile.Droid
{
    [Activity(Label = "ChooseStoredServerActivity")]
    public class ChooseStoredServerActivity : Activity
    {
        const int NEWSERVER_REQUEST = 3;
        const int EDITSERVER_REQUEST = 4;

        private static readonly IServerConnectionStore connectionStore = new AndroidServerConnectionStore();
        private static readonly List<ServerConnection> serverConnections = new List<ServerConnection>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ChooseStoredServer);

            TextView currentServerNameView = FindViewById<TextView>(Resource.Id.currentServerName);
            Button editServerButton = FindViewById<Button>(Resource.Id.editServerButton);

            ListView companiesListView = FindViewById<ListView>(Resource.Id.companiesList);
            ArrayAdapter<ServerConnection> adapter = new ArrayAdapter<ServerConnection>(
                this, Resource.Id.companiesList, serverConnections);
            companiesListView.Adapter = adapter;

            Button chooseServerButton = FindViewById<Button>(Resource.Id.chooseServerButton);
            Button addNewServerButton = FindViewById<Button>(Resource.Id.addNewServerButton);

            ServerConnection connection = connectionStore.CurrentConnection;

            editServerButton.Click += (sender, e) => {
                Intent intent = new Intent(this, typeof(ChooseServerActivity));
                if (connection != null)
                {
                    intent.PutExtra("companyName", connection.CompanyName);
                    intent.PutExtra("hostname", connection.Host);
                }
                StartActivityForResult(intent, EDITSERVER_REQUEST);
            };

            chooseServerButton.Click += (sender, e) => {
                connectionStore.CurrentConnection = serverConnections[companiesListView.SelectedItemPosition];
                SetResult(Result.Ok);
                Finish();
            };

            addNewServerButton.Click += (sender, e) => {
                Intent intent = new Intent(this, typeof(ChooseServerActivity));
                StartActivityForResult(intent, NEWSERVER_REQUEST);
            };

            string app_name = Resources.GetString(Resource.String.app_name);
            PopulateViews(currentServerNameView)
                .FireAndForgetSafeAsync(new LogErrorHandler(app_name));
        }

        private async Task PopulateViews(TextView currentServerNameView)
        {
            ServerConnection connection = connectionStore.CurrentConnection;

            currentServerNameView.Text = connection?.CompanyName 
                ?? Resources.GetString(Resource.String.notSelected);

            serverConnections.AddRange(await connectionStore.ListConnections());
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == NEWSERVER_REQUEST)
            {
                if (resultCode == Result.Ok)
                {
                    ServerConnection newConnection = new ServerConnection(
                         data.GetStringExtra("hostname"),
                         data.GetStringExtra("companyName")
                         );
                    connectionStore.AddConnection(newConnection);
                    connectionStore.CurrentConnection = newConnection;

                }
            }
            if (requestCode == EDITSERVER_REQUEST)
            {
                if (resultCode == Result.Ok)
                {
                    connectionStore.CurrentConnection.CompanyName = data.GetStringExtra("companyName");
                    connectionStore.CurrentConnection.Host = data.GetStringExtra("hostname");
                }
            }
        }
    }
}
