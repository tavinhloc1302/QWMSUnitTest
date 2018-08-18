using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class EmployeeRoleRepositoryTest : RepositoryBaseTest<EmployeeRole>, IEmployeeRoleRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<EmployeeRole> GetObjectList()
        {
            return new List<EmployeeRole>()
            {
                DataRecords.EMPLOYEE_ROLE_NORMAL,
                DataRecords.EMPLOYEE_ROLE_DELETED
            };
        }

        public override async Task<EmployeeRole> GetAsync(Expression<Func<EmployeeRole, bool>> where)
        {
            var result = DataRecords.EMPLOYEE_ROLE_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.EMPLOYEE_ROLE_DELETED);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.EMPLOYEE_ROLE_NORMAL);
                    break;
            }
            return result;
        }
    }
}
