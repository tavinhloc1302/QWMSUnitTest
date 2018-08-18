using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class EmployeeGroup_SystemFunctionRepositoryTest : RepositoryBaseTest<EmployeeGroup_SystemFunction>, IEmployeeGroup_SystemFunctionRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<EmployeeGroup_SystemFunction> GetObjectList()
        {
            return new List<EmployeeGroup_SystemFunction>() {
                DataRecords.ESFUNCTION_DELETED,
                DataRecords.ESFUNCTION_NORMAL,
            };
        }

        public override async Task<EmployeeGroup_SystemFunction> GetAsync(Expression<Func<EmployeeGroup_SystemFunction, bool>> where)
        {
            var result = DataRecords.ESFUNCTION_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.ESFUNCTION_DELETED);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.ESFUNCTION_NORMAL);
                    break;
            }
            return result;
        }
    }
}
