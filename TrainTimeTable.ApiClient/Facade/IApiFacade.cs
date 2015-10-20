using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainTimeTable.ApiClient.Models;
using TrainTimeTable.ApiClient.Response;

namespace TrainTimeTable.ApiClient.Facade
{
    public interface IApiFacade
    {
        Task<ApiResponse<List<StationResponse>>> SearchStationByName(string pattern);
        Task<ApiResponse<Token>> Auth(string email,string password);
        Task<ApiResponse<TrainShedules>> GetShedule(long from,long to,DateTime date,int page);
        Task<ApiResponse<List<StationResponse>>> GetAllStationsCoordinates();
        Task<ApiResponse<List<StationResponse>>> GetAllStations();
        Task<ApiResponse<TrainStopsResponse>> GetTrain(string uid,DateTime date);
    }
}