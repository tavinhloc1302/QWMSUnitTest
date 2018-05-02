using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class CarrierVendorViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string nameVi { get; set; }

        public string nameEn { get; set; }

        public string shortName { get; set; }

        public string addressVi { get; set; }

        public string addressEn { get; set; }

        public string taxCode { get; set; }

        public string contactPerson { get; set; }

        public string department { get; set; }

        public string telNo { get; set; }

        public bool isDelete { get; set; }
    }
}
