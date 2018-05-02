using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class WeightRecordRepository : AsyncRepository<WeightRecord>, IWeightRecordRepository
	{
		public WeightRecordRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
