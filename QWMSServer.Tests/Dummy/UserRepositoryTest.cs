using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public class UserRepositoryTest : RepositoryBaseTest<User>, IUserRepository
    {
        public override IList<User> GetObjectList()
        {
            return new List<User>() {
                new User() {
                    Code = "0123",
                    employees = new List<Employee>() {
                        new Employee(),
                        new Employee()
                    },
                    password = "password",
                    username ="skyrider1",
                    ID = 1,
                    isDelete = false
                },
                new User() {
                    Code = "3210",
                    employees = new List<Employee>(),
                    password = "password",
                    username ="skyrider2",
                    ID = 2,
                    isDelete = false
                }
            };
        }
    }
}
