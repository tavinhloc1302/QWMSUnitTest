using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public class CompanyRepositoryTest : RepositoryBaseTest<Company>, ICompanyRepository
    {
        public override IList<Company> GetObjectList()
        {
            return new List<Company>() {
                new Company() {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    nameEn = "Sky Rider 1",
                    nameVi = "Sky Rider 1",

                },
                new Company() {
                    code = "3210",
                    ID = 2,
                    isDelete = false,
                    nameEn = "Sky Rider 2",
                    nameVi = "Sky Rider 2",
                }
            };
        }
    }
}
