using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTimeTable.Shared.Models.Map
{
    public class StationModel
    {
        public long Ecr { get; set; }
        public long ExpressCode { get; set; }
        public string StationName { get; set; }

        public GeoModel Position { get; set; }
        public Uri ImageSourceUri { get; set; }
    }
}
