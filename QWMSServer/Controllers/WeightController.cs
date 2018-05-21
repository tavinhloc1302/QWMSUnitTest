using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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

        [HttpGet]
        [Route("GetQueueList", Name = "GetQueueList")]
        public async Task<ResponseViewModel<QueueListViewModel>> GetQueueList()
        {
            return await _weightService.GetQueueList();
        }

        [HttpGet]
        [Route("CallNextTruck/{truckGroupID}", Name = "CallNextTruck")]
        public async Task<ResponseViewModel<QueueListViewModel>> CallNextTruck(int truckGroupID)
        {
            return await _weightService.CallNextTruck(truckGroupID);
        }

        [HttpPost]
        [Route("AddTruckCamera", Name = "AddTruckCamera")]
        public async Task<ResponseViewModel<WeightRecordViewModel>> AddTruckCamera(string filename, byte[] fileContent)
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

        [HttpPost]
        [Route("UpdateWeightValue", Name = "UpdateWeightValue")]
        public async Task<ResponseViewModel<WeightRecordViewModel>> UpdateWeightValue([FromBody]WeightDataViewModel weightDataViewModel)
        {
            return await _weightService.UpdateWeightValue(weightDataViewModel);
        }

        [HttpGet]
        [Route("GetWeightValueByGatePassID/{gatePassID}", Name = "GetWeightValueByGatePassID")]
        public async Task<ResponseViewModel<WeightRecordViewModel>> GetWeightValueByGatePassID(int gatePassID)
        {
            return await _weightService.GetWeightValueByGatePassID(gatePassID);
        }

        [HttpPost]
        [Route("UpdateLaneStatus", Name = "UpdateLaneStatus")]
        public async Task<ResponseViewModel<UpdateLaneStatusViewModel>> UpdateLaneStatus([FromBody]UpdateLaneStatusViewModel updateLaneStatusViewModel)
        {
            return await _weightService.UpdateLaneStatus(updateLaneStatusViewModel);
        }
    }
}