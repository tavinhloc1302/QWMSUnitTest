using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class OrderRepositoryTest : RepositoryBaseTest<Order>, IOrderRepository
    {
        public override IList<Order> GetObjectList()
        {
            return new List<Order>() {
                DataRecords.ORDER_NORMAL_DELI,
                DataRecords.ORDER_NORMAL_PURCHASE,
                DataRecords.ORDER_NORMAL_TYPE_OTHER,
            };
        }

        public override async Task<Order> GetAsync(Expression<Func<Order, bool>> where)
        {
            var sampleObject = new Order()
            {
                ID = 1,
                code = "1234",
                orderTypeID = DataRecords.ORDER_TYPE_DELIVERY.ID,
                orderType = DataRecords.ORDER_TYPE_DELIVERY,
                grossWeight = 10,
                gatePassID = DataRecords.GATE_PASS_NORMAL.ID,
                gatePass = DataRecords.GATE_PASS_NORMAL,
                plantID = null,
                plant = null,
                doID = null,
                deliveryOrder = null,
                poID = null,
                purchaseOrder = null,
                isDelete = false,
            };

            switch (FLAG_GET_ASYNC)
            {
                case 1:
                    sampleObject.isDelete = true;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return sampleObject;
        }
    }
}
