using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using TrainTimeTable.Api.Controllers.Models;
using TrainTimeTable.Api.Controllers.Settings;
using TrainTimeTable.Api.Dto;

namespace TrainTimeTable.Api.Controllers.Services
{
    public interface IYandexApiService
    {
        Task<TrainShedules> LoadSheduleByScr(StationSearch stationSearch);
        Task<TrainStops> LoadTrain(TrainRequest trainRequest);
    }

    public class YandexApiService : IYandexApiService
    {
        private string _apiKey;
        private RestClient _restClient;

        public YandexApiService(ISettings settings)
        {
            _apiKey=settings.YandexApiKey;
            _restClient = new RestClient();
            _restClient.BaseUrl = new Uri(settings.YandexBaseUrl);
        }

        public async Task<TrainShedules> LoadSheduleByScr(StationSearch stationSearch)
        {
            var request=new RestRequest(Method.GET);
            _restClient.BaseUrl = new Uri(_restClient.BaseUrl.ToString() + "search/");
            request.AddParameter("apikey", _apiKey);
            request.AddParameter("format", "json");
            request.AddParameter("system", "express");
            request.AddParameter("from", stationSearch.From);
            request.AddParameter("to", stationSearch.To);
            request.AddParameter("lang", "ru");
            request.AddParameter("page", stationSearch.Page);
            request.AddParameter("date", stationSearch.Date.ToString("yyyy-MM-dd"));
            var uri=_restClient.BuildUri(request);
            var response = await _restClient.ExecuteTaskAsync(request);
            var resultObject=JsonConvert.DeserializeObject<YandexApiResponse>(response.Content);
            return ConvertToModel(resultObject);

        }

        public async Task<TrainStops> LoadTrain(TrainRequest trainRequest)
        {
            var request = new RestRequest(Method.GET);
            _restClient.BaseUrl=new Uri(_restClient.BaseUrl.ToString()+ "thread/");
            request.AddParameter("apikey", _apiKey);
            request.AddParameter("format", "json");
            request.AddParameter("lang", "ru");
            request.AddParameter("date", trainRequest.Date.ToString("yyyy-MM-dd"));
            request.AddParameter("uid", trainRequest.Uid);
            request.AddParameter("show_systems", "all");
            
            var uri = _restClient.BuildUri(request);
            var response = await _restClient.ExecuteTaskAsync(request);
            var resultObject = JsonConvert.DeserializeObject<YandexTrainApiResponse>(response.Content);
            return ConvertTrainToModel(resultObject);

        }

        private TrainShedules ConvertToModel(YandexApiResponse yandexApiResponse)
        {
            var trainShedules=new TrainShedules();
            var arrivalStation = yandexApiResponse.Threads.Select(x => x.From).FirstOrDefault();
            var departureStation = yandexApiResponse.Threads.Select(x => x.To).FirstOrDefault();
            trainShedules.Pagination=new PaginationModel() {HasNext = yandexApiResponse.Pagination.HasNext,Page = yandexApiResponse.Pagination.Page,PageCount = yandexApiResponse.Pagination.PageCount, PerPage = yandexApiResponse.Pagination.PerPage,Total = yandexApiResponse.Pagination.Total};
            if (arrivalStation != null)
            {
                trainShedules.ArrivalStation = arrivalStation.Title;
            }
            if (departureStation != null)
            {
                trainShedules.DepartureStation = departureStation.Title;
            }

            trainShedules.TrainTreads =
                yandexApiResponse.Threads.Select(x => new TrainTread() {Uid = x.Train.Uid,Arrival = x.Arrival,Departure = x.DepartureTime,Stops = x.Stops,TrainTitle = x.Train.Title,Duration = x.Duration}).ToList();
            return trainShedules;
        }


        public TrainStops ConvertTrainToModel(YandexTrainApiResponse apiResponse)
        {
            return new TrainStops()
            {
                Days = apiResponse.Days,
                ExceptDays = apiResponse.ExceptDays,
                Number = apiResponse.Number,
                ShortTitle = apiResponse.ShortTitle,
                StartTime = apiResponse.StartTime,
                Stops = apiResponse.Stops.Select(x => new Stops() {Arrival = x.Arrival,Departure = x.Departure,Duration = x.Duration,Platform = x.Platform,Station = new StationDto() {Ecr = x.Station.Code.Ecr,ExpressCode = x.Station.Code.Express,StationName = x.Station.Title} }).ToList()
            };

        }
    }
}
