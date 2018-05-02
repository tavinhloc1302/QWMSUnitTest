using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class DeliveryOrderTypeRepository : AsyncRepository<DeliveryOrderType>, IDeliveryOrderTypeRepository
	{
		public DeliveryOrderTypeRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
