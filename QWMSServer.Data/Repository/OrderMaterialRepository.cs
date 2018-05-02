using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class OrderMaterialRepository : AsyncRepository<OrderMaterial>, IOrderMaterialRepository
	{
		public OrderMaterialRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
