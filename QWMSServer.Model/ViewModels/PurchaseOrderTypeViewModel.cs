using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class PurchaseOrderTypeViewModel
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string description { get; set; }

        public bool isDelete{ get; set; }
    }
}
