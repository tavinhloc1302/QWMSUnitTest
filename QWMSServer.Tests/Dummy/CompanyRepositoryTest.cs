using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class CompanyRepositoryTest : RepositoryBaseTest<Company>, ICompanyRepository
    {
        public static int FLAG_DELETE = 0;

        public override IList<Company> GetObjectList()
        {
            return new List<Company>() {
                DataRecords.COMPANY_NORMAL, DataRecords.COMPANY_NORMAL_2
            };
        }

        public override async Task<Company> GetAsync(Expression<Func<Company, bool>> where)
        {
            var result = DataRecords.COMPANY_NORMAL;

            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.COMPANY_NORMAL);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.COMPANY_NORMAL_2);
                    break;
            }
            
            return result;
        }
    }
}
