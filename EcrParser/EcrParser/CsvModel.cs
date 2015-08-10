using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcrParser
{
    public class CsvModel
    {
        public string Esr { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }

        public override string ToString()
        {
            return Esr;
        }
    }
}
