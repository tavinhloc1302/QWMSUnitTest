using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class OrderTypeRepositoryTest : RepositoryBaseTest<OrderType>, IOrderTypeRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<OrderType> GetObjectList()
        {
            return new List<OrderType>() {
                DataRecords.ORDER_TYPE_DELIVERY,
                DataRecords.ORDER_TYPE_PURCHASE,
                DataRecords.ORDER_TYPE_INTERNAL,
                DataRecords.ORDER_TYPE_OTHER,
            };
        }

        public override async Task<OrderType> GetAsync(Expression<Func<OrderType, bool>> where)
        {
            var result = DataRecords.ORDER_TYPE_OTHER;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.ORDER_TYPE_OTHER);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.ORDER_TYPE_DELIVERY);
                    break;
            }
            return result;
        }
    }
}
