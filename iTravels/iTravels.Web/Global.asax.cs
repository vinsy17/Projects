using Castle.Windsor;
using Sample.Utilities;
using Sample.Utilities.Installers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace iTravels.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();

        }
        protected void Application_End()
        {
            if (container != null)
            {
                container.Dispose();
            }
        }
        protected void Application_EndRequest()
        {
            if (Context.Items["AjaxPermissionDenied"] is bool)
            {
                Context.Response.StatusCode = 401;
                Context.Response.End();
            }
        }
        private static void BootstrapContainer()
        {
            container = new WindsorContainer();
            container.Install(new RepositoriesInstaller());
            container.Install(new ServicesInstaller());
            container.Install(new ControllersInstaller());
            //container.Install(new RepositoryInstaller());

            //container = new WindsorContainer()
            //    .Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}
