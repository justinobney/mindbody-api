using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBR_Models
{
    public class CFBR_Class
    {
        public int? ID { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public string HumanDate { get { return StartTime.Value.ToString(); } }
        public int? SignupCount { get; set; }
        public bool Cancelled { get; set; }
        public object DataObj { get; set; }
    }
}
