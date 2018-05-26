using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System.Collections.Generic;
using System.Linq;

namespace QWMSServer.Tests.Dummy
{
    public class LoadingBayRepositoryTest : RepositoryBaseTest<LoadingBay>, ILoadingBayRepository
    {
        public override IList<LoadingBay> GetObjectList()
        {
            return new List<LoadingBay>() {
                DataRecords.LOADING_BAY_NORMAL,
                DataRecords.LOADING_BAY_NORMAL_2,
                DataRecords.LOADING_BAY_DELETED,
            };
        }
    }
}
