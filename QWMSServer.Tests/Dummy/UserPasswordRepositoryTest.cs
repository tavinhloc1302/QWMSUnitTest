using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class UserPasswordRepositoryTest : RepositoryBaseTest<UserPassword>, IUserPasswordRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<UserPassword> GetObjectList()
        {
            return new List<UserPassword>
            {
                DataRecords.USERPASSWORD_NORMAL,
                DataRecords.USERPASSWORD_DELETED
            };
        }

        public override async Task<UserPassword> GetAsync(Expression<Func<UserPassword, bool>> where)
        {
            var result = DataRecords.USERPASSWORD_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.USERPASSWORD_DELETED);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.USERPASSWORD_NORMAL);
                    break;
            }
            return result;
        }
    }
}
