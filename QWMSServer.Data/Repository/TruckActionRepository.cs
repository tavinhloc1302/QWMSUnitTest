using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class TruckActionRepository : AsyncRepository<TruckGroup>, ITruckActionRepository
	{
		public TruckActionRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
