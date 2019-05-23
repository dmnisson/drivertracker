using System.Collections.Generic;

namespace DriverTracker.Mobile.Droid
{
    class AndroidServerConnectionStore : IServerConnectionStore
    {
        public ServerConnection CurrentConnection { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void AddConnection(ServerConnection connection)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteConnection(int id)
        {
            throw new System.NotImplementedException();
        }

        public ServerConnection GetConnection(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ServerConnection> ListConnections()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateConnection(int id, ServerConnection connection)
        {
            throw new System.NotImplementedException();
        }
    }
}