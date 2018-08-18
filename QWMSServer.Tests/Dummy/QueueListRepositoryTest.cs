using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class QueueListRepositoryTest : RepositoryBaseTest<QueueList>, IQueueListRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<QueueList> GetObjectList()
        {
            return new List<QueueList>() {
                DataRecords.QUEUE_LIST_NORMAL,
                DataRecords.QUEUE_LIST_NORMAL_2,
                DataRecords.QUEUE_LIST_DELETED,
            };
        }

        public override async Task<QueueList> GetAsync(Expression<Func<QueueList, bool>> where)
        {
            var result = DataRecords.QUEUE_LIST_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.QUEUE_LIST_DELETED);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.QUEUE_LIST_NORMAL);
                    break;
            }
            return result;
        }
    }
}
