using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcrParser
{
    public class EcrModel
    {
        public string Ecr { get; set; }
        public string StationImageUrl { get; set; }
        public string ExpressCode { get; set; }
        public string StationName { get; set; }
        public Geometry Position { get; set; }
        public int RegionId { get; set; }

        public long OpenStreetMapNode { get; set; }

        public string OpenStreetMapUrl { get; set; }

    }

    public class Geometry
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
