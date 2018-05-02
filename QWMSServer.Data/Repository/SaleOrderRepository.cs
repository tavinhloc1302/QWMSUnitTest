using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class SaleOrderRepository : AsyncRepository<SaleOrder>, ISaleOrderRepository
	{
		public SaleOrderRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
