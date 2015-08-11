using System.Threading.Tasks;
using System.Web.Http;
using TrainTimeTable.Api.Controllers.ApiResults;
using TrainTimeTable.Api.Dao.Repositories;

namespace TrainTimeTable.Api.Controllers.Controllers
{
    [System.Web.Http.RoutePrefix("api/station")]
  
    public class StationController:CustomApiController
    {
        private readonly IStationRepository _stationRepository;

        public StationController(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }


        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetStation()
        {
            return SuccessApiResult(_stationRepository.GetStations());
        }
    }
}
