using System;
using System.Collections.Generic;
using System.Text;

namespace CebuContactTracing.Models
{
    class BarangayListModel
    {
        public List<BarangayModel> data { get; set; }
        public bool success { get; set; }
        public object errorCode { get; set; }
    }
}
