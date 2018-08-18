using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class StateRepositoryTest : RepositoryBaseTest<State>, IStateRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<State> GetObjectList()
        {
            return new List<State>() {
                DataRecords.STATE_NORMAL,
                DataRecords.STATE_DELETED
            };
        }

        public override async Task<State> GetAsync(Expression<Func<State, bool>> where)
        {
            var result = DataRecords.STATE_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.STATE_DELETED);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.STATE_NORMAL);
                    break;
            }
            return result;
        }
    }
}
