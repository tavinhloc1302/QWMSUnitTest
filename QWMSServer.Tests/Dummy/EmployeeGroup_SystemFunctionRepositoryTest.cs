using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class EmployeeGroup_SystemFunctionRepositoryTest : RepositoryBaseTest<EmployeeGroup_SystemFunction>, IEmployeeGroup_SystemFunctionRepository
    {
        public override IList<EmployeeGroup_SystemFunction> GetObjectList()
        {
            throw new NotImplementedException();
        }
    }
}
