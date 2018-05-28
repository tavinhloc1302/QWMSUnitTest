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
    public class PlantRepositoryTest : IPlantRepository
    {
        public IQueryable<Plant> Objects => new List<Plant>() {
            new Plant() {
                code = "0123",
                ID = 1,
                isDelete = false,
                nameEn = "Sky Rider 1",
                nameVi = "Sky Rider 1",
                company = new Company
                {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    nameEn = "Company 1",
                    nameVi = "Company 1"
                }
            },
            new Plant() {
                code = "3210",
                ID = 2,
                isDelete = false,
                nameEn = "Sky Rider 2",
                nameVi = "Sky Rider 2",
                company = new Company
                {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    nameEn = "Company 1",
                    nameVi = "Company 1"
                }
            }
        }.AsQueryable();

        public void Add(Plant entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<Plant, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(Plant entity)
        {
        }

        public void Delete(Expression<Func<Plant, bool>> where)
        {
        }

        public async Task<IEnumerable<Plant>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<Plant> GetAsync(Expression<Func<Plant, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<Plant> GetAsync(Expression<Func<Plant, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<Plant> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<Plant>> GetManyAsync(Expression<Func<Plant, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<Plant>> GetManyAsync(Expression<Func<Plant, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<Plant> Query(Expression<Func<Plant, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(Plant entity)
        {
        }
    }
}
