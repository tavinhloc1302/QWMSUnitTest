using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class EmployeeRoleRepository : AsyncRepository<EmployeeRole>, IEmployeeRoleRepository
	{
		public EmployeeRoleRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
