using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class CameraRepositoryTest : RepositoryBaseTest<Camera>, ICameraRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<Camera> GetObjectList()
        {
            return new List<Camera>()
            {
                DataRecords.CAMERA_NORMAL,
                DataRecords.CAMERA_DELETED
            };
        }

        public override async Task<Camera> GetAsync(Expression<Func<Camera, bool>> where)
        {
            var result = DataRecords.CAMERA_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.CAMERA_DELETED);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.CAMERA_NORMAL);
                    break;
            }
            return result;
        }
    }
}
