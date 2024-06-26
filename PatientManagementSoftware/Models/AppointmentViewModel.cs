using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PatientManagementSoftware.Models
{
    public class AppointmentViewModel
    {
        
        public int Id { get; set; }

        public int PatientID { get; set; }

        public string PatientName { get; set; }

        public int DoctorID { get; set; }

        public string DoctorName { get; set; }


        public DateTime AppointmentDateTime { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }

        

        public List<AppointmentViewModel> Appointments { get; set; }

    }
}