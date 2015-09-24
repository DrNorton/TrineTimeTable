﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrainTimeTable.LocalEntities.Repositories
{
    public interface IStationRepository
    {
        Task AddStationsIfNotExists(IEnumerable<Station> stations);
        Task AddStation(Station station);
        Task<Station> FindByScr(long ecr);
    }
}