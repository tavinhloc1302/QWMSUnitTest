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
using System.Collections.Generic;
using System.Text;

namespace QWMSServer.Tests.ServiceTest
{
    [TestClass]
    public class QueueServiceTest
    {
        public static int CANNOT_BE_MATCHED_ID = -1;
        public static String CANNOT_BE_MATCHED_CODE = "__!@#__";

        private readonly IUnitOfWork _unitOfWork;
        private readonly GatePassRepositoryTest _gatePassRepository;
        private readonly StateRepositoryTest _stateRepository;
        private readonly LaneRepositoryTest _laneRepository;
        private readonly TruckRepositoryTest _truckRepository;
        private readonly QueueListRepositoryTest _queueListRepository;
        private readonly RFIDCardRepositoryTest _RFIDCardRepository;
        private readonly EmployeeRepositoryTest _employeepository;
        private readonly SaleOrderRepositoryTest _saleOrderRepository;
        private readonly DeliveryOrderRepositoryTest _deliveryOrderRepository;
        private readonly OrderRepositoryTest _orderRepository;
        private readonly CarrierVendorRepositoryTest _carrierVendorRepository;
        private readonly CustomerRepositoryTest _customerRepository;
        private readonly DeliveryOrderTypeRepositoryTest _deliveryOrderTypeRepository;
        private readonly CustomerWarehouseRepositoryTest _customerWarehouseRepository;
        private readonly OrderMaterialRepositoryTest _orderMaterialRepository;
        private readonly MaterialRepositoryTest _materialRepository;
        private readonly DriverRepositoryTest _driverRepository;
        private readonly UnitTypeRepositoryTest _unitTypeRepository;
        private readonly LoadingBayRepositoryTest _loadingBayRepository;
        private readonly ICommonService _commonService;
        private readonly PurchaseOrderRepositoryTest _purchaseOrderRepository;
        private readonly PurchaseOrderTypeRepositoryTest _purchaseOrderTypeRepository;
        private readonly PlantRepositoryTest _plantRepository;
        private readonly IOrderTypeRepository _orderTypeRepository;

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
            _saleOrderRepository = new SaleOrderRepositoryTest();
            _deliveryOrderRepository = new DeliveryOrderRepositoryTest();
            _orderRepository = new OrderRepositoryTest();
            _carrierVendorRepository = new CarrierVendorRepositoryTest();
            _customerRepository = new CustomerRepositoryTest();
            _deliveryOrderTypeRepository = new DeliveryOrderTypeRepositoryTest();
            _customerWarehouseRepository = new CustomerWarehouseRepositoryTest();
            _orderMaterialRepository = new OrderMaterialRepositoryTest();
            _materialRepository = new MaterialRepositoryTest();
            _driverRepository = new DriverRepositoryTest();
            //_unitTypeRepository = new UnitTypeRepositoryTest();
            _loadingBayRepository = new LoadingBayRepositoryTest();
            //_commonService = new CommonServiceTest();
            _purchaseOrderRepository = new PurchaseOrderRepositoryTest();
            //_purchaseOrderTypeRepository = new PurchaseOrderTypeRepositoryTest();
            _plantRepository = new PlantRepositoryTest();

            _queueService = new QueueService(
                _unitOfWork, _gatePassRepository, _stateRepository,
                _laneRepository, _truckRepository, _queueListRepository,
                _RFIDCardRepository, _employeepository, _saleOrderRepository,
                _deliveryOrderRepository, _orderRepository, _carrierVendorRepository,
                _customerRepository, _deliveryOrderTypeRepository,
                _customerWarehouseRepository, _orderMaterialRepository,
                _materialRepository, _driverRepository, _unitTypeRepository,
                _loadingBayRepository, _commonService, _purchaseOrderRepository,
                _purchaseOrderTypeRepository, _plantRepository, _orderTypeRepository
            );
        }

        [TestMethod]
        public async Task TestMethod_GetAllGatePass_Ok()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            var response = await _queueService.GetAllGatePass();
            GatePassRepositoryTest.ResetDummyFlags();

