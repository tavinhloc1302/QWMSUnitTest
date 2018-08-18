using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QWMSServer.Tests.Dummy
{
    public class TokenRepositoryTest : RepositoryBaseTest<Token>, ITokenRepository
    {
        public static int FLAG_DELETE = -1;

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

        public override IList<Token> GetObjectList()
        {
            return new List<Token>
            {
                DataRecords.TOKEN_NORMAL,
                DataRecords.TOKEN_NORMAL_2
            };
        }
    }
}
