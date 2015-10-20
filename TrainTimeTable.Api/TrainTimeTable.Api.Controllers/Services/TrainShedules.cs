using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTimeTable.Api.Dto;

namespace TrainTimeTable.Api.Controllers.Services
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
        public double Duration { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public string Stops { get; set; }
        public string TrainTitle { get; set; }

        public string Uid { get; set; }
    }


    public class TrainStops
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
     
        public StationDto Station { get; set; }
      
        public int StopTime { get; set; }
       
        public double? Duration { get; set; }
    }
    
}
