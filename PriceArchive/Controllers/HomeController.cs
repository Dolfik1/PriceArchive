using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PriceArchive.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var cookie = new HttpCookie() 
            { 
                Name = "auth_cookie",
                Value = DateTime.Now.ToString("dd.MM.yyyy"),
                Expires = DateTime.Now.AddMinutes(10),
            };
            Response.SetCookie(cookie);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Куда я попал?";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Контакты.";

            return View();
        }
    }
}