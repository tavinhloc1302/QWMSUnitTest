using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class OrderTypeRepository : AsyncRepository<OrderType>, IOrderTypeRepository
	{
		public OrderTypeRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
