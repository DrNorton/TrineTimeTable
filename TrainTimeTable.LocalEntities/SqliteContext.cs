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
            var connection = new SQLiteConnectionWithLock(_platform,
                new SQLiteConnectionString(_dbPath, storeDateTimeAsTicks: true));
            _connectionFactory = new Func<SQLiteConnectionWithLock>(() =>connection);
            CreateTables();
        }

        public SQLiteAsyncConnection CreateConnection()
        {
            return new SQLiteAsyncConnection(_connectionFactory);
        }

        private  void CreateTables()
        {
            using (var conn = new SQLiteConnection(_platform, _dbPath))
            {
                conn.CreateTable<Image>();
                conn.CreateTable<Position>();
                conn.CreateTable<Station>();
                conn.CreateTable<FavoriteTrainPath>();
                conn.Close();
            }
         
        }
    }
}
