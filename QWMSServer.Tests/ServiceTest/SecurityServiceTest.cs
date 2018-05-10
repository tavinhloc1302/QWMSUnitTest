using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _unitOfWork = new UnitOfWorkTest();
            _queueListRepository = new QueueListRepositoryTest();
            _gatePassRepository = new GatePassRepositoryTest();
            _stateRepository = new StateRepositoryTest();
            _stateRecordRepository = new StateRecordRepositoryTest();
            _rfidCardRepository = new RFIDCardRepositoryTest();
            _securityServices = new SecurityServices(_unitOfWork, _queueListRepository, _gatePassRepository, _stateRecordRepository, _rfidCardRepository, _stateRepository);
        }

        [TestMethod]
        public async Task TestMethod_GetTrucks(string truckCondition)
        {
            var actualResult = await _securityServices.GetTrucks(truckCondition);
            var expectedResult = new ResponseViewModel<QueueListViewModel>() {

            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_GetGatePassByRFID(string rfidCode)
        {
            var actualResult = await _securityServices.GetGatePassByRFID(rfidCode);
            var expectedResult = new ResponseViewModel<GatePassViewModel>() {

            };
            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestMethod]
        public async Task TestMethod_RegisterSecurityCheck(string rfidCode)
        {
            var actualResult = await _securityServices.RegisterSecurityCheck(rfidCode);
            var expectedResult = new ResponseViewModel<GatePassViewModel>() {

            };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task TestMethod_ConfirmSecurityCheck(GatePassViewModel gatePassView)
        {
            var actualResult = await _securityServices.ConfirmSecurityCheck(gatePassView);
            var expectedResult = new ResponseViewModel<GatePassViewModel>() {

            };
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
