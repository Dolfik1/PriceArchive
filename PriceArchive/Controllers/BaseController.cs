using Ninject;
using PriceArchive.Global.Auth;
using PriceArchive.Global.Config;
using PriceArchive.Mappers;
using PriceArchive.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PriceArchive.Controllers
{
    public abstract class BaseController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

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

        [Inject]
        public IConfig Config { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            try
            {
                var cultureInfo = new CultureInfo(Config.Lang);

                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
            catch (Exception ex)
            {
                logger.Error("Culture not found", ex);
            }

            base.Initialize(requestContext);
        }
    }
}
