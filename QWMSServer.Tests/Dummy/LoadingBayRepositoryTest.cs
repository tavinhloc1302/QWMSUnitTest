using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public override async Task<LoadingBay> GetAsync(Expression<Func<LoadingBay, bool>> where)
        {
            var sampleObject = new LoadingBay()
            {
                ID = 1,
                code = "1111",
                nameVi = "Bai dau 1",
                nameEn = "Bay 1",
                warehouseID = null,
                warehouse = null,
                isDelete = false,

            };

            switch (FLAG_GET_ASYNC)
            {
                case 0:
                    sampleObject = null;
                    break;
                case 1:
                    sampleObject.isDelete = true;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return sampleObject;
        }
    }
}
