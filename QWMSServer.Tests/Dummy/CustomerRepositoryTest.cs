using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class CustomerRepositoryTest : RepositoryBaseTest<Customer>, ICustomerRepository
    {
        public override IList<Customer> GetObjectList()
        {
            return new List<Customer>() {
                DataRecords.CUSTOMER_NORMAL,
                DataRecords.CUSTOMER_DELETED,
            };
        }

        public override async Task<Customer> GetAsync(Expression<Func<Customer, bool>> where)
        {
            var sampleObject = new Customer()
            {
                ID = 1,
                code = "1111",
                nameVi = "KH 1",
                nameEn = "Cus 1",
                shortName = "K 1",
                invoiceAddressVi = "Ho Chi Minh",
                invoiceAddressEn = "HCMC",
                taxCode = "Tax 1",
                contactPerson = "Contact 1",
                telNo = "0908832000",
                faxNo = "11111111",
                email = "cus1@yopmail.com",
                isDelete = false
            };

            return this.SimpleGetPatcher(sampleObject);
        }
    }
}
