using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ERPAnimalia.Mapper;
using System.Globalization;
using System.Threading;

namespace ERPAnimalia
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();
            var currentCulture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                currentCulture.NumberFormat.NumberDecimalSeparator = ".";
               currentCulture.NumberFormat.NumberGroupSeparator = " ";
               currentCulture.NumberFormat.CurrencyDecimalSeparator = ".";

               Thread.CurrentThread.CurrentCulture = currentCulture;


        }
        //protected void Application_BeginRequest()
        //{
        //    var currentCulture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        //    currentCulture.NumberFormat.NumberDecimalSeparator = ".";
        //    currentCulture.NumberFormat.NumberGroupSeparator = " ";
        //    currentCulture.NumberFormat.CurrencyDecimalSeparator = ".";

        //    Thread.CurrentThread.CurrentCulture = currentCulture;
        //    //Thread.CurrentThread.CurrentUICulture = currentCulture;
        //}
    }
}
