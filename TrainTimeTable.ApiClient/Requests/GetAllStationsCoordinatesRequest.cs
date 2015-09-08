using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
