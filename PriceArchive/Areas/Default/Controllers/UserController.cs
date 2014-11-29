using PriceArchive.Global;
using PriceArchive.Model;
using PriceArchive.Models.Info;
using PriceArchive.Models.ViewModels;
using PriceArchive.Tools;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PriceArchive.Areas.Default.Controllers
{
    public class UserController : DefaultController
    {
        public ActionResult Index(int page = 1, string searchString = null)
        {
            ViewBag.Search = searchString;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var list = SearchEngine.Search(searchString, Repository.Users).AsQueryable();
                PageableData<User> data = new PageableData<User>(list, page, 5);
                return View(data);
            }
            else
            {
                PageableData<User> data = new PageableData<User>(Repository.Users, page, 5);
                return View(data);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            var newUserView = new UserView();
            return View(newUserView);
        }

        [HttpPost]
        public ActionResult Register(UserView userView)
        {
            if (userView.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Текст с картинки введен неверно");
            }
            var anyUser = Repository.Users.Any(p => string.Compare(p.Email, userView.Email) == 0);
            if (anyUser)
            {
                ModelState.AddModelError("Email", "Пользователь с таким email уже зарегистрирован");
            }
            anyUser = Repository.Users.Any(p => string.Compare(p.Login, userView.Login) == 0);
            if (anyUser)
            {
                ModelState.AddModelError("Login", "Пользователь с таким логином уже зарегистрирован");
            }
            

            if (ModelState.IsValid)
            {
                var user = (User)ModelMapper.Map(userView, typeof(UserView), typeof(User));

                Repository.CreateUser(user);
                return RedirectToAction("Index");
            }
            return View(userView);
        }

        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Arial");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return null;
        }

    }
}
