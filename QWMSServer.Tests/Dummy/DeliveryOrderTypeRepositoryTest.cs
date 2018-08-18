using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class DeliveryOrderTypeRepositoryTest : RepositoryBaseTest<DeliveryOrderType>, IDeliveryOrderTypeRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<DeliveryOrderType> GetObjectList()
        {
            return new List<DeliveryOrderType>() {
                DataRecords.DELIVERY_ORDER_TYPE_NORMAL,
                DataRecords.DELIVERY_ORDER_TYPE_DELETED,
            };
        }

        public override async Task<DeliveryOrderType> GetAsync(Expression<Func<DeliveryOrderType, bool>> where)
        {
            var result = DataRecords.DELIVERY_ORDER_TYPE_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.DELIVERY_ORDER_TYPE_DELETED);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.DELIVERY_ORDER_TYPE_NORMAL);
                    break;
            }
            return result;
        }
    }
}
