using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CFBR_Appointments.ViewModels;
using CFBR_Models;

namespace CFBR_Appointments.Controllers
{
    public class HomeController : Controller
    {

        //
        // GET: /Home/
        public ActionResult Index()
        {
            //Mindbody_API.mindbodyonline.appointments.GetBookableItemsResult result = Mindbody_API.Appointments_API.GetBookableItems();
            //CFBR_Class[] model = Mindbody_API.Class_API.GetAllClasses();
            //Mindbody_API.mindbodyonline.clients.GetClientsResult result = Mindbody_API.Clients_API.GetAllClients();
            //Mindbody_API.mindbodyonline.classes.AddClientsToClassesResult result = Mindbody_API.Class_API.BookClientClass();
            //Mindbody_API.mindbodyonline.clients.ValidateLoginResult result = Mindbody_API.Clients_API.LoginClient("{email}", "{password}");

            //return Json(Mindbody_API.Clients_API.GetClientSchedule(), JsonRequestBehavior.AllowGet); 

            CFBR_User user = (CFBR_User)Session["user"];
            SchedulePageViewModel model = new SchedulePageViewModel(user.ClientId);

            return View(model);
        }

        public ActionResult Schedule(int id)
        {
            CFBR_User user = (CFBR_User)Session["user"];
            CFBR_Validation_Response resp =  Mindbody_API.Class_API.BookClientClass(user, id);
            
            if (resp.IsValid)
            {
                this.AddSuccessMessage("Successful Booking");
            }
            else {
                this.AddErrorMessage(resp.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Cancel(int id)
        {
            CFBR_User user = (CFBR_User)Session["user"];
            CFBR_Validation_Response resp = Mindbody_API.Class_API.CancelClientClass(user, id);

            if (resp.IsValid)
            {
                this.AddInfoMessage("Successfully canceled scheduled class.. :(");
            }
            else
            {
                this.AddErrorMessage(resp.Message);
            }

            return RedirectToAction("Index");
        }

    }
}
