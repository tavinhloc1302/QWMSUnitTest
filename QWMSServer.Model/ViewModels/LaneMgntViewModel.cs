using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class LaneMgntViewModel
    {
        public int ID { get; set; }

        public string LaneName { get; set; }

        public int status { get; set; }

        public string usingStatus { get; set; }

        public DateTime? inTime { get; set; }

        public DateTime? outTime { get; set; }

        public int KPI { get; set; }

        public string truckType { get; set; }

        public int progress { get; set; }

        public string plateNumber { get; set; }

        public string loadingBay { get; set; }
    }
}
