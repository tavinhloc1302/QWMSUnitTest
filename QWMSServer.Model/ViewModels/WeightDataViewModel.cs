using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class WeightDataViewModel
    {
        public int gatePassID { get; set; }

        public string fontCameraName { get; set; }

        public string gearCameraName { get; set; }

        public string cabinCameraName { get; set; }

        public string containerCameraName { get; set; }

        public int employeeID { get; set; }

        public float weightValue { get; set; }

        public int weighBridgeID { get; set; }
    }
}
