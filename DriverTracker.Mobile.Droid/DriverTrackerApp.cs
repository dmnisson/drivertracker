using Android.App;
using Android.Content;
using Autofac;

namespace DriverTracker.Mobile.Droid
{
    /// <summary>
    /// Global functionality for the DriverTracker mobile app.
    /// </summary>
    public class DriverTrackerApp : Application
    {
        public static IContainer Container { get; private set; }

        public static void Main(string[] args)
        {
            // build the Autofac container
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register((c, p) => new AndroidAuthenticationService
            {
                Host = p.Named<string>("Host"),
                RefreshContext = p.Named<Context>("RefreshContext")
            }).As<IAuthenticationService>();
            builder.Register((c, p) => new AndroidServerConnectionStoreFactory
            {
                AppContext = p.Named<Context>("AppContext")
            }).As<IServerConnectionStoreFactory>();
            Container = builder.Build();

           
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // start the activity
            Intent intent = new Intent(ApplicationContext, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }
    }
}
