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
        public static int FLAG_DELETE = -1;

        public override IList<User> GetObjectList()
        {
            return new List<User>() {
                DataRecords.USER_NORMAL_1,
                DataRecords.USER_NORMAL_2
            };
        }

        public override async Task<User> GetAsync(Expression<Func<User, bool>> where)
        {
            var result = DataRecords.USER_NORMAL_1;

            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.USER_NORMAL_1);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.USER_NORMAL_2);
                    break;
            }

            return result;
        }
    }
}
