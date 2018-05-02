using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class LaneTypeRepository : AsyncRepository<LaneType>, ILaneTypeRepository
	{
		public LaneTypeRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
