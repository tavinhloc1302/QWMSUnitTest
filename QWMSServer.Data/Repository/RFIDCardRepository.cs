using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class RFIDCardRepository : AsyncRepository<RFIDCard>, IRFIDCardRepository
	{
		public RFIDCardRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
