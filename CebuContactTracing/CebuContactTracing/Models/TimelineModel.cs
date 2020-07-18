using CebuContactTracing.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CebuContactTracing.Models
{
    class TimelineModel : ExtendedBindableObject
    {
        public string Country { get; set; }
        public string Province { get; set; }
        public string Date { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
    }
}
