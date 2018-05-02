using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class PurchaseOrderRepository : AsyncRepository<PurchaseOrder>, IPurchaseOrderRepository
	{
		public PurchaseOrderRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
