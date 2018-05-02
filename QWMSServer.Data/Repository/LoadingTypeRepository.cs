using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class LoadingTypeRepository : AsyncRepository<LoadingType>, ILoadingTypeRepository
	{
		public LoadingTypeRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
