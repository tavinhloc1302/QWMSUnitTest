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
    public class TokenRepositoryTest : ITokenRepository
    {
        public IQueryable<Token> Objects => throw new NotImplementedException();

        public void Add(Token entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Token entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<Token, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Token Get(Expression<Func<Token, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Token Get(Expression<Func<Token, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Token> GetAll()
        {
            throw new NotImplementedException();
        }

        public Token GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Token> GetMany(Expression<Func<Token, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Token> GetMany(Expression<Func<Token, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Token> Query(Expression<Func<Token, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(Token entity)
        {
            throw new NotImplementedException();
        }
    }
}
