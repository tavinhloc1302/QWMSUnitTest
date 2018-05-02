using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class TruckTypeViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string desciption { get; set; }

        public bool isDelete { get; set; }
    }
}
