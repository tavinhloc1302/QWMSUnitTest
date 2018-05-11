using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class RFIDCardRepositoryTest : IRFIDCardRepository
    {
        public IQueryable<RFIDCard> Objects => new List<RFIDCard>() {
                new RFIDCard() {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    status = 1
                },
                new RFIDCard() {
                    code = "3210",
                    ID = 2,
                    isDelete = false,
                    status = 1
                }
            }.AsQueryable();

        public void Add(RFIDCard entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<RFIDCard, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(RFIDCard entity)
        {
        }

        public void Delete(Expression<Func<RFIDCard, bool>> where)
        {
        }

        public async Task<IEnumerable<RFIDCard>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<RFIDCard> GetAsync(Expression<Func<RFIDCard, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<RFIDCard> GetAsync(Expression<Func<RFIDCard, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<RFIDCard> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<RFIDCard>> GetManyAsync(Expression<Func<RFIDCard, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<RFIDCard>> GetManyAsync(Expression<Func<RFIDCard, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<RFIDCard> Query(Expression<Func<RFIDCard, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(RFIDCard entity)
        {
        }
    }
}
