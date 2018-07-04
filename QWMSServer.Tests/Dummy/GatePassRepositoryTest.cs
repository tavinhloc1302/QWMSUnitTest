using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class GatePassRepositoryTest : RepositoryBaseTest<GatePass>, IGatePassRepository
    {
        public override IList<GatePass> GetObjectList()
        {
            return new List<GatePass>() {
                DataRecords.GATE_PASS_NORMAL,
                DataRecords.GATE_PASS_DELETED,
                DataRecords.GATE_PASS_FOR_UPDATE,
                DataRecords.GATE_PASS_1ST_ORDER_DELI_TYPE_NOT_PUMP,
                DataRecords.GATE_PASS_1ST_ORDER_DELI_TYPE_PUMP,
                DataRecords.GATE_PASS_1ST_ORDER_PURCHASE,
                DataRecords.GATE_PASS_1ST_ORDER_TYPE_OTHER,
            };
        }

        public override async Task<GatePass> GetAsync(Expression<Func<GatePass, bool>> where)
        {
            return SimpleGetPatcher(DataRecords.GATE_PASS_NORMAL);
        }
    }
}
