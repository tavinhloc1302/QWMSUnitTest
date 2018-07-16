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

            streamProvider = Request.Content.ReadAsMultipartAsync().Result;
            if (streamProvider != null &&
               streamProvider.Contents != null &&
               streamProvider.Contents.Count > 0)
            {
                for (int i = 0; i < streamProvider.Contents.Count; i++)
                {
                    tmpBuff = streamProvider.Contents[i].ReadAsByteArrayAsync().Result;
                    lprService.PlateLicenseReconigze(tmpBuff, out tmpLprResults);
                    if (tmpLprResults.Count > 0)
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

        [HttpGet]
        [Route("BadgeReader/GetAllStatus", Name = "GetAllBadgeReaderStatus")]
        public async Task<bool> GetAllBadgeReaderStatus()
        {
            bool stt;
            stt = DeviceConfig._deviceService.GetAllStatus();
            return stt;
        }

        [HttpGet]
        [Route("BadgeReader/GetStatus/{rfidReaderCode}", Name = "GetBadgeReaderStatus")]
        public async Task<bool> GetBadgeReaderStatus(string rfidReaderCode)
        {
            bool stt;
            stt = DeviceConfig._deviceService.GetStatus(rfidReaderCode);
            return stt;
        }

        [HttpGet]
        [Route("BadgeReader/GetRFIDCode/{rfidReaderCode}", Name = "GetRFIDCode")]
        public async Task<string> GetRFIDCode(string rfidReaderCode)
        {
            string uid;
            uid = DeviceConfig._deviceService.GetUID(rfidReaderCode, true);
            return uid;
        }

        [HttpPost]
        [Route("BadgeReader/CheckConection", Name = "CheckBadgeReaderConnection")]
        public async Task<bool> CheckBadgeReaderConnection([FromBody]BadgeReader badgeReader)
        {
            return DeviceConfig._deviceService.CheckBadgeReaderConnection(badgeReader);
        }
    }
}
