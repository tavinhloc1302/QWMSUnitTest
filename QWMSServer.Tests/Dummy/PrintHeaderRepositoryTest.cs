using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class PrintHeaderRepositoryTest : RepositoryBaseTest<PrintHeader>, IPrintHeaderRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<PrintHeader> GetObjectList()
        {
            return new List<PrintHeader>() {
                DataRecords.PRINTHEADER_DELETED,
                DataRecords.PRINTHEADER_NORMAL,
            };
        }

        public override async Task<PrintHeader> GetAsync(Expression<Func<PrintHeader, bool>> where)
        {
            var result = DataRecords.PRINTHEADER_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.PRINTHEADER_DELETED);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.PRINTHEADER_NORMAL);
                    break;
            }
            return result;
        }
    }
}
