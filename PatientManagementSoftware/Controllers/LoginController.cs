using PatientManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientManagementSoftware.DAL;
using System.Web.Security;

namespace PatientManagementSoftware.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        string Pass;
        public ActionResult Index()
        {
            return View();
        }

        private bool AuthenticateUser(string username, string password)
        {
            // Call the stored procedure to authenticate user
            DataAccessLayer dal = new DataAccessLayer();

            string query = "SPLoginUser";
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password)
            };

            DataTable result = dal.ExecuteStoredProcedure(query, parameters);

            foreach (DataRow row in result.Rows)
            {

                Pass = row["Password"].ToString();

            }
            if (result.Rows.Count > 0)
            {
                if (password == Pass)
                {
                    return true;
                }
            }
            
            return false;
        }



        /*  Update Your Login Method in LoginCotroller
 // GET: /Account/Login
 [HttpPost]
 [ValidateAntiForgeryToken]
 public ActionResult Login(LoginViewModel model)
 {
     if (ModelState.IsValid)
     {
         // Call stored procedure to authenticate user
         bool isAuthenticated = AuthenticateUser(model.Username, model.Password);
         if (isAuthenticated)
         {
             Session["UserID"] = model.Id;

             Session["User"] = model.Username;

             return RedirectToAction("Index", "Dashboard");
        
         }
         else
         {
             // If authentication fails, return to login page with error
             ViewBag.ErrorMessage = "Invalid Login Credentials!";
             ModelState.AddModelError(string.Empty, "Invalid username or password");
         }

     }
     // If login fails or ModelState is not valid, return to login page with errors
     model.Types = GetTypesFromDatabase();
     return View(model);
 }
     private List<string> GetTypesFromDatabase()
 {
     // Method to fetch types from the database
     // Replace this with your actual implementation
     return new List<string> { "Doctor", "Staff", "Admin", "Patient" };
 }
         * */


        // GET: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Call stored procedure to authenticate user
                bool isAuthenticated = AuthenticateUser(model.Username, model.Password);
                if (isAuthenticated)
                {

                    FormsAuthentication.SetAuthCookie(model.Username,false);

                    /* Session["UserID"] = model.id;

                     Session["User"] = model.Username;*/

                    Session["UserID"] = model.Username;

                    Session["User"] = model.Username;

                    return RedirectToAction("Index", "Dashboard");
                    
                }
                else
                {

                    ViewBag.ErrorMessage = "Invalid Login Credentials!";
                    ModelState.AddModelError(string.Empty, "Invalid username or password");

                    // If authentication fails, return to login page with error
                    return RedirectToAction("Index","Login");

                    // If authentication fails, return to login page with error 
                }

            }
            // If login fails or ModelState is not valid, return to login page with errors
            model.Types = GetTypesFromDatabase();
            return View(model);
        }
        private List<string> GetTypesFromDatabase()
        {
            // Method to fetch types from the database
            // Replace this with your actual implementation
            return new List<string> { "Doctor", "Staff", "Admin", "Patient" };
        }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Register()
        {
            return View();  
        }


        [HttpPost]
        public ActionResult Admin_Registration(LoginViewModel model)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();

            string Query = $"INSERT INTO tbl_users (Username,Email,Password,MobileNumber) VALUES('{model.Username}','{model.Email}','{model.Password}',{model.MobileNumber})";

            if (ModelState.IsValid)
            {
                DataTable result = dataAccessLayer.ExecuteQuery(Query);

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult RegisterAdmin(LoginViewModel model)
        {

            DataAccessLayer layer = new DataAccessLayer();

            string Query = $"INSERT INTO tbl_users (Username,Email,Password,ContactNumber) VALUES('{model.Username}','{model.Email}','{model.Password}',{model.MobileNumber})";

            DataTable result = layer.ExecuteQuery(Query);

            return RedirectToAction("Index");
           /* if (ModelState.IsValid)
            {
                
            }
            else
            {
                return RedirectToAction("Register");

            }*/
        }

    }
}