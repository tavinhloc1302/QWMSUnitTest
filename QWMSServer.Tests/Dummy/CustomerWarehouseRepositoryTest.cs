using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class CustomerWarehouseRepositoryTest : RepositoryBaseTest<CustomerWarehouse>, ICustomerWarehouseRepository
    {
        public override IList<CustomerWarehouse> GetObjectList()
        {
            return new List<CustomerWarehouse>() {
            };
        }

        public override async Task<CustomerWarehouse> GetAsync(Expression<Func<CustomerWarehouse, bool>> where)
        {
            var sampleObject = new CustomerWarehouse()
            {
                ID = 1,
                deliveryCode = "1111",
                warehouseName = "Warehouse 1",
                deliveryAddressVi = "Ho Chi Minh",
                deliveryAddressEn = "HCMC",
                isDelete = false,
                customerID = DataRecords.CUSTOMER_NORMAL.ID,
                customer = DataRecords.CUSTOMER_NORMAL,

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
