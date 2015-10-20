using System.Net.Http;
using TrainTimeTable.ApiClient.Requests.Base;

namespace TrainTimeTable.ApiClient.Requests
{
    public class GetAllStationsRequest: BaseParamRequest
    {
        public override string Controller
        {
            get { return "station"; }
        }

        public override string MethodName
        {
            get { return "GetAllStations"; }
        }

        public GetAllStationsRequest()
        {
            base.Type = HttpMethod.Post;
        }
    }
}
