using QWMSServer.Data.Infrastructures;
using System;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    class UnitOfWorkTest : IUnitOfWork
    {
        // 0: false
        // 1: true
        // Other: Exception
        public static int FLAG_SAVE = 1;

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
            switch (FLAG_SAVE)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    throw new InvalidOperationException();
            }
        }

        public Task<bool> SaveChangesAsync()
        {
            var result = this.SaveChanges();
            return Task.FromResult(result);
        }
    }
}
