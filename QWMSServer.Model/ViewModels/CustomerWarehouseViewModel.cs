using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class CustomerWarehouseViewModel
    {
        public int ID { get; set; }

        public string deliveryCode { get; set; }

        public string warehouseName { get; set; }

        public string deliveryAddressVi { get; set; }

        public string deliveryAddressEn { get; set; }

        public bool isDelete { get; set; }

        public CustomerViewModel customer { get; set; }
    }
}
