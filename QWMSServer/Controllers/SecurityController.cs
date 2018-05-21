using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using System.Net.Http;
using System.Net;
using System.Web.Hosting;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using QWMSServer.Filter;

namespace QWMSServer.Controllers
{
    [RoutePrefix("Security")]
    public class SecurityController : ApiController
    {
        ISecurityServicecs _securityServices;
        public SecurityController (ISecurityServicecs securityServicecs)
        {
            _securityServices = securityServicecs;
        }

        //[AuthenticateRequire]
        [HttpGet]
        [Route("Get/Trucks/{truckCondition}", Name = "GetTrucks")]
        public async Task<ResponseViewModel<QueueListViewModel>> GetTrucks(string truckCondition)
        {
            return await _securityServices.GetTrucks(truckCondition);
        }

        [HttpGet]
        [Route("Get/GatePass/{rfidNo}", Name = "GetGatePassByRFID")]
        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByRFID(string rfidNo)
        {
            return await _securityServices.GetGatePassByRFID(rfidNo);
        }

        [HttpGet]
        [Route("Get/RegisterSecurityCheck/{rfidNo}", Name = "RegisterSecurityCheck")]
        public async Task<ResponseViewModel<GatePassViewModel>> RegisterSecurityCheck(string rfidNo)
        {
            return await _securityServices.RegisterSecurityCheck(rfidNo);
        }

        [HttpPost]
        [Route("Post/ConfirmSecurityCheck", Name = "ConfirmSecurityCheck")]
        public async Task<ResponseViewModel<GatePassViewModel>> ConfirmSecurityCheck([FromBody] SecurityUpdateStateViewModel updateStateView)
        {
            return await _securityServices.ConfirmSecurityCheck(updateStateView);
        }

        [HttpGet]
        [Route("Get/Image")]
        public async Task<HttpResponseMessage> GetImage()
        {
            HttpResponseMessage resMesg;
            MultipartFormDataContent MIMEContent;
            string filePath;
            ResponseViewModel<RFIDCardViewModel> ret = new ResponseViewModel<RFIDCardViewModel>();
            
            filePath = Request.Headers.GetValues("filePath").First();
            MIMEContent = _securityServices.GetDriverImage(filePath);

            if(MIMEContent != null)
            {
                resMesg = new HttpResponseMessage(HttpStatusCode.OK);
                resMesg.Content = MIMEContent;
            }
            else
            {
                resMesg = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            
            return resMesg;
        }
    }
}