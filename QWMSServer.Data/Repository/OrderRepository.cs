using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class OrderRepository : AsyncRepository<Order>, IOrderRepository
	{
		public OrderRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
