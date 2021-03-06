﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TrainTimeTable.Api.Dto;

namespace TrainTimeTable.Api.Dao.Repositories
{
    public interface IStationRepository
    {
        Task<IEnumerable<StationDto>> SearchStationByName(string pattern);
        Task<IEnumerable<StationDto>> GetAllStations();
        Task<IEnumerable<StationDto>> GetAllStationsWithoutCoordinates();
    }
}