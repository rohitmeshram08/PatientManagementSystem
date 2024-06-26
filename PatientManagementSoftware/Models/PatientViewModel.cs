using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml.Linq;

namespace PatientManagementSoftware.Models
{
    public class PatientViewModel
    {

        public int PatientID {  get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string ContactNumber {  get; set; }
        public string Gender { get; set; }


    }
}