using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace PriceArchive.Controllers
{
    public class AdminPanelController : Controller
    {
        public ActionResult IndexPanel()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Вход в админ-панель";

            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Message = "Пользователи.";

            return View();
        }

        public ActionResult Shops()
        {
            ViewBag.Message = "Магазины.";

            return View();
        }
    }
}