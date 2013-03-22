using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class ControllerHelpers
    {
        public static void AddErrorMessage(this Controller controller, string errorMessage)
        {
            controller.TempData["ErrorMessage"] = errorMessage;
        }

        public static void AddSuccessMessage(this Controller controller, string successMessage)
        {
            controller.TempData["SuccessMessage"] = successMessage;
        }

        public static void AddInfoMessage(this Controller controller, string infoMessage)
        {
            controller.TempData["InfoMessage"] = infoMessage;
        }
    }
}
