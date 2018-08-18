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
        // 1: Deli order
        // 2: Purchase order
        // 3: Internal order
        // Other: Other type order
        public static int FLAG_ORDER_TYPE = 0;

        public new static void ResetDummyFlags()
        {
            Type parent = typeof(GatePassRepositoryTest).BaseType;
            var parentFunc = parent.GetMethod("ResetDummyFlags");
            parentFunc.Invoke(null, null);

            FLAG_ORDER_TYPE = 0;
        }

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
            var sampleGate = DataRecords.GATE_PASS_NORMAL;
            sampleGate = this.InjectOrderWithType(sampleGate);

            return SimpleGetPatcher(sampleGate);
        }

        protected GatePass InjectOrderWithType(GatePass gatepass)
        {
            Order order = null;
            switch (FLAG_ORDER_TYPE)
            {
                case 1: order = DataRecords.ORDER_NORMAL_DELI; break;
                case 2: order = DataRecords.ORDER_NORMAL_PURCHASE; break;
                case 3: order = DataRecords.ORDER_NORMAL_INTERNAL; break;
                default: order = DataRecords.ORDER_NORMAL_TYPE_OTHER; break;
            }

            gatepass.orders = new List<Order>() { order };
            return gatepass;
        }
    }
}
