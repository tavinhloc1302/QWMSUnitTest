using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Repository
{
    public interface ITokenRepository :  ISyncRepository<Token>
    {

    }
}
