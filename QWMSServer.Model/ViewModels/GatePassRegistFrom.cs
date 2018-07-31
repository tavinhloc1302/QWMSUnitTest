using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class GatePassRegistFrom
    {
        public int gatePassID { get; set; }

        public string driverImageFileName { get; set; }

        public string employeeRFID { get; set; }

        public string driverRFID { get; set; }

        public int loadingBayID { get; set; }
    }
}
