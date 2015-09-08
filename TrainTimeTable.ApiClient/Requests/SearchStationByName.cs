using System.Net.Http;
using TrainTimeTable.ApiClient.Requests.Base;

namespace TrainTimeTable.ApiClient.Requests
{
    public class SearchStationByName:BaseParamRequest
    {

        public override string Controller
        {
            get { return "station"; }
        }

        public override string MethodName
        {
            get { return "GetStation"; }
        }

        public SearchStationByName(string pattern)
        {
            base.Params.Add("Pattern",pattern);
            base.Type=HttpMethod.Post;
        }
    }
}
