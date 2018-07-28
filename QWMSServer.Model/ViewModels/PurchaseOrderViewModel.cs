using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class PurchaseOrderViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string poNumber { get; set; }

        //public DateTime createDate { get; set; }

        public int? carrierVendorID { get; set; }

        public string remark { get; set; }

        public string sloc { get; set; }

        public int? poTypeID { get; set; }

        public string invoiceNumber { get; set; }

        public bool isDelete { get; set; }

        public CarrierVendorViewModel carrierVendor { get; set; }

        public PurchaseOrderTypeViewModel purchaseOrderType { get; set; }
    }
}
