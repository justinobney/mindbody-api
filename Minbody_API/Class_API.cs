using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFBR_Models;
using Mindbody_API.mindbodyonline.classes;

namespace Mindbody_API
{
    public class Class_API
    {

        const string SOURCE_NAME = "ENTER_API_NAME";
        const string SOURCE_PASSWORD = "ENTER_API_KEY";
        private static int[] SITE_IDS = { -99 };

        const string MNGR_NAME = "owner";
        const string MNGR_PASSWORD = "demo1234";

        public static CFBR_Class[] GetAllClasses()
        {
            ///////////////////////
            // Standard API call //
            ///////////////////////

            // Create Service
            ClassService classService = new ClassService();

            // Create request
            GetClassesRequest request = new GetClassesRequest();

            // Create and fill credentials
            request.SourceCredentials = new SourceCredentials();
            request.SourceCredentials.SourceName = SOURCE_NAME;
            request.SourceCredentials.Password = SOURCE_PASSWORD;
            request.SourceCredentials.SiteIDs = SITE_IDS;

            request.UserCredentials = new UserCredentials();
            request.UserCredentials.Username = MNGR_NAME;
            request.UserCredentials.Password = MNGR_PASSWORD;
            request.UserCredentials.SiteIDs = SITE_IDS;

            request.StartDateTime = DateTime.Now;
            request.EndDateTime = DateTime.Now.Date.AddDays(7.0);

            //request.ClientID = "100015619";

            request.SchedulingWindow = true;
            //request.HideCanceledClasses = true;

            // Run call with request and fill result

            GetClassesResult gcr = classService.GetClasses(request);

            CFBR_Class[] result = gcr
                .Classes.Where(cl => cl.ClassDescription.Program.Name == "Classes").Select(c => new CFBR_Class()
                {
                    ID = c.ID,
                    Description = c.ClassDescription.Name,
                    StartTime = c.StartDateTime,
                    SignupCount = c.TotalBooked,
                    Cancelled = c.IsCanceled,
                    DataObj = c
                }).OrderBy(c => c.StartTime).ToArray();

            return result;
        }

        public static CFBR_Validation_Response BookClientClass(CFBR_User cf_user, int cf_class_id)
        {
            ///////////////////////
            // Standard API call //
            ///////////////////////

            // Create Service
            ClassService classService = new ClassService();

            // Create request
            AddClientsToClassesRequest request = new AddClientsToClassesRequest();

            // Create and fill credentials
            request.SourceCredentials = new SourceCredentials();
            request.SourceCredentials.SourceName = SOURCE_NAME;
            request.SourceCredentials.Password = SOURCE_PASSWORD;
            request.SourceCredentials.SiteIDs = SITE_IDS;

            string[] client_ids = { cf_user.ClientId }; // Add User ID
            request.ClientIDs = client_ids;

            int[] class_ids = { cf_class_id }; // Add Class ID
            request.ClassIDs = class_ids;

            request.RequirePayment = false;

            request.Test = false;

            // Run call with request and fill result  
            AddClientsToClassesResult api_result = classService.AddClientsToClasses(request);

            CFBR_Validation_Response result = new CFBR_Validation_Response();
            
            result.DataObj = api_result;
            result.IsValid = (api_result.Status == StatusCode.Success);
            result.Message = api_result.Message;
            
            return result;           
        }

        public static CFBR_Validation_Response CancelClientClass(CFBR_User cf_user, int cf_class_id)
        {
            // Create Service
            ClassService classService = new ClassService();

            // Create request
            RemoveClientsFromClassesRequest request = new RemoveClientsFromClassesRequest();

            // Create and fill credentials
            request.SourceCredentials = new SourceCredentials();
            request.SourceCredentials.SourceName = SOURCE_NAME;
            request.SourceCredentials.Password = SOURCE_PASSWORD;
            request.SourceCredentials.SiteIDs = SITE_IDS;

            string[] client_ids = { cf_user.ClientId }; // Add User ID
            request.ClientIDs = client_ids;

            int[] class_ids = { cf_class_id }; // Add Class ID
            request.ClassIDs = class_ids;

            request.Test = false;

            // Run call with request and fill result  
            RemoveClientsFromClassesResult api_result = classService.RemoveClientsFromClasses(request);

            CFBR_Validation_Response result = new CFBR_Validation_Response();

            result.DataObj = api_result;
            result.IsValid = (api_result.Status == StatusCode.Success);
            result.Message = api_result.Message;

            return result;
        }
        
        public static GetEnrollmentsResult GetEnrollments()
        {
            // Create Service
            ClassService classService = new ClassService();

            // Create request
            GetEnrollmentsRequest request = new GetEnrollmentsRequest();

            // Create and fill credentials
            request.SourceCredentials = new SourceCredentials();
            request.SourceCredentials.SourceName = SOURCE_NAME;
            request.SourceCredentials.Password = SOURCE_PASSWORD;
            request.SourceCredentials.SiteIDs = SITE_IDS;

            request.UserCredentials = new UserCredentials();
            request.UserCredentials.Username = MNGR_NAME;
            request.UserCredentials.Password = MNGR_PASSWORD;
            request.UserCredentials.SiteIDs = SITE_IDS;

            request.EndDate = DateTime.Now.AddDays(1);
            string[] fields = { "Enrollments.Classes" };
            request.Fields = fields;

            return classService.GetEnrollments(request);
        }
    }
}
