using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public class WarehouseRepositoryTest : RepositoryBaseTest<Warehouse>, IWarehouseRepository
    {
        public override IList<Warehouse> GetObjectList()
        {
            return new List<Warehouse>() {
                new Warehouse() {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    nameEn = "Sky Rider 1",
                    nameVi = "Sky Rider 1",
                    loadingBays = new List<LoadingBay>(),
                    plantID = 1
                },
                new Warehouse() {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    nameEn = "Sky Rider 1",
                    nameVi = "Sky Rider 1",
                    loadingBays = new List<LoadingBay>(),
                    plantID = 1
                }
            };
        }
    }
}
