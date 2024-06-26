using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientManagementSoftware.Models
{
    public class MedicalRecordViewModel
    {
       /* CREATE TABLE MedicalRecords(
          RecordID INT PRIMARY KEY,
          PatientID INT,
          DoctorID INT,
          Diagnosis NVARCHAR(255),
          Treatment NVARCHAR(255),
          Medications NVARCHAR(255),
          TestResults NVARCHAR(255),
          FOREIGN KEY(PatientID) REFERENCES Patients(PatientID),
          FOREIGN KEY(DoctorID) REFERENCES Doctors(DoctorID)
     );*/
        public int RecordID { get; set; }
        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public string Diagnosis { get; set; }

        public string Treatment { get; set; }

        public string Medications {  get; set; }

        public string PatientName { get; set; }

        public string DoctorName { get; set; }

        public string BloodGroup { get; set; }

        public List<MedicalRecordViewModel> MedicalRecords { get; set; }

    }
}