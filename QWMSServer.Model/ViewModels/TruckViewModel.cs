using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class TruckViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string plateNumber { get; set; }

        public float weightValueRegistWithCalofig { get; set; }

        public int carrierVendorID { get; set; }

        public float truckLenght { get; set; }

        public float truckHeight { get; set; }

        public float truckWidth { get; set; }

        public float containerLenght { get; set; }

        public float containerWidth { get; set; }

        public float containerHeight { get; set; }

        public float truckNetWeight { get; set; }

        public float weightValueRegistWithTransportDepartment { get; set; }

        public float totalWeight { get; set; }

        public int expireYear { get; set; }

        public int truckTypeID { get; set; }

        public int loadingTypeID { get; set; }

        public int KPI { get; set; }

        public bool isDelete { get; set; }

        public TruckTypeViewModel truckType { get; set; }

        public LoadingTypeViewModel loadingType { get; set; }

        public CarrierVendorViewModel carrierVendor { get; set; }
    }
}
