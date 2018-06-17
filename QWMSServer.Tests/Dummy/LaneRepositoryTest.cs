using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class LaneRepositoryTest : RepositoryBaseTest<Lane>, ILaneRepository
    {
        public static int FLAG_DELETE = 0;

        public override IList<Lane> GetObjectList()
        {
            return new List<Lane>() {
                DataRecords.LANE_NORMAL,
                DataRecords.LANE_DELETED,
            };
        }

        public override async Task<Lane> GetAsync(Expression<Func<Lane, bool>> where)
        {
            var result = DataRecords.LANE_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                default:
                    result = this.SimpleGetPatcher(DataRecords.LANE_DELETED);
                    break;
            }
            return result;
        }
    }
}
