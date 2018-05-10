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
    public class RFIDCardRepositoryTest : IRFIDCardRepository
    {
        public IQueryable<RFIDCard> Objects => throw new NotImplementedException();

        public void Add(RFIDCard entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<RFIDCard, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(RFIDCard entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<RFIDCard, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RFIDCard>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RFIDCard> GetAsync(Expression<Func<RFIDCard, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<RFIDCard> GetAsync(Expression<Func<RFIDCard, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<RFIDCard> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RFIDCard>> GetManyAsync(Expression<Func<RFIDCard, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RFIDCard>> GetManyAsync(Expression<Func<RFIDCard, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RFIDCard> Query(Expression<Func<RFIDCard, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(RFIDCard entity)
        {
            throw new NotImplementedException();
        }
    }
}
