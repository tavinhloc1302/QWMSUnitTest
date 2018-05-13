using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System.Collections.Generic;
using System.Linq;

namespace QWMSServer.Tests.Dummy
{
    public class LoadingTypeRepositoryTest : RepositoryBaseTest<LoadingType>, ILoadingTypeRepository
    {
        public override IQueryable<LoadingType> Objects => new List<LoadingType>() {
                new LoadingType() {
                    ID = 1,
                    code = "1111",
                    description = "1111 Desc",
                    isDelete = false
                },new LoadingType() {
                    ID = 1,
                    code = "2222",
                    description = "2222 Desc",
                    isDelete = true
                },new LoadingType() {
                    ID = 1,
                    code = "1111",
                    description = "1111 Desc",
                    isDelete = false
                }
            }.AsQueryable();
    }
}
