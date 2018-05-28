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
    public class UnitTypeRepositoryTest : IUnitTypeRepository
    {
        public IQueryable<UnitType> Objects => new List<UnitType>() {
            new UnitType
            {
                code = "0123",
                description = "Unittype 1",
                ID = 1,
                isDelete = false
            },
            new UnitType
            {
                code = "0123",
                description = "Unittype 1",
                ID = 1,
                isDelete = false
            }
        }.AsQueryable();

        public void Add(UnitType entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<UnitType, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(UnitType entity)
        {
        }

        public void Delete(Expression<Func<UnitType, bool>> where)
        {
        }

        public async Task<IEnumerable<UnitType>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<UnitType> GetAsync(Expression<Func<UnitType, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<UnitType> GetAsync(Expression<Func<UnitType, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<UnitType> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<UnitType>> GetManyAsync(Expression<Func<UnitType, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<UnitType>> GetManyAsync(Expression<Func<UnitType, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<UnitType> Query(Expression<Func<UnitType, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(UnitType entity)
        {
        }
    }
}
