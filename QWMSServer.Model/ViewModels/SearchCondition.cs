using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class SearchCondition
    {
        public int reportType { get; set; }

        public DateTime fromDate { get; set; }

        public DateTime toDate { get; set; }

        public string gatePassCode { get; set; }

        public int materialWeight { get; set; }

        public int customerID { get; set; }

        public int weightType { get; set; }

        public int weightEmployeeID { get; set; }

        public int printEmployeeID { get; set; }

        public int materialID { get; set; }

        public int weighBridgeID { get; set; }

        public string plateNumber { get; set; }

        public int truckID { get; set; }

        public int currentEmployeeID { get; set; }

        public int currentEmployeeGroupID { get; set; }

        public string actionScreen { get; set; }

        public string actionType { get; set; }

        public string target { get; set; }

        public string targetValue { get; set; }

        public int driverID { get; set; }
    }
}
