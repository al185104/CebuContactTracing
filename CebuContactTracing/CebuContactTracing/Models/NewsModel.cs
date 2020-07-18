using CebuContactTracing.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CebuContactTracing.Models
{
    class NewsModel : ExtendedBindableObject
    {
        public int nid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string author { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public DateTime publishedAt { get; set; }
        public DateTime addedOn { get; set; }
        public string siteName { get; set; }
        public string language { get; set; }
        public string countryCode { get; set; }
        public int status { get; set; }
    }
}
