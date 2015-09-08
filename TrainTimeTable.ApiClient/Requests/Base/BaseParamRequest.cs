using System.Collections.Generic;
using System.Net.Http;
using TrainTimeTable.ApiClient.Models;

namespace TrainTimeTable.ApiClient.Requests.Base
{
    public abstract class BaseParamRequest:IRequest
    {
        private Dictionary<string, object> _params;

        public BaseParamRequest()
        {
            _params = new Dictionary<string, object>();
            Type = HttpMethod.Get; // По умолчанию
        }

        public void AddParam(string key, string value)
        {
            _params.Add(key,value);
        }

       public abstract string Controller { get; }
       public abstract  string MethodName { get; }

       public Dictionary<string, object> Params
       {
            get { return _params; }
            set { _params = value; }
       }


       public Token Token { get; set; }



       public HttpMethod Type { get; set; }

       public string BaseUrl { get; set; }
    }
}
