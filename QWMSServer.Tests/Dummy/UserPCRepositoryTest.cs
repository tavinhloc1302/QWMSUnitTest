using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class UserPCRepositoryTest : RepositoryBaseTest<UserPC>, IUserPCRepository
    {
        public override IList<UserPC> GetObjectList()
        {
            throw new NotImplementedException();
        }
    }
}
