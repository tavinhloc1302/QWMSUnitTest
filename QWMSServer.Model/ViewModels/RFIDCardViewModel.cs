using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class RFIDCardViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public int status { get; set; }

        public bool isDelete { get; set; }
    }
}
