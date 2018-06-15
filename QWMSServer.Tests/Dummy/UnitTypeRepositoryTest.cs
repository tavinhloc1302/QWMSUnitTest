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
    public class UnitTypeRepositoryTest : RepositoryBaseTest<UnitType>, IUnitTypeRepository
    {
        public override IList<UnitType> GetObjectList()
        {
            return new List<UnitType>() {
                new UnitType
                {
                    code = "0123",
                    description = "Unittype 1",
                    ID = 1,
                    isDelete = false
                },
                new UnitType
                {
                    code = "0123",
                    description = "Unittype 1",
                    ID = 1,
                    isDelete = false
                }
            };
        }

        public override async Task<UnitType> GetAsync(Expression<Func<UnitType, bool>> where)
        {
            return new UnitType
            {
                code = "0123",
                description = "Unittype 1",
                ID = 1,
                isDelete = false
            };
        }
    }
}
