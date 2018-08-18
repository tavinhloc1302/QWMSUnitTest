using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class UserPCRepositoryTest : RepositoryBaseTest<UserPC>, IUserPCRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<UserPC> GetObjectList()
        {
            return new List<UserPC>
            {
                DataRecords.USERPC_NORMAL,
                DataRecords.USERPC_NORMAL_2
            };
        }

        public override async Task<UserPC> GetAsync(Expression<Func<UserPC, bool>> where)
        {
            var result = DataRecords.USERPC_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.USERPC_NORMAL_2);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.USERPC_NORMAL);
                    break;
            }
            return result;
        }
    }
}
