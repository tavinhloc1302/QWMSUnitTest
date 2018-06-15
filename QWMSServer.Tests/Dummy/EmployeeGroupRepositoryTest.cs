using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class EmployeeGroupRepositoryTest : RepositoryBaseTest<EmployeeGroup>, IEmployeeGroupRepository
    {
        public override IList<EmployeeGroup> GetObjectList()
        {
            return new List<EmployeeGroup>() {
                new EmployeeGroup() {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    description = "Group 1"
                },
                new EmployeeGroup() {
                    code = "3210",
                    ID = 2,
                    isDelete = false,
                    description = "Group 2"
                }
            };
        }
        public override async Task<EmployeeGroup> GetAsync(Expression<Func<EmployeeGroup, bool>> where)
        {
            return new EmployeeGroup()
            {
                code = "0123",
                ID = 1,
                isDelete = false,
                description = "Group 1"
            };
        }
    }
}
