using Microsoft.VisualStudio.TestTools.UnitTesting;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using QWMSServer.Tests.Dummy;
using System.Threading.Tasks;
using AutoMapper;

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
        public async Task TestMethod_GetTrucks_NullParam()
        {
            var actualResult = await _securityServices.GetTrucks(null);
            Assert.IsNotNull(actualResult);
        }

        // ---------------------------------------------> End GetTrucks test cases

        // ---------------------------------------------> Begin GetGatePassByRFID test cases

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID()
        {
            var actualResult = await _securityServices.GetGatePassByRFID("0123456789");
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID_ShouldFail_NoCode()
        {
            var actualResult = await _securityServices.GetGatePassByRFID(null);
            Assert.IsNotNull(actualResult);
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
            GatePassViewModel gatePassView = new GatePassViewModel() { code = "0123" };
            var actualResult = await _securityServices.ConfirmSecurityCheck(gatePassView);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async Task TestMethod_ConfirmSecurityCheck_ShouldFail_NoCode()
        {
            GatePassViewModel gatePassView = new GatePassViewModel();
            var actualResult = await _securityServices.ConfirmSecurityCheck(gatePassView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_ConfirmSecurityCheck_ShouldFail_NullModel()
        {
            var actualResult = await _securityServices.ConfirmSecurityCheck(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End ConfirmSecurityCheck test cases
    }
}
