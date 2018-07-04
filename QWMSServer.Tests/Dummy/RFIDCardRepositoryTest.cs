using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class RFIDCardRepositoryTest : RepositoryBaseTest<RFIDCard>, IRFIDCardRepository
    {
        public override IList<RFIDCard> GetObjectList()
        {
            return new List<RFIDCard>()
            {
                DataRecords.RFID_CARD_NORMAL,
                DataRecords.RFID_CARD_NORMAL_2,
                DataRecords.RFID_CARD_DELETED,
            };
        }

        public override async Task<RFIDCard> GetAsync(Expression<Func<RFIDCard, bool>> where)
        {
            var result = DataRecords.RFID_CARD_NORMAL;
            return SimpleGetPatcher(result);
        }
    }
}
