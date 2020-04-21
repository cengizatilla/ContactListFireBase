using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace contactListWithFirebase.Models.Entities
{
    public class Contact
    {
        public string dbId { get; set; }
        [DisplayName("İsim")]
        public string firstName { get; set; }
        [DisplayName("Soyisim")]
        public string lastName { get; set; }
        [DisplayName("Telefon I")]
        public string communicationValueI { get; set; }
        [DisplayName("Telefon II")]
        public string communicationValueII { get; set; }
        [DisplayName("Email Adres")]
        public string communicationValueIII { get; set; }
        [DisplayName("Not")]
        public string notes { get; set; }
    }

   
}