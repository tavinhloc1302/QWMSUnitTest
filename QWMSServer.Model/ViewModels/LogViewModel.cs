using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class LogViewModel
    {
        public DateTime dateTime { get; set; }
        public int UserID { get; set; }
        public string cardReaderIP { get; set; }
        public string cardReadID { get; set; }
        public int funcID { get; set; }
        public int functionIDResult { get; set; }
        public string reasonOfResult { get; set; }
        public byte[] deviceStatus;
        public int gatePassID { get; set; }
        public int gatePassState { get; set; }
    }
}
