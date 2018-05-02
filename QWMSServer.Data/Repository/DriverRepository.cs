using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class DriverRepository : AsyncRepository<Driver>, IDriverRepository
	{
		public DriverRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
