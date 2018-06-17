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
        public override IList<UnitType> GetObjectList()
        {
            return new List<UnitType>() {
                DataRecords.UNITTYPE_NORMAL_1,
                DataRecords.UNITTYPE_NORMAL_2
            };
        }

        public override async Task<UnitType> GetAsync(Expression<Func<UnitType, bool>> where)
        {
            return DataRecords.UNITTYPE_NORMAL_1;
        }
    }
}
