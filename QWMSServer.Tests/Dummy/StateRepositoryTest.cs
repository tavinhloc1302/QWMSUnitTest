using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class StateRepositoryTest : IStateRepository
    {
        public IQueryable<State> Objects => new List<State>() {
                new State() {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    desciption = "Sky Rider 1",
                    order = 1
                },
                new State() {
                    code = "3210",
                    ID = 2,
                    isDelete = false,
                    desciption = "Sky Rider 2",
                    order = 2
                }
            }.AsQueryable();

        public void Add(State entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<State, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(State entity)
        {
        }

        public void Delete(Expression<Func<State, bool>> where)
        {
        }

        public async Task<IEnumerable<State>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<State> GetAsync(Expression<Func<State, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<State> GetAsync(Expression<Func<State, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<State> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<State>> GetManyAsync(Expression<Func<State, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<State>> GetManyAsync(Expression<Func<State, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<State> Query(Expression<Func<State, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(State entity)
        {
        }
    }
}
