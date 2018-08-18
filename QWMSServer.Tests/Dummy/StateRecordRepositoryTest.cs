using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class StateRecordRepositoryTest : RepositoryBaseTest<StateRecord>, IStateRecordRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<StateRecord> GetObjectList()
        {
            return new List<StateRecord>() {
                DataRecords.STATERECORD_NORMAL,
                DataRecords.STATERECORD_DELETED
            };
        }

        
    }
}
