using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PatientManagementSoftware.Models
{
    public class DoctorViewModel
    {
        

        public int DoctorID { get; set; }

        public string Name { get; set; }

        public string Specialization { get; set; } 

        public string ContactNumber { get; set; }

        public string Availability{ get; set; }

        /*public List<DoctorViewModel> Doctors { get; set; }*/

    }
}