using QWMSServer.Data.Infrastructures;

using System;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    class UnitOfWorkTest : IUnitOfWork
    {
        public UnitOfWorkTest()
        {

        }

        public System.Data.Entity.DbContextTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public bool Exists()
        {
            return true;
        }

        public bool SaveChanges()
        {
            return true;
        }

        public Task<bool> SaveChangesAsync()
        {
            return Task.FromResult(true);
        }
    }
}
