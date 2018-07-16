using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class WeightRecordViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public DateTime weightTime { get; set; }

        public float weightValue { get; set; }

        public string frontCameraCapturePath { get; set; }

        public string gearCameraCapturePath { get; set; }

        public string cabinCameraCapturePath { get; set; }

        public string containerCameraCapturePath { get; set; }

        public int weightNo { get; set; }

        public bool isDelete { get; set; }

        public Employee employee { get; set; }

        public WeighBridge weighBridge { get; set; }

        public GatePass gatePass { get; set; }

        public bool isSuccess { get; set; }

        public bool isOverWeight { get; set; }

        public string comment { get; set; }

        public string PCIP { get; set; }
    }
}
