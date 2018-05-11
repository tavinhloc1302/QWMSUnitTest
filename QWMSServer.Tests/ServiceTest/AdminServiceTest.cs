using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using QWMSServer.Tests.Dummy;

namespace QWMSServer.Tests.ServiceTest
{
    [TestClass]
    public class AdminServiceTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ICarrierVendorRepository _carrierRepository;
        private readonly IUserRepository _userRepository;
        private readonly AdminService _adminService;

        private static object isInitialized=false;

        static AdminServiceTest()
        {
            lock (isInitialized)
            {
                if ((bool) isInitialized) return;
                AutoMapperConfig.Configure();
                isInitialized = true;
            }
        }

        public AdminServiceTest()
        {
            _unitOfWork = new UnitOfWorkTest();
            _customerRepository = new CustomerRepositoryTest();
            _driverRepository = new DriverRepositoryTest();
            _carrierRepository = new CarrierVendorRepositoryTest();
            _userRepository = new UserRepositoryTest();
            _adminService = new AdminService(_unitOfWork, _customerRepository, _driverRepository, _carrierRepository, _userRepository);

            
        }

        [TestMethod]
        public async Task TestMethod_SaveChangesAsync()
        {
            var actualResult = await _adminService.SaveChangesAsync();
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCarrier()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel() { code = "0123" };
            var actualResult = await _adminService.CreateNewCarrier(carrierView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCustomer()
        {
            CustomerViewModel customerView = new CustomerViewModel() { code = "0123" };
            var actualResult = await _adminService.CreateNewCustomer(customerView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewDriver()
        {
            DriverViewModel driverView = new DriverViewModel() { code = "0123" };
            var actualResult = await _adminService.CreateNewDriver(driverView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCarrier()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel();
            var actualResult = await _adminService.DeleteCarrier(carrierView);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCustomer()
        {
            CustomerViewModel customerView = new CustomerViewModel();
            var actualResult = await _adminService.DeleteCustomer(customerView);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteDriver()
        {
            DriverViewModel driverView = new DriverViewModel() { code = "0123" };
            var actualResult = await _adminService.DeleteDriver(driverView);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetAllCarrier()
        {
            var actualResult = await _adminService.GetAllCarrier();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetAllCustomer()
        {
            var actualResult = await _adminService.GetAllCustomer();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetAllDriver()
        {
            var actualResult = await _adminService.GetAllDriver();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetCarrierByCode()
        {
            var actualResult = await _adminService.GetCarrierByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetCustomerByCode()
        {
            var actualResult = await _adminService.GetCustomerByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetDriverByCode()
        {
            var actualResult = await _adminService.GetDriverByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetUserPermission()
        {
            var actualResult = await _adminService.GetUserPermission(1);
            Assert.IsTrue(actualResult.Count == 0);
        }

        [TestMethod]
        public async Task TestMethod_SearchCarrier()
        {
            var actualResult = await _adminService.SearchCarrier("0123");
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchCustomer()
        {
            var actualResult = await _adminService.SearchCustomer("0123");
            var expectedResult = new ResponseViewModel<CustomerViewModel>();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchDriver()
        {
            var actualResult = await _adminService.SearchDriver("0123");
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_UpdateCarrier()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel() { code = "0123" };
            var actualResult = await _adminService.UpdateCarrier(carrierView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateCustomer()
        {
            CustomerViewModel customerView = new CustomerViewModel() { code = "0123" };
            var actualResult = await _adminService.UpdateCustomer(customerView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateDriver()
        {
            DriverViewModel driverView = new DriverViewModel() { code = "0123" };
            var actualResult = await _adminService.UpdateDriver(driverView);
            Assert.IsNotNull(actualResult.responseData);
        }
    }
}
