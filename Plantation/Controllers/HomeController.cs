using System.Collections.Generic;
using System.Web.Mvc;
using Plantation.Models.DB;
using Plantation.Repository;

namespace Plantation.Controllers
{
    public class HomeController : Controller
    {
        public LoginRepository db = new LoginRepository();
        // GET: Home
        public ActionResult Index()
        {
            if (Session["username"] == null) {
                return RedirectToAction("Login");
            } else {
                return RedirectToAction("Dashboard");
            }
       
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login a)
        {
            if (ModelState.IsValid) {
                Login data = db.FindByUsername(a.USERNAME);

                if (data != null)
                {
                    if (data.USERPASSWORD == a.USERPASSWORD)
                    {
                        Session["username"] = data.USERNAME;
                        Session["userid"] = data.SID;
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        TempData["Message"] = "Invalid Password";
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    TempData["Message"] = "Invalid Username";
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Login");
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public PartialViewResult Sidebar()
        {

            int userid = (int)System.Web.HttpContext.Current.Session["userid"];
            MenuRepository mn = new MenuRepository();
            List<MENU> retVal = mn.GetAllByUserId(userid);
            return PartialView(retVal);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");

        }
    }
}