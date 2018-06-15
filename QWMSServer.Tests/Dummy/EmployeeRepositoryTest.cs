using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public override async Task<Employee> GetAsync(Expression<Func<Employee, bool>> where)
        {
            return new Employee()
            {
                ID = 1,
                code = "0123",
                isDelete = false,
                firstName = "Van Hoang",
                lastName = "Dinh",
                userID = null,
                user = null
            };
        }
    }
}
