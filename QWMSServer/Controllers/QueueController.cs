using QWMSServer.Data.Services;
using QWMSServer.Filter;
using QWMSServer.Model.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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
        [AuthenticateRequire]
        [HttpGet]
        [Route("GatePass/GetAll", Name = "GetAllGatePass")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetAllGatePass()
        {
            return await _queueService.GetAllGatePass();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GatePass/GetByID/{id}", Name = "GetByID")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByID(int id)
        {
            return await _queueService.GetGatePassByID(id);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GatePass/GetByDriverID/{id}", Name = "GetByDriverID")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByDriverID(int id)
        {
            return await _queueService.GetGatePassByDriverID(id);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GatePass/GetByDriverIDNo/{driverIDNo}", Name = "GetByDriverIDNo")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByDriverIDNo(string driverIDNo)
        {
            return await _queueService.GetGatePassByDriverIDNo(driverIDNo);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GatePass/GetByCode/{Code}", Name = "GetByCode")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByCode(string Code)
        {
            return await _queueService.GetGatePassByCode(Code);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GatePass/GetByRFID/{Code}", Name = "GetByRFID")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByRFID(string Code)
        {
            return await _queueService.GetGatePassByRFID(Code);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GatePass/GetByPlateNumber/{PlateNumber}", Name = "GetByPlateNumber")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByPlateNumber(string PlateNumber)
        {
            return await _queueService.GetGatePassByPlateNumber(PlateNumber);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("GatePass/UpdateGatePass", Name = "UpdateGatePass")]
        public async Task<ResponseViewModel<GatePassViewModel>> UpdateGatePass([FromBody]GatePassViewModel gatePassViewModel)
        {
            return await _queueService.UpdateGatePass(gatePassViewModel);
        }

        [AuthenticateRequire]
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

        [AuthenticateRequire]
        [HttpPost]
        [Route("GatePass/UpdateGatePassWithRFIDCode", Name = "UpdateGatePassWithRFIDCode")]
        public async Task<ResponseViewModel<GatePassViewModel>> UpdateGatePassWithRFIDCode([FromBody]GatePassViewModel gatePassViewModel)
        {
            return await _queueService.UpdateGatePass(gatePassViewModel);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("GatePass/CreateRegisteredQueueItem/", Name = "CreateRegisteredQueueItem")]
        public async Task<ResponseViewModel<GatePassViewModel>> CreateRegisteredQueueItem([FromBody]GatePassRegistFrom gatePassRegistFrom)
        {
            return await _queueService.CreateRegisteredQueueItem(gatePassRegistFrom.gatePassID, 
                                                                 gatePassRegistFrom.driverImageFileName, 
                                                                 gatePassRegistFrom.employeeRFID, 
                                                                 gatePassRegistFrom.driverRFID,
                                                                 gatePassRegistFrom.loadingBayID);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GatePass/ReOrderQueue/", Name = "ReOrderQueue")]
        public async Task<ResponseViewModel<GenericResponseModel>> ReOrderQueue()
        {
            return await _queueService.ReOrderQueue();
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("DO/Import", Name = "ImportDO")]
        public async Task<ResponseViewModel<DOViewModel>> ImportDO([FromBody]List<DOViewModel> listDO)
        {
            return await _queueService.ImportDO(listDO);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("PO/Import", Name = "ImportPO")]
        public async Task<ResponseViewModel<POViewModel>> ImportPO([FromBody]List<POViewModel> listPO)
        {
            return await _queueService.ImportPO(listPO);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("DO/GetAllNotPlaned/{customerCode}", Name = "GetAllDONotPlaned")]
        public async Task<ResponseViewModel<OrderViewModel>> GetAllDONotPlaned(string customerCode)
        {
            return await _queueService.GetAllDONotPlaned(customerCode);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("PO/GetAllNotPlaned/{vendorCode}", Name = "GetAllPONotPlaned")]
        public async Task<ResponseViewModel<OrderViewModel>> GetAllPONotPlaned(string vendorCode)
        {
            return await _queueService.GetAllPONotPlaned(vendorCode);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("GatePass/CreateGatepassWithDO", Name = "CreateGatepassWithDO")]
        public async Task<ResponseViewModel<CreateGatePassViewModel>> CreateGatepassWithDO([FromBody]CreateGatePassViewModel createGatePassViewModel)
        {
            return await _queueService.CreateGatepassWithDO(createGatePassViewModel);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("GatePass/CreateGatepassWithPO", Name = "CreateGatepassWithPO")]
        public async Task<ResponseViewModel<CreateGatePassViewModel>> CreateGatepassWithPO([FromBody]CreateGatePassViewModel createGatePassViewModel)
        {
            return await _queueService.CreateGatepassWithPO(createGatePassViewModel);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("GatePass/CreateGatepassSP", Name = "CreateGatepassSP")]
        public async Task<ResponseViewModel<CreateGatePassViewModel>> CreateGatepassSP([FromBody]CreateGatePassViewModel createGatePassViewModel)
        {
            return await _queueService.CreateGatepassSP(createGatePassViewModel);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GatePass/GetAllLoadingBay", Name = "GetAllLoadingBay")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> GetAllLoadingBay()
        {
            return await _queueService.GetAllLoadingBay();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("LoadingBay/GetLoadingBayByTruck/{truckCode}", Name = "GetLoadingBayByTruck")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> GetLoadingBayByTruck(string truckCode)
        {
            return await _queueService.GetLoadingBayByTruck(truckCode);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GatePass/SearchGatePass/{searchText}", Name = "SearchGatePass")]
        public async Task<ResponseViewModel<GatePassViewModel>> SearchGatePass(string searchText)
        {
            return await _queueService.SearchGatePass(searchText);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GatePass/DeleteGatePass/{id}", Name = "DeleteGatePass")]
        public async Task<ResponseViewModel<GatePassViewModel>> DeleteGatePass(int id)
        {
            return await _queueService.DeleteGatePass(id);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GetAllOrder", Name = "GetAllOrder")]
        public async Task<ResponseViewModel<OrderViewModel>> GetAllOrder()
        {
            return await _queueService.GetAllOrder();
        }
        
        [AuthenticateRequire]
        [HttpPost]
        [Route("AddNewOrder", Name = "AddNewOrder")]
        public async Task<ResponseViewModel<OrderViewModel>> AddNewOrder(OrderViewModel orderView)
        {
            return await _queueService.AddNewOrder(orderView);
        }
        
        [AuthenticateRequire]
        [HttpPost]
        [Route("DeleteOrder", Name = "DeleteOrder")]
        public async Task<ResponseViewModel<OrderViewModel>> DeleteOrder(OrderViewModel orderView)
        {
            return await _queueService.DeleteOrder(orderView);
        }
        
        [AuthenticateRequire]
        [HttpPost]
        [Route("AddOrderMaterial", Name = "AddOrderMaterial")]
        public async Task<ResponseViewModel<OrderViewModel>> AddOrderMaterial(OrderMaterialViewModel orderMaterialView)
        {
            return await _queueService.AddOrderMaterial(orderMaterialView);
        }
        
        [AuthenticateRequire]
        [HttpPost]
        [Route("DeleteOrderMaterial", Name = "DeleteOrderMaterial")]
        public async Task<ResponseViewModel<OrderViewModel>> DeleteOrderMaterial(OrderMaterialViewModel orderMaterialView)
        {
            return await _queueService.DeleteOrderMaterial(orderMaterialView);
        }
    }
}