            var gates = response.responseDatas;
            Assert.IsTrue(gates.Count() > 0);
        }

        [TestMethod]
        public async Task TestMethod_GetAllGatePass_NoGate()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 0;
            var response = await _queueService.GetAllGatePass();
            GatePassRepositoryTest.ResetDummyFlags();

            var gates = response.responseDatas;
            Assert.IsTrue(gates.Count() == 0);
        }

        [TestMethod]
        public async Task TestMethod_GetAllGatePass_Exception()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = -1;
            var response = await _queueService.GetAllGatePass();
            GatePassRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(response.errorCode, ResponseCode.ERR_NO_OBJECT_FOUND);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByID_Found()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            var response = await _queueService.GetGatePassByID(1);
            GatePassRepositoryTest.ResetDummyFlags();

            var gate = response.responseData;
            Assert.IsNotNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByID_NotFound()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 0;
            var response = await _queueService.GetGatePassByID(1);
            GatePassRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(response.errorCode, ResponseCode.ERR_NO_OBJECT_FOUND);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByID_Exception()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = -1;
            var response = await _queueService.GetGatePassByID(1);
            GatePassRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(response.errorCode, ResponseCode.ERR_NO_OBJECT_FOUND);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByDriverID_Found()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            var response = await _queueService.GetGatePassByDriverID(1);
            GatePassRepositoryTest.ResetDummyFlags();

            var gate = response.responseData;
            Assert.IsNotNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByDriverID_NotFound()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 0;
            var response = await _queueService.GetGatePassByDriverID(1);
            GatePassRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(response.errorCode, ResponseCode.ERR_NO_OBJECT_FOUND);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByDriverID_Exception()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = -1;
            var response = await _queueService.GetGatePassByDriverID(1);
            GatePassRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(response.errorCode, ResponseCode.ERR_NO_OBJECT_FOUND);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByCode_Found()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            var response = await _queueService.GetGatePassByCode("ABC");
            GatePassRepositoryTest.ResetDummyFlags();

            var gate = response.responseData;
            Assert.IsNotNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByCode_NotFound()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 0;
            var response = await _queueService.GetGatePassByCode("ABC");
            GatePassRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(response.errorCode, ResponseCode.ERR_NO_OBJECT_FOUND);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByCode_Exception()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = -1;
            var response = await _queueService.GetGatePassByCode("ABC");
            GatePassRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(response.errorCode, ResponseCode.ERR_NO_OBJECT_FOUND);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID_Found()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            var response = await _queueService.GetGatePassByRFID("ABC");
            GatePassRepositoryTest.ResetDummyFlags();

            var gate = response.responseData;
            Assert.IsNotNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID_NotFound()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 0;
            var response = await _queueService.GetGatePassByRFID("ABC");
            GatePassRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(response.errorCode, ResponseCode.ERR_NO_OBJECT_FOUND);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID_Exception()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = -1;
            var response = await _queueService.GetGatePassByRFID("ABC");
            GatePassRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(response.errorCode, ResponseCode.ERR_NO_OBJECT_FOUND);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByPlateNumber_Found()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            var response = await _queueService.GetGatePassByPlateNumber("ABC");
            GatePassRepositoryTest.ResetDummyFlags();

            var gate = response.responseData;
            Assert.IsNotNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByPlateNumber_NotFound()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 0;
            var response = await _queueService.GetGatePassByPlateNumber("ABC");
            GatePassRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(response.errorCode, ResponseCode.ERR_NO_OBJECT_FOUND);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByPlateNumber_Exception()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = -1;
            var response = await _queueService.GetGatePassByPlateNumber("ABC");
            GatePassRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(response.errorCode, ResponseCode.ERR_NO_OBJECT_FOUND);
        }

        [TestMethod]
        public async Task TestMethod_UpdateGatePass_Ok()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            GatePassRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            var sampleGateView = new GatePassViewModel();
            sampleGateView.driver = new DriverViewModel();

            var updateResponse = await _queueService.UpdateGatePass(sampleGateView);
            GatePassRepositoryTest.ResetDummyFlags();

            var gate = updateResponse.responseData;
            Assert.IsNotNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_UpdateGatePass_SaveFail()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            GatePassRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            UnitOfWorkTest.FLAG_SAVE = 0;
            var sampleGateView = new GatePassViewModel();
            sampleGateView.driver = new DriverViewModel();

            var updateResponse = await _queueService.UpdateGatePass(sampleGateView);
            GatePassRepositoryTest.ResetDummyFlags();
            UnitOfWorkTest.ResetDummyFlags();

            var gate = updateResponse.responseData;
            Assert.AreEqual(updateResponse.errorCode, ResponseCode.ERR_DB_FAIL_TO_SAVE);
        }

        [TestMethod]
        public async Task TestMethod_UpdateGatePass_SaveException()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            GatePassRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            UnitOfWorkTest.FLAG_SAVE = -1;
            var sampleGateView = new GatePassViewModel();
            sampleGateView.driver = new DriverViewModel();

            var updateResponse = await _queueService.UpdateGatePass(sampleGateView);
            GatePassRepositoryTest.ResetDummyFlags();
            UnitOfWorkTest.ResetDummyFlags();

            Assert.AreEqual(updateResponse.errorCode, ResponseCode.ERR_SEC_UNKNOW);
        }

        [TestMethod]
        public async Task TestMethod_UpdateGatePass_NullView()
        {
            var updateResponse = await _queueService.UpdateGatePass(null);
            Assert.AreEqual(updateResponse.errorCode, ResponseCode.ERR_SEC_UNKNOW);
        }

        [TestMethod]
        public void TestMethod_AddDriverPicture_OK()
        {
            var fileName = "testDriverAvatar.png";
            var driverAvatarData = Encoding.ASCII.GetBytes("ABC");

            var response = _queueService.AddDriverPicture(fileName, driverAvatarData);
            Assert.IsTrue(response.booleanResponse);
        }

        [TestMethod]
        public void TestMethod_AddDriverPicture_NullName()
        {
            var driverAvatarData = Encoding.ASCII.GetBytes("ABC");

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
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            GatePassRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            var sampleGateView = new GatePassViewModel();
            sampleGateView.driver = new DriverViewModel();

            var updateResponse = await _queueService.UpdateGatePassWithRFIDCode(sampleGateView);
            GatePassRepositoryTest.ResetDummyFlags();

            var gate = updateResponse.responseData;
            Assert.IsNotNull(gate);
        }

        [TestMethod]
        public async Task TestMethod_UpdateGatePassWithRFIDCode_SaveFail()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            GatePassRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            UnitOfWorkTest.FLAG_SAVE = 0;
            var sampleGateView = new GatePassViewModel();
            sampleGateView.driver = new DriverViewModel();

            var updateResponse = await _queueService.UpdateGatePassWithRFIDCode(sampleGateView);
            GatePassRepositoryTest.ResetDummyFlags();
            UnitOfWorkTest.ResetDummyFlags();

            var gate = updateResponse.responseData;
            Assert.AreEqual(updateResponse.errorCode, ResponseCode.ERR_DB_FAIL_TO_SAVE);
        }

        [TestMethod]
        public async Task TestMethod_UpdateGatePassWithRFIDCode_SaveException()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            GatePassRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            UnitOfWorkTest.FLAG_SAVE = -1;
            var sampleGateView = new GatePassViewModel();
            sampleGateView.driver = new DriverViewModel();

            var updateResponse = await _queueService.UpdateGatePassWithRFIDCode(sampleGateView);
            GatePassRepositoryTest.ResetDummyFlags();
            UnitOfWorkTest.ResetDummyFlags();

            var gate = updateResponse.responseData;
            Assert.AreEqual(updateResponse.errorCode, ResponseCode.ERR_SEC_UNKNOW);
        }

        [TestMethod]
        public async Task TestMethod_UpdateGatePassWithRFIDCode_NullView()
        {
            var updateResponse = await _queueService.UpdateGatePassWithRFIDCode(null);
            Assert.AreEqual(updateResponse.errorCode, ResponseCode.ERR_SEC_UNKNOW);
        }

        [TestMethod]
        public async Task TestMethod_CreateRegisteredQueueItem_Ok()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            GatePassRepositoryTest.FLAG_ORDER_TYPE = 1;
            RFIDCardRepositoryTest.FLAG_GET_ASYNC = 1;
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            LaneRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            UnitOfWorkTest.FLAG_SAVE = 1;

            var response = await _queueService.CreateRegisteredQueueItem(
                1, "avatar.png", "ABC", "ABC", 1);
            GatePassRepositoryTest.ResetDummyFlags();
            RFIDCardRepositoryTest.ResetDummyFlags();
            LaneRepositoryTest.ResetDummyFlags();
            TruckRepositoryTest.ResetDummyFlags();

            Assert.IsTrue(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_CreateRegisteredQueueItem_InternalOrder()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            GatePassRepositoryTest.FLAG_ORDER_TYPE = 3;
            RFIDCardRepositoryTest.FLAG_GET_ASYNC = 1;
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            UnitOfWorkTest.FLAG_SAVE = 1;

            var response = await _queueService.CreateRegisteredQueueItem(
                1, "avatar.png", "ABC", "ABC", 1);
            GatePassRepositoryTest.ResetDummyFlags();
            RFIDCardRepositoryTest.ResetDummyFlags();
            LaneRepositoryTest.ResetDummyFlags();
            TruckRepositoryTest.ResetDummyFlags();

            Assert.IsTrue(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_CreateRegisteredQueueItem_SaveFail()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            GatePassRepositoryTest.FLAG_ORDER_TYPE = 1;
            RFIDCardRepositoryTest.FLAG_GET_ASYNC = 1;
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            UnitOfWorkTest.FLAG_SAVE = 0;

            var response = await _queueService.CreateRegisteredQueueItem(
                1, "avatar.png", "ABC", "ABC", 1);
            GatePassRepositoryTest.ResetDummyFlags();
            RFIDCardRepositoryTest.ResetDummyFlags();
            LaneRepositoryTest.ResetDummyFlags();
            TruckRepositoryTest.ResetDummyFlags();

            Assert.IsFalse(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_CreateRegisteredQueueItem_SaveException()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            GatePassRepositoryTest.FLAG_ORDER_TYPE = 1;
            RFIDCardRepositoryTest.FLAG_GET_ASYNC = 1;
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            UnitOfWorkTest.FLAG_SAVE = -1;

            var response = await _queueService.CreateRegisteredQueueItem(
                1, "avatar.png", "ABC", "ABC", 1);
            GatePassRepositoryTest.ResetDummyFlags();
            RFIDCardRepositoryTest.ResetDummyFlags();
            LaneRepositoryTest.ResetDummyFlags();
            TruckRepositoryTest.ResetDummyFlags();

            Assert.IsFalse(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_CreateRegisteredQueueItem_NoGatePass()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 0;

            var response = await _queueService.CreateRegisteredQueueItem(
                1, "avatar.png", "ABC", "ABC", 1);
            GatePassRepositoryTest.ResetDummyFlags();

            Assert.IsFalse(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_CreateRegisteredQueueItem_NulInput()
        {
            var response = await _queueService.CreateRegisteredQueueItem(
                1, null, null, null, 1);
            Assert.IsFalse(response.booleanResponse);
        }

        [TestMethod]
        public void TestMethod_FindTruckGroup_DeliPump()
        {
            var sampleGate = DataRecords.GATE_PASS_1ST_ORDER_DELI_TYPE_PUMP;

            // Using wrong Enum, should be `OrderTypeConst`
            var truckGroup = _queueService.findTruckGroup(sampleGate);
            Assert.AreEqual(Constant.TRUCKGROUP2X, truckGroup);
        }

        [TestMethod]
        public void TestMethod_FindTruckGroup_DeliNotPump()
        {
            var sampleGate = DataRecords.GATE_PASS_1ST_ORDER_DELI_TYPE_NOT_PUMP;

            // Using wrong Enum, should be `OrderTypeConst`
            var truckGroup = _queueService.findTruckGroup(sampleGate);
            Assert.AreEqual(Constant.TRUCKGROUP1X, truckGroup);
        }

        [TestMethod]
        public void TestMethod_FindTruckGroup_Purchase()
        {
            var sampleGate = DataRecords.GATE_PASS_1ST_ORDER_PURCHASE;

            // Using wrong Enum, should be `OrderTypeConst`
            var truckGroup = _queueService.findTruckGroup(sampleGate);
            Assert.AreEqual(Constant.TRUCKGROUP3X, truckGroup);
        }

        [TestMethod]
        public void TestMethod_FindTruckGroup_NotDeliOrPurchase()
        {
            var sampleGate = DataRecords.GATE_PASS_1ST_ORDER_TYPE_OTHER;

            // Using wrong Enum, should be `OrderTypeConst`
            var truckGroup = _queueService.findTruckGroup(sampleGate);
            Assert.AreEqual(Constant.TRUCKGROUP3X, truckGroup);
        }

        [TestMethod]
        public async Task TestMethod_AssignLane_Ok()
        {
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            LaneRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;

            var lowestKpiLaneId = await _queueService.assignLane(1, 1);
            LaneRepositoryTest.ResetDummyFlags();
            TruckRepositoryTest.ResetDummyFlags();
            QueueListRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(lowestKpiLaneId, 0);
        }

        [TestMethod]
        public async Task TestMethod_AssignLane_NoLoadingBay()
        {
            LaneRepositoryTest.FLAG_GET_ASYNC = 0;
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;

            // Should check the object returned at line ` var rlane = await _laneRepository.GetByIdAsync(laneID);`
            var lowestKpiLaneId = await _queueService.assignLane(1, 1);
            LaneRepositoryTest.ResetDummyFlags();
            TruckRepositoryTest.ResetDummyFlags();
            QueueListRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(lowestKpiLaneId, 0);
        }

        [TestMethod]
        public async Task TestMethod_AssignLane_NoTruck()
        {
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckRepositoryTest.FLAG_GET_ASYNC = 0;
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;

            // Should check the object returned at line ` var rlane = await _laneRepository.GetByIdAsync(laneID);`
            var lowestKpiLaneId = await _queueService.assignLane(1, 1);
            LaneRepositoryTest.ResetDummyFlags();
            TruckRepositoryTest.ResetDummyFlags();
            QueueListRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(lowestKpiLaneId, 0);
        }

        [TestMethod]
        public async Task TestMethod_AssignLane_NoQueue()
        {
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            LaneRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            QueueListRepositoryTest.FLAG_GET_ASYNC = 0;

            var lowestKpiLaneId = await _queueService.assignLane(1, 1);
            LaneRepositoryTest.ResetDummyFlags();
            TruckRepositoryTest.ResetDummyFlags();
            QueueListRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(lowestKpiLaneId, 0);
        }

        [TestMethod]
        public async Task TestMethod_ReOrderQueue_Ok()
        {
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;
            QueueListRepositoryTest.FLAG_UPDATE = 1;
            UnitOfWorkTest.FLAG_SAVE = 1;

            var response = await _queueService.ReOrderQueue();
            LaneRepositoryTest.ResetDummyFlags();
            TruckRepositoryTest.ResetDummyFlags();
            QueueListRepositoryTest.ResetDummyFlags();
            UnitOfWorkTest.ResetDummyFlags();

            Assert.IsTrue(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_ReOrderQueue_CleanQueueSaveFail()
        {
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;
            QueueListRepositoryTest.FLAG_UPDATE = 1;
            UnitOfWorkTest.FLAG_SAVE = 0;

            var response = await _queueService.ReOrderQueue();
            LaneRepositoryTest.ResetDummyFlags();
            TruckRepositoryTest.ResetDummyFlags();
            QueueListRepositoryTest.ResetDummyFlags();
            UnitOfWorkTest.ResetDummyFlags();

            Assert.IsFalse(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_ReOrderQueue_CleanQueueSaveException()
        {
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;
            QueueListRepositoryTest.FLAG_UPDATE = 1;
            UnitOfWorkTest.FLAG_SAVE = -1;

            var response = await _queueService.ReOrderQueue();
            LaneRepositoryTest.ResetDummyFlags();
            TruckRepositoryTest.ResetDummyFlags();
            QueueListRepositoryTest.ResetDummyFlags();
            UnitOfWorkTest.ResetDummyFlags();

            Assert.IsFalse(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_ReOrderQueue_AssignLaneSaveFail()
        {
            LaneRepositoryTest.FLAG_GET_ASYNC = 1;
            TruckRepositoryTest.FLAG_GET_ASYNC = 1;
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;
            QueueListRepositoryTest.FLAG_UPDATE = 1;
            UnitOfWorkTest.FLAG_SAVE = 1;
            UnitOfWorkTest.FLAG_SAVE_2 = 0;

            var response = await _queueService.ReOrderQueue();
            LaneRepositoryTest.ResetDummyFlags();
            TruckRepositoryTest.ResetDummyFlags();
            QueueListRepositoryTest.ResetDummyFlags();
            UnitOfWorkTest.ResetDummyFlags();

            Assert.IsFalse(response.booleanResponse);
        }

        private List<DOViewModel> GetSampleDoList()
        {
            return new List<DOViewModel>() {
                new DOViewModel() {
                    dayCreate = "01/01/2018",
                    dOCode = "DO1111",
                    dOItemCode = "DOITEM1111",
                    materialCode = "MAR1111",
                    materialName = "Mar 1111",
                    quanlity = 11,
                    unit = "piece",
                    sOCode = "SO Number 1",
                    customerCode = "CUS1111",
                    customerName = "Cus 1111",
                    shipToCode = "SHIPTO1111",
                    warehouseName = "Ware 1111",
                    deliveryAddress = "Addr 1111",
                    carrierCode = "CARI1111",
                    carrierName = "Cari 11111",
                    plant = "plant 11111",
                    sLoc = "sloc 1111",
                    remark = "remark 1111",
                }
            };
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_Ok()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            OrderRepositoryTest.FLAG_GET_ASYNC = 1;
            OrderRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            OrderMaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialRepositoryTest.FLAG_ADD = 0;

            var DoList = GetSampleDoList();
            // Wrong logic `for (int j = 0; i < checkOderCode_OrderMaterial.Count(); j++)`
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();
            OrderMaterialRepositoryTest.ResetDummyFlags();
            MaterialRepositoryTest.ResetDummyFlags();
            MaterialRepositoryTest.ResetDummyFlags();

            Assert.AreEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_DOCodeDuplicated()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            OrderRepositoryTest.FLAG_GET_ASYNC = 1;
            OrderRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            OrderMaterialRepositoryTest.FLAG_GET_ASYNC = 10;
            MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialRepositoryTest.FLAG_ADD = 0;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();
            OrderMaterialRepositoryTest.ResetDummyFlags();
            MaterialRepositoryTest.ResetDummyFlags();
            MaterialRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_SONullAndSaveException()
        {
            UnitOfWorkTest.FLAG_SAVE = -1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 0;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            OrderRepositoryTest.FLAG_GET_ASYNC = 1;
            OrderMaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialRepositoryTest.FLAG_ADD = 0;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();
            OrderMaterialRepositoryTest.ResetDummyFlags();
            MaterialRepositoryTest.ResetDummyFlags();
            MaterialRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_DONull()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 0;
            OrderRepositoryTest.FLAG_GET_ASYNC = 1;
            OrderMaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialRepositoryTest.FLAG_ADD = 0;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            CarrierVendorRepositoryTest.ResetDummyFlags();
            DeliveryOrderTypeRepositoryTest.ResetDummyFlags();
            CustomerWarehouseRepositoryTest.ResetDummyFlags();
            CustomerRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();
            OrderMaterialRepositoryTest.ResetDummyFlags();
            MaterialRepositoryTest.ResetDummyFlags();
            MaterialRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_DONullAndOrderNull()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerWarehouseRepositoryTest.FLAG_GET_ASYNC = 1;
            PlantRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 0;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            OrderRepositoryTest.FLAG_GET_ASYNC = 0;
            OrderMaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialRepositoryTest.FLAG_ADD = 0;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            CarrierVendorRepositoryTest.ResetDummyFlags();
            DeliveryOrderTypeRepositoryTest.ResetDummyFlags();
            CustomerWarehouseRepositoryTest.ResetDummyFlags();
            PlantRepositoryTest.ResetDummyFlags();
            CustomerRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();
            OrderMaterialRepositoryTest.ResetDummyFlags();
            MaterialRepositoryTest.ResetDummyFlags();
            MaterialRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_NoCarrier()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 0;
            OrderRepositoryTest.FLAG_GET_ASYNC = 0;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 0;
            //OrderRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            //OrderMaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            //MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            //MaterialRepositoryTest.FLAG_ADD = 0;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_NoCustomer()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 0;
            OrderRepositoryTest.FLAG_GET_ASYNC = 0;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerRepositoryTest.FLAG_GET_ASYNC = 0;
            //OrderRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            //OrderMaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            //MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            //MaterialRepositoryTest.FLAG_ADD = 0;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();
            CarrierVendorRepositoryTest.ResetDummyFlags();
            CustomerRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_NoDeliOrderType()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 0;
            OrderRepositoryTest.FLAG_GET_ASYNC = 0;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderTypeRepositoryTest.FLAG_GET_ASYNC = 0;
            //OrderRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            //OrderMaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            //MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            //MaterialRepositoryTest.FLAG_ADD = 0;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();
            CarrierVendorRepositoryTest.ResetDummyFlags();
            CustomerRepositoryTest.ResetDummyFlags();
            DeliveryOrderTypeRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_NoCustomerWarehouse()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 0;
            OrderRepositoryTest.FLAG_GET_ASYNC = 0;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerWarehouseRepositoryTest.FLAG_GET_ASYNC = 0;
            //OrderRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            //OrderMaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            //MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            //MaterialRepositoryTest.FLAG_ADD = 0;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();
            CarrierVendorRepositoryTest.ResetDummyFlags();
            CustomerRepositoryTest.ResetDummyFlags();
            DeliveryOrderTypeRepositoryTest.ResetDummyFlags();
            CustomerWarehouseRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_CreateDeliOrderFailed()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 0;
            OrderRepositoryTest.FLAG_GET_ASYNC = 0;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerWarehouseRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC_2 = 0;
            //OrderRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            //OrderMaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            //MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            //MaterialRepositoryTest.FLAG_ADD = 0;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();
            CarrierVendorRepositoryTest.ResetDummyFlags();
            CustomerRepositoryTest.ResetDummyFlags();
            DeliveryOrderTypeRepositoryTest.ResetDummyFlags();
            CustomerWarehouseRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_NoPlant()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 0;
            OrderRepositoryTest.FLAG_GET_ASYNC = 0;
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderTypeRepositoryTest.FLAG_GET_ASYNC = 1;
            CustomerWarehouseRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            PlantRepositoryTest.FLAG_GET_ASYNC = 0;
            //OrderRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            //OrderMaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            //MaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            //MaterialRepositoryTest.FLAG_ADD = 0;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();
            CarrierVendorRepositoryTest.ResetDummyFlags();
            CustomerRepositoryTest.ResetDummyFlags();
            DeliveryOrderTypeRepositoryTest.ResetDummyFlags();
            CustomerWarehouseRepositoryTest.ResetDummyFlags();
            PlantRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_InvalidSOCode()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 11;
            OrderRepositoryTest.FLAG_GET_ASYNC = 1;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_InvalidOrder()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            OrderRepositoryTest.FLAG_GET_ASYNC = 0;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportDO_NoMarterial()
        {
            UnitOfWorkTest.FLAG_SAVE = 1;
            SaleOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            DeliveryOrderRepositoryTest.FLAG_GET_ASYNC = 1;
            OrderRepositoryTest.FLAG_GET_ASYNC = 1;
            OrderRepositoryTest.FLAG_GET_ASYNC_2 = 1;
            OrderMaterialRepositoryTest.FLAG_GET_ASYNC = 1;
            MaterialRepositoryTest.FLAG_GET_ASYNC = 0;
            MaterialRepositoryTest.FLAG_ADD = 0;

            var DoList = GetSampleDoList();
            var response = await _queueService.ImportDO(DoList);

            UnitOfWorkTest.ResetDummyFlags();
            SaleOrderRepositoryTest.ResetDummyFlags();
            DeliveryOrderRepositoryTest.ResetDummyFlags();
            OrderRepositoryTest.ResetDummyFlags();
            OrderMaterialRepositoryTest.ResetDummyFlags();
            MaterialRepositoryTest.ResetDummyFlags();
            MaterialRepositoryTest.ResetDummyFlags();

            Assert.AreNotEqual(0, response.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ImportPO_Ok()
        {
            var PoList = new List<POViewModel>() {
                new POViewModel() {
                    pOCode = "1111",
                    pOItemCode = "item 1111",
                    materialCode = "MAR1111",
                    materialName = "Mar 1111",
                    quanlity = 11,
                    unit = "piece",
                    vendorCode = "VEN1111",
                    vendorName = "Ven 1111",
                    billCode = "BILL1111",
                    plant = "plant 1111",
                    sLoc = "sloc 1111",
                    remark = "remark 1111",
                }
            };

            var response = await _queueService.ImportPO(PoList);
            Assert.IsTrue(response.booleanResponse);
        }

        [TestMethod]
        public async Task TestMethod_GetAllLoadingBay_NotFound()
        {
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 0;
            var response = await _queueService.GetAllLoadingBay();
            Assert.AreEqual(0, response.errorCode);

            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 0;
        }

        [TestMethod]
        public async Task TestMethod_GetAllLoadingBay_Ok()
        {
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 1;
            var response = await _queueService.GetAllLoadingBay();
            Assert.IsNotNull(response.responseDatas);

            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 0;
        }

        [TestMethod]
        public async Task TestMethod_GetAllLoadingBay_Exception()
        {
            LoadingBayRepositoryTest.FLAG_GET_ASYNC = -1;
            var response = await _queueService.GetAllLoadingBay();
            Assert.AreNotEqual(0, response.errorCode);

            LoadingBayRepositoryTest.FLAG_GET_ASYNC = 0;
        }
    }
}
