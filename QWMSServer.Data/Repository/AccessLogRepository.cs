using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class AccessLogRepository : AsyncRepository<AccessLog>, IAccessLogRepository
	{
		public AccessLogRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
