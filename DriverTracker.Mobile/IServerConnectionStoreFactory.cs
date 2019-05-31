using System;
namespace DriverTracker.Mobile
{
    /// <summary>
    /// Interface for creating connection stores for specific platforms/databases
    /// </summary>
    public interface IServerConnectionStoreFactory
    {
        /// <summary>
        /// Creates the connection store object for the platform.
        /// </summary>
        /// <returns>The connection store.</returns>
        IServerConnectionStore CreateConnectionStore();
    }
}
