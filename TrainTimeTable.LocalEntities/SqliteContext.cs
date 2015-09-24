using System;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;

namespace TrainTimeTable.LocalEntities
{
    public class SqliteContext
    {
        private readonly ISQLitePlatform _platform;
        private readonly DatabaseInitializator _init;
        private string _dbPath;
        private Func<SQLiteConnectionWithLock> _connectionFactory;

        public SqliteContext(ISQLitePlatform platform,DatabaseInitializator init)
        {
            _platform = platform;
            _init = init;
            _dbPath = init.Path;
            _connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(_platform, new SQLiteConnectionString(_dbPath, storeDateTimeAsTicks: false)));
            CreateTables();
        }

        public SQLiteAsyncConnection CreateConnection()
        {
            return new SQLiteAsyncConnection(_connectionFactory);
        }

        private async void CreateTables()
        {
            var conn = CreateConnection();
            await conn.CreateTableAsync<Station>();
            await conn.CreateTableAsync<FavoriteTrainPath>();
        }
    }
}
