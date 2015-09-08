using Newtonsoft.Json;

namespace TrainTimeTable.ApiClient.Models
{
    public class ApiResponse<T>
    {
        public int ErrorCode { get; set; }
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }
        [JsonProperty("Result")]
        public T Result { get; set; }
    }
}
