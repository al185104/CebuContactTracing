using System;
using System.Collections.Generic;
using System.Text;

namespace CebuContactTracing.Models
{
    class BarangayModel
    {
        public DateTime dateCreated { get; set; }
        public object lastUpdated { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public Municipality municipality { get; set; }
    }
}
