using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class PlantRepositoryTest : RepositoryBaseTest<Plant>, IPlantRepository
    {
        public override IList<Plant> GetObjectList()
        {
            return new List<Plant>() {
                new Plant() {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    nameEn = "Sky Rider 1",
                    nameVi = "Sky Rider 1",
                    company = DataRecords.COMPANY_NORMAL
                },
                new Plant() {
                    code = "3210",
                    ID = 2,
                    isDelete = false,
                    nameEn = "Sky Rider 2",
                    nameVi = "Sky Rider 2",
                    company = DataRecords.COMPANY_NORMAL_2
                }
            };
        }

        public override async Task<Plant> GetAsync(Expression<Func<Plant, bool>> where)
        {
            return new Plant()
            {
                code = "0123",
                ID = 1,
                isDelete = false,
                nameEn = "Sky Rider 1",
                nameVi = "Sky Rider 1",
                company = DataRecords.COMPANY_NORMAL
            };
        }
    }
}
