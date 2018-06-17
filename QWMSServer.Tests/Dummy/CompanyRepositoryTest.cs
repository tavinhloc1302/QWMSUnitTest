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
        public static int FLAG_DELETE_NO_ID = 0;

        public override IList<Company> GetObjectList()
        {
            return new List<Company>() {
                DataRecords.COMPANY_NORMAL, DataRecords.COMPANY_NORMAL_2
            };
        }

        public override async Task<Company> GetAsync(Expression<Func<Company, bool>> where)
        {
            var result = DataRecords.COMPANY_NORMAL;
            switch (FLAG_DELETE_NO_ID)
            {
                case 1:
                    result = null;
                    break;
                case 0:
                default:
                    break;
            }
            return result;
        }
    }
}
