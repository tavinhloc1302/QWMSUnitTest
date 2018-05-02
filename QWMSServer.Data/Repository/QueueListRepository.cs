using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class QueueListRepository : AsyncRepository<QueueList>, IQueueListRepository
	{
		public QueueListRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
