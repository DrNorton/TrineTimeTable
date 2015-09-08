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
        private readonly traintimetable_dbEntities _context;

        public StationRepository(traintimetable_dbEntities context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StationDto>> SearchStationByName(string pattern)
        {
            return _context.Stations.Where(x=>x.StationName.ToLower().Contains(pattern.ToLower())).Select(x => new StationDto() {Ecr = x.Ecr,ExpressCode=x.ExpressCode,StationName = x.StationName});
        }


        public async Task<IEnumerable<StationDto>> GetAllStations()
        {
            return _context.Stations.Where(x=>x.Position.Latitude!=null && x.Position.Longitude!=null).Select(x => new StationDto() { Ecr = x.Ecr, ExpressCode = x.ExpressCode, StationName = x.StationName,Position = new PositionDto() {Latitude = x.Position.Latitude,Longitude = x.Position.Longitude} }).ToList();
        }
    }
}
