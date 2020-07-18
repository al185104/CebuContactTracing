using CebuContactTracing.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CebuContactTracing.Models
{
    public class FamilyModel : ExtendedBindableObject
    {
        public string address { get; set; }
        public int barangay_id { get; set; }
        public string contactNumber { get; set; }
        public string familyCode { get; set; }
        public ObservableCollection<FamilyMember> familyMembers { get; set; } = new ObservableCollection<FamilyMember>();
        public string registrationType { get; set; }
    }

    public class FamilyMember : ExtendedBindableObject
    {
        public int age { get; set; }
        public int barangay_id { get; set; }
        public string firstName { get; set; }
        public string gender { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string password { get; set; }
        public string username { get; set; }
    }
}
