using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

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
