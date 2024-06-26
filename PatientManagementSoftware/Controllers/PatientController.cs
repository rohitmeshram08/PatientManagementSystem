using PatientManagementSoftware.DAL;
using PatientManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace PatientManagementSoftware.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        // GET: Patient

        DataAccessLayer dal;
        public ActionResult Index()
        {   
            dal = new DataAccessLayer();    
          

            string query = "ManagePatientsDML";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter
                ("@Action","select")
            };

            DataTable dataTable = dal.ExecuteStoredProcedure (query, parameters);

            List<PatientViewModel> patientList = new List<PatientViewModel>();

            foreach(DataRow row in dataTable.Rows)
            {
                PatientViewModel patientViewModel = new PatientViewModel()
                {
                    PatientID = Convert.ToInt32(row["PatientID"]),
                    Name = row["Name"].ToString(),
                    DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
                    Address = row["Address"].ToString(),
                    ContactNumber = row["ContactNumber"].ToString(),
                    Gender = row["Gender"].ToString()
                };
                patientList.Add(patientViewModel);
            }

            return View(patientList);
        }


        public ActionResult Register()
        {
            return View();
        }

        public ActionResult SavePatient( PatientViewModel model)
        {

            if (ModelState.IsValid)
            {
                dal = new DataAccessLayer();

                // Prepare parameters for the stored procedure @
                SqlParameter[] parameters = new SqlParameter[]
                {// here @Name is the paramter of the database
                    new SqlParameter("@Name", model.Name),  //and the model.name is the value we are getting from Our model class
                    new SqlParameter("@DateOfBirth", model.DateOfBirth),
                    new SqlParameter("@Address", model.Address),
                    new SqlParameter("@ContactNumber", model.ContactNumber),
                    new SqlParameter("@Gender", model.Gender),


                    new SqlParameter("@Action", "Insert")
                };

                // Call the ExecuteStoredProcedure method with the insert action
                DataTable result = dal.ExecuteStoredProcedure("ManagePatientsDML", parameters);

                // You can handle the result here if needed

                // Redirect to the Index action
                return RedirectToAction("Index","Patient");
            }
            return View();
        }

        public ActionResult Edit(int PatientId)
        {
            dal = new DataAccessLayer();

            string Query = "GetDataByID";

            PatientViewModel model = new PatientViewModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ChooseTable", "Patient"),
                new SqlParameter("@PatientID", PatientId)

            };

            DataTable patientdt = dal.ExecuteStoredProcedure(Query, parameters);

            foreach (DataRow dr in patientdt.Rows)
            {
                model.PatientID = Convert.ToInt32(dr["PatientID"]);
                model.Name = dr["Name"].ToString();
                model.Address = dr["Address"].ToString();
                model.ContactNumber = dr["ContactNumber"].ToString();
                model.Gender = dr["Gender"].ToString();
                model.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);

            };
            return View(model);

        }

        [HttpPost]
        public ActionResult Update(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                dal = new DataAccessLayer();

                string query = "ManagePatientsDML";

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                 new SqlParameter("@Action", "update"),
                 new SqlParameter("@PatientID",  model.PatientID),
                 new SqlParameter("Name", model.Name),
                                                                                //and the model.name is the value we are getting from Our model class
                 new SqlParameter("@Address", model.Address),
                 new SqlParameter("@ContactNumber", model.ContactNumber),
                 new SqlParameter("@Gender",  model.Gender)
                };

                DataTable dataTable = dal.ExecuteStoredProcedure(query, sqlParameters);

                return RedirectToAction("Index");
            }

            return View(model);

        }

        public ActionResult Delete(int PatientId)
        {
            dal = new DataAccessLayer();

            string Query = "[ManagePatientsDML]";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Action","delete"),
                new SqlParameter("@PatientID",PatientId),
            };

            DataTable result = dal.ExecuteStoredProcedure(Query, sqlParameters);
            if (result.Rows.Count > 0)
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