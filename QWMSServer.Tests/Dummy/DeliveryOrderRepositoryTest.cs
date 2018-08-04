using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class DeliveryOrderRepositoryTest : RepositoryBaseTest<DeliveryOrder>, IDeliveryOrderRepository
    {
        public override IList<DeliveryOrder> GetObjectList()
        {
            return new List<DeliveryOrder>()
            {
            };
        }

        public override async Task<DeliveryOrder> GetAsync(Expression<Func<DeliveryOrder, bool>> where)
        {
            var sampleObject = new DeliveryOrder()
            {
                ID = 1,
                code = "1111",
                customerID = DataRecords.CUSTOMER_NORMAL.ID,
                customer = DataRecords.CUSTOMER_NORMAL,
                carrierVendorID = DataRecords.CARRIER_VENDOR_NORMAL.ID,
                carrierVendor = DataRecords.CARRIER_VENDOR_NORMAL,
                remark = "remark",
                sloc = "sloc",
                doTypeID = DataRecords.DELIVERY_ORDER_TYPE_NORMAL.ID,
                deliveryOrderType = DataRecords.DELIVERY_ORDER_TYPE_NORMAL,
                customerWarehouseID = null,
                customerWarehouse = null,
                isDelete = false,
            };

            switch (GetFlagGet())
            {
                case 0:
                    sampleObject = null;
                    break;
                case 1:
                    break;
                case 2:
                    sampleObject.isDelete = true;
                    break;
                case 11:
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return sampleObject;
        }
    }
}
