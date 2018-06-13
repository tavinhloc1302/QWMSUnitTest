using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public class EmployeeRoleRepositoryTest : RepositoryBaseTest<EmployeeRole>, IEmployeeRoleRepository
    {
        public override IList<EmployeeRole> GetObjectList()
        {
            return new List<EmployeeRole>()
            {
                new EmployeeRole
                {
                    Code = "0123",
                    description = "Employee Role 1",
                    ID = 1,
                    isDelete = false
                },
                new EmployeeRole
                {
                    Code = "3210",
                    description = "Employee Role 2",
                    ID = 2,
                    isDelete = false
                }
            };
        }
    }
}
