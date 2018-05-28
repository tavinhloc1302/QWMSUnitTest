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
        private readonly IMaterialRepository _materialRepository;
        private readonly IUnitTypeRepository _unittypeRepository;
        private readonly ITruckRepository _truckRepository;
        private readonly ITruckTypeRepository _truckTypeRepository;
        private readonly ILoadingTypeRepository _loadingTypeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeGroupRepository _employeeGroupRepository;
        private readonly IEmployeeRoleRepository _employeeRoleRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly ILoadingBayRepository _loadingBayRepository;
        private readonly ILaneRepository _laneRepository;
        private readonly AdminService _adminService;

        public AdminServiceTest()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Configure();

            _unitOfWork = new UnitOfWorkTest();
            _customerRepository = new CustomerRepositoryTest();
            _driverRepository = new DriverRepositoryTest();
            _carrierRepository = new CarrierVendorRepositoryTest();
            _userRepository = new UserRepositoryTest();
            _materialRepository = new MaterialRepositoryTest();
            _unittypeRepository = new UnitTypeRepositoryTest();
            _truckRepository = new TruckRepositoryTest();
            _loadingTypeRepository = new LoadingTypeRepositoryTest();
            _employeeRepository = new EmployeeRepositoryTest();
            _employeeGroupRepository = new EmployeeGroupRepositoryTest();
            _plantRepository = new PlantRepositoryTest();
            _companyRepository = new CompanyRepositoryTest();
            _warehouseRepository = new WarehouseRepositoryTest();
            _loadingBayRepository = new LoadingBayRepositoryTest();
            _laneRepository = new LaneRepositoryTest();

            _adminService = new AdminService(_unitOfWork, _customerRepository, _driverRepository, _carrierRepository, _userRepository, _materialRepository, _unittypeRepository, _truckRepository, _truckTypeRepository, _loadingTypeRepository, _employeeRepository, _employeeGroupRepository, _employeeRoleRepository, _plantRepository, _companyRepository, _warehouseRepository, _loadingBayRepository, _laneRepository);
        }

        [TestMethod]
        public async Task TestMethod_SaveChangesAsync()
        {
            var actualResult = await _adminService.SaveChangesAsync();
            Assert.IsTrue(actualResult);
        }

        // ---------------------------------------------> Begin CreateNewCarrier test cases

        [TestMethod]
        public async Task TestMethod_CreateNewCarrier()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel() { code = "0123" };
            var actualResult = await _adminService.CreateNewCarrier(carrierView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCarrier_ShouldFail_NoCode()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel();
            var actualResult = await _adminService.CreateNewCarrier(carrierView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCarrier_ShouldFail_NullModel()
        {
            var actualResult = await _adminService.CreateNewCarrier(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewCarrier test cases

        // ---------------------------------------------> Begin CreateNewCustomer test cases

        [TestMethod]
        public async Task TestMethod_CreateNewCustomer()
        {
            CustomerViewModel customerView = new CustomerViewModel() { code = "0123" };
            var actualResult = await _adminService.CreateNewCustomer(customerView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCustomer_ShouldFail_NoCode()
        {
            CustomerViewModel customerView = new CustomerViewModel();
            var actualResult = await _adminService.CreateNewCustomer(customerView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCustomer_ShouldFail_NullModel()
        {
            var actualResult = await _adminService.CreateNewCustomer(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewCarrier test cases

        // ---------------------------------------------> Begin CreateNewDriver test cases

        [TestMethod]
        public async Task TestMethod_CreateNewDriver()
        {
            DriverViewModel driverView = new DriverViewModel() { code = "0123" };
            var actualResult = await _adminService.CreateNewDriver(driverView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewDriver_ShouldFail_NoCode()
        {
            DriverViewModel driverView = new DriverViewModel();
            var actualResult = await _adminService.CreateNewDriver(driverView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewDriver_ShouldFail_NullModel()
        {
            var actualResult = await _adminService.CreateNewDriver(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewCarrier test cases

        // ---------------------------------------------> Begin DeleteCarrier test cases

        [TestMethod]
        public async Task TestMethod_DeleteCarrier()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel();
            var actualResult = await _adminService.DeleteCarrier(carrierView);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCarrier_ShouldFail_NullModel()
        {
            var actualResult = await _adminService.DeleteCarrier(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End DeleteCarrier test cases

        // ---------------------------------------------> Begin DeleteCustomer test cases

        [TestMethod]
        public async Task TestMethod_DeleteCustomer()
        {
            CustomerViewModel customerView = new CustomerViewModel();
            var actualResult = await _adminService.DeleteCustomer(customerView);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCustomer_ShouldFail_NullModel()
        {
            var actualResult = await _adminService.DeleteCustomer(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End DeleteCustomer test cases

        // ---------------------------------------------> Begin DeleteDriver test cases
        [TestMethod]
        public async Task TestMethod_DeleteDriver()
        {
            DriverViewModel driverView = new DriverViewModel() { code = "0123" };
            var actualResult = await _adminService.DeleteDriver(driverView);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteDriver_ShouldFail_NoCode()
        {
            DriverViewModel driverView = new DriverViewModel();
            var actualResult = await _adminService.DeleteDriver(driverView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteDriver_ShouldFail_NullModel()
        {
            var actualResult = await _adminService.DeleteDriver(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End DeleteCustomer test cases

        // ---------------------------------------------> Begin GetAllCarrier test cases

        [TestMethod]
        public async Task TestMethod_GetAllCarrier()
        {
            var actualResult = await _adminService.GetAllCarrier();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End DeleteCustomer test cases

        // ---------------------------------------------> Begin GetAllCustomer test cases

        [TestMethod]
        public async Task TestMethod_GetAllCustomer()
        {
            var actualResult = await _adminService.GetAllCustomer();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End GetAllCustomer test cases

        // ---------------------------------------------> Begin GetAllDriver test cases

        [TestMethod]
        public async Task TestMethod_GetAllDriver()
        {
            var actualResult = await _adminService.GetAllDriver();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End GetAllDriver test cases

        // ---------------------------------------------> Begin GetCarrierByCode test cases

        [TestMethod]
        public async Task TestMethod_GetCarrierByCode()
        {
            var actualResult = await _adminService.GetCarrierByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetCarrierByCode_ShouldFail_NoCode()
        {
            var actualResult = await _adminService.GetCarrierByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End GetCarrierByCode test cases

        // ---------------------------------------------> Begin GetCustomerByCode test cases

        [TestMethod]
        public async Task TestMethod_GetCustomerByCode()
        {
            var actualResult = await _adminService.GetCustomerByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetCustomerByCode_ShouldFail_NoCode()
        {
            var actualResult = await _adminService.GetCustomerByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End GetCustomerByCode test cases

        // ---------------------------------------------> Begin GetDriverByCode test cases

        [TestMethod]
        public async Task TestMethod_GetDriverByCode()
        {
            var actualResult = await _adminService.GetDriverByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetDriverByCode_ShouldFail_NoCode()
        {
            var actualResult = await _adminService.GetDriverByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End GetDriverByCode test cases

        // ---------------------------------------------> Begin SearchCarrier test cases

        [TestMethod]
        public async Task TestMethod_SearchCarrier()
        {
            var actualResult = await _adminService.SearchCarrier("0123");
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchCarrier_ShouldFail_NoCode()
        {
            var actualResult = await _adminService.SearchCarrier(null);
            Assert.IsNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End SearchCarrier test cases

        // ---------------------------------------------> Begin SearchCustomer test cases

        [TestMethod]
        public async Task TestMethod_SearchCustomer()
        {
            var actualResult = await _adminService.SearchCustomer("0123");
            var expectedResult = new ResponseViewModel<CustomerViewModel>();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchCustomer_ShouldFail_NoCode()
        {
            var actualResult = await _adminService.SearchCustomer(null);
            var expectedResult = new ResponseViewModel<CustomerViewModel>();
            Assert.IsNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End SearchCustomer test cases

        // ---------------------------------------------> Begin SearchDriver test cases

        [TestMethod]
        public async Task TestMethod_SearchDriver()
        {
            var actualResult = await _adminService.SearchDriver("0123");
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchDriver_ShouldFail_NoCode()
        {
            var actualResult = await _adminService.SearchDriver(null);
            Assert.IsNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End SearchDriver test cases

        // ---------------------------------------------> Begin UpdateCarrier test cases

        [TestMethod]
        public async Task TestMethod_UpdateCarrier()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel() { code = "0123" };
            var actualResult = await _adminService.UpdateCarrier(carrierView);
            Assert.IsNotNull(actualResult.responseData);
        }

        public async Task TestMethod_UpdateCarrier_ShouldFail_NullModel()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel() { code = "0123" };
            var actualResult = await _adminService.UpdateCarrier(null);
            Assert.IsNull(actualResult.responseData);
        }

        public async Task TestMethod_UpdateCarrier_ShouldFail_NoCode()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel();
            var actualResult = await _adminService.UpdateCarrier(carrierView);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End UpdateCarrier test cases

        // ---------------------------------------------> Begin UpdateCustomer test cases

        [TestMethod]
        public async Task TestMethod_UpdateCustomer()
        {
            CustomerViewModel customerView = new CustomerViewModel() { code = "0123" };
            var actualResult = await _adminService.UpdateCustomer(customerView);
            Assert.IsNotNull(actualResult.responseData);
        }

        public async Task TestMethod_UpdateCustomer_ShouldFail_NullModel()
        {
            var actualResult = await _adminService.UpdateCustomer(null);
            Assert.IsNull(actualResult.responseData);
        }

        public async Task TestMethod_UpdateCustomer_ShouldFail_NoCode()
        {
            CustomerViewModel customerView = new CustomerViewModel();
            var actualResult = await _adminService.UpdateCustomer(customerView);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End UpdateCustomer test cases

        // ---------------------------------------------> Begin UpdateDriver test cases

        [TestMethod]
        public async Task TestMethod_UpdateDriver()
        {
            DriverViewModel driverView = new DriverViewModel() { code = "0123" };
            var actualResult = await _adminService.UpdateDriver(driverView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateDriver_ShouldFail_NoCode()
        {
            DriverViewModel driverView = new DriverViewModel();
            var actualResult = await _adminService.UpdateDriver(driverView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateDriver_ShouldFail_NullModel()
        {
            var actualResult = await _adminService.UpdateDriver(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End UpdateDriver test cases
    }
}
