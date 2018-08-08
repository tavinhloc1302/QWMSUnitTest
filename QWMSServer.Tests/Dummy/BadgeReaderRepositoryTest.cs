using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class BadgeReaderRepositoryTest : RepositoryBaseTest<BadgeReader>, IBadgeReaderRepository
    {
        public static int FLAG_DELETE = 0;

        public override IList<BadgeReader> GetObjectList()
        {
            return new List<BadgeReader>()
            {
                DataRecords.BADGEREADER_NORMAL,
                DataRecords.BADGEREADER_DELETED
            };
        }

        public override async Task<BadgeReader> GetAsync(Expression<Func<BadgeReader, bool>> where)
        {
            var result = DataRecords.BADGEREADER_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.BADGEREADER_NORMAL);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.BADGEREADER_DELETED);
                    break;
            }
            return result;
        }
    }
}
