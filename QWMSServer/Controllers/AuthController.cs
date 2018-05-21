using QWMSServer.Data.Services;
using QWMSServer.Filter;
using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
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

        //[AuthenticateRequire]
        [HttpGet]
        [Route("GetPermission/{userID}", Name = "GetPermission")]
        public async Task<ResponseViewModel<SystemFunctionViewModel>> GetUserPermission(int userID)
        {
            return await _authService.GetUserPermission(userID);
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

        //[AuthenticateRequire]
        [HttpPost]
        [Route("User/Logout", Name = "Logout")]
        public Task<bool> Logout()
        {
            var token = Request.Headers.GetValues("Token").First();
            return Task.FromResult(_authService.Logout(token));
        }
    }
}