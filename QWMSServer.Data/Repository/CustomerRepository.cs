using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class CustomerRepository : AsyncRepository<Customer>, ICustomerRepository
	{
		public CustomerRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
