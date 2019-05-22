using System.Threading.Tasks;

namespace DriverTracker.Mobile
{
    /// <summary>Stores the information needed to connect to a DriverTracker server.</summary>
    public class ServerConnection
    {
        /// <summary>
        /// The name of the company to which the server belongs.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; private set; }

        /// <summary>
        /// The host to which to connect (may include a port number)
        /// </summary>
        /// <value>The host.</value>
        public string Host { get; private set; }

        /// <summary>
        /// The JWT token.
        /// </summary>
        /// <value>The token, or null if not yet authenticated.</value>
        public string Jwt { get; private set; }

        public ServerConnection(string host, string companyName = null)
        {
            CompanyName = companyName ?? "DriverTracker Server at " + host;
            Host = host;
            Jwt = null;
        }

        /// <summary>
        /// Authenticate the specified user
        /// </summary>
        /// <param name="user">Username.</param>
        /// <param name="pass">Password.</param>
        /// <param name="authenticationService">Service to use for authentication.</param>
        public async Task Authenticate(string user, string pass, IAuthenticationService authenticationService)
        {
            Jwt = await authenticationService.MakeToken(user, pass);
            await authenticationService.SetRefreshInterval((newToken) => Jwt = newToken);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:DriverTracker.Mobile.ServerConnection"/> is authenticated.
        /// </summary>
        /// <value><c>true</c> if is authenticated; otherwise, <c>false</c>.</value>
        public bool IsAuthenticated => Jwt != null;
    }
}
