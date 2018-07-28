using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using QWMSServer.Data.Common;
using QWMSServer.Data.Services;
using QWMSServer.DeviceService;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;

namespace QWMSServer.Controllers
{
    [RoutePrefix("Device")]
    public class DeviceController : ApiController
    {
        [HttpPost]
        [Route("Camera/PlateLicenseReconigze", Name = "PlateLicenseReconigze")]
        public async Task<ResponseViewModel<String>> PlateLicenseReconigze()
        {
            LPRAPI lprService;
            List<LPRResult> lprResults, tmpLprResults;
            MultipartMemoryStreamProvider streamProvider;
            byte[] tmpBuff;
            ResponseViewModel<String> ret;
            List<string> listNumber;
            lprService = new LPRAPI(HttpContext.Current.Server.MapPath(@"~\bin"));
            lprResults = new List<LPRResult>();

            //streamProvider = Request.Content.ReadAsMultipartAsync().Result;
            streamProvider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(streamProvider);
            if (streamProvider != null &&
               streamProvider.Contents != null &&
               streamProvider.Contents.Count > 0)
            {
                for (int i = 0; i < streamProvider.Contents.Count; i++)
                {
                    tmpBuff = streamProvider.Contents[i].ReadAsByteArrayAsync().Result;
                    lprService.PlateLicenseReconigze(tmpBuff, out tmpLprResults);
                    if (tmpLprResults != null && tmpLprResults.Count > 0)
                    {
                        lprResults.AddRange(tmpLprResults);
                    }
                }
            }

            listNumber = new List<string>();
            if (lprResults.Count > 0)
            {
                for (int i = 0; i < lprResults.Count; i++)
                {
                    listNumber.Add(System.Text.Encoding.UTF8.GetString(lprResults[i].plateText));
                }
            }
            ret = ResponseConstructor<String>.ConstructEnumerableData(ResponseCode.SUCCESS, listNumber);

            return ret;
        }

        [HttpPost]
        [Route("BadgeReader/GetStates", Name = "GetBadgeReaderStates")]
        public async Task<List<BadgeReader>> GetBadgeReaderStates([FromBody]List<BadgeReader> badgeReaderViews)
        {
            List<BadgeReader> ret;
            ret = await DeviceConfig._deviceService.GetManyBadgeReaderState(badgeReaderViews);
            return ret;
        }

        [HttpPost]
        [Route("BadgeReader/CheckConection", Name = "CheckBadgeReaderConnection")]
        public async Task<int> CheckBadgeReaderConnection([FromBody]BadgeReader badgeReader)
        {
            return DeviceConfig._deviceService.CheckBadgeReaderConnection(badgeReader);
        }

        [HttpPost]
        [Route("Controller/GetStates", Name = "GetControllerStates")]
        public async Task<List<Controller>> GetControllerStattes([FromBody]List<Controller> controllerViews)
        {
            List<Controller> ret;
            ret = await DeviceConfig._deviceService.GetManyControllerState(controllerViews);
            return ret;
        }

        [HttpPost]
        [Route("Sensor/GetStates", Name = "GetSensorStates")]
        public async Task<List<Sensor>> GetSensorStattes([FromBody]List<Sensor> sensorViews)
        {
            List<Sensor> ret;
            ret = await DeviceConfig._deviceService.GetManySensorState(sensorViews);
            return ret;
        }

        [HttpPost]
        [Route("Barrier/GetStates", Name = "GetBarrierStates")]
        public async Task<List<Barrier>> GetBarrierStattes([FromBody]List<Barrier> barrierViews)
        {
            List<Barrier> ret;
            ret = await DeviceConfig._deviceService.GetManyBarrierState(barrierViews);
            return ret;
        }

        [HttpPost]
        [Route("HazardLight/GetStates", Name = "GetHazardLightStates")]
        public async Task<List<HazardLight>> GetHazardLightStattes([FromBody]List<HazardLight> hazardLightViews)
        {
            List<HazardLight> ret;
            ret = await DeviceConfig._deviceService.GetManyHazardLightState(hazardLightViews);
            return ret;
        }

        [HttpPost]
        [Route("Camera/GetStates", Name = "GetCameraStates")]
        public async Task<List<Camera>> GetCameraStattes([FromBody]List<Camera> cameraViews)
        {
            List<Camera> ret;
            ret = await DeviceConfig._deviceService.GetManyCameraState(cameraViews);
            return ret;
        }

        [HttpPost]
        [Route("Weighbridge/GetStates", Name = "GetWeighbridgeStates")]
        public async Task<List<WeighBridge>> GetWeighbridgeStattes([FromBody]List<WeighBridge> weightbridgeViews)
        {
            List<WeighBridge> ret;
            ret = await DeviceConfig._deviceService.GetManyWeighbridgeState(weightbridgeViews);
            return ret;
        }

        [HttpPost]
        [Route("Camera/UpdateStates", Name = "UpdateCameraState")]
        public async Task<bool> UpdateCameraState([FromBody]List<Camera> cameraViews)
        {
            bool ret;
            ret = await DeviceConfig._deviceService.UpdateCameraState(cameraViews);
            return ret;
        }

        [HttpPost]
        [Route("Weighbridge/UpdateStates", Name = "UpdateWeighbridgeState")]
        public async Task<bool> UpdateWeighbridgeState([FromBody]List<WeighBridge> weighbridgeViews)
        {
            bool ret;
            ret = await DeviceConfig._deviceService.UpdateWeighbridgeState(weighbridgeViews);
            return ret;
        }

        [HttpGet]
        [Route("Barrier/Open/{barrierCode}", Name = "OpenBarrier")]
        public async Task<bool> OpenBarrier(string barrierCode)
        {
            bool ret;
            ret = await DeviceConfig._deviceService.OpenBarrier(barrierCode);
            return ret;
        }

        [HttpGet]
        [Route("Barrier/Close/{barrierCode}", Name = "CloseBarrier")]
        public async Task<bool> CloseBarrier(string barrierCode)
        {
            bool ret;
            ret = await DeviceConfig._deviceService.CloseBarrier(barrierCode);
            return ret;
        }

        [HttpGet]
        [Route("HazardLight/Open/{hazardLightCode}", Name = "OpenHazardLight")]
        public async Task<bool> OpenHazardLight(string hazardLightCode)
        {
            bool ret;
            ret = await DeviceConfig._deviceService.OpenHazardLight(hazardLightCode);
            return ret;
        }

        [HttpGet]
        [Route("HazardLight/Close/{hazardLightCode}", Name = "CloseHazardLight")]
        public async Task<bool> CloseHazardLight(string hazardLightCode)
        {
            bool ret;
            ret = await DeviceConfig._deviceService.CloseHazardLight(hazardLightCode);
            return ret;
        }

    }
}
