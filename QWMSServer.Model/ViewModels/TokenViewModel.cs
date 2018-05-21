using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class TokenViewModel
    {
        public string TokenString { get; set; }

        public DateTime IssuedOn { get; set; }

        public int ExpiresIn { get; set; }
    }
}
