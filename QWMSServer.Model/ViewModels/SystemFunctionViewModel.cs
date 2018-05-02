using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class SystemFunctionViewModel
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string functionName { get; set; }

        public string functionDescription { get; set; }

        public string API { get; set; }
    }
}
