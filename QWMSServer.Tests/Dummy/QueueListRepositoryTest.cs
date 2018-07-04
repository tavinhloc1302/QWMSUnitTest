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
        public static int FLAG_DELETE = 0;

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
            return result;
        }
    }
}
