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
            var expectedResult = true;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCarrier(CarrierVendorViewModel carrierView)
        {
            var actualResult = await _adminService.CreateNewCarrier(carrierView);
            var expectedResult = new ResponseViewModel<CarrierVendorViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCustomer(CustomerViewModel customerView)
        {
            var actualResult = await _adminService.CreateNewCustomer(customerView);
            var expectedResult = new ResponseViewModel<CustomerViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewDriver(DriverViewModel driverView)
        {
            var actualResult = await _adminService.CreateNewDriver(driverView);
            var expectedResult = new ResponseViewModel<DriverViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCarrier(CarrierVendorViewModel carrierView)
        {
            var actualResult = await _adminService.DeleteCarrier(carrierView);
            var expectedResult = new ResponseViewModel<CarrierVendorViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCustomer(CustomerViewModel customerView)
        {
            var actualResult = await _adminService.DeleteCustomer(customerView);
            var expectedResult = new ResponseViewModel<CustomerViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_DeleteDriver(DriverViewModel driverView)
        {
            var actualResult = await _adminService.DeleteDriver(driverView);
            var expectedResult = new ResponseViewModel<DriverViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_GetAllCarrier()
        {
            var actualResult = await _adminService.GetAllCarrier();
            var expectedResult = new ResponseViewModel<CarrierVendorViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_GetAllCustomer()
        {
            var actualResult = await _adminService.GetAllCustomer();
            var expectedResult = new ResponseViewModel<CustomerViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_GetAllDriver()
        {
            var actualResult = await _adminService.GetAllDriver();
            var expectedResult = new ResponseViewModel<DriverViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_GetCarrierByCode(string code)
        {
            var actualResult = await _adminService.GetCarrierByCode(code);
            var expectedResult = new ResponseViewModel<CarrierVendorViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_GetCustomerByCode(string code)
        {
            var actualResult = await _adminService.GetCustomerByCode(code);
            var expectedResult = new ResponseViewModel<CustomerViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_GetDriverByCode(string code)
        {
            var actualResult = await _adminService.GetDriverByCode(code);
            var expectedResult = new ResponseViewModel<DriverViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_GetUserPermission(int userID)
        {
            var actualResult = await _adminService.GetUserPermission(userID);
            var expectedResult = new ResponseViewModel<SystemFunctionViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_SearchCarrier(string code)
        {
            var actualResult = await _adminService.SearchCarrier(code);
            var expectedResult = new ResponseViewModel<CarrierVendorViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_SearchCustomer(string code)
        {
            var actualResult = await _adminService.SearchCustomer(code);
            var expectedResult = new ResponseViewModel<CustomerViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_SearchDriver(string code)
        {
            var actualResult = await _adminService.SearchDriver(code);
            var expectedResult = new ResponseViewModel<DriverViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_UpdateCarrier(CarrierVendorViewModel carrierView)
        {
            var actualResult = await _adminService.UpdateCarrier(carrierView);
            var expectedResult = new ResponseViewModel<CarrierVendorViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_UpdateCustomer(CustomerViewModel customerView)
        {
            var actualResult = await _adminService.UpdateCustomer(customerView);
            var expectedResult = new ResponseViewModel<CustomerViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_UpdateDriver(DriverViewModel driverView)
        {
            var actualResult = await _adminService.UpdateDriver(driverView);
            var expectedResult = new ResponseViewModel<DriverViewModel>()
            {
                
            };
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
