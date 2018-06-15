using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class MaterialRepositoryTest : RepositoryBaseTest<Material>, IMaterialRepository
    {
        public override IList<Material> GetObjectList()
        {
            return new List<Material>() {
                new Material() {
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
                },
                new Material() {
                    code = "3210",
                    ID = 2,
                    isDelete = false,
                    grossWeight = 1,
                    materialNameEn = "Material 2",
                    materialNameVi = "Material 2",
                    netWeight = 1,
                    unit = new UnitType
                    {
                        code = "0123",
                        description = "Unittype 1",
                        ID = 1,
                        isDelete = false
                    },
                    unitID = 1
                }
            };
        }

        public override async Task<Material> GetAsync(Expression<Func<Material, bool>> where)
        {
            return new Material()
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
        }
    }
}
