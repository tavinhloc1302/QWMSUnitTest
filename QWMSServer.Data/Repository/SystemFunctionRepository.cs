using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class SystemFunctionRepository : AsyncRepository<SystemFunction>, ISystemFunctionRepository
	{
		public SystemFunctionRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
