using System.Net.Http;
using TrainTimeTable.ApiClient.Requests.Base;

namespace TrainTimeTable.ApiClient.Requests
{
    public class AuthRequest:BaseParamRequest
    {

        public override string Controller
        {
            get { return "token"; }
        }

        public override string MethodName
        {
            get { return ""; }
        }

        public AuthRequest(string phone,string password)
        {
            base.Params.Add("grant_type", "password");
            base.Params.Add("userName", phone);
            base.Params.Add("password", password);
            base.Type=HttpMethod.Post;
        }
    }
}
