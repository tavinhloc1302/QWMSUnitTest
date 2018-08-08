using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class EmployeeGroupRepositoryTest : RepositoryBaseTest<EmployeeGroup>, IEmployeeGroupRepository
    {
        public static int FLAG_DELETE = 0;
        public override IList<EmployeeGroup> GetObjectList()
        {
            return new List<EmployeeGroup>() {
                DataRecords.EMPLOYEE_GROUP_NORMAL,
                DataRecords.EMPLOYEE_GROUP_DELETED
            };
        }
        public override async Task<EmployeeGroup> GetAsync(Expression<Func<EmployeeGroup, bool>> where)
        {
            var result = DataRecords.EMPLOYEE_GROUP_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0:
                    result = this.SimpleGetPatcher(DataRecords.EMPLOYEE_GROUP_NORMAL);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.EMPLOYEE_GROUP_DELETED);
                    break;
            }
            return result;
        }
    }
}
