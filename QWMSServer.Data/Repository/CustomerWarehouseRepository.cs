using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
    class CustomerWarehouseRepository : AsyncRepository<CustomerWarehouse>, ICustomerWarehouseRepository
    {
        public CustomerWarehouseRepository(IDBContext dbContext) : base(dbContext)
        {
        }
    }
}
