using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class DriverViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string nameVi { get; set; }

        public string nameViNoSign { get; set; }

        public string nameEn { get; set; }

        public string IDNo { get; set; }

        public string driverLicenseNo { get; set; }

        public string phoneNo { get; set; }

        public string remark { get; set; }

        public bool isDelete { get; set; }

        public string carrierNameVi { get; set; }

        public string carrierVendorCode { get; set; }

        public CarrierVendorViewModel carrierVendor { get; set; }
    }
}
