using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcrParser
{
    public class WikiStationModel
    {
        public long Ecr { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }

        public byte[] ByteImage { get; set; }
        public byte[] Thumb { get; set; }
    }
}
