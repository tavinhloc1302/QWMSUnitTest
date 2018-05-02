using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class UnitTypeRepository : AsyncRepository<UnitType>, IUnitTypeRepository
	{
		public UnitTypeRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
