using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Data.Services;
using QWMSServer.Tests.Dummy;

namespace QWMSServer.Tests.ServiceTest
{
    [TestClass]
    public class AdminServiceTest
    {
        private readonly IUnitOfWork _unitOfWork = new UnitOfWorkTest();
        private readonly ICustomerRepository _customerRepository = new CustomerRepositoryTest();
        private readonly IDriverRepository _driverRepository = new DriverRepositoryTest();
        private readonly ICarrierVendorRepository _carrierRepository = new CarrierVendorRepositoryTest();
        private readonly IUserRepository _userRepository = new UserRepositoryTest();

        [TestMethod]
        public async Task TestMethod_SaveChangesAsync()
        {
            AdminService adminService = new AdminService(_unitOfWork, _customerRepository, _driverRepository, _carrierRepository, _userRepository);
            var ret = await adminService.SaveChangesAsync();
            var expected_ret = true;
            Assert.AreEqual(expected_ret, ret);
        }
    }
}
