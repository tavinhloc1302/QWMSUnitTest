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
        private readonly IAuthService _authService;
        private readonly IWarehouseService _warehouseService;

        public WarehouseServiceTest()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Configure();
            _unitOfWork = new UnitOfWorkTest();
            _queueListRepository = new QueueListRepositoryTest();
            _gatePassRepository = new GatePassRepositoryTest();
            _laneRepository = new LaneRepositoryTest();

            _warehouseService = new WarehouseService(_gatePassRepository, _unitOfWork, _laneRepository, _queueListRepository, _authService);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckIn()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                gatePassID = 1,
                employeeID = 1,
                employeeRFID = "0123",
                theoryWeighValue = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckIn(viewModel);
            Assert.IsTrue(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckIn_NoGatePassID()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                employeeID = 1,
                employeeRFID = "0123",
                theoryWeighValue = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckIn(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckIn_NoEmployeeId()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                gatePassID = 1,
                employeeRFID = "0123",
                theoryWeighValue = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckIn(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckIn_NoRFID()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                gatePassID = 1,
                employeeID = 1,
                theoryWeighValue = 1
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
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel();
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckOut(null);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckOut_NoGatePassID()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                employeeID = 1,
                employeeRFID = "0123",
                theoryWeighValue = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckOut(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckOut_NoEmployeeId()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                gatePassID = 1,
                employeeRFID = "0123",
                theoryWeighValue = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheckOut(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheckOut_NoRFID()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                gatePassID = 1,
                employeeID = 1,
                theoryWeighValue = 1
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
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel();
            var actualResult = await _warehouseService.UpdateTheoryWeighValue(null);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTheoryWeighValue_NoGatePassID()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                employeeID = 1,
                employeeRFID = "0123",
                theoryWeighValue = 1
            };
            var actualResult = await _warehouseService.UpdateTheoryWeighValue(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTheoryWeighValue_NoEmployeeId()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                gatePassID = 1,
                employeeRFID = "0123",
                theoryWeighValue = 1
            };
            var actualResult = await _warehouseService.UpdateTheoryWeighValue(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTheoryWeighValue_NoRFID()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                gatePassID = 1,
                employeeID = 1,
                theoryWeighValue = 1
            };
            var actualResult = await _warehouseService.UpdateTheoryWeighValue(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTheoryWeighValue_NoModel()
        {
            var actualResult = await _warehouseService.UpdateTheoryWeighValue(null);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheck()
        {
            TheoryWeighValueModel theoryWeighValueModel = new TheoryWeighValueModel();
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheck(null);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheck_NoGatePassID()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                employeeID = 1,
                employeeRFID = "0123",
                theoryWeighValue = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheck(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheck_NoEmployeeId()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                gatePassID = 1,
                employeeRFID = "0123",
                theoryWeighValue = 1
            };
            var actualResult = await _warehouseService.UpdateTruckOnWarehouseCheck(viewModel);
            Assert.IsFalse(actualResult.booleanResponse);
        }

        [TestMethod]
        public async Task UpdateTruckOnWarehouseCheck_NoRFID()
        {
            TheoryWeighValueModel viewModel = new TheoryWeighValueModel()
            {
                gatePassID = 1,
                employeeID = 1,
                theoryWeighValue = 1
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
