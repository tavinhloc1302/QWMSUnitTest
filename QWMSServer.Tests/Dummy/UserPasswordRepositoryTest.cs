using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class UserPasswordRepositoryTest : RepositoryBaseTest<UserPassword>, IUserPasswordRepository
    {
        public override IList<UserPassword> GetObjectList()
        {
            throw new NotImplementedException();
        }
    }
}
