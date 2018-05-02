using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class ReWeightRecordRepository : AsyncRepository<ReWeightRecord>, IReWeightRecordRepository
	{
		public ReWeightRecordRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
