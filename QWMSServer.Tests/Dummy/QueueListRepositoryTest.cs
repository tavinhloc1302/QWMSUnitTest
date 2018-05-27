using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class QueueListRepositoryTest : RepositoryBaseTest<QueueList>, IQueueListRepository
    {
        public override IList<QueueList> GetObjectList()
        {
            return new List<QueueList>() {
                DataRecords.QUEUE_LIST_NORMAL,
                DataRecords.QUEUE_LIST_NORMAL_2,
                DataRecords.QUEUE_LIST_DELETED,
            };
        }
    }
}
