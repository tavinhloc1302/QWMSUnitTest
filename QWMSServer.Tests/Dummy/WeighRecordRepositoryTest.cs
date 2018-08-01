using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public class WeighRecordRepositoryTest : RepositoryBaseTest<WeightRecord>, IWeightRecordRepository
    {
        public override IList<WeightRecord> GetObjectList()
        {
            throw new NotImplementedException();
        }
    }
}
