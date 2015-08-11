using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTimeTable.Api.Dao.Repositories;
using TrainTimeTable.Api.Dto;

namespace TrainTimeTable.Api.EfDao.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly TrainTimeTableContext _context;

        public StationRepository(TrainTimeTableContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StationDto>> GetStations()
        {
            return _context.Set<Station>().ToList().Select(x => new StationDto() {Ecr = x.Ecr,ExpressCode=x.ExpressCode,StationName = x.StationName});
        }
    }
}
