using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class PlantRepository : AsyncRepository<Plant>, IPlantRepository
	{
		public PlantRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
