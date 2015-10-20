using System;
using System.Collections.Generic;

namespace TrainTimeTable.ApiClient.Response
{
    public class TrainStopsResponse
    {
        public string ExceptDays { get; set; }

        public string Title { get; set; }

        public string StartTime { get; set; }

        public string Number { get; set; }

        public string ShortTitle { get; set; }

        public string Days { get; set; }

        public List<Stops> Stops { get; set; }
    }

    public class Stops
    {

        public DateTime? Arrival { get; set; }
        public DateTime? Departure { get; set; }
        public string Terminal { get; set; }
        public string Platform { get; set; }

        public StationResponse Station { get; set; }

        public int StopTime { get; set; }

        public double? Duration { get; set; }
    }
}
