using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class DeliveryOrderRepository : AsyncRepository<DeliveryOrder>, IDeliveryOrderRepository
	{
		public DeliveryOrderRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
