using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class ResponseViewModel<T> where T : class
    {
        public int errorCode { get; set; }
        public string errorText { get; set; }
        public IEnumerable<T> responseDatas { get; set; }
        public T responseData { get; set; }
        public bool booleanResponse { get; set; }
        public LogViewModel logViewModel { get; set; }
    }
}
