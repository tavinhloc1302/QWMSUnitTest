using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class ReportViewModel
    {
        public int reportType { get; set; }

        public int weightID { get; set; }

        public string gatePassCode { get; set; }

        public string truckPlateNumber { get; set; }

        public string driverName { get; set; }

        public string customerName { get; set; }

        public string materialName { get; set; }

        public string materialWeight { get; set; }

        public DateTime firstWeightTime { get; set; }

        public DateTime secondWeightTime { get; set; }

        public string firstWeightEmployeeName { get; set; }

        public string secondWeightEmployeeName { get; set; }

        public float firstWeightValue { get; set; }

        public float secondWeightvalue { get; set; }

        public int weightType { get; set; }

        public string frontCameraCapturePath { get; set; }

        public string gearCameraCapturePath { get; set; }

        public string cabinCameraCapturePath { get; set; }

        public string containerCameraCapturePath { get; set; }

        public DateTime weightTime { get; set; }

        public string weightEmployeeName { get; set; }

        public float weightvalue { get; set; }

        public float truckWeight { get; set; }

        public bool isOverWeight { get; set; }

        public int weightNo { get; set; }

        public int? printNo { get; set; }

        public DateTime printDate { get; set; }

        public string printEmployeeName { get; set; }

        public string secondFrontCameraCapturePath { get; set; }

        public string secondGearCameraCapturePath { get; set; }

        public string secondCabinCameraCapturePath { get; set; }

        public string secondContainerCameraCapturePath { get; set; }

        public bool isSuccess { get; set; } // 31

        public DateTime createDate { get; set; } // 32
    }
}
