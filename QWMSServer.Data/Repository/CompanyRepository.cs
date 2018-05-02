using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class CompanyRepository : AsyncRepository<Company>, ICompanyRepository
	{
		public CompanyRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
