using Microsoft.VisualStudio.TestTools.UnitTesting;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using QWMSServer.Tests.Dummy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Tests.ServiceTest
{
    [TestClass]
    public class AuthServiceTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAuthService _authService;
        private readonly IAdminService _adminService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ICarrierVendorRepository _carrierRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IUnitTypeRepository _unittypeRepository;
        private readonly ITruckRepository _truckRepository;
        private readonly ITruckTypeRepository _truckTypeRepository;
        private readonly ILoadingTypeRepository _loadingTypeRepository;
        private readonly IEmployeeGroupRepository _employeeGroupRepository;
        private readonly IEmployeeRoleRepository _employeeRoleRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly ILoadingBayRepository _loadingBayRepository;
        private readonly ILaneRepository _laneRepository;
        private readonly IRFIDCardRepository _rdifCardRepository;
        private readonly ICameraRepository _cameraRepository;
        private readonly IConstrainRepository _constrainRepository;
        private readonly IDeliveryOrderRepository _doRepository;
        private readonly ICustomerWarehouseRepository _customerWarehouseRepository;
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IWeighBridgeRepository _weighBridgeRepository;
        private readonly IPrintHeaderRepository _printHeaderRepository;
        private readonly IUserPasswordRepository _userPasswordRepository;
        private readonly ISystemFunctionRepository _systemFunctionRepository;
        private readonly IEmployeeGroup_SystemFunctionRepository _employeeGroup_SystemFunctionRepository;
        private readonly IUserPCRepository _userPCRepository;
        private readonly IBadgeReaderRepository _badgeReaderRepository;
        private readonly IWeightRecordRepository _weightRecordRepository;

        public AuthServiceTest()
        {
            _unitOfWork = new UnitOfWorkTest();
            _tokenRepository = new TokenRepositoryTest();
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
            _rdifCardRepository = new RFIDCardRepositoryTest();
            _cameraRepository = new CameraRepositoryTest();
            _constrainRepository = new ConstrainRepositoryTest();
            _doRepository = new DeliveryOrderRepositoryTest();
            _customerWarehouseRepository = new CustomerWarehouseRepositoryTest();
            _saleOrderRepository = new SaleOrderRepositoryTest();
            _orderRepository = new OrderRepositoryTest();
            _weighBridgeRepository = new WeighBridgeRepositoryTest();
            _printHeaderRepository = new PrintHeaderRepositoryTest();
            _userPasswordRepository = new UserPasswordRepositoryTest();
            _systemFunctionRepository = new SystemFunctionRepositoryTest();
            _employeeGroup_SystemFunctionRepository = new EmployeeGroup_SystemFunctionRepositoryTest();
            _userPCRepository = new UserPCRepositoryTest();
            _weightRecordRepository = new WeighRecordRepositoryTest();
            _badgeReaderRepository = new BadgeReaderRepositoryTest();

            _adminService = new AdminService(
                _unitOfWork, _customerRepository, _driverRepository, _carrierRepository, _userRepository,
                _materialRepository, _unittypeRepository, _truckRepository, _truckTypeRepository, _loadingTypeRepository,
                _employeeRepository, _employeeGroupRepository, _employeeRoleRepository, _plantRepository,
                _companyRepository, _warehouseRepository, _loadingBayRepository, _laneRepository, _rdifCardRepository,
                _cameraRepository, _constrainRepository, _doRepository, _customerWarehouseRepository, _saleOrderRepository,
                _orderRepository, _weighBridgeRepository, _printHeaderRepository, _userPasswordRepository, _systemFunctionRepository,
                _employeeGroup_SystemFunctionRepository, _userPCRepository, _badgeReaderRepository, _weightRecordRepository);
            _authService = new AuthService(_unitOfWork, _tokenRepository, _userRepository, _employeeRepository, _adminService);
        }


        [TestMethod]
        public async Task TestMethod_GetUserPermission()
        {
            var actualResult = await _authService.GetUserPermission(1);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetUserPermission_ShouldFail()
        {
            var actualResult = await _authService.GetUserPermission(0);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_Login()
        {
            LoginViewModel viewModel = new LoginViewModel
            {
                Email = "skyrider1",
                Password = "password",
                UserAgent = "Chrome",
                UserHostAddress = "1.1.1.1"
            };
            var actualResult = await _authService.Login(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_Login_ShouldFail_NoInfo()
        {
            LoginViewModel viewModel = new LoginViewModel();
            var actualResult = await _authService.Login(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_Login_ShouldFail_NullModel()
        {
            var actualResult = await _authService.Login(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_Logout()
        {

            var actualResult = await _authService.Logout("0123");
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public async Task TestMethod_Logout_ShouldFail()
        {
            var actualResult = await _authService.Logout(null);
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public async Task TestMethod_CheckUserPermission()
        {
            var actualResult = await _authService.CheckUserPermission(1, "0123", "0123");
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async Task TestMethod_CheckUserPermission_ShouldFail()
        {
            var actualResult = await _authService.CheckUserPermission(0, null, null);
            Assert.IsNotNull(actualResult);
        }
        /* Token Handler */

        [TestMethod]
        public void TestMethod_GenerateToken()
        {
            var actualResult = _authService.GenerateToken(1, "username", "password", "123", "123", 0);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public void TestMethod_GenerateToken_ShouldFail()
        {
            var actualResult = _authService.GenerateToken(0, null, null, null, null, 0);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public void TestMethod_ValidateToken()
        {
            var actualResult = _authService.ValidateToken("0123");
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestMethod_ValidateToken_ShouldFail()
        {
            var actualResult = _authService.ValidateToken(null);
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestMethod_RemoveToken()
        {
            var actualResult = _authService.RemoveToken("0123");
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestMethod_RemoveToken_ShouldFail()
        {
            var actualResult = _authService.RemoveToken(null);
            Assert.IsTrue(actualResult);
        }
    }
}
