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
        public async Task<ResponseViewModel<GatePassViewModel>> ConfirmSecurityCheck([FromBody] GatePassViewModel updateStateView)
        {
            return await _securityServices.ConfirmSecurityCheck(updateStateView);
        }

        [HttpGet]
        [Route("Get/Image/{imagePath}")]
        public async Task<HttpResponseMessage> GetImage(string imagePath)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            String filePath = HostingEnvironment.MapPath("~/Images/bitlogo.jpg");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Image image = Image.FromStream(fileStream);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            result.Content = new ByteArrayContent(memoryStream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            fileStream.Close();

            return result;
        }
    }
}