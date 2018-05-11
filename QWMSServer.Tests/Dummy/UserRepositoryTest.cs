using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class UserRepositoryTest : IUserRepository
    {
        public IQueryable<User> Objects => new List<User>() {
                new User() {
                    Code = "0123",
                    employees = new List<Employee>() {
                        new Employee(),
                        new Employee()
                    },
                    password = "password",
                    username ="skyrider1",
                    ID = 1,
                    isDelete = false
                },
                new User() {
                    Code = "3210",
                    employees = new List<Employee>(),
                    password = "password",
                    username ="skyrider2",
                    ID = 2,
                    isDelete = false
                }
            }.AsQueryable();

        public void Add(User entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<User, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(User entity)
        {
        }

        public void Delete(Expression<Func<User, bool>> where)
        {
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<User>> GetManyAsync(Expression<Func<User, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<User>> GetManyAsync(Expression<Func<User, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<User> Query(Expression<Func<User, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(User entity)
        {
        }
    }
}
