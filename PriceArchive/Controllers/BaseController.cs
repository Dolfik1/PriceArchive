using Ninject;
using PriceArchive.Global.Auth;
using PriceArchive.Mappers;
using PriceArchive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PriceArchive.Controllers
{
    public abstract class BaseController : Controller
    {
        [Inject]
        public IRepository Repository { get; set; }

        [Inject]
        public IMapper ModelMapper { get; set; }

        [Inject]
        public IAuthentication Auth { get; set; }
        public User CurrentUser
        {
            get
            {
                return ((UserIndentity)Auth.CurrentUser.Identity).User;
            }
        }
    }
}
