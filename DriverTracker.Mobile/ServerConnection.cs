using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace DriverTracker.Mobile
{
    /// <summary>Stores the information needed to connect to a DriverTracker server.</summary>
    [Table("Connections")]
    public class ServerConnection
    {
        /// <summary>
        /// A unique identifier for this connection in the device-local store
        /// </summary>
        /// <value>The identifier.</value>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// The name of the company to which the server belongs.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }

        /// <summary>
        /// The host to which to connect (may include a port number)
        /// </summary>
        /// <value>The host.</value>
        public string Host { get; set; }

        /// <summary>
        /// The JWT token.
        /// </summary>
        /// <value>The token, or null if not yet authenticated.</value>
        public string Jwt { get; set; }

        /// <summary>
        /// Creates an empty ServerConnection object
        /// </summary>
        public ServerConnection() { }

        /// <summary>
        /// Creates a ServerConnection wth the given host and the option of a
        /// company name. If company name not specified, derives from host.
        /// </summary>
        /// <param name="host">Host.</param>
        /// <param name="companyName">Company name.</param>
        public ServerConnection(string host, string companyName = null)
        {
            CompanyName = companyName ?? "DriverTracker Server at " + host;
            Host = host;
            Jwt = null;
        }

        /// <summary>
        /// Authenticate the specified user. Does NOT automatically set refresh interval.
        /// </summary>
        /// <param name="user">Username.</param>
        /// <param name="pass">Password.</param>
        /// <param name="authenticationService">Service to use for authentication.</param>
        public async Task Authenticate(string user, string pass, IAuthenticationService authenticationService)
        {
            Jwt = await authenticationService.MakeToken(user, pass);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:DriverTracker.Mobile.ServerConnection"/> is authenticated.
        /// </summary>
        /// <value><c>true</c> if is authenticated; otherwise, <c>false</c>.</value>
        public bool IsAuthenticated
        {
            get
            {
                if (Jwt != null)
                {
                    var payload = JWT.JsonWebToken.DecodeToObject(Jwt, "", false) as IDictionary<string, object>;

                    // check if token has expired
                    if (payload.ContainsKey("exp") && payload["exp"] != null)
                    {
                        int exp = Convert.ToInt32(payload["exp"]);
                        var secondsSinceEpoch = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
                        if (secondsSinceEpoch >= exp) return false;
                    }
                }
                return Jwt != null;
            }
        }
    }
}
