using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public override async Task<LoadingType> GetAsync(Expression<Func<LoadingType, bool>> where)
        {
            return DataRecords.LOADING_TYPE_NORMAL;
        }
    }
}
