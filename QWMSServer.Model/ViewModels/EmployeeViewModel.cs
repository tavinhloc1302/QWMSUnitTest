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

        public int? RFIDCardID { get; set; }

        public int? EmployeeGroupID { get; set; }

        public RFIDCard rfidCard { get; set; }

        public ICollection<UserViewModel> users { get; set; }

        public EmployeeGroup employeeGroup { get; set; }
    }
}
