using Microsoft.VisualStudio.TestTools.UnitTesting;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using QWMSServer.Tests.Dummy;
using System.Threading.Tasks;
using AutoMapper;
using System.IO;
using System;
using System.Linq;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Data.Common;

namespace QWMSServer.Tests.ServiceTest
{
    [TestClass]
    public class QueueServiceTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGatePassRepository _gatePassRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ILaneRepository _laneRepository;
        private readonly ITruckRepository _truckRepository;
        private readonly IQueueListRepository _queueListRepository;
        private readonly IRFIDCardRepository _RFIDCardRepository;
        private readonly IEmployeeRepository _employeepository;
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly IDeliveryOrderRepository _deliveryOrderRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICarrierVendorRepository _carrierVendorRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryOrderTypeRepository _deliveryOrderTypeRepository;
        private readonly ICustomerWarehouseRepository _customerWarehouseRepository;
        private readonly IOrderMaterialRepository _orderMaterialRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly QueueService _queueService;

        public QueueServiceTest()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Configure();

            _unitOfWork = new UnitOfWorkTest();
            _gatePassRepository = new GatePassRepositoryTest();
            _stateRepository = new StateRepositoryTest();
            // _laneRepository = new LaneRepositoryTest();
            _truckRepository = new TruckRepositoryTest();
            _queueListRepository = new QueueListRepositoryTest();
            _RFIDCardRepository = new RFIDCardRepositoryTest();
            // _employeepository = new EmployeeRepositoryTest();
            // _saleOrderRepository = new SaleOrderRepositoryTest();
            // _deliveryOrderRepository = new DeliveryOrderRepositoryTest();
            // _orderRepository = new OrderRepositoryTest();
            _carrierVendorRepository = new CarrierVendorRepositoryTest();
            _customerRepository = new CustomerRepositoryTest();
            // _deliveryOrderTypeRepository = new DeliveryOrderTypeRepositoryTest();
            // _customerWarehouseRepository = new CustomerWarehouseRepositoryTest();
            // _orderMaterialRepository = new OrderMaterialRepositoryTest();
            // _materialRepository = new MaterialRepositoryTest();
            _queueService = new QueueService(
                _unitOfWork, _gatePassRepository, _stateRepository,
                _laneRepository, _truckRepository, _queueListRepository,
                _RFIDCardRepository, _employeepository, _saleOrderRepository,
                _deliveryOrderRepository, _orderRepository,
                _carrierVendorRepository, _customerRepository,
                _deliveryOrderTypeRepository, _customerWarehouseRepository,
                _orderMaterialRepository, _materialRepository
            );
        }

        protected static bool IsAvailableGatePass(GatePass gate)
        {
            return gate.stateID != 0 && gate.isDelete == false;
        }

        protected static bool IsDeletedGatePass(GatePass gate)
        {
            return gate.isDelete == true;
        }

        protected static bool IsBusyGatePass(GatePass gate)
        {
            return gate.stateID == 0;
        }

        [TestMethod]
        public async Task TestMethod_GetAllGatePass()
        {
            var actualResult = await _queueService.GetAllGatePass();
            Assert.IsNotNull(actualResult);
        }

        protected async Task<GatePass> GetSampleGatePass(Func<GatePass, bool> filterFunc = null)
        {
            var allGates = await _gatePassRepository.GetAllAsync();
            if (filterFunc == null)
            {
                return allGates.First();
            }

            return allGates.First(filterFunc);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByID_Found()
        {
            var sampleGate = await this.GetSampleGatePass(QueueServiceTest.IsAvailableGatePass);
            Assert.IsNotNull(sampleGate);

            var response = await _queueService.GetGatePassByID(sampleGate.ID);
            var gate = response.responseData;
            Assert.AreEqual(gate, sampleGate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByID_NotFound()
        {
            var index = -1;
            var response = await _queueService.GetGatePassByID(index);

            var gate = response.responseData;
            Assert.AreEqual(gate, null);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByDriverID_Found()
        {
            var sampleGate = await this.GetSampleGatePass(
                g => g.driver != null && QueueServiceTest.IsAvailableGatePass(g));
            Assert.IsNotNull(sampleGate);

            var response = await _queueService.GetGatePassByDriverID(sampleGate.driver.ID);

            var gate = response.responseData;
            Assert.AreEqual(gate.driver.ID, sampleGate.driver.ID);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByDriverID_NotFound()
        {
            var index = -1;
            var response = await _queueService.GetGatePassByDriverID(index);

            var gate = response.responseData;
            Assert.IsNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByCode_Found()
        {
            var sampleGate = await this.GetSampleGatePass(
                g => !String.IsNullOrEmpty(g.code) && QueueServiceTest.IsAvailableGatePass(g));
            Assert.IsNotNull(sampleGate);

            var response = await _queueService.GetGatePassByCode(sampleGate.code);

            var gate = response.responseData;
            Assert.AreEqual(gate.code, sampleGate.code);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByCode_NotFound()
        {
            var index = "__!@#__";
            var response = await _queueService.GetGatePassByCode(index);

            var gate = response.responseData;
            Assert.IsNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID_Found()
        {
            var sampleGate = await this.GetSampleGatePass(
                g => g.RFIDCardID != null && QueueServiceTest.IsAvailableGatePass(g));
            Assert.IsNotNull(sampleGate);

            var sampleRFIDCard = await this._RFIDCardRepository.GetByIdAsync(sampleGate.RFIDCardID.Value);
            var response = await _queueService.GetGatePassByRFID(sampleRFIDCard.code);

            var gate = response.responseData;
            Assert.AreEqual(gate.RFIDCardID, sampleGate.RFIDCardID);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID_NotFound()
        {
            var index = "__!@#__";
            var response = await _queueService.GetGatePassByRFID(index);

            var gate = response.responseData;
            Assert.IsNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByPlateNumber_Found()
        {
            var sampleGate = await this.GetSampleGatePass(
                g => g.truckID != null && QueueServiceTest.IsAvailableGatePass(g));
            Assert.IsNotNull(sampleGate);

            var response = await _queueService.GetGatePassByPlateNumber(sampleGate.truck.plateNumber);

            var gate = response.responseData;
            Assert.AreEqual(gate.truck.plateNumber, sampleGate.truck.plateNumber);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByPlateNumber_NotFound()
        {
            var index = "__!@#__";
            var response = await _queueService.GetGatePassByPlateNumber(index);

            var gate = response.responseData;
            Assert.IsNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_UpdateGatePass()
        {
            var sampleDriverCamCapturePath = "Path random, yolo!";

            var sampleGate = await this.GetSampleGatePass();
            var response = await _queueService.GetGatePassByID(sampleGate.ID);

            var gate = response.responseData;
            gate.driverCamCapturePath = sampleDriverCamCapturePath;

            var updateResponse = await _queueService.UpdateGatePass(gate);
            var updatedGate = updateResponse.responseData;
            Assert.AreEqual(sampleDriverCamCapturePath, updatedGate.driverCamCapturePath);
        }

        [TestMethod]
        public void TestMethod_AddDriverPicture_OK()
        {
            var fileName = "testDriverAvatar.png";
            var driverAvatarData = File.ReadAllBytes("../../Resources/driver_avatar.png");

            var add_ok = _queueService.AddDriverPicture(fileName, driverAvatarData);
            Assert.IsTrue(add_ok);

            string addedFilePath = Constant.DriverCapturePath + fileName;
            var addedDriverAvatarData = File.ReadAllBytes(addedFilePath);

            Assert.AreEqual(driverAvatarData, addedDriverAvatarData);
        }

        [TestMethod]
        public void TestMethod_AddDriverPicture_NullName()
        {
            var driverAvatarData = File.ReadAllBytes("../../Resources/driver_avatar.png");

            var add_ok = _queueService.AddDriverPicture(null, driverAvatarData);
            Assert.IsFalse(add_ok);
        }

        [TestMethod]
        public void TestMethod_AddDriverPicture_NullData()
        {
            var fileName = "testDriverAvatar.png";

            var add_ok = _queueService.AddDriverPicture(fileName, null);
            Assert.IsFalse(add_ok);
        }
    }
}
