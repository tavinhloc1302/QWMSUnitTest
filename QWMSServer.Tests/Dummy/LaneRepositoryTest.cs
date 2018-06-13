using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

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
    }
}
