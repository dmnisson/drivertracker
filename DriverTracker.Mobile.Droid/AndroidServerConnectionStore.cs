using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System;
using SQLite;
using Android.Content;

namespace DriverTracker.Mobile.Droid
{
    class AndroidServerConnectionStore : IServerConnectionStore
    {
        private readonly SQLiteConnection _database;
        private readonly Context _context;

        public AndroidServerConnectionStore(SQLiteConnection database, Context context)
        {
            _database = database;
            _context = context;

            _ = _database.CreateTable<ServerConnection>();

            PopulateDefaultServers();
        }

        [Conditional("DEBUG")]
        private void PopulateDefaultServers()
        {
            if (_database.Table<ServerConnection>().Count() == 0)
            {
                // populate default servers only if there are none already in the database
                ServerConnection defaultConnection = new ServerConnection
                {
                    Host = _context.Resources.GetString(Resource.String.defaultHost)
                    
                };
            }
        }

        public ServerConnection CurrentConnection
        {
            get
            {
                using (ISharedPreferences prefs = GetContextSharedPreferences())
                {
                    int id = prefs.GetInt(_context.Resources.GetString(Resource.String.current_connection_id_key), -1);
                    return id == -1 ? null : _database.Get<ServerConnection>(id);
                }
            }
            set {
                using (ISharedPreferences prefs = GetContextSharedPreferences())
                {
                    using (ISharedPreferencesEditor editor = prefs.Edit())
                    {
                        _ = editor.PutInt(_context.Resources.GetString(Resource.String.current_connection_id_key),
                            value.ID);
                        _ = editor.Commit();
                    }
                }
            }
        }

        private ISharedPreferences GetContextSharedPreferences()
        {
            return _context.GetSharedPreferences(
                                _context.Resources.GetString(Resource.String.connection_prefs_key),
                                FileCreationMode.Private);
        }

        public async Task AddConnection(ServerConnection connection)
        {
            _ = await Task.Run(() => _database.Insert(connection));
        }

        public async Task DeleteConnection(int id)
        {
            _ = await Task.Run(() => _database.Delete<ServerConnection>(id));
        }

        public async Task<ServerConnection> GetConnection(int id)
        {
            return await Task.Run(() => _database.Get<ServerConnection>(id));
        }

        public async Task<IEnumerable<ServerConnection>> ListConnections()
        {
            var table = _database.Table<ServerConnection>();
            return await Task.Run(() => table.ToList());
        }

        public async Task UpdateConnection(int id, ServerConnection connection)
        {
            _ = await Task.Run(() => _database.Update(connection));
        }
    }
}