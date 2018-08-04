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
    public class WarehouseServiceTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGatePassRepository _gatePassRepository;
        private readonly ILaneRepository _laneRepository;
        private readonly IQueueListRepository _queueListRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
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
        private readonly IOrderMaterialRepository _orderMaterialRepository;
        private readonly IWeightRecordRepository _weightRecordRepository;

        private readonly IWarehouseService _warehouseService;
        private readonly IAuthService _authService;
        private readonly IAdminService _adminService;

        public WarehouseServiceTest()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Configure();

            _unitOfWork = new UnitOfWorkTest();
            _queueListRepository = new QueueListRepositoryTest();
            _gatePassRepository = new GatePassRepositoryTest();
            _laneRepository = new LaneRepositoryTest();
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
            _badgeReaderRepository = new BadgeReaderRepositoryTest();
            _orderMaterialRepository = new OrderMaterialRepositoryTest();
            _weightRecordRepository = new WeighRecordRepositoryTest();
            _tokenRepository = new TokenRepositoryTest();

            _authService = new AuthService(_unitOfWork, _tokenRepository, _userRepository, _employeeRepository, _adminService);
            _adminService = new AdminService(
                _unitOfWork, _customerRepository, _driverRepository, _carrierRepository, _userRepository,
                _materialRepository, _unittypeRepository, _truckRepository, _truckTypeRepository, _loadingTypeRepository,
                _employeeRepository, _employeeGroupRepository, _employeeRoleRepository, _plantRepository,
                _companyRepository, _warehouseRepository, _loadingBayRepository, _laneRepository, _rdifCardRepository,
                _cameraRepository, _constrainRepository, _doRepository, _customerWarehouseRepository, _saleOrderRepository,
                _orderRepository, _weighBridgeRepository, _printHeaderRepository, _userPasswordRepository, _systemFunctionRepository,
                _employeeGroup_SystemFunctionRepository, _userPCRepository, _badgeReaderRepository, _weightRecordRepository);
            _warehouseService = new WarehouseService(_gatePassRepository, _unitOfWork, _laneRepository, _queueListRepository, _authService,
                _orderMaterialRepository, _orderRepository);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckIn()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckIn(viewModel);
            Assert.IsTrue(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckIn_NoGatePassID()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckIn(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckIn_NoEmployeeId()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckIn(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckIn_NoRFID()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckIn(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckIn_NoModel()
        {
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckIn(null);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckOut()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel();
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckOut(null);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckOut_NoGatePassID()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckOut(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckOut_NoEmployeeId()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckOut(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckOut_NoRFID()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckOut(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckOut_NoModel()
        {
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckOut(null);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTheoryWeighValue()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel();
            var actualResult = await _warehouseService.UpdateQCWeighValue(null);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTheoryWeighValue_NoGatePassID()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateQCWeighValue(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTheoryWeighValue_NoEmployeeId()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateQCWeighValue(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTheoryWeighValue_NoRFID()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateQCWeighValue(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTheoryWeighValue_NoModel()
        {
            var actualResult = await _warehouseService.UpdateQCWeighValue(null);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheck()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel();
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheck(null);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheck_NoGatePassID()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheck(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheck_NoEmployeeId()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheck(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheck_NoRFID()
        {
            WarehouseCheckModel viewModel = new WarehouseCheckModel
            {
                gatePassCode = "0123",
                QCGrossWeight = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheck(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheck_NoModel()
        {
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheck(null);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task GetLaneForWarehouseManagement()
        {
            string code = "";
            var actualResult = await _warehouseService.GetLaneForWarehouseManagement(code);
        }

        [TestMethod]
        public async Task GetLaneForWarehouseManagement_NoCode()
        {
            var actualResult = await _warehouseService.GetLaneForWarehouseManagement(null);
        }
    }
}
