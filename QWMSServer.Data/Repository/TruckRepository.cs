using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class TruckRepository : AsyncRepository<Truck>, ITruckRepository
	{
		public TruckRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
