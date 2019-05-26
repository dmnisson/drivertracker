using System.Threading.Tasks;
using Java.IO;

namespace DriverTracker.Mobile
{
    /// <summary>
    /// Interface for authentication with a DriverTracker server.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Makes a new token.
        /// </summary>
        /// <returns>The token.</returns>
        /// <param name="user">Username.</param>
        /// <param name="pass">Password.</param>
        Task<string> MakeToken(string user, string pass);

        /// <summary>
        /// Refreshes a token.
        /// </summary>
        /// <returns>The new token.</returns>
        /// <param name="oldToken">Token to refresh.</param>
        Task<string> RefreshToken(string oldToken);

        /// <summary>Sets a background timer to refresh a token at specified time intervals.
        /// Unsets the timer if refresh fails.</summary>
        /// <param name="connection">The connection whose token to update</param>
        /// <param name="interval">The interval in ms.</param>
        Task SetRefreshInterval(ServerConnection connection, int interval = 3600000);
    }
}