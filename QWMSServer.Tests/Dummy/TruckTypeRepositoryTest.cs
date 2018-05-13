using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System.Collections.Generic;
using System.Linq;

namespace QWMSServer.Tests.Dummy
{
    public class TruckTypeRepositoryTest : RepositoryBaseTest<TruckType>, ITruckTypeRepository
    {
        public override IQueryable<TruckType> Objects => new List<TruckType>() {
                new TruckType() {
                    ID = 1,
                    code = "1111",
                    desciption = "1111 Desc",
                    isDelete = false
                },
                new TruckType() {
                    ID = 2,
                    code = "2222",
                    desciption = "2222 Desc",
                    isDelete = true
                },
                new TruckType() {
                    ID = 3,
                    code = "3333",
                    desciption = "3333 Desc",
                    isDelete = false
                }
            }.AsQueryable();
    }
}
