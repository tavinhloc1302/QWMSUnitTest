using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class StateRepository : AsyncRepository<State>, IStateRepository
	{
		public StateRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
