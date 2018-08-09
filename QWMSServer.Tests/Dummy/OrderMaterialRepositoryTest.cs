using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class OrderMaterialRepositoryTest : RepositoryBaseTest<OrderMaterial>, IOrderMaterialRepository
    {
        public static int FLAG_DELETE = 0;

        public override IList<OrderMaterial> GetObjectList()
        {
            return new List<OrderMaterial>() {
                DataRecords.ORDERMATERIAL_NORMAL,
                DataRecords.ORDERMATERIAL_DELETED
            };
        }

        public override async Task<OrderMaterial> GetAsync(Expression<Func<OrderMaterial, bool>> where)
        {

            var result = DataRecords.ORDERMATERIAL_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.ORDERMATERIAL_DELETED);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.ORDERMATERIAL_NORMAL);
                    break;
            }
            return result;
        }
    }
}
