using Mindbody_API.mindbodyonline.clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFBR_Models;

namespace Mindbody_API
{
    public class Clients_API
    {

        const string SOURCE_NAME = "ENTER_API_NAME";
        const string SOURCE_PASSWORD = "ENTER_API_KEY";
        private static int[] SITE_IDS = { -99 };

        const string MNGR_NAME = "owner";
        const string MNGR_PASSWORD = "demo1234";

        public static GetClientsResult GetAllClients() {

            ///////////////////////
            // Standard API call //
            ///////////////////////

            // Create Service
            ClientService service = new ClientService();

            // Create request
            GetClientsRequest request = new GetClientsRequest();

            // Create and fill credentials
            request.SourceCredentials = new SourceCredentials();
            request.SourceCredentials.SourceName = SOURCE_NAME;
            request.SourceCredentials.Password = SOURCE_PASSWORD;
            request.SourceCredentials.SiteIDs = SITE_IDS;

            request.UserCredentials = new UserCredentials();
            request.UserCredentials.Username = MNGR_NAME;
            request.UserCredentials.Password = MNGR_PASSWORD;
            request.UserCredentials.SiteIDs = SITE_IDS;

            request.PageSize = 50;

                //string[] client_ids = {"100015484"};
            //request.ClientIDs = client_ids;

            request.SearchText = "";

            // Run call with request and fill result  
            GetClientsResult result = service.GetClients(request);

            return result;
        }

        public static CFBR_Validation_Response LoginClient(string username, string password) {
            ClientService service = new ClientService();

            // Create request
            ValidateLoginRequest request = new ValidateLoginRequest();

            // Create and fill credentials
            request.SourceCredentials = new SourceCredentials();
            request.SourceCredentials.SourceName = SOURCE_NAME;
            request.SourceCredentials.Password = SOURCE_PASSWORD;
            request.SourceCredentials.SiteIDs = SITE_IDS;

            request.Username = username;
            request.Password = password;

            ValidateLoginResult api_result = service.ValidateLogin(request);

            CFBR_Validation_Response result = new CFBR_Validation_Response();

            if (api_result.Status == StatusCode.Success)
            {
                result.IsValid = true;
                result.DataObj = new CFBR_User()
                {
                    ClientId = api_result.Client.ID,
                    Username = api_result.Client.Email,
                    SessionGuid = api_result.GUID
                };
                result.Message = "SUCCESS";
            }
            else {
                result.IsValid = false;
                result.Message = "Error logging in";
            }

            return result;
        }

        public static CFBR_Class[] GetClientSchedule(string clients_id)
        {
            // Create Service
            ClientService service = new ClientService();

            // Create request
            GetClientScheduleRequest request = new GetClientScheduleRequest();

            // Create and fill credentials
            request.SourceCredentials = new SourceCredentials();
            request.SourceCredentials.SourceName = SOURCE_NAME;
            request.SourceCredentials.Password = SOURCE_PASSWORD;
            request.SourceCredentials.SiteIDs = SITE_IDS;

            request.UserCredentials = new UserCredentials();
            request.UserCredentials.Username = MNGR_NAME;
            request.UserCredentials.Password = MNGR_PASSWORD;
            request.UserCredentials.SiteIDs = SITE_IDS;

            request.ClientID = clients_id;
            request.EndDate = DateTime.Now.AddDays(7);

            CFBR_Class[] result = service.GetClientSchedule(request)
                .Visits
                .Where(c => c.StartDateTime > DateTime.Now.AddHours(-1.0))
                .Select(v => new CFBR_Class()
                {
                    ID = v.ClassID,
                    StartTime = v.StartDateTime
                }).ToArray();

            return result;
        }
    }
}
