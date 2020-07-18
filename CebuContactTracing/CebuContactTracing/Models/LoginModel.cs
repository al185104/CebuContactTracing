using CebuContactTracing.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CebuContactTracing.Models
{
    class LoginModel : ExtendedBindableObject
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
