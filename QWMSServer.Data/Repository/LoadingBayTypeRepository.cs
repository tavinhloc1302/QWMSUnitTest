using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class LoadingBayTypeRepository : AsyncRepository<LoadingBayType>, ILoadingBayTypeRepository
	{
		public LoadingBayTypeRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
