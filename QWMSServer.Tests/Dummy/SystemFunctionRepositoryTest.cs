using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public class SystemFunctionRepositoryTest : RepositoryBaseTest<SystemFunction>, ISystemFunctionRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<SystemFunction> GetObjectList()
        {
            throw new NotImplementedException();
        }
    }
}
