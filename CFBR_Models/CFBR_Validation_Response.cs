using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBR_Models
{
    public class CFBR_Validation_Response
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public object DataObj { get; set; }
    }
}
