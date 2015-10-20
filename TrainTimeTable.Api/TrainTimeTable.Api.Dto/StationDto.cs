using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTimeTable.Api.Dto
{
    public class StationDto
    {
        public string Ecr { get; set; }
        public string ExpressCode { get; set; }
        public string StationName { get; set; }

        public ImageDto Image { get; set; }

        
        public PositionDto Position { get; set; }
        
    }
}
