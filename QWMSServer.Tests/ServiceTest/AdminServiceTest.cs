using Microsoft.VisualStudio.TestTools.UnitTesting;

using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using QWMSServer.Tests.Dummy;

using System.Threading.Tasks;

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
            _employeeRoleRepository = new EmployeeRoleRepositoryTest();
            _truckTypeRepository = new TruckTypeRepositoryTest();

            _adminService = new AdminService(
                _unitOfWork, _customerRepository, _driverRepository, _carrierRepository, _userRepository,
                _materialRepository, _unittypeRepository, _truckRepository, _truckTypeRepository, _loadingTypeRepository,
                _employeeRepository, _employeeGroupRepository, _employeeRoleRepository, _plantRepository,
                _companyRepository, _warehouseRepository, _loadingBayRepository, _laneRepository);
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
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel() { code = "0123" };
            var actualResult = await _adminService.CreateNewCarrier(carrierView);
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCarrier_NoCode()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel();
            var actualResult = await _adminService.CreateNewCarrier(carrierView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCarrier_NullModel()
        {
            var actualResult = await _adminService.CreateNewCarrier(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewCarrier test cases

        // ---------------------------------------------> Begin CreateNewCustomer test cases

        [TestMethod]
        public async Task TestMethod_CreateNewCustomer()
        {
            CustomerRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerViewModel customerView = new CustomerViewModel() { code = "0123" };
            var actualResult = await _adminService.CreateNewCustomer(customerView);
            CustomerRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCustomer_NoCode()
        {
            CustomerViewModel customerView = new CustomerViewModel();
            var actualResult = await _adminService.CreateNewCustomer(customerView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCustomer_NullModel()
        {
            var actualResult = await _adminService.CreateNewCustomer(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewCarrier test cases

        // ---------------------------------------------> Begin CreateNewDriver test cases

        [TestMethod]
        public async Task TestMethod_CreateNewDriver()
        {
            DriverRepositoryTest.FLAG_GET_ASYNC = 1;
            DriverViewModel driverView = new DriverViewModel() { code = "0123" };
            var actualResult = await _adminService.CreateNewDriver(driverView);
            DriverRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewDriver_NoCode()
        {
            DriverViewModel driverView = new DriverViewModel();
            var actualResult = await _adminService.CreateNewDriver(driverView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewDriver_NullModel()
        {
            var actualResult = await _adminService.CreateNewDriver(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewCarrier test cases

        // ---------------------------------------------> Begin DeleteCarrier test cases

        [TestMethod]
        public async Task TestMethod_DeleteCarrier()
        {
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel()
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteCarrier(carrierView);
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCarrier_WrongID()
        {
            CarrierVendorRepositoryTest.FLAG_DELETE = 2;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeleteCarrier(carrierView);
            CarrierVendorRepositoryTest.FLAG_DELETE = 0;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCarrier_NoID()
        {
            CarrierVendorRepositoryTest.FLAG_DELETE = 1;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel();
            var actualResult = await _adminService.DeleteCarrier(carrierView);
            CarrierVendorRepositoryTest.FLAG_DELETE = 0;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCarrier_NullModel()
        {
            var actualResult = await _adminService.DeleteCarrier(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End DeleteCarrier test cases

        // ---------------------------------------------> Begin DeleteCustomer test cases

        [TestMethod]
        public async Task TestMethod_DeleteCustomer()
        {
            CustomerRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerViewModel customerView = new CustomerViewModel()
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteCustomer(customerView);
            CustomerRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCustomer_NoID()
        {
            CustomerRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerRepositoryTest.FLAG_DELETE = 1;
            CustomerViewModel customerView = new CustomerViewModel();
            var actualResult = await _adminService.DeleteCustomer(customerView);
            CustomerRepositoryTest.FLAG_GET_ASYNC = 0;
            CustomerRepositoryTest.FLAG_DELETE = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCustomer_WrongID()
        {
            CustomerRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerRepositoryTest.FLAG_DELETE = 2;
            CustomerViewModel customerView = new CustomerViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeleteCustomer(customerView);
            CustomerRepositoryTest.FLAG_GET_ASYNC = 0;
            CustomerRepositoryTest.FLAG_DELETE = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCustomer_NullModel()
        {
            var actualResult = await _adminService.DeleteCustomer(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End DeleteCustomer test cases

        // ---------------------------------------------> Begin DeleteDriver test cases
        [TestMethod]
        public async Task TestMethod_DeleteDriver()
        {
            DriverRepositoryTest.FLAG_GET_ASYNC = 1;
            DriverViewModel driverView = new DriverViewModel() { code = "0123" };
            var actualResult = await _adminService.DeleteDriver(driverView);
            DriverRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteDriver_NoCode()
        {
            DriverViewModel driverView = new DriverViewModel();
            var actualResult = await _adminService.DeleteDriver(driverView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteDriver_NullModel()
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
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetCarrierByCode("0123");
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetCarrierByCode_NoCode()
        {
            var actualResult = await _adminService.GetCarrierByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End GetCarrierByCode test cases

        // ---------------------------------------------> Begin GetCustomerByCode test cases

        [TestMethod]
        public async Task TestMethod_GetCustomerByCode()
        {
            CustomerRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetCustomerByCode("0123");
            CustomerRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetCustomerByCode_NoCode()
        {
            var actualResult = await _adminService.GetCustomerByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End GetCustomerByCode test cases

        // ---------------------------------------------> Begin GetDriverByCode test cases

        [TestMethod]
        public async Task TestMethod_GetDriverByCode()
        {
            DriverRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetDriverByCode("0123");
            DriverRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetDriverByCode_NoCode()
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
        public async Task TestMethod_SearchCarrier_NoCode()
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
        public async Task TestMethod_SearchCustomer_NoCode()
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
        public async Task TestMethod_SearchDriver_NoCode()
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

        public async Task TestMethod_UpdateCarrier_NullModel()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel() { code = "0123" };
            var actualResult = await _adminService.UpdateCarrier(null);
            Assert.IsNull(actualResult.responseData);
        }

        public async Task TestMethod_UpdateCarrier_NoCode()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel();
            var actualResult = await _adminService.UpdateCarrier(carrierView);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End UpdateCarrier test cases

        // ---------------------------------------------> Begin UpdateCustomer test case

        [TestMethod]
        public async Task TestMethod_UpdateCustomer()
        {
            CustomerViewModel customerView = new CustomerViewModel() { code = "0123" };
            var actualResult = await _adminService.UpdateCustomer(customerView);
            Assert.IsNotNull(actualResult.responseData);
        }

        public async Task TestMethod_UpdateCustomer_NullModel()
        {
            var actualResult = await _adminService.UpdateCustomer(null);
            Assert.IsNull(actualResult.responseData);
        }

        public async Task TestMethod_UpdateCustomer_NoCode()
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
            DriverViewModel driverView = new DriverViewModel() { code = "0123", carrierVendor = new CarrierVendorViewModel { code = "0123" } };
            var actualResult = await _adminService.UpdateDriver(driverView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateDriver_NoCode()
        {
            DriverViewModel driverView = new DriverViewModel();
            var actualResult = await _adminService.UpdateDriver(driverView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateDriver_NullModel()
        {
            var actualResult = await _adminService.UpdateDriver(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End UpdateDriver test cases

        // ---------------------------------------------> Begin GetAllMaterial test cases

        [TestMethod]
        public async Task TestMethod_GetAllMaterial()
        {
            var actualResult = await _adminService.GetAllMaterial();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End GetAllMaterial test cases

        // ---------------------------------------------> Begin SearchMaterial test cases

        [TestMethod]
        public async Task TestMethod_SearchMaterial()
        {
            var actualResult = await _adminService.SearchMaterial("0123");
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchMaterial_NoCode()
        {
            var actualResult = await _adminService.SearchMaterial(null);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End SearchMaterial test cases

        // ---------------------------------------------> Begin GetMaterialByCode test cases

        [TestMethod]
        public async Task TestMethod_GetMaterialByCode()
        {
            MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetMaterialByCode("0123");
            MaterialRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetMaterialByCode_NoCode()
        {
            var actualResult = await _adminService.GetMaterialByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End SearchMaterial test cases

        // ---------------------------------------------> Begin CreateNewMaterial test cases

        [TestMethod]
        public async Task TestMethod_CreateNewMaterial()
        {
            MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialViewModel viewModel = new MaterialViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewMaterial(viewModel);
            MaterialRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewMaterial_NoCode_ShouldFail()
        {
            // Code field must have been validated before any operation
            // Because validation is not handled, this test case should fail
            MaterialViewModel viewModel = new MaterialViewModel();
            var actualResult = await _adminService.CreateNewMaterial(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewMaterial_NoModel()
        {
            var actualResult = await _adminService.CreateNewMaterial(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewMaterial test cases

        // ---------------------------------------------> Begin UpdateMaterial test cases

        [TestMethod]
        public async Task TestMethod_UpdateMaterial()
        {
            MaterialViewModel viewModel = new MaterialViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateMaterial(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateMaterial_NoCode()
        {
            MaterialViewModel viewModel = new MaterialViewModel();
            var actualResult = await _adminService.UpdateMaterial(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateMaterial_NullModel()
        {
            var actualResult = await _adminService.UpdateMaterial(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        // ---------------------------------------------> End UpdateMaterial test cases

        // ---------------------------------------------> Begin DeleteMaterial test cases

        [TestMethod]
        public async Task TestMethod_DeleteMaterial()
        {
            MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialRepositoryTest.FLAG_DELETE = 0;
            MaterialViewModel viewModel = new MaterialViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteMaterial(viewModel);
            MaterialRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteMaterial_NoID()
        {
            MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialRepositoryTest.FLAG_DELETE = 0;
            MaterialViewModel viewModel = new MaterialViewModel();
            var actualResult = await _adminService.DeleteMaterial(viewModel);
            MaterialRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteMaterial_NullModel()
        {
            var actualResult = await _adminService.DeleteMaterial(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End UpdateMaterial test cases

        // ---------------------------------------------> Begin GetAllUnitType test cases

        [TestMethod]
        public async Task TestMethod_GetAllUnitType()
        {
            var actualResult = await _adminService.GetAllUnitType();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End GetAllUnitType test cases

        // ---------------------------------------------> Begin SearchUnitType test cases

        [TestMethod]
        public async Task TestMethod_SearchUnitType()
        {
            var actualResult = await _adminService.SearchUnitType("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchUnitType_NoCode()
        {
            var actualResult = await _adminService.SearchUnitType(null);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End SearchUnitType test cases

        // ---------------------------------------------> Begin GetUnitTypeByCode test cases

        [TestMethod]
        public async Task TestMethod_GetUnitTypeByCode()
        {
            UnitTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetUnitTypeByCode("0123");
            UnitTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetUnitTypeByCode_NoCode()
        {
            var actualResult = await _adminService.GetUnitTypeByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End GetUnitTypeByCode test cases

        // ---------------------------------------------> Begin CreateNewUnitType test cases

        [TestMethod]
        public async Task TestMethod_CreateNewUnitType()
        {
            UnitTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            UnitTypeViewModel viewModel = new UnitTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewUnitType(viewModel);
            UnitTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewUnitType_NoCode_ShouldFail()
        {
            // Code field must have been validated before any operation
            // Because validation is not handled, this test case should fail
            UnitTypeViewModel viewModel = new UnitTypeViewModel();
            var actualResult = await _adminService.CreateNewUnitType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewUnitType_NullModel()
        {
            var actualResult = await _adminService.CreateNewUnitType(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewUnitType test cases

        [TestMethod]
        public async Task TestMethod_UpdateUnitType()
        {
            UnitTypeViewModel viewModel = new UnitTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateUnitType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateUnitType_NoCode()
        {
            UnitTypeViewModel viewModel = new UnitTypeViewModel();
            var actualResult = await _adminService.UpdateUnitType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateUnitType_NullModel()
        {
            var actualResult = await _adminService.UpdateUnitType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUnitType()
        {
            UnitTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            UnitTypeRepositoryTest.FLAG_DELETE = 0;
            UnitTypeViewModel viewModel = new UnitTypeViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteUnitType(viewModel);
            UnitTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUnitType_NoID()
        {
            UnitTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            UnitTypeRepositoryTest.FLAG_DELETE = 0;
            UnitTypeViewModel viewModel = new UnitTypeViewModel();
            var actualResult = await _adminService.DeleteUnitType(viewModel);
            UnitTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUnitType_WrongID()
        {
            UnitTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            UnitTypeRepositoryTest.FLAG_DELETE = 0;
            UnitTypeViewModel viewModel = new UnitTypeViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeleteUnitType(viewModel);
            UnitTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUnitType_NoModel()
        {
            var actualResult = await _adminService.DeleteUnitType(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllTruck()
        {
            var actualResult = await _adminService.GetAllTruck();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchTruck()
        {
            var actualResult = await _adminService.SearchTruck("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchTruck_NoCode()
        {
            var actualResult = await _adminService.SearchTruck(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetTruckByCode()
        {
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetTruckByCode("0123");
            TruckRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetTruckByCode_NoCode()
        {
            var actualResult = await _adminService.GetTruckByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewTruck()
        {
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckViewModel viewModel = new TruckViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewTruck(viewModel);
            TruckRepositoryTest.FLAG_GET_ASYNC = 0;
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 0;
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewTruck_NoCode()
        {
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckViewModel viewModel = new TruckViewModel();
            var actualResult = await _adminService.CreateNewTruck(viewModel);
            TruckRepositoryTest.FLAG_GET_ASYNC = 0;
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 0;
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewTruck_NoModel()
        {
            var actualResult = await _adminService.CreateNewTruck(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateTruck()
        {
            TruckViewModel viewModel = new TruckViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateTruck(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateTruck_NoCode()
        {
            TruckViewModel viewModel = new TruckViewModel();
            var actualResult = await _adminService.UpdateTruck(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateTruck_NoModel()
        {
            var actualResult = await _adminService.UpdateTruck(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruck()
        {
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckRepositoryTest.FLAG_DELETE = 0;
            TruckViewModel viewModel = new TruckViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteTruck(viewModel);
            TruckRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruck_NoID()
        {
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckRepositoryTest.FLAG_DELETE = 1;
            TruckViewModel viewModel = new TruckViewModel();
            var actualResult = await _adminService.DeleteTruck(viewModel);
            TruckRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruck_WrongID()
        {
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckRepositoryTest.FLAG_DELETE = 2;
            TruckViewModel viewModel = new TruckViewModel() { ID = 0 };
            var actualResult = await _adminService.DeleteTruck(viewModel);
            TruckRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruck_NoModel()
        {
            var actualResult = await _adminService.DeleteTruck(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllTruckType()
        {
            var actualResult = await _adminService.GetAllTruckType();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchTruckType()
        {
            var actualResult = await _adminService.SearchTruckType("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchTruckType_NoCode()
        {
            var actualResult = await _adminService.SearchTruckType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetTruckTypeByCode()
        {
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetTruckTypeByCode("0123");
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetTruckTypeByCode_NoCode()
        {
            var actualResult = await _adminService.GetTruckTypeByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewTruckType()
        {
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckTypeViewModel viewModel = new TruckTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewTruckType(viewModel);
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewTruckType_NoCode_ShouldFail()
        {
            TruckTypeViewModel viewModel = new TruckTypeViewModel();
            var actualResult = await _adminService.CreateNewTruckType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewTruckType_NoModel()
        {
            var actualResult = await _adminService.CreateNewTruckType(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateTruckType()
        {
            TruckTypeViewModel viewModel = new TruckTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateTruckType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateTruckType_NoCode()
        {
            TruckTypeViewModel viewModel = new TruckTypeViewModel();
            var actualResult = await _adminService.UpdateTruckType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateTruckType_NoModel()
        {
            var actualResult = await _adminService.UpdateTruckType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruckType()
        {
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckTypeRepositoryTest.FLAG_DELETE = 0;
            TruckTypeViewModel viewModel = new TruckTypeViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteTruckType(viewModel);
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruckType_NoID()
        {
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckTypeRepositoryTest.FLAG_DELETE = 0;
            TruckTypeViewModel viewModel = new TruckTypeViewModel();
            var actualResult = await _adminService.DeleteTruckType(viewModel);
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruckType_WrongID()
        {
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckTypeRepositoryTest.FLAG_DELETE = 0;
            TruckTypeViewModel viewModel = new TruckTypeViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeleteTruckType(viewModel);
            TruckTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruckType_NoModel()
        {
            var actualResult = await _adminService.DeleteTruckType(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllLoadingType()
        {
            var actualResult = await _adminService.GetAllLoadingType();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchLoadingType()
        {
            var actualResult = await _adminService.SearchLoadingType("0123");
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchLoadingType_NoCode()
        {
            var actualResult = await _adminService.SearchLoadingType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetLoadingTypeByCode()
        {
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetLoadingTypeByCode("0123");
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetLoadingTypeByCode_NoCode()
        {
            var actualResult = await _adminService.GetLoadingTypeByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLoadingType()
        {
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewLoadingType(viewModel);
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLoadingType_NoCode_ShouldFail()
        {
            // Code field must have been validated before any operation
            // Because validation is not handled, this test case should fail
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel();
            var actualResult = await _adminService.CreateNewLoadingType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLoadingType_NoModel()
        {
            var actualResult = await _adminService.CreateNewLoadingType(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLoadingType()
        {
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateLoadingType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLoadingType_NoCode()
        {
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel();
            var actualResult = await _adminService.UpdateLoadingType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLoadingType_NoModel()
        {
            var actualResult = await _adminService.UpdateLoadingType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingType()
        {
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            LoadingTypeRepositoryTest.FLAG_DELETE = 0;
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteLoadingType(viewModel);
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingType_NoID()
        {
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            LoadingTypeRepositoryTest.FLAG_DELETE = 1;
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel();
            var actualResult = await _adminService.DeleteLoadingType(viewModel);
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingType_WrongID()
        {
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            LoadingTypeRepositoryTest.FLAG_DELETE = 2;
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeleteLoadingType(viewModel);
            LoadingTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingType_NoModel()
        {
            var actualResult = await _adminService.DeleteLoadingType(null);
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetAllEmployee()
        {
            var actualResult = await _adminService.GetAllEmployee();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchEmployee()
        {
            var actualResult = await _adminService.SearchEmployee("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchEmployee_NoCode()
        {
            var actualResult = await _adminService.SearchEmployee(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetEmployeeByCode()
        {
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetEmployeeByCode("0123");
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetEmployeeByCode_NoCode()
        {
            var actualResult = await _adminService.GetEmployeeByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployee()
        {
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 1;
            EmployeeViewModel viewModel = new EmployeeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewEmployee(viewModel);
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployee_NoCode()
        {
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 1;
            EmployeeViewModel viewModel = new EmployeeViewModel();
            var actualResult = await _adminService.CreateNewEmployee(viewModel);
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployee_NoModel()
        {
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.CreateNewEmployee(null);
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployee()
        {
            EmployeeViewModel viewModel = new EmployeeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateEmployee(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployee_NoCode()
        {
            EmployeeViewModel viewModel = new EmployeeViewModel();
            var actualResult = await _adminService.UpdateEmployee(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployee_NoModel()
        {
            var actualResult = await _adminService.UpdateEmployee(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployee()
        {
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 1;
            EmployeeRepositoryTest.FLAG_DELETE = 0;
            EmployeeViewModel viewModel = new EmployeeViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteEmployee(viewModel);
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployee_NoID()
        {
            EmployeeRepositoryTest.FLAG_DELETE = 1;
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 1;
            EmployeeViewModel viewModel = new EmployeeViewModel();
            var actualResult = await _adminService.DeleteEmployee(viewModel);
            EmployeeRepositoryTest.FLAG_DELETE = 0;
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployee_WrongID()
        {
            EmployeeRepositoryTest.FLAG_DELETE = 2;
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 1;
            EmployeeViewModel viewModel = new EmployeeViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeleteEmployee(viewModel);
            EmployeeRepositoryTest.FLAG_DELETE = 0;
            EmployeeRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployee_NoModel()
        {
            var actualResult = await _adminService.DeleteEmployee(null);
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetAllEmployeeGroup()
        {
            var actualResult = await _adminService.GetAllEmployeeGroup();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchEmployeeGroup()
        {
            var actualResult = await _adminService.SearchEmployeeGroup("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchEmployeeGroup_NoCode()
        {
            var actualResult = await _adminService.SearchEmployeeGroup(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetEmployeeGroupByCode()
        {
            EmployeeGroupRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetEmployeeGroupByCode("0123");
            EmployeeGroupRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetEmployeeGroupByCode_NoCode()
        {
            var actualResult = await _adminService.GetEmployeeGroupByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployeeGroup()
        {
            EmployeeGroupRepositoryTest.FLAG_GET_ASYNC = 1;
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewEmployeeGroup(viewModel);
            EmployeeGroupRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployeeGroup_NoCode()
        {
            EmployeeGroupRepositoryTest.FLAG_GET_ASYNC = 1;
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel();
            var actualResult = await _adminService.CreateNewEmployeeGroup(viewModel);
            EmployeeGroupRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployeeGroup_NoModel()
        {
            var actualResult = await _adminService.CreateNewEmployeeGroup(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployeeGroup()
        {
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateEmployeeGroup(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployeeGroup_NoCode()
        {
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel();
            var actualResult = await _adminService.UpdateEmployeeGroup(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployeeGroup_NoModel()
        {
            var actualResult = await _adminService.UpdateEmployeeGroup(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeGroup()
        {
            EmployeeGroupRepositoryTest.FLAG_GET_ASYNC = 1;
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteEmployeeGroup(viewModel);
            EmployeeGroupRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeGroup_NoID()
        {
            EmployeeGroupRepositoryTest.FLAG_DELETE = 1;
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel();
            var actualResult = await _adminService.DeleteEmployeeGroup(viewModel);
            EmployeeGroupRepositoryTest.FLAG_DELETE = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeGroup_WrongID()
        {
            EmployeeGroupRepositoryTest.FLAG_DELETE = 2;
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeleteEmployeeGroup(viewModel);
            EmployeeGroupRepositoryTest.FLAG_DELETE = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeGroup_NoModel()
        {
            var actualResult = await _adminService.DeleteEmployeeGroup(null);
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetAllUser()
        {
            var actualResult = await _adminService.GetAllUser();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchUser()
        {
            var actualResult = await _adminService.SearchUser("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchUser_NoCode()
        {
            var actualResult = await _adminService.SearchUser(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetUserByCode()
        {
            UserRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetUserByCode("0123");
            UserRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetUserByCode_NoCode()
        {
            var actualResult = await _adminService.GetUserByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewUser()
        {
            UserRepositoryTest.FLAG_GET_ASYNC = 1;
            UserViewModel viewModel = new UserViewModel
            {
                Code = "0123"
            };
            var actualResult = await _adminService.CreateNewUser(viewModel);
            UserRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewUser_NoCode()
        {
            UserRepositoryTest.FLAG_GET_ASYNC = 1;
            UserViewModel viewModel = new UserViewModel();
            var actualResult = await _adminService.CreateNewUser(viewModel);
            UserRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewUser_NoModel()
        {
            var actualResult = await _adminService.CreateNewUser(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateUser()
        {
            UserViewModel viewModel = new UserViewModel
            {
                Code = "0123"
            };
            var actualResult = await _adminService.UpdateUser(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateUser_NoCode()
        {
            UserViewModel viewModel = new UserViewModel();
            var actualResult = await _adminService.UpdateUser(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateUser_NoModel()
        {
            var actualResult = await _adminService.UpdateUser(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUser()
        {
            UserRepositoryTest.FLAG_GET_ASYNC = 1;
            UserRepositoryTest.FLAG_DELETE = 0;
            UserViewModel viewModel = new UserViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteUser(viewModel);
            UserRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUser_NoID()
        {
            UserRepositoryTest.FLAG_GET_ASYNC = 1;
            UserRepositoryTest.FLAG_DELETE = 0;
            UserViewModel viewModel = new UserViewModel();
            var actualResult = await _adminService.DeleteUser(viewModel);
            UserRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUser_WrongID()
        {
            UserRepositoryTest.FLAG_GET_ASYNC = 1;
            UserRepositoryTest.FLAG_DELETE = 0;
            UserViewModel viewModel = new UserViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeleteUser(viewModel);
            UserRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUser_NoModel()
        {
            var actualResult = await _adminService.DeleteUser(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllEmployeeRole()
        {
            var actualResult = await _adminService.GetAllEmployeeRole();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchEmployeeRole()
        {
            var actualResult = await _adminService.SearchEmployeeRole("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchEmployeeRole_NoCode()
        {
            var actualResult = await _adminService.SearchEmployeeRole(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetEmployeeRoleByCode()
        {
            EmployeeRoleRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetEmployeeRoleByCode("0123");
            EmployeeRoleRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetEmployeeRoleByCode_NoCode()
        {
            var actualResult = await _adminService.GetEmployeeRoleByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployeeRole()
        {
            EmployeeRoleRepositoryTest.FLAG_GET_ASYNC = 1;
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel
            {
                Code = "0123"
            };
            var actualResult = await _adminService.CreateNewEmployeeRole(viewModel);
            EmployeeRoleRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployeeRole_NoCode()
        {
            EmployeeRoleRepositoryTest.FLAG_GET_ASYNC = 1;
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel();
            var actualResult = await _adminService.CreateNewEmployeeRole(viewModel);
            EmployeeRoleRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployeeRole_NoModel()
        {
            var actualResult = await _adminService.CreateNewEmployeeRole(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployeeRole()
        {
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel
            {
                Code = "0123"
            };
            var actualResult = await _adminService.UpdateEmployeeRole(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployeeRole_NoCode()
        {
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel();
            var actualResult = await _adminService.UpdateEmployeeRole(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployeeRole_NoModel()
        {
            var actualResult = await _adminService.UpdateEmployeeRole(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeRole()
        {
            EmployeeRoleRepositoryTest.FLAG_GET_ASYNC = 1;
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteEmployeeRole(viewModel);
            EmployeeRoleRepositoryTest.FLAG_GET_ASYNC = 1;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeRole_NoID()
        {
            EmployeeRoleRepositoryTest.FLAG_DELETE = 1;
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel();
            var actualResult = await _adminService.DeleteEmployeeRole(viewModel);
            EmployeeRoleRepositoryTest.FLAG_DELETE = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeRole_WrongID()
        {
            EmployeeRoleRepositoryTest.FLAG_DELETE = 2;
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeleteEmployeeRole(viewModel);
            EmployeeRoleRepositoryTest.FLAG_DELETE = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeRole_NoModel()
        {
            var actualResult = await _adminService.DeleteEmployeeRole(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllPlant()
        {
            var actualResult = await _adminService.GetAllPlant();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchPlant()
        {
            var actualResult = await _adminService.SearchPlant("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchPlant_NoCode()
        {
            var actualResult = await _adminService.SearchPlant(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetPlantByCode()
        {
            PlantRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetPlantByCode("0123");
            PlantRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetPlantByCode_NoCode()
        {
            var actualResult = await _adminService.GetPlantByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewPlant()
        {
            CompanyRepositoryTest.FLAG_GET_ASYNC = 1;
            PlantRepositoryTest.FLAG_GET_ASYNC = 1;
            PlantViewModel viewModel = new PlantViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewPlant(viewModel);
            PlantRepositoryTest.FLAG_GET_ASYNC = 0;
            CompanyRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewPlant_NoCode()
        {
            CompanyRepositoryTest.FLAG_GET_ASYNC = 1;
            PlantRepositoryTest.FLAG_GET_ASYNC = 1;
            PlantViewModel viewModel = new PlantViewModel();
            var actualResult = await _adminService.CreateNewPlant(viewModel);
            PlantRepositoryTest.FLAG_GET_ASYNC = 0;
            CompanyRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewPlant_NoModel()
        {
            var actualResult = await _adminService.CreateNewPlant(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdatePlant()
        {
            PlantViewModel viewModel = new PlantViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdatePlant(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdatePlant_NoCode()
        {
            PlantViewModel viewModel = new PlantViewModel();
            var actualResult = await _adminService.UpdatePlant(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdatePlant_NoModel()
        {
            var actualResult = await _adminService.UpdatePlant(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeletePlant()
        {
            PlantRepositoryTest.FLAG_GET_ASYNC = 1;
            PlantRepositoryTest.FLAG_DELETE = 0;
            PlantViewModel viewModel = new PlantViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeletePlant(viewModel);
            PlantRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeletePlant_NoID()
        {
            PlantRepositoryTest.FLAG_GET_ASYNC = 1;
            PlantRepositoryTest.FLAG_DELETE = 1;
            PlantViewModel viewModel = new PlantViewModel();
            var actualResult = await _adminService.DeletePlant(viewModel);
            PlantRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeletePlant_WrongID()
        {
            PlantRepositoryTest.FLAG_GET_ASYNC = 1;
            PlantRepositoryTest.FLAG_DELETE = 2;
            PlantViewModel viewModel = new PlantViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeletePlant(viewModel);
            PlantRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeletePlant_NoModel()
        {
            var actualResult = await _adminService.DeletePlant(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllCompany()
        {
            var actualResult = await _adminService.GetAllCompany();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchCompany()
        {
            var actualResult = await _adminService.SearchCompany("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchCompany_NoCode()
        {
            var actualResult = await _adminService.SearchCompany(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetCompanyByCode()
        {
            CompanyRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetCompanyByCode("0123");
            CompanyRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetCompanyByCode_NoCode()
        {
            var actualResult = await _adminService.GetCompanyByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCompany()
        {
            CompanyRepositoryTest.FLAG_GET_ASYNC = 1;
            CompanyViewModel viewModel = new CompanyViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewCompany(viewModel);
            CompanyRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCompany_NoCode()
        {
            CompanyRepositoryTest.FLAG_GET_ASYNC = 1;
            CompanyViewModel viewModel = new CompanyViewModel();
            var actualResult = await _adminService.CreateNewCompany(viewModel);
            Assert.IsNotNull(actualResult.responseData);
            CompanyRepositoryTest.FLAG_GET_ASYNC = 0;
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCompany_NoModel()
        {
            var actualResult = await _adminService.CreateNewCompany(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateCompany()
        {
            CompanyViewModel viewModel = new CompanyViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateCompany(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateCompany_NoCode()
        {
            CompanyViewModel viewModel = new CompanyViewModel();
            var actualResult = await _adminService.UpdateCompany(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateCompany_NoModel()
        {
            var actualResult = await _adminService.UpdateCompany(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCompany()
        {
            CompanyRepositoryTest.FLAG_GET_ASYNC = 1;
            CompanyViewModel viewModel = new CompanyViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteCompany(viewModel);
            CompanyRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCompany_NoID()
        {
            CompanyRepositoryTest.FLAG_DELETE = 1;
            CompanyViewModel viewModel = new CompanyViewModel();
            var actualResult = await _adminService.DeleteCompany(viewModel);
            CompanyRepositoryTest.FLAG_DELETE = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCompany_WrongID()
        {
            CompanyRepositoryTest.FLAG_DELETE = 2;
            CompanyViewModel viewModel = new CompanyViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeleteCompany(viewModel);
            CompanyRepositoryTest.FLAG_DELETE = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCompany_NoModel()
        {
            var actualResult = await _adminService.DeleteCompany(null);
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetAllWarehouse()
        {
            var actualResult = await _adminService.GetAllWarehouse();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchWarehouse()
        {
            var actualResult = await _adminService.SearchWarehouse("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchWarehouse_NoCode()
        {
            var actualResult = await _adminService.SearchWarehouse(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetWarehouseByCode()
        {
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetWarehouseByCode("0123");
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetWarehouseByCode_NoCode()
        {
            var actualResult = await _adminService.GetWarehouseByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewWarehouse()
        {
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 1;
            PlantRepositoryTest.FLAG_GET_ASYNC = 1;
            WarehouseViewModel viewModel = new WarehouseViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewWarehouse(viewModel);
            PlantRepositoryTest.FLAG_GET_ASYNC = 0;
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewWarehouse_NoCode()
        {
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 1;
            PlantRepositoryTest.FLAG_GET_ASYNC = 1;
            WarehouseViewModel viewModel = new WarehouseViewModel();
            var actualResult = await _adminService.CreateNewWarehouse(viewModel);
            PlantRepositoryTest.FLAG_GET_ASYNC = 0;
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewWarehouse_NoModel()
        {
            PlantRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.CreateNewWarehouse(null);
            PlantRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateWarehouse()
        {
            WarehouseViewModel viewModel = new WarehouseViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateWarehouse(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateWarehouse_NoCode()
        {
            WarehouseViewModel viewModel = new WarehouseViewModel();
            var actualResult = await _adminService.UpdateWarehouse(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateWarehouse_NoModel()
        {
            var actualResult = await _adminService.UpdateWarehouse(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteWarehouse()
        {
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 1;
            WarehouseRepositoryTest.FLAG_DELETE = 0;
            WarehouseViewModel viewModel = new WarehouseViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteWarehouse(viewModel);
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteWarehouse_NoID()
        {
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 1;
            WarehouseRepositoryTest.FLAG_DELETE = 0;
            WarehouseViewModel viewModel = new WarehouseViewModel();
            var actualResult = await _adminService.DeleteWarehouse(viewModel);
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteWarehouse_WrongID()
        {
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 1;
            WarehouseRepositoryTest.FLAG_DELETE = 0;
            WarehouseViewModel viewModel = new WarehouseViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeleteWarehouse(viewModel);
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteWarehouse_NoModel()
        {
            var actualResult = await _adminService.DeleteWarehouse(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllLoadingBay()
        {
            var actualResult = await _adminService.GetAllLoadingBay();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchLoadingBay()
        {
            var actualResult = await _adminService.SearchLoadingBay("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchLoadingBay_NoCode()
        {
            var actualResult = await _adminService.SearchLoadingBay(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetLoadingBayByCode()
        {
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetLoadingBayByCode("0123");
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetLoadingBayByCode_NoCode()
        {
            var actualResult = await _adminService.GetLoadingBayByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLoadingBay()
        {
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 1;
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 1;
            LoadingBayViewModel viewModel = new LoadingBayViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewLoadingBay(viewModel);
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 0;
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLoadingBay_NoCode()
        {
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 1;
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 1;
            LoadingBayViewModel viewModel = new LoadingBayViewModel();
            var actualResult = await _adminService.CreateNewLoadingBay(viewModel);
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 0;
            WarehouseRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLoadingBay_NoModel()
        {
            var actualResult = await _adminService.CreateNewLoadingBay(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLoadingBay()
        {
            LoadingBayViewModel viewModel = new LoadingBayViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateLoadingBay(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLoadingBay_NoCode()
        {
            LoadingBayViewModel viewModel = new LoadingBayViewModel();
            var actualResult = await _adminService.UpdateLoadingBay(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLoadingBay_NoModel()
        {
            var actualResult = await _adminService.UpdateLoadingBay(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingBay()
        {
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 1;
            LoadingBayRepositoryTest.FLAG_DELETE = 0;
            LoadingBayViewModel viewModel = new LoadingBayViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteLoadingBay(viewModel);
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingBay_NoID()
        {
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 1;
            LoadingBayRepositoryTest.FLAG_DELETE = 1;
            LoadingBayViewModel viewModel = new LoadingBayViewModel();
            var actualResult = await _adminService.DeleteLoadingBay(viewModel);
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingBay_WrongID()
        {
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 1;
            LoadingBayRepositoryTest.FLAG_DELETE = 2;
            LoadingBayViewModel viewModel = new LoadingBayViewModel() { ID = 0 };
            var actualResult = await _adminService.DeleteLoadingBay(viewModel);
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingBay_NoModel()
        {
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.DeleteLoadingBay(null);
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetAllLane()
        {
            var actualResult = await _adminService.GetAllLane();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchLane()
        {
            var actualResult = await _adminService.SearchLane("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchLane_NoCode()
        {
            var actualResult = await _adminService.SearchLane(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetLaneByCode()
        {
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _adminService.GetLaneByCode("0123");
            LaneRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetLaneByCode_NoCode()
        {
            var actualResult = await _adminService.GetLaneByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLane()
        {
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            LaneViewModel viewModel = new LaneViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewLane(viewModel);
            LaneRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLane_NoCode()
        {
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            LaneViewModel viewModel = new LaneViewModel();
            var actualResult = await _adminService.CreateNewLane(viewModel);
            LaneRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLane_NoModel()
        {
            var actualResult = await _adminService.CreateNewLane(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLane()
        {
            LaneViewModel viewModel = new LaneViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateLane(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLane_NoCode()
        {
            LaneViewModel viewModel = new LaneViewModel();
            var actualResult = await _adminService.UpdateLane(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLane_NoModel()
        {
            var actualResult = await _adminService.UpdateLane(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLane()
        {
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            LaneViewModel viewModel = new LaneViewModel
            {
                ID = 1
            };
            var actualResult = await _adminService.DeleteLane(viewModel);
            LaneRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLane_NoID()
        {
            LaneRepositoryTest.FLAG_DELETE = 1;
            LaneViewModel viewModel = new LaneViewModel();
            var actualResult = await _adminService.DeleteLane(viewModel);
            LaneRepositoryTest.FLAG_DELETE = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLane_WrongID()
        {
            LaneRepositoryTest.FLAG_DELETE = 2;
            LaneViewModel viewModel = new LaneViewModel()
            {
                ID = 0
            };
            var actualResult = await _adminService.DeleteLane(viewModel);
            LaneRepositoryTest.FLAG_DELETE = 0;
            Assert.IsNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLane_NoModel()
        {
            var actualResult = await _adminService.DeleteLane(null);
            Assert.IsNull(actualResult.responseDatas);
        }
    }
}
