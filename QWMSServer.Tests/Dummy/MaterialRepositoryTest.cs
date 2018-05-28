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
    public class MaterialRepositoryTest : IMaterialRepository
    {
        public IQueryable<Material> Objects => new List<Material>() {
            new Material() {
                code = "0123",
                ID = 1,
                isDelete = false,
                grossWeight = 1,
                materialNameEn = "Material 1",
                materialNameVi = "Material 1",
                netWeight = 1,
                unit = new UnitType
                {
                    code = "0123",
                    description = "Unittype 1",
                    ID = 1,
                    isDelete = false
                },
                unitID = 1
            },
            new Material() {
                code = "3210",
                ID = 2,
                isDelete = false,
                grossWeight = 1,
                materialNameEn = "Material 2",
                materialNameVi = "Material 2",
                netWeight = 1,
                unit = new UnitType
                {
                    code = "0123",
                    description = "Unittype 1",
                    ID = 1,
                    isDelete = false
                },
                unitID = 1
            }
        }.AsQueryable();

        public void Add(Material entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<Material, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(Material entity)
        {
        }

        public void Delete(Expression<Func<Material, bool>> where)
        {
        }

        public async Task<IEnumerable<Material>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<Material> GetAsync(Expression<Func<Material, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<Material> GetAsync(Expression<Func<Material, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<Material> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<Material>> GetManyAsync(Expression<Func<Material, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<Material>> GetManyAsync(Expression<Func<Material, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<Material> Query(Expression<Func<Material, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(Material entity)
        {
        }
    }
}
