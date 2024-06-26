using PatientManagementSoftware.DAL;
using PatientManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace PatientManagementSoftware.Controllers
{
    [Authorize]

    public class DoctorController : Controller
    {
        DataAccessLayer dal ;

        // GET: Doctor
        public ActionResult Index()
        {
            dal = new DataAccessLayer();

            List<DoctorViewModel> doctorsList = new List<DoctorViewModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@Action", "select")
            };
            DataTable dataTable = dal.ExecuteStoredProcedure("ManageDoctorsDML", parameters);

           
            foreach (DataRow data in dataTable.Rows)
            {
                DoctorViewModel doctor = new DoctorViewModel()
                {
                    DoctorID = Convert.ToInt32(data["DoctorID"]),
                    Name = data["Name"].ToString(),
                    Specialization = data["Specialization"].ToString(),
                    ContactNumber = data["ContactNumber"].ToString(),
                    Availability = data["Availability"].ToString(),
                }; 
                doctorsList.Add(doctor);
            } 
            return View(doctorsList);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveDoctor(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                dal = new DataAccessLayer();

                // Prepare parameters for the stored procedure
                SqlParameter[] parameters = new SqlParameter[]
                {// here @Name is the paramter of the database
                 new SqlParameter("@Name", "Dr."+(model.Name).ToUpperInvariant()),  //and the model.name is the value we are getting from Our model class
                 new SqlParameter("@Specialization", model.Specialization),
                 new SqlParameter("@ContactNumber", model.ContactNumber),
                 new SqlParameter("@Availability", model.Availability),
                  new SqlParameter("@Action", "Insert")
                };

                // Call the ExecuteStoredProcedure method with the insert action
                DataTable result = dal.ExecuteStoredProcedure("ManageDoctorsDML", parameters);

                // You can handle the result here if needed

                // Redirect to the Index action
                return RedirectToAction("Index");
            }
            // If the model state is not valid, return the view with validation errors
            return View(model);
        }
        public ActionResult Edit(int DocID)
        {
            dal = new DataAccessLayer();

            string Query = "GetDataByID";

           DoctorViewModel model = new DoctorViewModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ChooseTable", "Doctor"),
                new SqlParameter("@DoctorID", DocID)

            };

            DataTable patientdt = dal.ExecuteStoredProcedure(Query, parameters);

            foreach (DataRow dr in patientdt.Rows)
            {
                model.DoctorID = Convert.ToInt32(dr["DoctorID"]);
                model.Name = dr["Name"].ToString();
                model.Specialization = dr["Specialization"].ToString();
                model.ContactNumber = dr["ContactNumber"].ToString();
                model.Availability= dr["Availability"].ToString() ; 

            };
            return View(model);

        }
        [HttpPost]  
        public ActionResult Update(DoctorViewModel model)
        {   
            if (ModelState.IsValid)
            {
                dal = new DataAccessLayer();

                string query = "ManageDoctorsDML";

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                 new SqlParameter("@Action", "update"),
                 new SqlParameter("@DoctorID", model.DoctorID),
                 new SqlParameter("Name", "Dr."+model.Name),
                                                                                //and the model.name is the value we are getting from Our model class
                 new SqlParameter("@Specialization", model.Specialization),
                 new SqlParameter("@ContactNumber", model.ContactNumber),
                 new SqlParameter("@Availability", model.Availability)
                };

                DataTable dataTable = dal.ExecuteStoredProcedure(query, sqlParameters);

                return RedirectToAction("Index");
            }

            return View(model);

        }


        public ActionResult Delete(int DocID)
        {
            dal= new DataAccessLayer();

            string Query = "ManageDoctorsDML";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Action","delete"),
                new SqlParameter("@DoctorID",DocID),
            };

            DataTable result= dal.ExecuteStoredProcedure(Query, sqlParameters);
            if(result.Rows.Count > 0)
            {
                return RedirectToAction("Register");
            }
            else
            {
                return RedirectToAction("Index");

            }
        }
    }
}