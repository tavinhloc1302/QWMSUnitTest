using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class EmployeeRepositoryTest : RepositoryBaseTest<Employee>, IEmployeeRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<Employee> GetObjectList()
        {
            return new List<Employee>() {
                DataRecords.EMPLOYEE_NORMAL,
                DataRecords.EMPLOYEE_DELETED,
            };
        }

        public override async Task<Employee> GetAsync(Expression<Func<Employee, bool>> where)
        {
            var result = DataRecords.EMPLOYEE_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.EMPLOYEE_NORMAL);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.EMPLOYEE_DELETED);
                    break;
            }
            return result;
        }
    }
}
