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

namespace TrainTimeTable.Api.Controllers.Services
{
    public interface IYandexApiService
    {
        Task<TrainShedules> LoadSheduleByScr(StationSearch stationSearch);
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
                yandexApiResponse.Threads.Select(x => new TrainTread() {Arrival = x.Arrival,Departure = x.DepartureTime,Stops = x.Stops,TrainTitle = x.Train.Title,Duration = x.Duration}).ToList();
            return trainShedules;
        }
    }
}
