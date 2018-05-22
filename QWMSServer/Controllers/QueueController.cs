using QWMSServer.Data.Services;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace QWMSServer.Controllers
{
    [RoutePrefix("Queue")]
    public class QueueController : ApiController
    {
        private readonly IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        /* Customer API */
        [HttpGet]
        [Route("GatePass/GetAll", Name = "GetAllGatePass")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetAllGatePass()
        {
            return await _queueService.GetAllGatePass();
        }

        [HttpGet]
        [Route("GatePass/GetByID/{id}", Name = "GetByID")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByID(int id)
        {
            return await _queueService.GetGatePassByID(id);
        }

        [HttpGet]
        [Route("GatePass/GetByDriverID/{id}", Name = "GetByDriverID")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByDriverID(int id)
        {
            return await _queueService.GetGatePassByDriverID(id);
        }

        [HttpGet]
        [Route("GatePass/GetByCode/{Code}", Name = "GetByCode")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByCode(string Code)
        {
            return await _queueService.GetGatePassByCode(Code);
        }

        [HttpGet]
        [Route("GatePass/GetByRFID/{Code}", Name = "GetByRFID")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByRFID(string Code)
        {
            return await _queueService.GetGatePassByRFID(Code);
        }

        [HttpGet]
        [Route("GatePass/GetByPlateNumber/{PlateNumber}", Name = "GetByPlateNumber")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByPlateNumber(string PlateNumber)
        {
            return await _queueService.GetGatePassByPlateNumber(PlateNumber);
        }

        [HttpPost]
        [Route("GatePass/UpdateGatePass", Name = "UpdateGatePass")]
        public async Task<ResponseViewModel<GatePassViewModel>> UpdateGatePass([FromBody]GatePassViewModel gatePassViewModel)
        {
            return await _queueService.UpdateGatePass(gatePassViewModel);
        }

        [HttpPost]
        [Route("GatePass/AddDriverPicture", Name = "AddDriverPicture")]
        public async Task<ResponseViewModel<GenericResponseModel>> AddDriverPicture()
        {
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            // extract file name and file contents
            /*var fileNameParam = provider.Contents[0].Headers.ContentDisposition.Parameters
                .FirstOrDefault(p => p.Name.ToLower() == "filename");
            string fileName = (fileNameParam == null) ? "" : fileNameParam.Value.Trim('"');*/
            string fileName = provider.Contents[0].Headers.ContentDisposition.Name;
            byte[] file = await provider.Contents[0].ReadAsByteArrayAsync();

            return _queueService.AddDriverPicture(fileName, file);
        }

        [HttpPost]
        [Route("GatePass/UpdateGatePassWithRFIDCode", Name = "UpdateGatePassWithRFIDCode")]
        public async Task<ResponseViewModel<GatePassViewModel>> UpdateGatePassWithRFIDCode([FromBody]GatePassViewModel gatePassViewModel)
        {
            return await _queueService.UpdateGatePass(gatePassViewModel);
        }

        [HttpPost]
        [Route("GatePass/CreateRegisteredQueueItem/", Name = "CreateRegisteredQueueItem")]
        public async Task<ResponseViewModel<GenericResponseModel>> CreateRegisteredQueueItem([FromBody]GatePassRegistFrom gatePassRegistFrom)
        {
            return await _queueService.CreateRegisteredQueueItem(gatePassRegistFrom.gatePassID, 
                                                                 gatePassRegistFrom.driverImageFileName, 
                                                                 gatePassRegistFrom.employeeRFID, 
                                                                 gatePassRegistFrom.driverRFID);
        }

        [HttpGet]
        [Route("GatePass/ReOrderQueue/", Name = "ReOrderQueue")]
        public async Task<ResponseViewModel<GenericResponseModel>> ReOrderQueue()
        {
            return await _queueService.ReOrderQueue();
        }

        [HttpPost]
        [Route("DO/Import", Name = "ImportDO")]
        public async Task<ResponseViewModel<DOViewModel>> ImportDO([FromBody]List<DOViewModel> listDO)
        {
            return await _queueService.ImportDO(listDO);
        }

        [HttpPost]
        [Route("PO/Import", Name = "ImportPO")]
        public async Task<ResponseViewModel<POViewModel>> ImportPO([FromBody]List<POViewModel> listPO)
        {
            return await _queueService.ImportPO(listPO);
        }

        [HttpGet]
        [Route("DO/GetAllNotPlaned/{customerCode}", Name = "GetAllDONotPlaned")]
        public async Task<ResponseViewModel<OrderViewModel>> GetAllDONotPlaned(string customerCode)
        {
            return await _queueService.GetAllDONotPlaned(customerCode);
        }

        [HttpGet]
        [Route("PO/GetAllNotPlaned/{vendorCode}", Name = "GetAllPONotPlaned")]
        public async Task<ResponseViewModel<OrderViewModel>> GetAllPONotPlaned(string vendorCode)
        {
            return await _queueService.GetAllPONotPlaned(vendorCode);
        }

        [HttpPost]
        [Route("GatePass/CreateGatepassWithDO", Name = "CreateGatepassWithDO")]
        public async Task<ResponseViewModel<CreateGatePassViewModel>> CreateGatepassWithDO([FromBody]CreateGatePassViewModel createGatePassViewModel)
        {
            return await _queueService.CreateGatepassWithDO(createGatePassViewModel);
        }

        [HttpPost]
        [Route("GatePass/CreateGatepassWithPO", Name = "CreateGatepassWithPO")]
        public async Task<ResponseViewModel<CreateGatePassViewModel>> CreateGatepassWithPO([FromBody]CreateGatePassViewModel createGatePassViewModel)
        {
            return await _queueService.CreateGatepassWithPO(createGatePassViewModel);
        }

        [HttpGet]
        [Route("GatePass/GetAllLoadingBay", Name = "GetAllLoadingBay")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> GetAllLoadingBay()
        {
            return await _queueService.GetAllLoadingBay();
        }

        [HttpGet]
        [Route("LoadingBay/GetLoadingBayByTruck/{truckCode}", Name = "GetLoadingBayByTruck")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> GetLoadingBayByTruck(string truckCode)
        {
            return await _queueService.GetLoadingBayByTruck(truckCode);
        }
    }
}