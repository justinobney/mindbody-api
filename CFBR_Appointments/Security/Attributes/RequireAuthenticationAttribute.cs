using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Mvc;

namespace CFBR_Appointments.Security.Attributes
{
    public class RequireAuthenticationAttribute : AuthorizeAttribute
    {
        
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool skipAuthorization = isAuthorizedExemption(filterContext);

            if (!skipAuthorization)
            {
                IPrincipal user = filterContext.HttpContext.User;
                if (user != null && user.Identity != null & user.Identity.IsAuthenticated)
                {

                    bool cflag = AuthorizeCore(filterContext.HttpContext);

                    cflag = (cflag || authorizeRoles(user)) && filterContext.HttpContext.Session["user"] != null;

                    if (!cflag)
                    {
                        sendAccessDenial(filterContext);
                    }
                }
                else
                {
                    base.OnAuthorization(filterContext);
                }
            }
        }


        protected bool isAuthorizedExemption(AuthorizationContext filterContext)
        {
            bool result = false;        // by default, do not exempt request from authorization
            //if (filterContext.HttpContext.Request.IsLocal)
            //{
            //    // if this is a local request, it might be exempted, if the current user is not already authenticated.
            //    if (filterContext.HttpContext.User.Identity.IsAuthenticated == false)
            //    {
            //        result = false;
            //    }

            //}
            //else
            //{
                
            //}

            // if the current action or controller have the AllowAnonymous attribute, the request is exempt from
            // authorization.
            result = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                 filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
                     typeof(AllowAnonymousAttribute), true);


            return result;
        }



        protected bool authorizeRoles(IPrincipal thisUser)
        {
            bool result = isInOverideRole(thisUser);

            if (!result)
            {
                foreach (string thisRole in Roles.Split(','))
                {
                    result = thisUser.IsInRole(thisRole);
                    if (result) break;     // if we find a match, stop looking
                }
            }
            return result;
        }


        protected void sendAccessDenial(AuthorizationContext filterContext)
        {
            string errorText = string.Format("User does not have access to the {0} action for {1}.", filterContext.ActionDescriptor.ActionName, filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
            filterContext.Controller.TempData["ErrorMessage"] = errorText;
            filterContext.Result = new RedirectToRouteResult("Default", new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login" }));
        }

        protected virtual bool isInOverideRole(IPrincipal thisUser)
        {
            if (thisUser == null)
                throw new ArgumentNullException("thisUser", "thisUser is null.");

            return thisUser.IsInRole("administrator");
        }
    }


}
