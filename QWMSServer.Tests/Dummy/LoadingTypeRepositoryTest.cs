using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public class LoadingTypeRepositoryTest : RepositoryBaseTest<LoadingType>, ILoadingTypeRepository
    {
        public override IList<LoadingType> GetObjectList()
        {
            return new List<LoadingType>() {
                DataRecords.LOADING_TYPE_NORMAL,
                DataRecords.LOADING_TYPE_DELETED,
            };
        }
    }
}
