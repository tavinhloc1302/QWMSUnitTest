using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class StateRecordRepositoryTest : RepositoryBaseTest<StateRecord>, IStateRecordRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<StateRecord> GetObjectList()
        {
            return new List<StateRecord>() {
                DataRecords.STATERECORD_NORMAL,
                DataRecords.STATERECORD_DELETED
            };
        }

        public override async Task<StateRecord> GetAsync(Expression<Func<StateRecord, bool>> where)
        {
            var result = DataRecords.STATERECORD_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.STATERECORD_DELETED);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.STATERECORD_NORMAL);
                    break;
            }
            return result;
        }
    }
}
