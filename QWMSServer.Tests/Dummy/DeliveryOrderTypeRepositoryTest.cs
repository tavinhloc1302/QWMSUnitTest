using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class DeliveryOrderTypeRepositoryTest : RepositoryBaseTest<DeliveryOrderType>, IDeliveryOrderTypeRepository
    {
        public override IList<DeliveryOrderType> GetObjectList()
        {
            return new List<DeliveryOrderType>() {
                DataRecords.DELIVERY_ORDER_TYPE_NORMAL,
                DataRecords.DELIVERY_ORDER_TYPE_DELETED,
            };
        }

        public override async Task<DeliveryOrderType> GetAsync(Expression<Func<DeliveryOrderType, bool>> where)
        {
            var sampleObject = new DeliveryOrderType()
            {
                ID = 1,
                code = "1111",
                description = "Normal",
                isDelete = false
            };

            return this.SimpleGetPatcher(sampleObject);
        }
    }
}
