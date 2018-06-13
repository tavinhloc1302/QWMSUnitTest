using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public class EmployeeRepositoryTest : RepositoryBaseTest<Employee>, IEmployeeRepository
    {
        public override IList<Employee> GetObjectList()
        {
            return new List<Employee>() {
                DataRecords.EMPLOYEE_NORMAL,
                DataRecords.EMPLOYEE_DELTED,
            };
        }
    }
}
