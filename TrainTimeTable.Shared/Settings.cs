using TrainTimeTable.ApiClient;
using TrainTimeTable.ApiClient.Models;

namespace TrainTimeTable.Shared
{
    public class Settings:IApiSettings
    {
        public string BaseUrl
        {
            get { return "http://traintimetableapi.azurewebsites.net/api"; }
        }

        //public string BaseUrl
        //{
        //    get { return "http://localhost:50672/api"; }
        //}

        
        public Token Token { get; set; }
    }
}
