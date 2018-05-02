using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class MaterialViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string materialNameVi { get; set; }

        public string materialNameEn { get; set; }

        public int? unitID { get; set; }

        public double netWeight { get; set; }

        public float grossWeight { get; set; }

        public bool isDelete { get; set; }

        public UnitTypeViewModel unit { get; set; }
    }
}
