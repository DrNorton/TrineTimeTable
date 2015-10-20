using System.Net.Http;
using TrainTimeTable.ApiClient.Requests.Base;

namespace TrainTimeTable.ApiClient.Requests
{
    public class GetAllStationsCoordinatesRequest: BaseParamRequest
    {
        public override string Controller
        {
            get { return "station"; }
        }

        public override string MethodName
        {
            get { return "GetAllStationsCoordinates"; }
        }

        public GetAllStationsCoordinatesRequest()
        {
            base.Type = HttpMethod.Post;
        }
    }
}
