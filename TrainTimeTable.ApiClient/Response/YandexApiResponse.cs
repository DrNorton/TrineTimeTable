using System;
using System.Collections.Generic;

namespace TrainTimeTable.ApiClient.Response
{
    public class TrainShedules
    {
        public PaginationModel Pagination { get; set; }
        public string ArrivalStation { get; set; }
        public string DepartureStation { get; set; }
        public List<TrainTread> TrainTreads { get; set; }
    }

    public class PaginationModel
    {
        public bool HasNext { get; set; }
        public int PerPage { get; set; }
        public int PageCount { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
    }
    public class TrainTread
    {
        public string Uid { get; set; }
        public double Duration { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public string Stops { get; set; }
        public string TrainTitle { get; set; }
    }
}
