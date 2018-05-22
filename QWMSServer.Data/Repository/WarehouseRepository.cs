using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class WarehouseRepository : AsyncRepository<Warehouse>, IWarehouseRepository
	{
		public WarehouseRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
