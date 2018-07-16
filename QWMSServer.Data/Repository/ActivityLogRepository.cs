using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class ActivityLogRepository : AsyncRepository<ActivityLog>, IActivityLogRepository
	{
		public ActivityLogRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
