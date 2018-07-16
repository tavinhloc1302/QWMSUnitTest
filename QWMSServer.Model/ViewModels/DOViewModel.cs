using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class DOViewModel
    {
        public string dayCreate { get; set; }
        public string dOCode { get; set; }
        public string dOItemCode { get; set; }
        public string materialCode { get; set; }
        public string materialName { get; set; }
        public int quanlity { get; set; }
        public string unit { get; set; }
        public string sOCode { get; set; }
        public string customerCode { get; set; }
        public string customerName { get; set; }
        public string shipToCode { get; set; }
        public string warehouseName { get; set; }
        public string deliveryAddress { get; set; }
        public string carrierCode { get; set; }
        public string carrierName { get; set; }
        public string plant { get; set; }
        public string sLoc { get; set; }
        public string remark { get; set; }
    }
}
