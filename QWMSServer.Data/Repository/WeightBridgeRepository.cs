using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class WeighBridgeRepository : AsyncRepository<WeighBridge>, IWeighBridgeRepository
	{
		public WeighBridgeRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
