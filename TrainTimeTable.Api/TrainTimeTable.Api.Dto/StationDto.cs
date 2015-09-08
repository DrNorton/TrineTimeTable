using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTimeTable.Api.Dto
{
    public class StationDto
    {
        public long Ecr { get; set; }
        public long ExpressCode { get; set; }
        public string StationName { get; set; }

        public PositionDto Position { get; set; }
        
    }
}
