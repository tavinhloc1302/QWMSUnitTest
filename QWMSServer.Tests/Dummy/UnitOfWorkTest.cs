using QWMSServer.Data.Infrastructures;
using QWMSServer.Tests.Utils;
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
        public static int COUNT_FLAG_SAVE = 1;
        public static int FLAG_SAVE_2 = 1;

        public UnitOfWorkTest()
        {

        }

        public static void ResetDummyFlags()
        {
            FLAG_SAVE = 1;
            FLAG_SAVE_2 = 1;
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
            switch (GetFlagGet())
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    throw new InvalidOperationException();
            }
        }

        public int GetFlagGet()
        {
            if (COUNT_FLAG_SAVE > 1)
            {
                var nextPropName = "FLAG_SAVE_" + COUNT_FLAG_SAVE.ToString();
                return ObjectUtils.GetProperty<int>(typeof(UnitOfWorkTest), nextPropName);
            }

            ++COUNT_FLAG_SAVE;
            return FLAG_SAVE;
        }

        public Task<bool> SaveChangesAsync()
        {
            var result = this.SaveChanges();
            return Task.FromResult(result);
        }
    }
}
