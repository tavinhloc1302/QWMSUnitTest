using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public interface IDeliveryOrderRepository : IAsyncRepository<DeliveryOrder>
	{
	}
}
