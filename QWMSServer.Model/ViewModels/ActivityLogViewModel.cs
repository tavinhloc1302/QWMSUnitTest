using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class ActivityLogViewModel
    {

        public int? employeeID { get; set; }

        public string employeeName { get; set; }

        public int? driverID { get; set; }

        public DateTime logTime { get; set; }

        public string screen { get; set; }

        public string action { get; set; }

        public string target { get; set; }

        public string targetValue { get; set; }

        public string StargetValue { get; set; }

        public string result { get; set; }

        public string comment { get; set; }

        public int? currentStateID { get; set; }

        public int? queueID { get; set; }

        public int? RFIDcardID { get; set; }

        public int? gatePassID { get; set; }


        public EmployeeViewModel employee { get; set; }

        public DriverViewModel driver { get; set; }

        public StateViewModel state { get; set; }

        public RFIDCardViewModel RFIDCard { get; set; }

        public GatePassViewModel gatePass { get; set; }
    }
}
