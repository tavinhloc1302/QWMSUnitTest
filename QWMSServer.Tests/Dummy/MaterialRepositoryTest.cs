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
        public static int FLAG_DELETE = 0;

        public override IList<Material> GetObjectList()
        {
            return new List<Material>() {
                DataRecords.MATERIAL_NORMAL,
                DataRecords.MATERIAL_DELETED
            };
        }

        public override async Task<Material> GetAsync(Expression<Func<Material, bool>> where)
        {
            var result = DataRecords.MATERIAL_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.MATERIAL_DELETED);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.MATERIAL_NORMAL);
                    break;
            }
            return result;
        }
    }
}
