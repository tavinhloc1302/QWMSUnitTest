using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class Employee_EmployeeGroupRepository : AsyncRepository<Employee_EmployeeGroup>, IEmployee_EmployeeGroupRepository
	{
		public Employee_EmployeeGroupRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
