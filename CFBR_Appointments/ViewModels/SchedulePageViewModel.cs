using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CFBR_Models;

namespace CFBR_Appointments.ViewModels
{
    public class SchedulePageViewModel
    {
        public CFBR_Class[] AvailableClasses { get; set; }
        public CFBR_Class[] ScheduledClasses { get; set; }

        public IEnumerable<IGrouping<DateTime, CFBR_Class>> AvailableClassesGrouped { get { return AvailableClasses.GroupBy(ac => ac.StartTime.Value.Date); } }
        public IEnumerable<IGrouping<DateTime, CFBR_Class>> ScheduledClassesGrouped { get { return ScheduledClasses.GroupBy(ac => ac.StartTime.Value.Date); } }

        public SchedulePageViewModel(string clientID) {
            CFBR_Class[] allClasses = Mindbody_API.Class_API.GetAllClasses();
            ScheduledClasses = Mindbody_API.Clients_API.GetClientSchedule(clientID);

            //Only select classes that are not scheduled to display...
            AvailableClasses = allClasses.Where(
                c => !ScheduledClasses.Select(sc => sc.ID)
                    .Contains(c.ID)
                ).ToArray();

            //Load data that comes from all class query into scheduled classes.
            foreach (CFBR_Class item in ScheduledClasses)
            {
                CFBR_Class this_class = allClasses.SingleOrDefault(c => item.ID == c.ID);
                if (this_class != null)
                {
                    item.SignupCount = this_class.SignupCount;
                }
                else {
                    item.SignupCount = 0;
                }
            }
        }
    }
}