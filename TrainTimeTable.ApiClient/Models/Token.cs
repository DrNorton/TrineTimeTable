namespace TrainTimeTable.ApiClient.Models
{
    public class Token
    {
        public string Value { get; set; }
        public int ExpiredIn { get; set; }
        public string TokenType { get; set; }
    }
}
