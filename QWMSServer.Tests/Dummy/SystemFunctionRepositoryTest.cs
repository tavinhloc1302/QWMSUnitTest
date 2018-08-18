using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class SystemFunctionRepositoryTest : RepositoryBaseTest<SystemFunction>, ISystemFunctionRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<SystemFunction> GetObjectList()
        {
            return new List<SystemFunction>
            {
                DataRecords.SYSTEMFUNCTION_NORMAL,
                DataRecords.SYSTEMFUNCTION_DELETE
            };
        }
        public override async Task<SystemFunction> GetAsync(Expression<Func<SystemFunction, bool>> where)
        {
            var result = DataRecords.SYSTEMFUNCTION_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.SYSTEMFUNCTION_DELETE);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.SYSTEMFUNCTION_NORMAL);
                    break;
            }
            return result;
        }
    }
}
