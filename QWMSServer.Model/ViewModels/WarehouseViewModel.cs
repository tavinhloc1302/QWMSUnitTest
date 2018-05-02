using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class WarehouseViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string nameVi { get; set; }

        public string nameEn { get; set; }

        public int plantID { get; set; }

        public bool isDelete { get; set; }

        public PlantViewModel plant { get; set; }

        public virtual ICollection<LoadingBayViewModel> loadingBays { get; set; }
    }
}
