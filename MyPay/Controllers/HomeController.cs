using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            //RepNCHL.GetBankList("cips");
           if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "AdminLogin");
            }
            else
            {
                return RedirectToAction("Index", "AdminLogin");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}