﻿using System.Collections.Generic;
using System.Threading.Tasks;


namespace TrainTimeTable.LocalEntities.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly SqliteContext _context;

        public StationRepository(SqliteContext context)
        {
            _context = context;
        }

        public async Task AddStationsIfNotExists(IEnumerable<Station> stations )
        {
            var connection = _context.CreateConnection();
            var table = connection.Table<Station>();
          
            foreach (var station in stations)
            {
                var isExists= (await table.Where(x => x.ExpressCode == station.ExpressCode).ToListAsync()).Count>0;
                if (!isExists)
                {
                    await connection.InsertAsync(station);
                }
            }
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
            return connection.Table<Station>().Where(x => x.StationName.Contains(name)).ToListAsync();
        }


        public Task<Station> FindByScr(long ecr)
        {
            var connection = _context.CreateConnection();
            var table = connection.Table<Station>();
            return  table.Where(x => x.Ecr == ecr).FirstOrDefaultAsync();
        }

    }
}
