using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class SecurityUpdateStateViewModel
    {
        public string gatePassCode { get; set; }

        public string confirmRFID { get; set; }

        public string updateStateCode { get; set; }
    }
}
