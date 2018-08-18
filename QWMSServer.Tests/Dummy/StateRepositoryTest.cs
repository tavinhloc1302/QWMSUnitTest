using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public class StateRepositoryTest : RepositoryBaseTest<State>, IStateRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<State> GetObjectList()
        {
            return new List<State>() {
                new State() {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    desciption = "Sky Rider 1",
                    order = 1
                },
                new State() {
                    code = "3210",
                    ID = 2,
                    isDelete = false,
                    desciption = "Sky Rider 2",
                    order = 2
                }
            };
        }
    }
}
