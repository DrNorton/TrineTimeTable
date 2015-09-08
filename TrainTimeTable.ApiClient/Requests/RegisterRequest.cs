using System.Net.Http;
using TrainTimeTable.ApiClient.Requests.Base;

namespace TrainTimeTable.ApiClient.Requests
{
    public class RegisterRequest:BaseParamRequest
    {

        public override string Controller
        {
            get { return "account"; }
        }

        public override string MethodName
        {
            get { return "register"; }
        }

        public RegisterRequest(string email,string password)
        {
            base.Params.Add("email",email);
            base.Params.Add("password", password);
            base.Type=HttpMethod.Post;
        }
    }
}
