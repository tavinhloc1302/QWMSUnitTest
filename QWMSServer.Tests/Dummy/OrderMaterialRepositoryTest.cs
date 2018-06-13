using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class OrderMaterialRepositoryTest : RepositoryBaseTest<OrderMaterial>, IOrderMaterialRepository
    {
        public override IList<OrderMaterial> GetObjectList()
        {
            return new List<OrderMaterial>() {
                //DataRecords.OrderMaterial_NORMAL_DELI,
                //DataRecords.OrderMaterial_NORMAL_PURCHASE,
                //DataRecords.OrderMaterial_NORMAL_TYPE_OTHER,
            };
        }

        public override async Task<OrderMaterial> GetAsync(Expression<Func<OrderMaterial, bool>> where)
        {
            var sampleObject = new OrderMaterial()
            {
                code = "1111",
                orderID = 1,
                materialID = 1,
                quantity = "10",
                grossWeight = 1.1F,
                theoryQuantity = "9",
                theoryGrossWeight = 1.0F,
                isDelete = false,
                order = null,
                material = null,
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
