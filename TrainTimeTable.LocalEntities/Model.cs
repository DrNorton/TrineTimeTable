using System;
using System.IO;
using Microsoft.Data.Entity;

namespace TrainTimeTable.LocalEntities
{
    public class DatabaseInitializator
    {
        public string Path { get; set; }
    }

    public class LocalDatabaseContext : DbContext
    {
        private readonly string _databaseFilePath;
        public DbSet<FavoriteTrainPath> FavoriteTrainPaths { get; set; }
        public DbSet<Station> Stations { get; set; }

    
        public LocalDatabaseContext(DatabaseInitializator init)
        {
            _databaseFilePath = init.Path;
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

