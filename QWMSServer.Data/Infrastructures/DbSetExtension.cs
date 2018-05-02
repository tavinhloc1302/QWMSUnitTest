using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Infrastructures
{
    public static class DbSetExtension
    {
        public static async Task<TEntity> FindAsync<TEntity>(this IDbSet<TEntity> @this, params object[] keyValues)
where TEntity : class
        {
            if (@this is DbSet<TEntity> thisDbSet)
            {
                return await thisDbSet.FindAsync(keyValues);
            }
            else
            {
                return @this.Find(keyValues);
            }
        }
    }
}
