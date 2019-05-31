using System.IO;
using System;
using SQLite;
using Android.Content;

namespace DriverTracker.Mobile.Droid
{
    public class AndroidServerConnectionStoreFactory : IServerConnectionStoreFactory
    {
        public Context AppContext { get; set; }

        public IServerConnectionStore CreateConnectionStore()
        {
            return new AndroidServerConnectionStore(new SQLiteConnection(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "connections.db3")), AppContext);
        }
    }
}
