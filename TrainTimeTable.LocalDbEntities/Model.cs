using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Microsoft.Data.Entity;

namespace TrainTimeTable.LocalDbEntities
{
    public class LocalDatabaseContext : DbContext
    {
        private readonly string _databaseFilePath;
        public DbSet<FavoriteTrainPath> FavoriteTrainPaths { get; set; }
        public DbSet<Station> Stations { get; set; }

        public LocalDatabaseContext()
        {
            _databaseFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "local.db");
        }
        public LocalDatabaseContext(string databaseFilePath)
        {
            _databaseFilePath = databaseFilePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFilePath = "local.db";
            try
            {
                databaseFilePath = _databaseFilePath;
            }
            catch (InvalidOperationException)
            { }

            optionsBuilder.UseSqlite($"Data source={databaseFilePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Make Blog.Url required
            //modelBuilder.Entity<Blog>()
            //    .Property(b => b.Url)
            //    .Required();
        }
    }

    public class FavoriteTrainPath
    {
        public int Id { get; set; }
        public Station From { get; set; }
        public Station To { get; set; }
    }

    public class Station
    {
        public int Id { get; set; }
        public long Ecr { get; set; }
        public long ExpressCode { get; set; }
        public string StationName { get; set; }
        public string ImageSourceUri { get; set; }
    }
}

