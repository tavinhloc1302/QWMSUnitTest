using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public class StateRecordRepositoryTest : RepositoryBaseTest<StateRecord>, IStateRecordRepository
    {
        public override IList<StateRecord> GetObjectList()
        {
            return new List<StateRecord>() {
                new StateRecord() {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    gatePassID = 1,
                    stateID = 1,
                    stateStatus = 1
                },
                new StateRecord() {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    gatePassID = 1,
                    stateID = 1,
                    stateStatus = 1
                }
            };
        }
    }
}
