using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class EmployeeViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public RFIDCardViewModel rfidCard { get; set; }

        //public ICollection<EmployeeGroupMapView> groupMaps { get; set; }
    }
}
