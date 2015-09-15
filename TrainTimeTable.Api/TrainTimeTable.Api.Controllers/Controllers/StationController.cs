using System;
using System.Threading.Tasks;
using System.Web.Http;
using TrainTimeTable.Api.Controllers.ApiResults;
using TrainTimeTable.Api.Controllers.Models;
using TrainTimeTable.Api.Controllers.Services;
using TrainTimeTable.Api.Dao.Repositories;

namespace TrainTimeTable.Api.Controllers.Controllers
{
    [System.Web.Http.RoutePrefix("api/station")]
  
    public class StationController:CustomApiController
    {
        private readonly IStationRepository _stationRepository;
        private readonly IYandexApiService _yandexApiService;

        public StationController(IStationRepository stationRepository,IYandexApiService yandexApiService)
        {
            _stationRepository = stationRepository;
            _yandexApiService = yandexApiService;
        }

        [System.Web.Http.Route("GetStation")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> GetStation(SearchPatternModel pattern)
        {
            return SuccessApiResult(await _stationRepository.SearchStationByName(pattern.Pattern));
        }

        [System.Web.Http.Route("GetAllStations")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> GetAllStations()
        {
            return SuccessApiResult(await _stationRepository.GetAllStationsWithoutCoordinates());
        }

        [System.Web.Http.Route("GetShedule")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> GetShedule(StationSearch stationSearch)
        {
            try
            {
                var result= await _yandexApiService.LoadSheduleByScr(stationSearch);
                return SuccessApiResult(result);
            }
            catch (Exception e)
            {
                return ErrorApiResult(100,e.Message);
            }
          
        }


        [System.Web.Http.Route("GetAllStationsCoordinates")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> GetAllStationsCoordinates()
        {
            try
            {
                var result = await _stationRepository.GetAllStations();
                return SuccessApiResult(result);
            }
            catch (Exception e)
            {
                return ErrorApiResult(100, e.Message);
            }

        }
    }
}
