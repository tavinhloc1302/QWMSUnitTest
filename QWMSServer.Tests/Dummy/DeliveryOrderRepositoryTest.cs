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
        public static int FLAG_DELETE = 0;

        public override IList<DeliveryOrder> GetObjectList()
        {
            return new List<DeliveryOrder>()
            {
                DataRecords.DELIVERYORDER_NORMAL,
                DataRecords.DELIVERYORDER_DELETED,
            };
        }

        public override async Task<DeliveryOrder> GetAsync(Expression<Func<DeliveryOrder, bool>> where)
        {
            var result = DataRecords.DELIVERYORDER_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.DELIVERYORDER_NORMAL);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.DELIVERYORDER_DELETED);
                    break;
            }
            return result;
        }
    }
}
