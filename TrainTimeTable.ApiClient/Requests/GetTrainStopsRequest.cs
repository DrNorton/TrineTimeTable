using System;
using System.Net.Http;
using TrainTimeTable.ApiClient.Requests.Base;

namespace TrainTimeTable.ApiClient.Requests
{
    public class GetTrainStopsRequest : BaseParamRequest
    {
        public override string Controller
        {
            get { return "station"; }
        }

        public override string MethodName
        {
            get { return "GetTrain"; }
        }

        public GetTrainStopsRequest(string uid,DateTime time)
        {
            base.Params.Add("Uid", uid);
            base.Params.Add("Date", time);
            base.Type = HttpMethod.Post;
        }
    }
}
