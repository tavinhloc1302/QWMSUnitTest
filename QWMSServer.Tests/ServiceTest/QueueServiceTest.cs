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
        public static int CANNOT_BE_MATCHED_ID = -1;
        public static String CANNOT_BE_MATCHED_CODE = "__!@#__";

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
        private readonly IDriverRepository _driverRepository;
        private readonly IUnitTypeRepository _unitTypeRepository;
        private readonly ILoadingBayRepository _loadingBayRepository;
        private readonly ICommonService _commonService;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IPurchaseOrderTypeRepository _purchaseOrderTypeRepository;
        private readonly IPlantRepository _plantRepository;

        private readonly QueueService _queueService;

        public QueueServiceTest()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Configure();

            _unitOfWork = new UnitOfWorkTest();
            _gatePassRepository = new GatePassRepositoryTest();
            _stateRepository = new StateRepositoryTest();
            _laneRepository = new LaneRepositoryTest();
            _truckRepository = new TruckRepositoryTest();
            _queueListRepository = new QueueListRepositoryTest();
            _RFIDCardRepository = new RFIDCardRepositoryTest();
            _employeepository = new EmployeeRepositoryTest();
            //_saleOrderRepository = new SaleOrderRepositoryTest();
            //_deliveryOrderRepository = new DeliveryOrderRepositoryTest();
            _orderRepository = new OrderRepositoryTest();
            _carrierVendorRepository = new CarrierVendorRepositoryTest();
            _customerRepository = new CustomerRepositoryTest();
            //_deliveryOrderTypeRepository = new DeliveryOrderTypeRepositoryTest();
            //_customerWarehouseRepository = new CustomerWarehouseRepositoryTest();
            //_orderMaterialRepository = new OrderMaterialRepositoryTest();
            //_materialRepository = new MaterialRepositoryTest();
            _driverRepository = new DriverRepositoryTest();
            //_unitTypeRepository = new UnitTypeRepositoryTest();
            //_loadingBayRepository = new LoadingBayRepositoryTest();
            //_commonService = new CommonServiceTest();
            //_purchaseOrderRepository = new PurchaseOrderRepositoryTest();
            //_purchaseOrderTypeRepository = new PurchaseOrderTypeRepositoryTest();
            //_plantRepository = new PlantRepositoryTest();
            _queueService = new QueueService(
               _unitOfWork, _gatePassRepository, _stateRepository, _laneRepository,
               _truckRepository, _queueListRepository, _RFIDCardRepository,
               _employeepository, _saleOrderRepository, _deliveryOrderRepository,
               _orderRepository, _carrierVendorRepository, _customerRepository,
               _deliveryOrderTypeRepository, _customerWarehouseRepository,
               _orderMaterialRepository, _materialRepository, _driverRepository,
               _unitTypeRepository, _loadingBayRepository, _commonService,
               _purchaseOrderRepository, _purchaseOrderTypeRepository,
               _plantRepository
            );
        }

        [TestMethod]
        public async Task TestMethod_GetAllGatePass()
        {
            var response = await _queueService.GetAllGatePass();

            var gates = response.responseDatas;
            Assert.AreEqual(gates.Count(), _gatePassRepository.Objects.Count());
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByID_Found()
        {
            var index = DataRecords.GATE_PASS_NORMAL.ID;
            var response = await _queueService.GetGatePassByID(index);

            var gate = response.responseData;
            Assert.AreEqual(index, gate.ID);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByID_NotFound()
        {
            var index = DataRecords.GATE_PASS_DELETED.ID;
            var response = await _queueService.GetGatePassByID(index);

            var gate = response.responseData;
            Assert.IsNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByDriverID_Found()
        {
            var index = DataRecords.GATE_PASS_NORMAL.driver.ID;
            var response = await _queueService.GetGatePassByDriverID(index);

            var gate = response.responseData;
            Assert.AreEqual(index, gate.driver.ID);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByDriverID_NotFound()
        {
            var index = DataRecords.GATE_PASS_DELETED.driver.ID;
            var response = await _queueService.GetGatePassByDriverID(index);

            var gate = response.responseData;
            Assert.IsNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByCode_Found()
        {
            var index = DataRecords.GATE_PASS_NORMAL.code;
            var response = await _queueService.GetGatePassByCode(index);

            var gate = response.responseData;
            Assert.AreEqual(index, gate.code);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByCode_NotFound()
        {
            var index = DataRecords.GATE_PASS_DELETED.code;
            var response = await _queueService.GetGatePassByCode(index);

            var gate = response.responseData;
            Assert.IsNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID_Found()
        {
            var index = DataRecords.GATE_PASS_NORMAL.RFIDCard.code;
            var response = await _queueService.GetGatePassByRFID(index);

            var gate = response.responseData;
            Assert.AreEqual(index, gate.RFIDCard.code);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID_NotFound()
        {
            var index = DataRecords.GATE_PASS_DELETED.RFIDCard.code;
            var response = await _queueService.GetGatePassByRFID(index);

            var gate = response.responseData;
            Assert.IsNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByPlateNumber_Found()
        {
            var index = DataRecords.GATE_PASS_NORMAL.truck.plateNumber;
            var response = await _queueService.GetGatePassByPlateNumber(index);

            var gate = response.responseData;
            Assert.AreEqual(index, gate.truck.plateNumber);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByPlateNumber_NotFound()
        {
            var index = DataRecords.GATE_PASS_DELETED.truck.plateNumber;
            var response = await _queueService.GetGatePassByPlateNumber(index);

            var gate = response.responseData;
            Assert.IsNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_UpdateGatePass()
        {
            var sampleDriverCamCapturePath = "Path random, yolo!";

            var sampleGate = DataRecords.GATE_PASS_FOR_UPDATE;
            sampleGate.driverCamCapturePath = sampleDriverCamCapturePath;
            var sampleGateView = Mapper.Map<GatePass, GatePassViewModel>(sampleGate);

            var updateResponse = await _queueService.UpdateGatePass(sampleGateView);
            var updatedGate = updateResponse.responseData;
            Assert.AreEqual(sampleDriverCamCapturePath, updatedGate.driverCamCapturePath);
        }

        [TestMethod]
        public void TestMethod_AddDriverPicture_OK()
        {
            var fileName = "testDriverAvatar.png";
            var driverAvatarData = File.ReadAllBytes("../../Resources/driver_avatar.png");

            var response = _queueService.AddDriverPicture(fileName, driverAvatarData);
            Assert.IsTrue(response.booleanResponse);

            //string addedFilePath = Constant.DriverCapturePath + fileName;
            //var addedDriverAvatarData = File.ReadAllBytes(addedFilePath);

            //Assert.AreEqual(driverAvatarData, addedDriverAvatarData);
        }

        [TestMethod]
        public void TestMethod_AddDriverPicture_NullName()
        {
            var driverAvatarData = File.ReadAllBytes("../../Resources/driver_avatar.png");

            var response = _queueService.AddDriverPicture(null, driverAvatarData);
            Assert.IsFalse(response.booleanResponse);
        }

        [TestMethod]
        public void TestMethod_AddDriverPicture_NullData()
        {
            var fileName = "testDriverAvatar.png";

            var response = _queueService.AddDriverPicture(fileName, null);
            Assert.IsFalse(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_UpdateGatePassWithRFIDCode_Ok()
        {
            var sampleRFIDCardId = DataRecords.RFID_CARD_NORMAL_2.ID;
            var sampleGate = DataRecords.GATE_PASS_FOR_UPDATE;

            var sampleGateView = Mapper.Map<GatePass, GatePassViewModel>(sampleGate);
            sampleGateView.RFIDCardID = sampleRFIDCardId;

            var updateResponse = await _queueService.UpdateGatePassWithRFIDCode(sampleGateView);
            var updatedGate = updateResponse.responseData;
            Assert.AreEqual(sampleRFIDCardId, updatedGate.RFIDCardID);
        }

        [TestMethod]
        public async Task TestMethod_UpdateGatePassWithRFIDCode_Null()
        {
            var updateResponse = await _queueService.UpdateGatePassWithRFIDCode(null);
            var updatedGate = updateResponse.responseData;
            Assert.IsNull(updatedGate);
        }

        [TestMethod]
        public async Task TestMethod_UpdateGatePassWithRFIDCode_NotFound()
        {
            var sampleRFIDCardId = DataRecords.RFID_CARD_NORMAL_2.ID;
            var sampleGate = DataRecords.GATE_PASS_DELETED;

            var sampleGateView = Mapper.Map<GatePass, GatePassViewModel>(sampleGate);
            sampleGateView.RFIDCardID = sampleRFIDCardId;

            var updateResponse = await _queueService.UpdateGatePassWithRFIDCode(sampleGateView);
            var updatedGate = updateResponse.responseData;
            Assert.IsNull(updatedGate);
        }

        [TestMethod]
        public async Task TestMethod_CreateRegisteredQueueItem_Ok()
        {
            var sampleGate = DataRecords.GATE_PASS_FOR_UPDATE;
            var sampleEmpRFIDCard = DataRecords.RFID_CARD_NORMAL;
            var sampleDriverRFIDCard = DataRecords.RFID_CARD_NORMAL_2;

            var response = await _queueService.CreateRegisteredQueueItem(
                sampleGate.ID, "avatar.png",
                sampleEmpRFIDCard.code,
                sampleDriverRFIDCard.code);
            Assert.IsTrue(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_CreateRegisteredQueueItem_NotFound_Gate()
        {
            var sampleEmpRFIDCard = DataRecords.RFID_CARD_NORMAL;
            var sampleDriverRFIDCard = DataRecords.RFID_CARD_NORMAL_2;

            var response = await _queueService.CreateRegisteredQueueItem(
                CANNOT_BE_MATCHED_ID, "avatar.png",
                sampleEmpRFIDCard.code,
                sampleDriverRFIDCard.code);
            Assert.IsFalse(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_CreateRegisteredQueueItem_NotFound_EmployeeRFID()
        {
            var sampleGate = DataRecords.GATE_PASS_FOR_UPDATE;
            var sampleDriverRFIDCard = DataRecords.RFID_CARD_NORMAL;

            var response = await _queueService.CreateRegisteredQueueItem(
                sampleGate.ID, "avatar.png",
                CANNOT_BE_MATCHED_CODE,
                sampleDriverRFIDCard.code);
            Assert.IsFalse(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_CreateRegisteredQueueItem_NotFound_DriverRFID()
        {
            var sampleGate = DataRecords.GATE_PASS_FOR_UPDATE;
            var sampleEmpRFIDCard = DataRecords.RFID_CARD_NORMAL;

            var response = await _queueService.CreateRegisteredQueueItem(
                sampleGate.ID, "avatar.png",
                sampleEmpRFIDCard.code,
                CANNOT_BE_MATCHED_CODE);
            Assert.IsFalse(response.booleanResponse);
        }

        [TestMethod]
        public void TestMethod_FindTruckGroup_DeliPump()
        {
            var sampleGate = DataRecords.GATE_PASS_1ST_ORDER_DELI_TYPE_PUMP;
            var truckGroup = _queueService.findTruckGroup(sampleGate);
            Assert.AreEqual(Constant.TRUCKGROUP2X, truckGroup);
        }

        [TestMethod]
        public void TestMethod_FindTruckGroup_DeliNotPump()
        {
            var sampleGate = DataRecords.GATE_PASS_1ST_ORDER_DELI_TYPE_NOT_PUMP;
            var truckGroup = _queueService.findTruckGroup(sampleGate);
            Assert.AreEqual(Constant.TRUCKGROUP1X, truckGroup);
        }

        [TestMethod]
        public void TestMethod_FindTruckGroup_Purchase()
        {
            var sampleGate = DataRecords.GATE_PASS_1ST_ORDER_PURCHASE;
            var truckGroup = _queueService.findTruckGroup(sampleGate);
            Assert.AreEqual(Constant.TRUCKGROUP3X, truckGroup);
        }

        [TestMethod]
        public void TestMethod_FindTruckGroup_NotDeliOrPurchase()
        {
            var sampleGate = DataRecords.GATE_PASS_1ST_ORDER_TYPE_OTHER;
            var truckGroup = _queueService.findTruckGroup(sampleGate);
            Assert.AreEqual(Constant.TRUCKGROUP3X, truckGroup);
        }

        [TestMethod]
        public async Task TestMethod_AssignLane_Ok()
        {
            var sampleLoadingBay = DataRecords.LOADING_BAY_NORMAL;
            var sampleTruck = DataRecords.TRUCK_NORMAL;
            var lowestKpiLaneId = await _queueService.assignLane(
                sampleLoadingBay.ID, sampleTruck.ID);
            var lowestKpiLane = await _laneRepository.GetByIdAsync(lowestKpiLaneId);
            Assert.IsNotNull(lowestKpiLane);
        }

        [TestMethod]
        public async Task TestMethod_AssignLane_NoLoadingBay()
        {
            var sampleLoadingBay = DataRecords.LOADING_BAY_DELETED;
            var sampleTruck = DataRecords.TRUCK_NORMAL;
            var lowestKpiLaneId = await _queueService.assignLane(
                sampleLoadingBay.ID, sampleTruck.ID);
            Assert.IsNull(lowestKpiLaneId);
        }

        [TestMethod]
        public async Task TestMethod_AssignLane_NoTruck()
        {
            var sampleLoadingBay = DataRecords.LOADING_BAY_NORMAL;
            var sampleTruck = DataRecords.TRUCK_DELETED;
            var lowestKpiLaneId = await _queueService.assignLane(
                sampleLoadingBay.ID, sampleTruck.ID);
            Assert.IsNull(lowestKpiLaneId);
        }

        [TestMethod]
        public async Task TestMethod_ReOrderQueue_Ok()
        {
            var response = await _queueService.ReOrderQueue();
            Assert.IsTrue(response.booleanResponse);
        }

        //[TestMethod]
        //public async Task TestMethod_ImportDO_Ok()
        //{
        //    var response = await _queueService.ImportDO();
        //    Assert.IsTrue(response.booleanResponse);
        //}
    }
}
