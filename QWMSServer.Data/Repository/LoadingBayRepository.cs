using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class LoadingBayRepository : AsyncRepository<LoadingBay>, ILoadingBayRepository
	{
		public LoadingBayRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
