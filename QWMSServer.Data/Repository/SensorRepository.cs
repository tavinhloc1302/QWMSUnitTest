using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class SensorRepository : AsyncRepository<Sensor>, ISensorRepository
	{
		public SensorRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
