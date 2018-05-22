using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class PlantViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string nameVi { get; set; }

        public string nameEn { get; set; }

        public int companyID { get; set; }

        public bool isDelete { get; set; }

        public CompanyViewModel company { get; set; }
    }
}
