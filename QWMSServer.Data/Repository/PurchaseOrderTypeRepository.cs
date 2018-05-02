using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class PurchaseOrderTypeRepository : AsyncRepository<PurchaseOrderType>, IPurchaseOrderTypeRepository
	{
		public PurchaseOrderTypeRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
