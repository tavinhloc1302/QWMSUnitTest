using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class WarehouseRepositoryTest : IWarehouseRepository
    {
        public IQueryable<Warehouse> Objects => new List<Warehouse>() {
            new Warehouse() {
                code = "0123",
                ID = 1,
                isDelete = false,
                nameEn = "Sky Rider 1",
                nameVi = "Sky Rider 1",
                loadingBays = new List<LoadingBay>(),
                plantID = 1
            },
            new Warehouse() {
                code = "0123",
                ID = 1,
                isDelete = false,
                nameEn = "Sky Rider 1",
                nameVi = "Sky Rider 1",
                loadingBays = new List<LoadingBay>(),
                plantID = 1
            }
        }.AsQueryable();

        public void Add(Warehouse entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<Warehouse, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(Warehouse entity)
        {
        }

        public void Delete(Expression<Func<Warehouse, bool>> where)
        {
        }

        public async Task<IEnumerable<Warehouse>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<Warehouse> GetAsync(Expression<Func<Warehouse, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<Warehouse> GetAsync(Expression<Func<Warehouse, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<Warehouse> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<Warehouse>> GetManyAsync(Expression<Func<Warehouse, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<Warehouse>> GetManyAsync(Expression<Func<Warehouse, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<Warehouse> Query(Expression<Func<Warehouse, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(Warehouse entity)
        {
        }
    }
}
