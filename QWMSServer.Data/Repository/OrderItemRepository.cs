using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class OrderItemRepository : AsyncRepository<OrderItem>, IOrderItemRepository
	{
		public OrderItemRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
