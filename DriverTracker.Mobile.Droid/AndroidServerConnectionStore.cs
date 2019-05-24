using System.Collections.Generic;
using System.Threading.Tasks;

namespace DriverTracker.Mobile.Droid
{
    class AndroidServerConnectionStore : IServerConnectionStore
    {
        public ServerConnection CurrentConnection { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public async Task AddConnection(ServerConnection connection)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteConnection(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServerConnection> GetConnection(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ServerConnection>> ListConnections()
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateConnection(int id, ServerConnection connection)
        {
            throw new System.NotImplementedException();
        }
    }
}