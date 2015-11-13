using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aeriksa.Models
{
    public class ContentModel
    {
        public string ContentDate { get; set; }
        public int CO2 { get; set; }
        public int PM10 { get; set; }
        public int PM2 { get; set; }
        public string Temp_high { get; set; }

        public string Temp_Low { get; set; }

        public string type { get; set; }
    }
}