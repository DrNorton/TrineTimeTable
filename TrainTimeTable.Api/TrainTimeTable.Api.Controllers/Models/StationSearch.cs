using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTimeTable.Api.Controllers.Models
{
    public class StationSearch
    {
        public long From { get; set; }
        public long To { get; set; }
        public DateTime Date { get; set; }
        public int Page { get; set; }
    }
}
