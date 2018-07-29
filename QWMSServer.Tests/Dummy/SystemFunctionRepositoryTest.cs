using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class SystemFunctionRepositoryTest : RepositoryBaseTest<SystemFunction>, ISystemFunctionRepository
    {
        public override IList<SystemFunction> GetObjectList()
        {
            throw new NotImplementedException();
        }
    }
}
