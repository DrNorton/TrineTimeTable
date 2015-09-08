using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrainTimeTable.Api.Controllers.Services
{
    public class YandexApiResponse
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("threads")]
        public List<Thread> Threads { get; set; }

    }

    public class Pagination
    {
        [JsonProperty("has_next")]
        public bool HasNext { get; set; }
        [JsonProperty("per_page")]
        public int PerPage { get; set; }
        [JsonProperty("page_count")]
        public int PageCount { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
    }


    public class Thread
    {
        [JsonProperty("arrival")]
        public DateTime Arrival { get; set; }
        [JsonProperty("duration")]
        public double Duration { get; set; }
        [JsonProperty("arrival_terminal")]
        public string ArrivalTerminal { get; set; }
        [JsonProperty("arrival_platform")]
        public string ArrivalPlatform { get; set; }
        [JsonProperty("from")]
        public Station From { get; set; }
        [JsonProperty("thread")]
        public Train Train { get; set; }
        [JsonProperty("departure_platform")]
        public string DeparturePlatform { get; set; }
        [JsonProperty("departure")]
        public DateTime DepartureTime { get; set; }
        [JsonProperty("stops")]
        public string Stops { get; set; }
        [JsonProperty("to")]
        public Station To { get; set; }
        [JsonProperty("departure_terminal")]
        public string DepartureTerminal { get; set; }
    }

    public class Train
    {
        [JsonProperty("transport_type")]
        public string TransportType { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("short_title")]
        public string ShortTitle { get; set; }

    }

    public class Station
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("station_type")]
        public string StationType { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("popular_title")]
        public string PopularTitle { get; set; }
        [JsonProperty("short_title")]
        public string ShortTitle { get; set; }
        [JsonProperty("transport_type")]
        public string TransportType { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
