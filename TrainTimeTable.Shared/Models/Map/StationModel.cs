using System;

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
