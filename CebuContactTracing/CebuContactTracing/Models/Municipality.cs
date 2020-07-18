using System;
using System.Collections.Generic;
using System.Text;

namespace CebuContactTracing.Models
{
    public class Municipality
    {
        public DateTime dateCreated { get; set; }
        public object lastUpdated { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public Province province { get; set; }
    }
}
