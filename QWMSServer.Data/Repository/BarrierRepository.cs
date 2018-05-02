using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class BarrierRepository : AsyncRepository<Barrier>, IBarrierRepository
	{
		public BarrierRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
