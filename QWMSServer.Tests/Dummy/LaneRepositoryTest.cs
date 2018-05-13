using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    class LaneRepositoryTest : ILaneRepository
    {
        public IQueryable<Lane> Objects => null;
            //new List<Lane>() {
            //    new Lane() {
            //        addressEn = "Address in English",
            //        addressVi = "Address in Vietnamese",
            //        code = "0123",
            //        contactPerson = "Galvin Nguyen",
            //        department = "Sky",
            //        ID = 1,
            //        isDelete = false,
            //        nameEn = "Sky Rider 1",
            //        nameVi = "Sky Rider 1",
            //        shortName = "SR1",
            //        taxCode = "0123",
            //        telNo ="0123456789"
            //    },
            //       new CarrierVendor() {
            //        addressEn = "Address in English",
            //        addressVi = "Address in Vietnamese",
            //        code = "3210",
            //        contactPerson = "Galvin Nguyen",
            //        department = "Sky",
            //        ID = 2,
            //        isDelete = false,
            //        nameEn = "Sky Rider 2",
            //        nameVi = "Sky Rider 2",
            //        shortName = "SR2",
            //        taxCode = "0123",
            //        telNo ="98765432100123456789"
            //    }
            //}.AsQueryable();

        public void Add(Lane entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<Lane, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(Lane entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<Lane, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Lane>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Lane> GetAsync(Expression<Func<Lane, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<Lane> GetAsync(Expression<Func<Lane, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<Lane> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Lane>> GetManyAsync(Expression<Func<Lane, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Lane>> GetManyAsync(Expression<Func<Lane, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Lane> Query(Expression<Func<Lane, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(Lane entity)
        {
            throw new NotImplementedException();
        }
    }
}
