using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class EmployeeRepository : AsyncRepository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
