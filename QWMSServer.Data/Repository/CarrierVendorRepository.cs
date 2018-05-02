using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class CarrierVendorRepository : AsyncRepository<CarrierVendor>, ICarrierVendorRepository
	{
		public CarrierVendorRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
