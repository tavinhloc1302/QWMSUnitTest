using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Infrastructures
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();

        bool SaveChanges();

        DbContextTransaction BeginTransaction();

        bool Exists();
    }
}
