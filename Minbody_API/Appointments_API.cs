using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mindbody_API.mindbodyonline.appointments;

namespace Mindbody_API
{
    public class Appointments_API
    {
        const string SOURCE_NAME = "ENTER_API_NAME";
        const string SOURCE_PASSWORD = "ENTER_API_KEY";
        private static int[] SITE_IDS = {-99};

        public static GetBookableItemsResult GetBookableItems() {
            ///////////////////////
            // Standard API call //
            ///////////////////////

            // Create Service
            AppointmentService service = new AppointmentService();

            // Create request
            GetBookableItemsRequest request = new GetBookableItemsRequest();

            // Create and fill credentials
            request.SourceCredentials = new SourceCredentials();
            request.SourceCredentials.SourceName = SOURCE_NAME;
            request.SourceCredentials.Password = SOURCE_PASSWORD;
            request.SourceCredentials.SiteIDs = SITE_IDS;

            // Run call with request and fill result  
            GetBookableItemsResult result = service.GetBookableItems(request);

            return result;
        }
    }
}
