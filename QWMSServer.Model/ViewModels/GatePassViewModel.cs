using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class GatePassViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public DateTime createDate { get; set; }

        public string driverCamCapturePath { get; set; }

        public DateTime enterTime { get; set; }

        public DateTime leaveTime { get; set; }

        public int RFIDCardID { get; set; }

        public int truckGroupID { get; set; }

        public DriverViewModel driver { get; set; }

        public TruckViewModel truck { get; set; }

        public StateViewModel state { get; set; }

        public TruckGroupViewModel truckGroup { get; set; }

        public RFIDCardViewModel RFIDCard { get; set; }

        //public EmployeeVieModel employee { get; set; }

        public ICollection<OrderViewModel> orders { get; set; }

        public ICollection<QueueListViewModel> queueLists { get; set; }

        public int loadingBayID { get; set; }
    }
}
