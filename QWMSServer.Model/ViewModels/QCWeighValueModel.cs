using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class QCWeighValueModel
    {
        public int employeeID { get; set; }

        public int gatePassID { get; set; }

        public float QCWeighValue { get; set; }

        public string employeeRFID { get; set; }
    }
}
