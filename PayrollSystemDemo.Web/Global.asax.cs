﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PayrollSystemDemo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Creates Database & Seeds data
            //Database.SetInitializer<PayrollContext>(new PayrollDbInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
