using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CFBR_Appointments.Helpers
{
    public class Helper
    {
        public static HttpRequest GetRequest()
        {
            return HttpContext.Current.Request;
        }
    }
}