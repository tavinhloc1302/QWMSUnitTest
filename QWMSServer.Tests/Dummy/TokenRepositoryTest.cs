using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public class TokenRepositoryTest : RepositoryBaseTest<Token>, ITokenRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<Token> GetObjectList()
        {
            return new List<Token>
            {
                new Token
                {
                    Id = 1,
                    ExpiresIn = 3600,
                    IssuedOn = new DateTime(1,1,2018),
                    TokenString = "AS9D8F7A9BN9A88DSRF76A7WQ64E5AS9DF89AS7E6",
                    UserId = 1
                },
                new Token
                {
                    Id = 2,
                    ExpiresIn = 3600,
                    IssuedOn = new DateTime(1,1,2018),
                    TokenString = "JQWG4F5U6J7HJKE4G5FH32K4J6H4J5G6F2LKJ5V3J",
                    UserId = 2
                }
            };
        }
    }
}
