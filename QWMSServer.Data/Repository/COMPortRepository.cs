using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class COMPortRepository : AsyncRepository<COMPort>, ICOMPortRepository
	{
		public COMPortRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
