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
    public class MaterialRepositoryTest : RepositoryBaseTest<Material>, IMaterialRepository
    {
        public override IList<Material> GetObjectList()
        {
            return new List<Material>()
            {
            };
        }

        public override async Task<Material> GetAsync(Expression<Func<Material, bool>> where)
        {
            var sampleObject = new Material()
            {
                code = "0123",
                ID = 1,
                isDelete = false,
                grossWeight = 1,
                materialNameEn = "Material 1",
                materialNameVi = "Material 1",
                netWeight = 1,
                unit = new UnitType
                {
                    code = "0123",
                    description = "Unittype 1",
                    ID = 1,
                    isDelete = false
                },
                unitID = 1
            };

            //new Material()
            //{
            //    code = "3210",
            //    ID = 2,
            //    isDelete = false,
            //    grossWeight = 1,
            //    materialNameEn = "Material 2",
            //    materialNameVi = "Material 2",
            //    netWeight = 1,
            //    unit = new UnitType
            //    {
            //        code = "0123",
            //        description = "Unittype 1",
            //        ID = 1,
            //        isDelete = false
            //    },
            //    unitID = 1
            //}

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
