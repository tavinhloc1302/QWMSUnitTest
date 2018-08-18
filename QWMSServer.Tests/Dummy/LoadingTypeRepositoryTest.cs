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
        public static int FLAG_DELETE = -1;

        public override IList<LoadingType> GetObjectList()
        {
            return new List<LoadingType>() {
                DataRecords.LOADING_TYPE_NORMAL,
                DataRecords.LOADING_TYPE_DELETED,
            };
        }

        public override async Task<LoadingType> GetAsync(Expression<Func<LoadingType, bool>> where)
        {
            var result = DataRecords.LOADING_TYPE_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.LOADING_TYPE_DELETED);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.LOADING_TYPE_NORMAL);
                    break;
            }
            return result;
        }
    }
}
