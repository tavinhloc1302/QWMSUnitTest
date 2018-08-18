using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class PlantRepositoryTest : RepositoryBaseTest<Plant>, IPlantRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<Plant> GetObjectList()
        {
            return new List<Plant>() {
                DataRecords.PLANT_DELETED,
                DataRecords.PLANT_NORMAL,
            };
        }

        public override async Task<Plant> GetAsync(Expression<Func<Plant, bool>> where)
        {
            var result = DataRecords.PLANT_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.PLANT_NORMAL);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.PLANT_DELETED);
                    break;
            }
            return result;
        }
    }
}
