using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class OrderMaterialRepositoryTest : RepositoryBaseTest<OrderMaterial>, IOrderMaterialRepository
    {
        public static string OBJ_CODE_IMPORT_DO_DUPLICATE = "DO1111_DOITEM1111";

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
            var obj = new OrderMaterial()
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
            
            switch (GetFlagGet())
            {
                case 0:
                    obj = null;
                    break;
                case 1:
                    break;
                case 2:
                    ObjectUtils.SetProperty(obj, "isDelete", true);
                    break;
                case 10:
                    obj.code = OBJ_CODE_IMPORT_DO_DUPLICATE;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return obj;
        }
    }
}
