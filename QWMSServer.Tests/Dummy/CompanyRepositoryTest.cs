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
    public class CompanyRepositoryTest : ICompanyRepository
    {
        public IQueryable<Company> Objects => new List<Company>() {
            new Company() {
                code = "0123",
                ID = 1,
                isDelete = false,
                nameEn = "Sky Rider 1",
                nameVi = "Sky Rider 1",
                
            },
            new Company() {
                code = "3210",
                ID = 2,
                isDelete = false,
                nameEn = "Sky Rider 2",
                nameVi = "Sky Rider 2",
            }
        }.AsQueryable();

        public void Add(Company entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<Company, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(Company entity)
        {
        }

        public void Delete(Expression<Func<Company, bool>> where)
        {
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<Company> GetAsync(Expression<Func<Company, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<Company> GetAsync(Expression<Func<Company, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<Company>> GetManyAsync(Expression<Func<Company, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<Company>> GetManyAsync(Expression<Func<Company, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<Company> Query(Expression<Func<Company, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(Company entity)
        {
        }
    }
}
