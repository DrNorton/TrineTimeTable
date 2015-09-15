using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace TrainTimeTable.LocalDbEntities.Repositories
{
    public class FavoriteTrainRepository
    {
        private readonly LocalDatabaseContext _context;

        public FavoriteTrainRepository(LocalDatabaseContext context)
        {
            _context = context;
        }

        public Task<List<FavoriteTrainPath>> GetAllFavorites()
        {
            return _context.FavoriteTrainPaths.ToListAsync();
        }

        //public Task<FavoriteTrainPath> GetFavorite(long from, long to)
        //{
        //    return _context.FavoriteTrainPaths.FirstAsync(x => x.FromStationCode == from && x.ToStationCode == to);
        //}

        //public Task AddToFavorites(long from, long to)
        //{
        //    _context.FavoriteTrainPaths.Add(new FavoriteTrainPath());
        //}
    }
}
