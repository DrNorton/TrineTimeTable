using System.Collections.Generic;
using System.Net.Http;
using TrainTimeTable.ApiClient.Models;

namespace TrainTimeTable.ApiClient.Requests.Base
{
    public interface IRequest
    {
        string BaseUrl { get; set; }
        string Controller { get; }
        string MethodName { get; }

        HttpMethod Type { get; set; }

        Token Token { get; set; }
        Dictionary<string, object> Params { get; } 
    }
}
