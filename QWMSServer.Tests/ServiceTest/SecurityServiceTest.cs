using Microsoft.VisualStudio.TestTools.UnitTesting;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using QWMSServer.Tests.Dummy;
using System.Threading.Tasks;
using AutoMapper;
using QWMSServer.Data.Common;

namespace QWMSServer.Tests.ServiceTest
{
    [TestClass]
    public class SecurityServiceTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueListRepository _queueListRepository;
        private readonly IGatePassRepository _gatePassRepository;
        private readonly IStateRecordRepository _stateRecordRepository;
        private readonly IRFIDCardRepository _rfidCardRepository;
        private readonly IStateRepository _stateRepository;
        private readonly SecurityServices _securityServices;

        public SecurityServiceTest()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Configure();
            _unitOfWork = new UnitOfWorkTest();
            _queueListRepository = new QueueListRepositoryTest();
            _gatePassRepository = new GatePassRepositoryTest();
            _stateRepository = new StateRepositoryTest();
            _stateRecordRepository = new StateRecordRepositoryTest();
            _rfidCardRepository = new RFIDCardRepositoryTest();
            _securityServices = new SecurityServices(_unitOfWork, _queueListRepository, _gatePassRepository, _stateRecordRepository, _rfidCardRepository, _stateRepository);
        }

        // ---------------------------------------------> Begin GetTrucks test cases

        [TestMethod]
        public async Task TestMethod_GetTrucks_NoParam()
        {
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _securityServices.GetTrucks(null);
            QueueListRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.AreEqual(ResponseCode.ERR_SEC_NOT_SUPPORT_CONDITION, actualResult.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_GetTrucks_All()
        {
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _securityServices.GetTrucks(SecurityServices.TRUCK_CONDITION_ALL);
            QueueListRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetTrucks_Calling()
        {
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _securityServices.GetTrucks(SecurityServices.TRUCK_CONDITION_CALLING);
            QueueListRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetTrucks_WaitingCall()
        {
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _securityServices.GetTrucks(SecurityServices.TRUCK_CONDITION_WAITING_CALL);
            QueueListRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetTrucks_1XXX_WaitingCall()
        {
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _securityServices.GetTrucks(SecurityServices.TRUCK_CONDITION_1XXX_WAITING_CALL);
            QueueListRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetTrucks_2XXX_WaitingCall()
        {
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _securityServices.GetTrucks(SecurityServices.TRUCK_CONDITION_2XXX_WAITING_CALL);
            QueueListRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_GetTrucks_3XXX_WaitingCall()
        {
            QueueListRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _securityServices.GetTrucks(SecurityServices.TRUCK_CONDITION_3XXX_WAITING_CALL);
            QueueListRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End GetTrucks test cases

        // ---------------------------------------------> Begin GetGatePassByRFID test cases

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID()
        {
            RFIDCardRepositoryTest.FLAG_GET_ASYNC = 1;
            GatePassRepositoryTest.FLAG_GET_ASYNC = 1;
            var actualResult = await _securityServices.GetGatePassByRFID("0123");
            RFIDCardRepositoryTest.FLAG_GET_ASYNC = 0;
            GatePassRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID_NoGatePass()
        {
            RFIDCardRepositoryTest.FLAG_GET_ASYNC = 1;
            GatePassRepositoryTest.FLAG_GET_ASYNC = 0;
            var actualResult = await _securityServices.GetGatePassByRFID("0123");
            RFIDCardRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.AreEqual(ResponseCode.ERR_SEC_NOT_FOUND_GATEPASS, actualResult.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID_NoParam()
        {
            RFIDCardRepositoryTest.FLAG_GET_ASYNC = 0;
            var actualResult = await _securityServices.GetGatePassByRFID(null);
            Assert.AreEqual(ResponseCode.ERR_SEC_NOT_FOUND_RFID, actualResult.errorCode);
        }

        // ---------------------------------------------> End GetGatePassByRFID test cases

        // ---------------------------------------------> Begin RegisterSecurityCheck test cases

        [TestMethod]
        public async Task TestMethod_RegisterSecurityCheck()
        {
            var actualResult = await _securityServices.RegisterSecurityCheck("01234567890");
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async Task TestMethod_RegisterSecurityCheck_ShouldFail_NoCode()
        {
            var actualResult = await _securityServices.RegisterSecurityCheck(null);
            Assert.IsNotNull(actualResult);
        }

        // ---------------------------------------------> End RegisterSecurityCheck test cases

        // ---------------------------------------------> Begin ConfirmSecurityCheck test cases

        [TestMethod]
        public async Task TestMethod_ConfirmSecurityCheck()
        {
            SecurityUpdateStateViewModel gatePassView = new SecurityUpdateStateViewModel() { gatePassCode = "123" };
            var actualResult = await _securityServices.ConfirmSecurityCheck(gatePassView);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async Task TestMethod_ConfirmSecurityCheck_ShouldFail_NoCode()
        {
            GatePassRepositoryTest.FLAG_GET_ASYNC = 0;
            SecurityUpdateStateViewModel gatePassView = new SecurityUpdateStateViewModel();
            var actualResult = await _securityServices.ConfirmSecurityCheck(gatePassView);
            Assert.AreEqual(ResponseCode.ERR_SEC_NOT_FOUND_GATEPASS, actualResult.errorCode);
        }

        [TestMethod]
        public async Task TestMethod_ConfirmSecurityCheck_ShouldFail_NoModel()
        {
            var actualResult = await _securityServices.ConfirmSecurityCheck(null);
            Assert.AreEqual(ResponseCode.ERR_SEC_WRONG_BODY_REQUEST_FORMAT, actualResult.errorCode);
        }

        // ---------------------------------------------> End ConfirmSecurityCheck test cases
    }
}
