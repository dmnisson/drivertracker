using System.Threading.Tasks;

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
        /// <param name="autoRenew">Set a background timer to automatically refresh the token.</param>
        Task<string> MakeToken(string user, string pass);

        /// <summary>
        /// Refreshes a token.
        /// </summary>
        /// <returns>The new token.</returns>
        /// <param name="oldToken">Token to refresh.</param>
        Task<string> RefreshToken(string oldToken);

        /// <summary>Sets a background timer to refresh a token at specified time intervals.
        /// Unsets the timer if refresh fails.</summary>
        /// <param name="callback">The function to call to set the new token.</param>
        /// <param name="interval">The interval in ms.</param>
        Task SetRefreshInterval(SetNewTokenCallback callback, int interval = 3600000);
    }

    /// <summary>
    /// A function to call to set a new token.
    /// </summary>
    public delegate void SetNewTokenCallback(string newToken);
}