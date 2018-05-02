using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class OrderViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public int orderTypeID { get; set; }

        public float grossWeight { get; set; }

        public int gatePassID { get; set; }

        public int warehouseID { get; set; }

        public int plantID { get; set; }

        public int doID { get; set; }

        public int poID { get; set; }

        public OrderTypeViewModel orderType { get; set; }

        public GatePassViewModel gatePass { get; set; }

        public WarehouseViewModel wareshouse { get; set; }

        //public PlantViewModel plant { get; set; }

        public DeliveryOrderViewModel deliveryOrder { get; set; }

        public PurchaseOrderViewModel purchaseOrder { get; set; }

        public ICollection<OrderMaterialViewModel> orderMaterials { get; set; }
    }
}
