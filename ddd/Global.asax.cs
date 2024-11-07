using GSD.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ddd
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_BeginRequest(object sender, EventArgs e) //automaticaly run when program is start
        {
            var persianCulture = new PersianCulture();
            persianCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            persianCulture.DateTimeFormat.LongDatePattern = "dddd d MMMM yyyy";
            persianCulture.DateTimeFormat.AMDesignator = "?.?";
            persianCulture.DateTimeFormat.PMDesignator = "?.?";
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;
        }
    }
}
