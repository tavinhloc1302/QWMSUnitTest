using QWMSServer.Data.Services;
using QWMSServer.Filter;
using QWMSServer.Model.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace QWMSServer.Controllers
{
    [RoutePrefix("Auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GetPermission/{userID}", Name = "GetPermission")]
        public async Task<ResponseViewModel<SystemFunctionViewModel>> GetUserPermission(int userID)
        {
            return await _authService.GetUserPermission(userID);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("DumpRequest", Name = "DumpRequest")]
        public async Task<ResponseViewModel<GenericResponseModel>> DumpRequest()
        {
            ResponseViewModel<GenericResponseModel> rp = new ResponseViewModel<GenericResponseModel>();
            rp.booleanResponse = true;
            return rp;
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("DumpRequestWithToken", Name = "DumpRequestWithToken")]
        public async Task<ResponseViewModel<GenericResponseModel>> DumpRequestWithToken()
        {
            ResponseViewModel<GenericResponseModel> rp = new ResponseViewModel<GenericResponseModel>();
            rp.booleanResponse = true;
            return rp;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("User/Login", Name = "Login")]
        public async Task<ResponseViewModel<UserViewModel>> Login([FromBody]LoginViewModel loginViewModel)
        {
            loginViewModel.UserHostAddress = HttpContext.Current.Request.UserHostAddress;
            loginViewModel.UserAgent = HttpContext.Current.Request.UserAgent;
            return await _authService.Login(loginViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("User/Logout", Name = "Logout")]
        public Task<bool> Logout()
        {
            var token = Request.Headers.GetValues("Token").First();
            return _authService.Logout(token);
        }
    }
}