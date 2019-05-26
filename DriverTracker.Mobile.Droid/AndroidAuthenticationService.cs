using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Java.IO;
using Newtonsoft.Json;

namespace DriverTracker.Mobile.Droid
{
    public class AndroidAuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Gets or sets the host to which to connect to obtain the token.
        /// </summary>
        /// <value>The authentication host.</value>
        public string Host { get; set; }

        /// <summary>
        /// The context in which to set refresh alarms.
        /// </summary>
        public Context RefreshContext { get; set; }

        public const string AuthRoot = "/api/account/";
        private const int REFRESH_TOKEN_REQUEST = 0;
        private AlarmManager alarmManager;
        private PendingIntent pendingIntent;

        public async Task<string> MakeToken(string user, string pass)
        {
#if DEBUG
            using (HttpClientHandler handler = new BypassSslValidationClientHandler())
#else
            using (HttpClientHandler handler = new HttpClientHandler())
#endif
            using (HttpClient client = new HttpClient(handler))
            {
                var login = new {
                    Input = (
                        Email: user,
                        Password: pass,
                        RememberMe: false
                    )
                };
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
                string url = "https://" + Host + AuthRoot + "maketoken";
                HttpResponseMessage result = await client.PostAsync(url, content);
                if (result.IsSuccessStatusCode)
                    return await result.Content.ReadAsStringAsync();

                return null;
            }
        }

        public async Task<string> RefreshToken(string oldToken)
        {
#if DEBUG
            using (HttpClientHandler handler = new BypassSslValidationClientHandler())
#else
            using (HttpClientHandler handler = new HttpClientHandler())
#endif
            using (HttpClient client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oldToken);
                string url = "https://" + Host + AuthRoot + "refreshtoken";

                HttpResponseMessage result = await client.PostAsync(url, new StringContent(""));
                if (result.IsSuccessStatusCode)
                    return await result.Content.ReadAsStringAsync();

                return null;
            }
        }

        /// <summary>
        /// Sets an interval to refresh the token.
        /// </summary>
        /// <returns>A task to set the refresh interval.</returns>
        /// <param name="interval">Interval.</param>
        public async Task SetRefreshInterval(ServerConnection connection, int interval = 3600000)
        {
            Intent alarmIntent = new Intent(RefreshContext, typeof(RefreshAlarmReceiver));
            alarmIntent.PutExtra("connectionID", connection.ID);
            alarmIntent.PutExtra("host", connection.Host);
            alarmIntent.PutExtra("oldToken", connection.Jwt);
            pendingIntent = PendingIntent.GetBroadcast(RefreshContext, REFRESH_TOKEN_REQUEST, alarmIntent, 0);

            alarmManager = (AlarmManager)RefreshContext.GetSystemService(Context.AlarmService);
            alarmManager.SetRepeating(AlarmType.RtcWakeup,
                Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds),
                interval, pendingIntent);
        }
    }
}
