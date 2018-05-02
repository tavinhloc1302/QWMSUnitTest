using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDBContext _dbContext;

        public UnitOfWork(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbContextTransaction BeginTransaction() => _dbContext.Database.BeginTransaction();

        public bool SaveChanges()
        {
            _dbContext.Database.Log = message => Trace.Write(message);
            var total = 0;
            total = _dbContext.SaveChanges();
            return total > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var total = 0;
            total = await _dbContext.SaveChangesAsync();
            return total > 0;
        }

        public bool Exists()
        {
            return _dbContext.Database.Exists();
        }
    }
}
