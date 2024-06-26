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
    public class InvoicingController : Controller
    {
        // GET: Invoicing

        DataAccessLayer dal;

        public List<PatientViewModel> PatientDDL() //This is a function which retunr list of type PatientViewModel
        {

            // PatientDDL Patient Drop down list
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

        public ActionResult Index()
        {
            dal = new DataAccessLayer();

            //Geting Dropdownlist Data

            ViewBag.PatientList = PatientDDL();

            string query = "ManageInvoiceDML";

            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@Action", "select")
            };

            DataTable dt = dal.ExecuteStoredProcedure(query, parameters);

            List<BillingViewModel> BillingList = new List<BillingViewModel>();

            foreach (DataRow dr in dt.Rows)
            {
                BillingViewModel model = new BillingViewModel()
                {
                    BillID = Convert.ToInt32(dr["BillID"]),
                    PatientID = Convert.ToInt32(dr["PatientID"]),
                    PatientName = dr["PatientName"].ToString(),
                    BillDate = Convert.ToDateTime(dr["BillDate"]),
                    Amount = Convert.ToDecimal(dr["Amount"].ToString()),
                    PaymentStatus = dr["PaymentStatus"].ToString()
                };
                BillingList.Add(model);
            }
            ViewBag.list = BillingList;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInvoice(BillingViewModel model)
        {
            if (ModelState.IsValid)
            {
                dal = new DataAccessLayer();

                ViewBag.PatientList = PatientDDL();

                string query = "ManageInvoiceDML";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Action","insert"),
                    new SqlParameter("@PatientID",model.PatientID),
                    new SqlParameter("@BillDate",model.BillDate),
                    new SqlParameter("@Amount",model.Amount),
                    new SqlParameter("@PaymentStatus",model.PaymentStatus)
                };

                DataTable dt = dal.ExecuteStoredProcedure(query, parameters);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Invoice()
        {
            ViewBag.PatientList = PatientDDL();


            return View();
        }

        /*public ActionResult CreateInvoice()
        {
            return View();
        }*/
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        public ActionResult PrintInvoice(int billId)
        {
            dal = new DataAccessLayer();

            BillingViewModel model = new BillingViewModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@BillID", billId)
            };

            DataTable patientdt = dal.ExecuteStoredProcedure("GetInvoiceByPatient", parameters);

            foreach (DataRow dr in patientdt.Rows)
            {
                model.BillID = Convert.ToInt32(dr["BillID"]);
                model.BillDate = Convert.ToDateTime(dr["BillDate"].ToString());
                model.Amount = Convert.ToDecimal(dr["Amount"].ToString());
                model.PatientID = Convert.ToInt32(dr["PatientID"].ToString());
                model.PatientName = dr["PatientName"].ToString();
                model.PaymentStatus = dr["PaymentStatus"].ToString();
            };
            return View(model);

        }

        public ActionResult Edit(int id)
        {
            BillingViewModel model = new BillingViewModel();

            string query = "GetDataByID";

            DataAccessLayer dataAccessLayer = new DataAccessLayer();

            ViewBag.PatientList1 = PatientDDL();

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@ChooseTable","Billing"),
                new SqlParameter("@BillID",id)
            };

            DataTable result = dataAccessLayer.ExecuteStoredProcedure(query, sqlParameter);

            foreach (DataRow dr in result.Rows)
            {
                model.BillID = Convert.ToInt32(dr["BillID"]);
                model.PatientID = Convert.ToInt32(dr["PatientID"]);
                model.BillDate = Convert.ToDateTime(dr["BillDate"]);
                model.Amount = Convert.ToDecimal(dr["Amount"]);
                model.PaymentStatus = dr["PaymentStatus"].ToString();
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Update(BillingViewModel model)
        {
            if (ModelState.IsValid)
            {
                dal = new DataAccessLayer();

                ViewBag.PatientList1 = PatientDDL();

                string query = "ManageInvoiceDML";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Action","update"),
                    new SqlParameter("BillID",model.BillID),
                    new SqlParameter("@PatientID",model.PatientID),
                    new SqlParameter("@BillDate",model.BillDate),
                    new SqlParameter("@Amount",model.Amount),
                    new SqlParameter("@PaymentStatus",model.PaymentStatus)
                };

                DataTable dt = dal.ExecuteStoredProcedure(query, parameters);
                return RedirectToAction("Index");
            }
            return View();
        }
        

        public ActionResult Delete(int id)
        {
            dal = new DataAccessLayer();

            string query = "[ManageInvoiceDML]";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Action","delete"),
                new SqlParameter("@BillID",id), 
            };

            DataTable result= dal.ExecuteStoredProcedure(query, sqlParameters);

            return RedirectToAction("Index");
        }

        public ActionResult print()
        {
            return View();
        }
    }


    
}

