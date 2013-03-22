using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CFBR_Appointments.Helpers;
using CFBR_Models;

namespace CFBR_Appointments.Controllers
{
    [CFBR_Appointments.Security.Attributes.AllowAnonymous]
    public class AccountController : Controller
    {
        //
        // GET: /Accounts/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            CFBR_Validation_Response resp = Mindbody_API.Clients_API.LoginClient(collection["username"], collection["password"]);

            if (resp.IsValid)
            {
                CFBR_User user = (CFBR_User)resp.DataObj;
                HttpCookie faCook = BuildFormsAuthCookie(user.SessionGuid, 1);
                Response.Cookies.Add(faCook);

                if (collection["remember"] != "false") {
                    string cookie_val = "true|" + collection["username"] + "|" + collection["password"];
                    Response.SetCookie(
                        new HttpCookie("remember", cookie_val)
                    );
                }
                
                Session["user"] = user;

                return RedirectToAction("Index", "Home");
            }
            else {
                HttpCookie cookie1 = new HttpCookie("remember", "");
                cookie1.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(cookie1);

                this.AddErrorMessage("Incorrect Username or Password");
                return View();
            }
        }

        public ActionResult Logout() 
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            HttpCookie cookie3 = new HttpCookie("remember", "");
            cookie3.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie3);

            return RedirectToAction("Login");
        }

        protected HttpCookie BuildFormsAuthCookie(string userName, double hoursToExpire)
        {
	        return BuildFormsAuthCookieShared(Helper.GetRequest(), userName, hoursToExpire);
        }

        public static HttpCookie BuildFormsAuthCookieShared(HttpRequest request, string userName, double hoursToExpire)
        {
	        HttpCookie result = default(HttpCookie);

	        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddHours(hoursToExpire), true, "");
	        string encrData = FormsAuthentication.Encrypt(ticket);
	        result = new HttpCookie(FormsAuthentication.FormsCookieName, encrData);
	        result.Expires = ticket.Expiration;
	        result.Path = FormsAuthentication.FormsCookiePath;
	        result.Domain = request.IsLocal ? "" : FormsAuthentication.CookieDomain;

	        return result;
        }

    }
}
