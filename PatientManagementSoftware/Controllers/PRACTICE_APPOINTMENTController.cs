using PatientManagementSoftware.DAL;
using PatientManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementSoftware.Controllers
{
    public class PRACTICE_APPOINTMENTController : Controller
    {
        // GET: PRACTICE_APPOINTMENT

        DataAccessLayer Layer;
        public ActionResult Index()
        {
            Layer = new DataAccessLayer();

            string Query = "ManageAppointmentsDML";

            List<AppointmentViewModel> appointmentList = new List<AppointmentViewModel>();

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
             new SqlParameter("@Action","select"),   

            };

            DataTable dataTable= Layer.ExecuteStoredProcedure(Query, sqlParameter);

            foreach(DataRow item in dataTable.Rows)
            {
                AppointmentViewModel appointmentViewModel = new AppointmentViewModel
                {
                    Id= Convert.ToInt32(item["AppointmentID"]),
                    PatientName= item["PatientName"].ToString(),
                    DoctorName= item["DoctorName"].ToString(),
                    AppointmentDateTime = Convert.ToDateTime(item["AppointmentDateTime"]),
                    Reason = item["Reason"].ToString(),
                    Status = item["status"].ToString()
                };

                appointmentList.Add(appointmentViewModel);
            }
            return  View(appointmentList);
        }

        public ActionResult SaveAppointment()
        {      
            ViewBag.PatientList=GetPatients();

            ViewBag.Doctorlist=GetDoctors();

            return View();

        }



        public ActionResult BookAppointment(AppointmentViewModel model)
        {
            Layer = new DataAccessLayer();

            string q = "ManageAppointmentsDML";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action","Insert"),
                new SqlParameter("@PatientID",model.PatientID),
                new SqlParameter("@doctorID",model.DoctorID),
                new SqlParameter("@AppointmentDateTime",model.AppointmentDateTime),
                new SqlParameter("@Reason",model.Reason),
                new SqlParameter("@Status",model.Status),
            };

            Layer.ExecuteStoredProcedure(q,parameters);

            return RedirectToAction("Index");   
        }

        public ActionResult Edit(int Id)
        {
            Layer= new DataAccessLayer();

            string q = "GetDataByID";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                 new SqlParameter("@ChooseTable","Appointment"),
                new SqlParameter("@AppointmentID",Id),  
            };

            AppointmentViewModel model = new AppointmentViewModel();


            ViewBag.PatientList = GetPatients();

            ViewBag.DoctorList=GetDoctors();

            DataTable dt = Layer.ExecuteStoredProcedure(q,sqlParameter);

            foreach (DataRow dr in dt.Rows)
            {
                model.Id = Convert.ToInt32(dr["AppointmentID"]);
                model.PatientID = Convert.ToInt32(dr["PatientID"]);
                model.DoctorID = Convert.ToInt32(dr["DoctorID"]);
                model.AppointmentDateTime = Convert.ToDateTime(dr["AppointmentDateTime"]);
                model.Reason = dr["Reason"].ToString();
                model.Status = dr["status"].ToString(); 
            }
                return View(model);
        }


        public ActionResult UpdateAppointment(AppointmentViewModel model)
        {

            Layer= new DataAccessLayer();

            string PROC_NAME = "ManageAppointmentsDML";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@Action","Update"),
                new SqlParameter("@AppointmentID",model.Id),
                new SqlParameter("@PatientID",model.PatientID),
                new SqlParameter("@DoctorID",model.DoctorID),
                new SqlParameter("@AppointmentDateTime",model.AppointmentDateTime),
                new SqlParameter("@Reason",model.Reason),
                new SqlParameter("@Status",model.Status),
            };

            Layer.ExecuteStoredProcedure(PROC_NAME, sqlParameter);
            return RedirectToAction("Index");
        }







        // DELETE METHOD
        public ActionResult Delete( int id)
        {
            Layer= new DataAccessLayer();

            string query = "ManageAppointmentsDML";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action","Delete"),
                new SqlParameter("@AppointmentID",id),
            };
            
            Layer.ExecuteStoredProcedure(query, parameters);

            return RedirectToAction("Index");
        }

        public List<PatientViewModel> GetPatients()
        {
            Layer= new DataAccessLayer();

            List<PatientViewModel> patients= new List<PatientViewModel>();

            string Query = "ManagePatientsDML";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Action","Select"),
            };

            DataTable dataTable = Layer.ExecuteStoredProcedure(Query,sqlParameters);

            foreach (DataRow row in dataTable.Rows)
            {
                patients.Add(new PatientViewModel()
                {
                    PatientID = Convert.ToInt32(row["PatientID"]),
                    Name = Convert.ToString(row["Name"]),
                });
            }
          
            return patients;
        }



        public List<DoctorViewModel>GetDoctors()
        {
            Layer= new DataAccessLayer();

            List<DoctorViewModel> doctorlist = new List<DoctorViewModel>();

            string query = "ManageDoctorsDML";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Action","Select"),
            };

            DataTable dt= Layer.ExecuteStoredProcedure(query,sqlParameters);

            foreach (DataRow row in dt.Rows)
            {
                doctorlist.Add(new DoctorViewModel()
                {
                    DoctorID = Convert.ToInt32(row["DoctorID"]),
                    Name = Convert.ToString(row["Name"]),
                });
            }
            return doctorlist;
        }        
    }  
}