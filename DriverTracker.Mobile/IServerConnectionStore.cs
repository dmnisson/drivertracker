using System.Collections.Generic;
using System.Threading.Tasks;

namespace DriverTracker.Mobile
{
    /// <summary>
    /// Interface for a device-local repository for storing ServerConnection
    /// objects.
    /// </summary>
    public interface IServerConnectionStore
    {
        /// <summary>
        /// Gets or sets the currently selected server connection.
        /// </summary>
        /// <value>The current connection, or <see langword="null"/> if not yet set.</value>
        ServerConnection CurrentConnection { get; set; }

        /// <summary>
        /// Lists the stored server connections.
        /// </summary>
        /// <returns>The stored server connections.</returns>
        Task<IEnumerable<ServerConnection>> ListConnections();

        /// <summary>
        /// Gets the stored server connection with the given ID
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The stored server connection.</returns>
        Task<ServerConnection> GetConnection(int id);

        /// <summary>
        /// Adds a new server connection to the store, and updates the passed
        /// object with the appropriate ID.
        /// </summary>
        /// <param name="connection">The connection.</param>
        Task AddConnection(ServerConnection connection);

        /// <summary>
        /// Updates the connection with the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="connection">The connection.</param>
        Task UpdateConnection(int id, ServerConnection connection);

        /// <summary>
        /// Deletes the connection with the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        Task DeleteConnection(int id);
    }
}
