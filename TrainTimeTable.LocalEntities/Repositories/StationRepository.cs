using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace TrainTimeTable.LocalEntities.Repositories
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
