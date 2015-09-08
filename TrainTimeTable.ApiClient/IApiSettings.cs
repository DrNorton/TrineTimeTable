using TrainTimeTable.ApiClient.Models;

namespace TrainTimeTable.ApiClient
{
    public interface IApiSettings
    {
        string BaseUrl { get; }

        Token Token { get; set; }
     
    }
}
