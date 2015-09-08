using Newtonsoft.Json;

namespace TrainTimeTable.ApiClient.Models
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string Value { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiredIn { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
}
