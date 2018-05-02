using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class MaterialRepository : AsyncRepository<Material>, IMaterialRepository
	{
		public MaterialRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
