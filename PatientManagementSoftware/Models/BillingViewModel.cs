using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientManagementSoftware.Models
{
    public class BillingViewModel
    {
        public int BillID { get; set; }

        public int PatientID { get; set; } = 0;
        
        public DateTime BillDate { get; set; }

        public decimal Amount { get; set;}

        public string PaymentStatus {  get; set; }

        public string PatientName { get; set; }

    }

}
