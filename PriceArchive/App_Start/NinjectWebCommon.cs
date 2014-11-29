using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using PriceArchive.Model;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using PriceArchive.Mappers;
using PriceArchive.Global.Auth;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PriceArchive.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PriceArchive.App_Start.NinjectWebCommon), "Stop")]
namespace PriceArchive.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<PriceArchiveDbDataContext>().ToMethod(c => new PriceArchiveDbDataContext(ConfigurationManager.ConnectionStrings[1].ConnectionString));
            kernel.Bind<IRepository>().To<SqlRepository>().InRequestScope();
            kernel.Bind<IMapper>().To<CommonMapper>().InSingletonScope();
            kernel.Bind<IAuthentication>().To<CustomAuthentication>().InRequestScope();
        }
    }
}
