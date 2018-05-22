using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public bool isDelete { get; set; }

        public ICollection<EmployeeViewModel> employees { get; set; }

        public TokenViewModel token { get; set; }
    }
}
