using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class OrderRepositoryTest : RepositoryBaseTest<Order>, IOrderRepository
    {
        public static int FLAG_DELETE = -1;

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
            var result = DataRecords.ORDER_NORMAL_DELI;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.ORDER_NORMAL_TYPE_OTHER);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.ORDER_NORMAL_DELI);
                    break;
            }
            return result;
        }
    }
}
