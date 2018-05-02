using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class WareshouseRepository : AsyncRepository<Wareshouse>, IWareshouseRepository
	{
		public WareshouseRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
