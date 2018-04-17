using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plantation.Repository;
using Plantation.Repository.Interface;
using Plantation.Models.DB;

namespace Plantation.Controllers
{
    public class CompanyController : Controller
    {
        private CompanyRepository ICP = new CompanyRepository();
        // GET: Company
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ICP.GetAll());
        }

        //
        // GET: /Company/Details/5
        public ActionResult Details(int? id)
        {

            return View(ICP.Find(id));
        }

        //
        // GET: /Company/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Company/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "SID, IDCOMPANY, COMPANYNAME, ADDRESS1, ADDRESS2, COUNTRY, PROVINCE, CITY, TELEPHONE1, TELEPHONE2, FAX1, FAX2, EMAIL, WEBSITE, POSCODE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE")] Company company)
        {
            if (ModelState.IsValid)
            {
                ICP.Add(company);
                return RedirectToAction("Index");
            }

            return View(company);
        }

        //
        // GET: /Company/Edit/5
        public ActionResult Edit(int id)
        {
            return View(ICP.Find(id));
        }

        // POST: /Company/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "SID, IDCOMPANY, COMPANYNAME, ADDRESS1, ADDRESS2, COUNTRY, PROVINCE, CITY, TELEPHONE1, TELEPHONE2, FAX1, FAX2, EMAIL, WEBSITE, POSCODE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE")] Company company, int id)
        {
            if (ModelState.IsValid)
            {
                ICP.Update(company);
            }
            return View(company);
        }

        //
        // GET: /Company/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ICP.Find(id));
        }

        //
        // POST: /Company/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ICP.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}