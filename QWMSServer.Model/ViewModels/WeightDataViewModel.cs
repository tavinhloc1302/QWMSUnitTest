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

        public string frontCameraName { get; set; }

        public string gearCameraName { get; set; }

        public string cabinCameraName { get; set; }

        public string containerCameraName { get; set; }

        public int employeeID { get; set; }

        public float weightValue { get; set; }

        public int weighBridgeID { get; set; }

        public string employeeRFID { get; set; }

        public bool isSuccess { get; set; }

        public string comment { get; set; }

        public bool isOverWeight { get; set; }

        public string PCIP { get; set; }
    }
}
