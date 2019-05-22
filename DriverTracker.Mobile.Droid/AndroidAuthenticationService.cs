using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

        public const string AuthRoot = "/api/account/";

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
                    Input = new {
                        Email = user,
                        Password = pass,
                        RememberMe = false
                    }
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

        public async Task SetRefreshInterval(SetNewTokenCallback callback, int interval = 3600000)
        {
            throw new NotImplementedException();
        }
    }
}
