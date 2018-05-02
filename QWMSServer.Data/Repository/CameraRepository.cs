using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class CameraRepository : AsyncRepository<Camera>, ICameraRepository
	{
		public CameraRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
