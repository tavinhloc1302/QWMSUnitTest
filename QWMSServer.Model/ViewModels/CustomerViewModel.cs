using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class CustomerViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string nameVi { get; set; }

        public string nameEn { get; set; }

        public string shortName { get; set; }

        public string invoiceAddressVi { get; set; }

        public string invoiceAddressEn { get; set; }

        public string taxCode { get; set; }

        public string contactPerson { get; set; }

        public string telNo { get; set; }

        public string faxNo { get; set; }

        public string email { get; set; }

        public virtual ICollection<CustomerWarehouseViewModel> customerWarehouses { get; set; }

        public bool isDelete { get; set; }
    }
}
