using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class LaneRepository : AsyncRepository<Lane>, ILaneRepository
	{
		public LaneRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
