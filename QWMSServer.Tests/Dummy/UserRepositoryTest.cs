using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class UserRepositoryTest : RepositoryBaseTest<User>, IUserRepository
    {
        public override IList<User> GetObjectList()
        {
            return new List<User>() {
                DataRecords.USER_NORMAL_1,
                DataRecords.USER_NORMAL_2
            };
        }

        public override async Task<User> GetAsync(Expression<Func<User, bool>> where)
        {
            return DataRecords.USER_NORMAL_1;
        }
    }
}
