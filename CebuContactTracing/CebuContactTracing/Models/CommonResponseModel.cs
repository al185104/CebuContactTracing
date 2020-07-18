using System;
using System.Collections.Generic;
using System.Text;

namespace CebuContactTracing.Models
{
    public class CommonResponseModel
{
        public object data { get; set; }
        public string errorCode { get; set; }
        public bool success { get; set; }

    }

}
