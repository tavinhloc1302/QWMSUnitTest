using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

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
    }
}
