using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class EmployeeGroup_SystemFunctionRepository : AsyncRepository<EmployeeGroup_SystemFunction>, IEmployeeGroup_SystemFunctionRepository
	{
		public EmployeeGroup_SystemFunctionRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
