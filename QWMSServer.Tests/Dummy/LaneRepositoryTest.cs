using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    class LaneRepositoryTest : RepositoryBaseTest<Lane>, ILaneRepository
    {
        public override IList<Lane> GetObjectList()
        {
            return new List<Lane>() {
                DataRecords.LANE_NORMAL,
                DataRecords.LANE_DELETED,
            };
        }

        public override async Task<Lane> GetAsync(Expression<Func<Lane, bool>> where)
        {
            return DataRecords.LANE_NORMAL;
        }
    }
}
