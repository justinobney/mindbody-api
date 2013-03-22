using System.Web;
using System.Web.Mvc;
using CFBR_Appointments.Security.Attributes;

namespace CFBR_Appointments
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RequireAuthenticationAttribute());
            //filters.Add(new CFBR_Appointments.Security.Attributes.AllowAnonymousAttribute());
        }
    }
}