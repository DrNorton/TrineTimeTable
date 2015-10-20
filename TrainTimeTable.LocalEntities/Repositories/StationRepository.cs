using System.Collections.Generic;
using System.Threading.Tasks;
using SQLiteNetExtensionsAsync.Extensions;


namespace TrainTimeTable.LocalEntities.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly SqliteContext _context;

        public StationRepository(SqliteContext context)
        {
            _context = context;
        }

        public  Task AddStationsIfNotExists(IEnumerable<Station> stations )
        {
            var connection = _context.CreateConnection();
            return connection.InsertOrReplaceAllWithChildrenAsync(stations,true);
        }

        public Task AddStation(Station station)
        {
            var connection = _context.CreateConnection();
            var findedStation=connection.GetAsync<Station>(station.Id);
            if (findedStation == null) return Task.CompletedTask;
            return connection.InsertAsync(station);
        }

        public Task<List<Station>> FindByName(string name)
        {
            var connection = _context.CreateConnection();
            return connection.Table<Station>().Where(x => x.StationName.ToLower().StartsWith(name.ToLower())).ToListAsync();
        }


        public Task<List<Station>> GetAll()
        {
            return _context.CreateConnection().GetAllWithChildrenAsync<Station>();
        }

        public Task<Station> FindByScr(long ecr)
        {
            var connection = _context.CreateConnection();
            var table = connection.Table<Station>();
            return  table.Where(x => x.Ecr == ecr).FirstOrDefaultAsync();
        }

    }
}
