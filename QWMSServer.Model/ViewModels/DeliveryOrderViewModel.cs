using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class DeliveryOrderViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        //public string doNumber { get; set; }

        public DateTime createDate { get; set; }

        public int? soID { get; set; }

        public int? customerID { get; set; }

        public int? carrierVendorID { get; set; }

        public string plantCode { get; set; }

        public string remark { get; set; }

        public string sloc { get; set; }

        public int? doTypeID { get; set; }

        public int? customerWarehouseID { get; set; }

        public bool isDelete { get; set; }

        public CustomerWarehouseViewModel customerWarehouse { get; set; }

        public CustomerViewModel customer { get; set; }

        public CarrierVendorViewModel carrierVendor { get; set; }

        public SaleOrderViewModel saleOrder { get; set; }

        public ICollection<OrderViewModel> order { get; set; }

        public DeliveryOrderTypeViewModel deliveryOrderType { get; set; }
    }
}
