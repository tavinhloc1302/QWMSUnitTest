using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class TheoryWeighValueModel
    {
        public int employeeID { get; set; }

        public int gatePassID { get; set; }

        public float theoryWeighValue { get; set; }

        public string employeeRFID { get; set; }
    }
}
