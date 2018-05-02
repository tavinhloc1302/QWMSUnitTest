using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class GatePassRepository : AsyncRepository<GatePass>, IGatePassRepository
	{
		public GatePassRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
