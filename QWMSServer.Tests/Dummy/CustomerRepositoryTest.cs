using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class CustomerRepositoryTest : RepositoryBaseTest<Customer>, ICustomerRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<Customer> GetObjectList()
        {
            return new List<Customer>() {
                DataRecords.CUSTOMER_NORMAL,
                DataRecords.CUSTOMER_DELETED,
            };
        }

        public override async Task<Customer> GetAsync(Expression<Func<Customer, bool>> where)
        {
            var result = DataRecords.CUSTOMER_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.CUSTOMER_DELETED);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.CUSTOMER_NORMAL);
                    break;
            }
            return result;
        }
    }
}
