using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace CFBR_Appointments
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// While authenticating a request, this method checks the user context and extracts the roles from the FormsAuthenticationTicket.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext ccontext = HttpContext.Current;
            if (ccontext.User != null && ccontext.User.Identity.IsAuthenticated)
            {
                FormsIdentity fi = ccontext.User.Identity as FormsIdentity;
                if (fi != null)
                {
                    FormsAuthenticationTicket tick = fi.Ticket;
                    string tdat = tick.UserData;
                    GenericPrincipal userPrin = new GenericPrincipal(fi, tdat.Split(','));
                    ccontext.User = userPrin;
                }
            }
        }
    }
}