using CebuContactTracing.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CebuContactTracing.Models
{
    public class StatisticsModel : ExtendedBindableObject
    {
        [JsonProperty("regionName")]
        public string RegionName { get; set; }

        [JsonProperty("casesCount")]
        public uint CasesCount { get; set; }

        [JsonProperty("recoveredCount")]
        public uint RecoveredCount { get; set; }

        [JsonProperty("deceasedCount")]
        public uint DeceasedCount { get; set; }

        [JsonProperty("regionFlagUrl")]
        public string RegionFlagUrl { get; set; }
    }
}
