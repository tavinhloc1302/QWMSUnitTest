using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class WeighRecordRepositoryTest : RepositoryBaseTest<WeightRecord>, IWeightRecordRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<WeightRecord> GetObjectList()
        {
            return new List<WeightRecord>
            {
                DataRecords.WEIGHRECORD_NORMAL,
                DataRecords.WEIGHRECORD_DELETED
            };
        }

        public override async Task<IEnumerable<WeightRecord>> GetManyAsync(Expression<Func<WeightRecord, bool>> where)
        {
            return new List<WeightRecord>
            {
                DataRecords.WEIGHRECORD_NORMAL,
                DataRecords.WEIGHRECORD_DELETED
            };
        }

        public override async Task<WeightRecord> GetAsync(Expression<Func<WeightRecord, bool>> where)
        {
            var result = DataRecords.WEIGHRECORD_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.WEIGHRECORD_NORMAL);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.WEIGHRECORD_DELETED);
                    break;
            }
            return result;
        }
    }
}
