using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class EmployeeGroupRepository : AsyncRepository<EmployeeGroup>, IEmployeeGroupRepository
	{
		public EmployeeGroupRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
