using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class UnitTypeRepositoryTest : RepositoryBaseTest<UnitType>, IUnitTypeRepository
    {
        public static int FLAG_DELETE = 0;

        public override IList<UnitType> GetObjectList()
        {
            return new List<UnitType>() {
                DataRecords.UNITTYPE_NORMAL_1,
                DataRecords.UNITTYPE_NORMAL_2
            };
        }

        public override async Task<UnitType> GetAsync(Expression<Func<UnitType, bool>> where)
        {
            var result = DataRecords.UNITTYPE_NORMAL_1;

            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.UNITTYPE_NORMAL_1);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.UNITTYPE_NORMAL_2);
                    break;
            }

            return result;
        }
    }
}
