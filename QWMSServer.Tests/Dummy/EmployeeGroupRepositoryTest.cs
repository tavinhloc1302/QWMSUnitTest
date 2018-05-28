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
    public class EmployeeGroupRepositoryTest : IEmployeeGroupRepository
    {
        public IQueryable<EmployeeGroup> Objects => new List<EmployeeGroup>() {
            new EmployeeGroup() {
                code = "0123",
                ID = 1,
                isDelete = false,
                description = "Group 1"
            },
            new EmployeeGroup() {
                code = "3210",
                ID = 2,
                isDelete = false,
                description = "Group 2"
            }
        }.AsQueryable();

        public void Add(EmployeeGroup entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<EmployeeGroup, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(EmployeeGroup entity)
        {
        }

        public void Delete(Expression<Func<EmployeeGroup, bool>> where)
        {
        }

        public async Task<IEnumerable<EmployeeGroup>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<EmployeeGroup> GetAsync(Expression<Func<EmployeeGroup, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<EmployeeGroup> GetAsync(Expression<Func<EmployeeGroup, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<EmployeeGroup> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<EmployeeGroup>> GetManyAsync(Expression<Func<EmployeeGroup, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<EmployeeGroup>> GetManyAsync(Expression<Func<EmployeeGroup, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<EmployeeGroup> Query(Expression<Func<EmployeeGroup, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(EmployeeGroup entity)
        {
        }
    }
}
