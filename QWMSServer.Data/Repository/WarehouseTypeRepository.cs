using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class WarehouseTypeRepository : AsyncRepository<WarehouseType>, IWarehouseTypeRepository
	{
		public WarehouseTypeRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
