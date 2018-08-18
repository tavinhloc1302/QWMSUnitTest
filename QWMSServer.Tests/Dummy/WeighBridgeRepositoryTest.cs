using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class WeighBridgeRepositoryTest : RepositoryBaseTest<WeighBridge>, IWeighBridgeRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<WeighBridge> GetObjectList()
        {
            return new List<WeighBridge>
            {
                DataRecords.WEIGHBRIDGE_NORMAL,
                DataRecords.WEIGHBRIDGE_DELETED
            };
        }

        public override async Task<Camera> GetAsync(Expression<Func<WeighBridge, bool>> where)
        {
            var result = DataRecords.WEIGHBRIDGE_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.WEIGHBRIDGE_NORMAL);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.WEIGHBRIDGE_DELETED);
                    break;
            }
            return result;
        }
    }
}
