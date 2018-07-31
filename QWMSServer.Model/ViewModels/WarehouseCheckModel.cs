using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class WarehouseCheckModel
    {
        public string gatePassCode { get; set; }

        public List<OrderMaterialViewModel> orderMaterialViewModels { get; set; }

        public List<int> orderIDs { get; set; }

        public float QCGrossWeight { get; set; }
    }
}
