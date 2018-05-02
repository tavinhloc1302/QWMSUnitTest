using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class QueueListViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public int gatePassID { get; set; }

        public DateTime queueTime { get; set; }

        public int queueOrder { get; set; }

        public int estimateTime { get; set; }

        public int truckID { get; set; }

        public int truckGroupID { get; set; }

        public int laneID { get; set; }

        public bool isDelete { get; set; }

        public TruckViewModel truck { get; set; }

        public LaneViewModel lane { get; set; }

        public GatePassViewModel gatePass { get; set; }

        public TruckGroupViewModel truckGroup { get; set; }
    }
}
