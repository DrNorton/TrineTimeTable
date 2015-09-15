using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace TrainTimeTable.LocalDbEntities.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly LocalDatabaseContext _context;

        public StationRepository(LocalDatabaseContext context)
        {
            _context = context;
        }

        public Task<List<Station>> GetAllStations()
        {
            return _context.Stations.ToListAsync();
        } 
    }
}
