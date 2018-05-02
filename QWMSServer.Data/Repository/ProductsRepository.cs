using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
    public class ProductsRepository : AsyncRepository<Products>, IProductsRepository
    {
        public ProductsRepository(IDBContext dbContext) : base(dbContext)
        {
        }
    }
}
