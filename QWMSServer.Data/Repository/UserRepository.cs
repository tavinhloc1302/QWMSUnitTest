using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class UserRepository : AsyncRepository<User>, IUserRepository
	{
		public UserRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
