using PatientManagementSoftware.DAL;
using PatientManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementSoftware.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        // GET: Appointment
        DataAccessLayer dataAccessLayer = new DataAccessLayer();
        public ActionResult Index()
        {
            dataAccessLayer = new DataAccessLayer();

            string query = "ManageAppointmentsDML";

            List<AppointmentViewModel> Appointmentist = new List<AppointmentViewModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@Action", "select")
            };

            DataTable dt = dataAccessLayer.ExecuteStoredProcedure(query,parameters);

            foreach (DataRow dr in dt.Rows)
            {
                AppointmentViewModel appointment = new AppointmentViewModel
                {
                    Id = Convert.ToInt32(dr["AppointmentID"]),
                    PatientName = dr["PatientName"].ToString(),
                    DoctorName= dr["DoctorName"].ToString(),
                    AppointmentDateTime = Convert.ToDateTime(dr["AppointmentDateTime"]),
                    Reason = dr["Reason"].ToString(),
                    Status = dr["status"].ToString()

                };
            Appointmentist.Add(appointment);
            }
            return View(Appointmentist);
        }


        public List<DoctorViewModel> DoctorDDL()
        {
            List<DoctorViewModel> doctorList = new List<DoctorViewModel>();

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAppointment(AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                dataAccessLayer = new DataAccessLayer();

                string query = "ManageAppointmentsDML";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Action","insert"),
                    new SqlParameter("@AppointmentID",model.Id),
                    new SqlParameter("@PatientID",model.PatientID),
                    new SqlParameter("@DoctorID",model.DoctorID),
                    new SqlParameter("@AppointmentDateTime",model.AppointmentDateTime),
                    new SqlParameter("@Reason",model.Reason),
                    new SqlParameter("@Status",model.Status)
                };

                DataTable dt = dataAccessLayer.ExecuteStoredProcedure(query, parameters);
                
            }
            return RedirectToAction("Index");
            /*return View();*/
        }

        public ActionResult BookAppointment() 
        {
            ViewBag.PatientList = PatientDDL();

            ViewBag.DoctorList=DoctorDDL();

                return View(); 
        }

        //Following us the method to retrier the data from the database by Appointment id and save it to the model
        public ActionResult EditAppointment(int id)     
        {

            dataAccessLayer= new DataAccessLayer();

            AppointmentViewModel model = new AppointmentViewModel();

            string query = "GetDataByID";

            ViewBag.PatientList = PatientDDL();

            ViewBag.DoctorList = DoctorDDL();

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@ChooseTable","Appointment"),
                new SqlParameter("@AppointmentID",id)
            };

            DataTable dataTable = dataAccessLayer.ExecuteStoredProcedure(query,sqlParameter);

            foreach (DataRow dr in dataTable.Rows)
            {
                model.Id = Convert.ToInt32(dr["AppointmentID"]);
                model.PatientID = Convert.ToInt32(dr["PatientID"]);
                model.DoctorID= Convert.ToInt32(dr["DoctorID"]);
                model.AppointmentDateTime = Convert.ToDateTime(dr["AppointmentDateTime"]);
                model.Reason= dr["Reason"].ToString();
                model.Status = dr["status"].ToString();
            }

            return View(model);  
        }
        [HttpPost]
        public ActionResult Update (AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                string querry = "ManageAppointmentsDML";

                SqlParameter[] parameters = new SqlParameter[]
                   {
                    new SqlParameter("@Action","update"),
                    new SqlParameter("@AppointmentID",model.Id),
                    new SqlParameter("@PatientID",model.PatientID),
                    new SqlParameter("@DoctorID",model.DoctorID),
                    new SqlParameter("@AppointmentDateTime",model.AppointmentDateTime),
                    new SqlParameter("@Reason",model.Reason),
                    new SqlParameter("@Status",model.Status)
                   };

                DataTable dataTable = dataAccessLayer.ExecuteStoredProcedure(querry, parameters);

                return RedirectToAction("Index");

            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            dataAccessLayer = new DataAccessLayer();

            string query = "[ManageAppointmentsDML]";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Action","delete"),
                new SqlParameter("@AppointmentID",id),
            };
            DataTable result = dataAccessLayer.ExecuteStoredProcedure(query, sqlParameters);

            if (result.Rows.Count > 0)
            {
                return RedirectToAction("BookAppointment");
            }
            else
            {
                
            }
            return RedirectToAction("Index");
        }




    }
   
}