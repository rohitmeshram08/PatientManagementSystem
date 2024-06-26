using PatientManagementSoftware.DAL;
using PatientManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementSoftware.Controllers
{
    [Authorize]
    public class MedicalRecordsController : Controller
    {
        // GET: MedicalRecords

        DataAccessLayer dataAccessLayer;
        public ActionResult Index()
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();

            List<MedicalRecordViewModel> records = new List<MedicalRecordViewModel>();

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@Action","Select"),
            };

            DataTable dataTable = dataAccessLayer.ExecuteStoredProcedure("SPMedicalRecordsDML", sqlParameter);

            foreach (DataRow data in dataTable.Rows)
            {
                MedicalRecordViewModel recordViewModel = new MedicalRecordViewModel
                {

                    RecordID = Convert.ToInt32(data["RecordID"]),
                    PatientName = data["PatientName"].ToString(),
                    DoctorName = data["DoctorName"].ToString(),
                    Diagnosis = data["Diagnosis"].ToString(),
                    Treatment = data["Treatment"].ToString(),
                    Medications = data["Medications"].ToString(),
                    BloodGroup = data["BloodGroup"].ToString(),

                };
                records.Add(recordViewModel);
            }
            return View(records);

        }

        public List<DoctorViewModel> DoctorDDL()
        {
            List<DoctorViewModel> doctorList = new List<DoctorViewModel>();

            DataAccessLayer dataAccessLayer = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@Action", "select")
            };

            DataTable doctordt = dataAccessLayer.ExecuteStoredProcedure("ManageDoctorsDML", parameters);

            foreach (DataRow dr in doctordt.Rows)
            {
                doctorList.Add(new DoctorViewModel
                {
                    DoctorID = Convert.ToInt32(dr["DoctorID"]),
                    Name = dr["Name"].ToString(),
                });
            }
            return doctorList;
        }


        public List<PatientViewModel> PatientDDL()
        {
            List<PatientViewModel> patientList = new List<PatientViewModel>();

            DataAccessLayer dataAccessLayer = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@Action", "select")
            };

            DataTable patientdt = dataAccessLayer.ExecuteStoredProcedure("ManagePatientsDML", parameters);

            foreach (DataRow dr in patientdt.Rows)
            {
                patientList.Add(new PatientViewModel
                {
                    PatientID = Convert.ToInt32(dr["PatientID"]),
                    Name = dr["Name"].ToString(),
                });
            }
            return patientList;
        }

        public ActionResult SaveMedicalRecord(MedicalRecordViewModel model)
        {
            string Query = "SPMedicalRecordsDML";

            if (ModelState.IsValid)
            {
                dataAccessLayer = new DataAccessLayer();

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                 new SqlParameter("@Action","Insert"),
                 new SqlParameter("@PatientID",model.PatientID),
                 new SqlParameter("@DoctorID",model.DoctorID),
                 new SqlParameter("@Diagnosis",model.Diagnosis),
                 new SqlParameter("@Treatment",model.Treatment),
                 new SqlParameter("@Medications",model.Medications),
                 new SqlParameter("@BloodGroup",model.BloodGroup),
                };

                DataTable result = dataAccessLayer.ExecuteStoredProcedure(Query, sqlParameters);

                return RedirectToAction("Index");


            }

            return View(model);

        }

        public ActionResult AddMedicalRecord(MedicalRecordViewModel model)
        {
            ViewBag.PatientList = PatientDDL();

            ViewBag.DoctorList = DoctorDDL();
            return View(model);
        }

        public ActionResult Edit(int ID)
        {
            dataAccessLayer = new DataAccessLayer();

            ViewBag.PatientList = PatientDDL();

            ViewBag.DoctorList = DoctorDDL();

            string Query = "GetDataByID";

            MedicalRecordViewModel model = new MedicalRecordViewModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ChooseTable", "MedicalRecord "),
                new SqlParameter("@RecordID", ID)
            };

            DataTable patientdt = dataAccessLayer.ExecuteStoredProcedure(Query, parameters);

            foreach (DataRow dr in patientdt.Rows)
            {
                model.RecordID = Convert.ToInt32(dr["RecordID"]);
                model.PatientID = Convert.ToInt32(dr["PatientID"]);
                model.DoctorID = Convert.ToInt32(dr["DoctorID"]);
                model.Diagnosis = dr["Diagnosis"].ToString();
                model.Treatment = dr["Treatment"].ToString();
                model.Medications = dr["Medications"].ToString();
                model.BloodGroup = dr["BloodGroup"].ToString();
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(MedicalRecordViewModel model)
        {
            string Query = "SPMedicalRecordsDML ";


            dataAccessLayer = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action","update"),
                new SqlParameter("@RecordID",model.RecordID),
                new SqlParameter("@PatientID",model.PatientID),
                new SqlParameter("@DoctorID",model.DoctorID),
                new SqlParameter("@Diagnosis",model.Diagnosis),
                new SqlParameter("@Treatment",model.Treatment),
                new SqlParameter("@Medications",model.Medications),
                new SqlParameter("@BloodGroup",model.BloodGroup),
            };

            DataTable result= dataAccessLayer.ExecuteStoredProcedure(Query, parameters);

            return RedirectToAction("Index");

            return View();

        }

        public ActionResult Delete(int id)
        {
            string query = "SPMedicalRecordsDML";

            dataAccessLayer = new DataAccessLayer();

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Action","Delete"),
                new SqlParameter("@RecordID", id),
            };
            DataTable result = dataAccessLayer.ExecuteStoredProcedure(query, sqlParameters);

            if(result.Rows.Count>0 )
            {
               

            }
            return RedirectToAction("Index");

            return View();

        }


    }
}