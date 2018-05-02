using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class StateRecordRepository : AsyncRepository<StateRecord>, IStateRecordRepository
	{
		public StateRecordRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
