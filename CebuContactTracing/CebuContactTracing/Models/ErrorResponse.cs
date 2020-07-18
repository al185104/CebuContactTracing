using System;
using System.Collections.Generic;
using System.Text;

namespace CebuContactTracing.Models
{
    class ErrorResponse
    {
        public DateTime timestamp { get; set; }
        public int status { get; set; }
        public string error { get; set; }
        public string message { get; set; }
        public string path { get; set; }
    }
}
