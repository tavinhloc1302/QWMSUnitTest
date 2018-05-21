using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class CompanyViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string nameVi { get; set; }

        public string nameEn { get; set; }

        public bool isDelete { get; set; }
    }
}
