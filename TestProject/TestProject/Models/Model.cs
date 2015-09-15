using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace TestProject.Models
{
    public class LocalDatabaseContext : DbContext
    {
        private readonly string _databaseFilePath;
        public DbSet<FavoriteTrainPath> FavoriteTrainPaths { get; set; }

        public LocalDatabaseContext()
        {

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
        public long FromStationCode { get; set; }
        public long ToStationCode { get; set; }
    }
}
