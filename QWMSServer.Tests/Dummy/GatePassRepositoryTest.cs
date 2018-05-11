using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class GatePassRepositoryTest : IGatePassRepository
    {
        public IQueryable<GatePass> Objects => new List<GatePass>() {
                new GatePass() {
                    createDate = DateTime.Now,
                    driver = new Driver(),
                    driverCamCapturePath = "Driver Path 1",
                    driverID = 1,
                    employee = new Employee(),
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    employeeID = 1,
                    enterTime = DateTime.Now,
                    leaveTime = DateTime.Now,
                    loadingBayID = 1,
                    orders = new List<Order>(),
                    queueLists = new List<QueueList>(),
                    RFIDCardID = 1,
                    stateID = 1,
                    truckID = 1,
                    truckGroupID = 1,
                    truckTyeID = 1
                },
                new GatePass() {
                    createDate = DateTime.Now,
                    driver = new Driver(),
                    driverCamCapturePath = "Driver Path 2",
                    driverID = 2,
                    employee = new Employee(),
                    code = "3210",
                    ID = 2,
                    isDelete = false,
                    employeeID = 2,
                    enterTime = DateTime.Now,
                    leaveTime = DateTime.Now,
                    loadingBayID = 2,
                    orders = new List<Order>(),
                    queueLists = new List<QueueList>(),
                    RFIDCardID = 2,
                    stateID = 2,
                    truckID = 2,
                    truckGroupID = 2,
                    truckTyeID = 2
                }
            }.AsQueryable();

        public void Add(GatePass entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<GatePass, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(GatePass entity)
        {
        }

        public void Delete(Expression<Func<GatePass, bool>> where)
        {
        }

        public async Task<IEnumerable<GatePass>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<GatePass> GetAsync(Expression<Func<GatePass, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<GatePass> GetAsync(Expression<Func<GatePass, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<GatePass> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<GatePass>> GetManyAsync(Expression<Func<GatePass, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<GatePass>> GetManyAsync(Expression<Func<GatePass, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<GatePass> Query(Expression<Func<GatePass, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(GatePass entity)
        {
        }
    }
}
