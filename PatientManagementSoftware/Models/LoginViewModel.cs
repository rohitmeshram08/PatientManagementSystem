using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientManagementSoftware.Models
{
    public class LoginViewModel
    {      
            public int id {  get; set; }    
            public string Username { get; set; }

            public string Email { get; set; }

            public string MobileNumber { get; set; }

            public string Password { get; set; }
            public string SelectedType { get; set; }
            public List<string> Types { get; set; }
            public bool RememberMe { get; set; }
       
    }
}