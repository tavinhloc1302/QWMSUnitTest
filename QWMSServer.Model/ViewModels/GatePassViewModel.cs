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

        public DateTime? createDate { get; set; }

        public string driverCamCapturePath { get; set; }

        public DateTime? enterTime { get; set; }

        public DateTime? leaveTime { get; set; }

        public DateTime? predictLeaveTime { get; set; }

        public DateTime? predictEnterTime { get; set; }

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

        public ICollection<WeightRecordViewModel> weightRecords { get; set; }

        public int loadingBayID { get; set; }

        public CustomerViewModel customer { get; set; }

        public CarrierVendorViewModel carrierVendor { get; set; }

        public MaterialViewModel material { get; set; }

        public int weightType { get; set; }

        public WarehouseViewModel warehouse { get; set; }

        public LoadingBayViewModel loadingBay { get; set; }

        public float? tareWeightValue { get; set; }

        public float? netWeightValue { get; set; }

        public string sealNo { get; set; } 

        public string printGoods { get; set; } 

        public float? registGrossWeight { get; set; } // 23

        public float? registNetWeight { get; set; }

        public float? QCGrossWeight { get; set; }

    }
}
