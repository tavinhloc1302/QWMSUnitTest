using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class EmployeeRoleRepositoryTest : IEmployeeRoleRepository
    {
        public IQueryable<EmployeeRole> Objects => new List<EmployeeRole>()
        {
            new EmployeeRole
            {
                Code = "0123",
                description = "Employee Role 1",
                ID = 1,
                isDelete = false
            },
            new EmployeeRole
            {
                Code = "3210",
                description = "Employee Role 2",
                ID = 2,
                isDelete = false
            }
        }.AsQueryable();

        public async Task<int> CountAsync(Expression<Func<EmployeeRole, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(EmployeeRole entity)
        {
        }

        public void Delete(Expression<Func<EmployeeRole, bool>> where)
        {
        }

        public async Task<IEnumerable<EmployeeRole>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<EmployeeRole> GetAsync(Expression<Func<EmployeeRole, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<EmployeeRole> GetAsync(Expression<Func<EmployeeRole, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<EmployeeRole> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<EmployeeRole>> GetManyAsync(Expression<Func<EmployeeRole, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<EmployeeRole>> GetManyAsync(Expression<Func<EmployeeRole, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<EmployeeRole> Query(Expression<Func<EmployeeRole, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(EmployeeRole entity)
        {
        }
    }
}
