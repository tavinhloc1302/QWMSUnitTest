using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class LaneViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string nameVi { get; set; }

        public string nameEn { get; set; }

        public int loadingBayID { get; set; }

        public int truckTypeID { get; set; }

        public int loadingTypeID { get; set; }

        public int minCapacity { get; set; }

        public int maxCapactity { get; set; }

        public bool isDelete { get; set; }

        public int status { get; set; }

        public int usingStatus { get; set; }

        public LoadingBayViewModel loadingBay { get; set; }

        public TruckTypeViewModel truckType { get; set; }

        public LoadingTypeViewModel loadingType { get; set; }
    }
}
