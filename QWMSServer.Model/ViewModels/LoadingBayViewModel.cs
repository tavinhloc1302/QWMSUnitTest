using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class LoadingBayViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string nameVi { get; set; }

        public string nameEn { get; set; }

        public int warehouseID { get; set; }

        public bool isDelete { get; set; }

        public WarehouseViewModel warehouse { get; set; }

        public ICollection<LaneViewModel> lanes { get; set; }
    }
}
