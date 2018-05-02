using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class GroupFunctionMapViewModel
    {
        public int systemFunctionID { get; set; }
        public SystemFunctionViewModel systemFunction { get; set; }
    }
}
