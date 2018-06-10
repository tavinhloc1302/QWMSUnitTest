using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class PlantRepositoryTest : RepositoryBaseTest<Plant>, IPlantRepository
    {         
        public override IList<Plant> GetObjectList()
        {
            return new List<Plant>() {
            };
        }

        public override async Task<Plant> GetAsync(Expression<Func<Plant, bool>> where)
        {
            var sampleObject = new Plant()
            {
                code = "0123",
                ID = 1,
                isDelete = false,
                nameEn = "Sky Rider 1",
                nameVi = "Sky Rider 1",
                company = new Company
                {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    nameEn = "Company 1",
                    nameVi = "Company 1"
                }
            };

            //new Plant()
            //{
            //    code = "3210",
            //    ID = 2,
            //    isDelete = false,
            //    nameEn = "Sky Rider 2",
            //    nameVi = "Sky Rider 2",
            //    company = new Company
            //    {
            //        code = "0123",
            //        ID = 1,
            //        isDelete = false,
            //        nameEn = "Company 1",
            //        nameVi = "Company 1"
            //    }
            //}

            return this.SimpleGetPatcher(sampleObject);
        }
    }
}
