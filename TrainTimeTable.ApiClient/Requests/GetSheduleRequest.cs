using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrainTimeTable.ApiClient.Requests.Base;

namespace TrainTimeTable.ApiClient.Requests
{
    public class GetSheduleRequest:BaseParamRequest
    {
        public override string Controller
        {
            get { return "station"; }
        }

        public override string MethodName
        {
            get { return "GetShedule"; }
        }

        public GetSheduleRequest(long from,long to,DateTime date,int page)
        {
            base.Params.Add("From", from);
            base.Params.Add("To", to);
            base.Params.Add("Date", date.ToString("yyyy-MM-dd"));
            base.Params.Add("Page", page);
            base.Type = HttpMethod.Post;
        }
    }
}
