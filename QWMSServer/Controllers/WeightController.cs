using QWMSServer.Data.Services;
using QWMSServer.Filter;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace QWMSServer.Controllers
{
    [RoutePrefix("Weight")]
    public class WeightController : ApiController
    {
        private readonly IWeightService _weightService;

        public WeightController(IWeightService weightService)
        {
            _weightService = weightService;
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GetQueueList", Name = "GetQueueList")]
        public async Task<ResponseViewModel<QueueListViewModel>> GetQueueList()
        {
            return await _weightService.GetQueueList();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("CallNextTruck/{truckGroupID}", Name = "CallNextTruck")]
        public async Task<ResponseViewModel<QueueListViewModel>> CallNextTruck(int truckGroupID)
        {
            return await _weightService.CallNextTruck(truckGroupID);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("CallNextTruck/{gatePassID}", Name = "CallNextTruckByGatePass")]
        public async Task<ResponseViewModel<QueueListViewModel>> CallNextTruckByGatePassID(int gatePassID)
        {
            return await _weightService.CallNextTruckByGatePassID(gatePassID);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("AddTruckCamera", Name = "AddTruckCamera")]
        public async Task<ResponseViewModel<GenericResponseModel>> AddTruckCamera(string filename, byte[] fileContent)
        {
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            // extract file name and file contents
            var fileNameParam = provider.Contents[0].Headers.ContentDisposition.Parameters
                .FirstOrDefault(p => p.Name.ToLower() == "filename");
            string fileName = (fileNameParam == null) ? "" : fileNameParam.Value.Trim('"');
            byte[] file = await provider.Contents[0].ReadAsByteArrayAsync();

            return _weightService.AddTruckCamera(fileName, file);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("UpdateWeightValue", Name = "UpdateWeightValue")]
        public async Task<ResponseViewModel<WeightRecordViewModel>> UpdateWeightValue([FromBody]WeightDataViewModel weightDataViewModel)
        {
            return await _weightService.UpdateWeightValue(weightDataViewModel);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GetWeightValueByGatePassID/{gatePassID}", Name = "GetWeightValueByGatePassID")]
        public async Task<ResponseViewModel<WeightRecordViewModel>> GetWeightValueByGatePassID(int gatePassID)
        {
            return await _weightService.GetWeightValueByGatePassID(gatePassID);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("UpdateLaneStatus", Name = "UpdateLaneStatus")]
        public async Task<ResponseViewModel<UpdateLaneStatusViewModel>> UpdateLaneStatus([FromBody]UpdateLaneStatusViewModel updateLaneStatusViewModel)
        {
            return await _weightService.UpdateLaneStatus(updateLaneStatusViewModel);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Gatepass/GetEmpty", Name = "GetEmptyGatepass")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetEmptyGatepass()
        {
            return await _weightService.GetEmptyGatepass();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("RFID/GetNotUse", Name = "GetRFIDNotUse")]
        public async Task<ResponseViewModel<RFIDCardViewModel>> GetRFIDNotUse()
        {
            return await _weightService.GetRFIDNotUse();
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Gatepass/Update", Name = "UpdateGatepassWeight")]
        public async Task<ResponseViewModel<GatePassViewModel>> UpdateGatepass([FromBody]GatePassViewModel gatepass)
        {
            return await _weightService.UpdateGatepass(gatepass);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GetTruckNetWeightValueByTruckID/{truckID}", Name = "GetTruckNetWeightValueByTruckID")]
        public async Task<ResponseViewModel<WeightRecordViewModel>> GetTruckNetWeightValueByTruckID(int truckID)
        {
            return await _weightService.GetTruckNetWeightValueByTruckID(truckID);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GetWB/{WBCode}", Name = "GetWB")]
        public async Task<ResponseViewModel<WeighBridge>> GetWB(string WBCode)
        {
            return await _weightService.GetWB(WBCode);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("WB/Update", Name = "UpdateWB")]
        public async Task<ResponseViewModel<WeighBridge>> UpdateWB([FromBody]WeighBridge WBView)
        {
            return await _weightService.UpdateWB(WBView);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GetAllWBConfig", Name = "GetAllWBConfig")]
        public async Task<ResponseViewModel<WeighbridgeConfiguration>> GetAllWBConfig()
        {
            return await _weightService.GetAllWBConfigs();
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("WBConfig/Update", Name = "UpdateWBConfig")]
        public async Task<ResponseViewModel<WeighbridgeConfiguration>> UpdateWBConfig([FromBody]WeighbridgeConfiguration WBConfigView)
        {
            return await _weightService.UpdateWBConfig(WBConfigView);
        }
    }
}