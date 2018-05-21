using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserHostAddress { get; set; }
        public string UserAgent { get; set; }
    }
}
