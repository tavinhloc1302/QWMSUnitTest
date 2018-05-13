using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    class TruckRepositoryTest : RepositoryBaseTest<Truck>, ITruckRepository
    {
        public override IQueryable<Truck> Objects => new List<Truck>() {
                new Truck() {
                    ID = 1,
                    code = "A001",
                    plateNumber = "1111",
                    weightValueRegistWithCalofig = 0.71111F,
                    carrierVendorID = 1,
                    truckLenght = 20.1111F,
                    truckHeight = 2.51111F,
                    truckWidth = 2.1111F,
                    containerLenght = 15.1111F,
                    containerWidth = 2.1111F,
                    containerHeight = 2.1111F,
                    truckNetWeight = 2.1111F,
                    weightValueRegistWithTransportDepartment = 3.1111F,
                    totalWeight = 4.1111F,
                    expireYear = 2025,
                    truckTypeID = 2,
                    loadingTypeID = 3,
                    KPI = 1111,
                    isDelete = false
                },
                new Truck() {
                    ID = 2,
                    code = "A002",
                    plateNumber = "2222",
                    weightValueRegistWithCalofig = 0.72222F,
                    carrierVendorID = 2,
                    truckLenght = 20.2222F,
                    truckHeight = 2.52222F,
                    truckWidth = 2.2222F,
                    containerLenght = 15.2222F,
                    containerWidth = 2.2222F,
                    containerHeight = 2.2222F,
                    truckNetWeight = 2.2222F,
                    weightValueRegistWithTransportDepartment = 3.2222F,
                    totalWeight = 4.2222F,
                    expireYear = 2025,
                    truckTypeID = 3,
                    loadingTypeID = 1,
                    KPI = 2222,
                    isDelete = false
                },
                new Truck() {
                    ID = 3,
                    code = "A003",
                    plateNumber = "3333",
                    weightValueRegistWithCalofig = 0.73333F,
                    carrierVendorID = 3,
                    truckLenght = 20.3333F,
                    truckHeight = 2.53333F,
                    truckWidth = 2.3333F,
                    containerLenght = 15.3333F,
                    containerWidth = 2.3333F,
                    containerHeight = 2.3333F,
                    truckNetWeight = 2.3333F,
                    weightValueRegistWithTransportDepartment = 3.3333F,
                    totalWeight = 4.3333F,
                    expireYear = 2025,
                    truckTypeID = 1,
                    loadingTypeID = 2,
                    KPI = 3333,
                    isDelete = false
                }
            }.AsQueryable();
    }
}
