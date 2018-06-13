using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

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
