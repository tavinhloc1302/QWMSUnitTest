using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class DeliveryOrderTypeViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string description { get; set; }

        public bool isDelete { get; set; }
    }
}
