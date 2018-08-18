using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class LoadingBayRepositoryTest : RepositoryBaseTest<LoadingBay>, ILoadingBayRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<LoadingBay> GetObjectList()
        {
            return new List<LoadingBay>() {
                DataRecords.LOADING_BAY_NORMAL,
                DataRecords.LOADING_BAY_NORMAL_2,
                DataRecords.LOADING_BAY_DELETED,
            };
        }

        public override async Task<LoadingBay> GetAsync(Expression<Func<LoadingBay, bool>> where)
        {
            var result = DataRecords.LOADING_BAY_NORMAL;

            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.LOADING_BAY_DELETED);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.LOADING_BAY_NORMAL);
                    break;
            }

            return result;
        }
    }
}
