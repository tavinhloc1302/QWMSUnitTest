using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class TruckTypeRepository : AsyncRepository<TruckType>, ITruckTypeRepository
	{
		public TruckTypeRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
