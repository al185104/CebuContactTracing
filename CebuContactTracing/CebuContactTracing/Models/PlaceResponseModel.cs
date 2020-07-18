using System;
using System.Collections.Generic;
using System.Text;

namespace CebuContactTracing.Models
{
    class PlaceResponseModel
    {
        public DateTime dateCreated { get; set; }
        public object lastUpdated { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string qrCode { get; set; }
        public Barangay barangay { get; set; }
        public bool status { get; set; }
        public string address { get; set; }
    }

    public class Barangay
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
