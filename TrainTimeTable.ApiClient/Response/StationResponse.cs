using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrainTimeTable.ApiClient.Response
{
    public class StationResponse
    {
        public long Ecr { get; set; }
        public long ExpressCode { get; set; }
        public string StationName { get; set; }

        public PositionDto Position { get; set; }

        [JsonIgnore]
        public Uri ImageSourceUri { get; set; }
    }
}
