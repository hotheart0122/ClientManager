using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManagerApp.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            //ViewBag.ClientId = ConfigurationManager.AppSettings["googleClientId"];
            //ViewBag.Secret = ConfigurationManager.AppSettings["googleSecret"];
            //ViewBag.appId = ConfigurationManager.AppSettings["appId"];
            //ViewBag.appSecret = ConfigurationManager.AppSettings["appSecret"];

            return View();
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