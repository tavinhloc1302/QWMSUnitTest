using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class ConstrainRepositoryTest : RepositoryBaseTest<Constrain>, IConstrainRepository
    {
        public static int FLAG_DELETE = 0;

        public override IList<Constrain> GetObjectList()
        {
            return new List<Constrain>()
            {
                DataRecords.CONSTRAIN_NORMAL,
                DataRecords.CONSTRAIN_DELETED
            };
        }

        public override async Task<Constrain> GetAsync(Expression<Func<Constrain, bool>> where)
        {
            var result = DataRecords.CONSTRAIN_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.CONSTRAIN_NORMAL);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.CONSTRAIN_DELETED);
                    break;
            }
            return result;
        }
    }
}
