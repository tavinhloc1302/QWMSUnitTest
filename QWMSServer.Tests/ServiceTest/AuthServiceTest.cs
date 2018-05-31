using Microsoft.VisualStudio.TestTools.UnitTesting;
using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Tests.ServiceTest
{
    [TestClass]
    public class AuthServiceTest
    {
        private readonly IAuthService _authService;

        [TestMethod]
        public async Task TestMethod_GetUserPermission()
        {
            var actualResult = await _authService.GetUserPermission(1);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetUserPermission_ShouldFail()
        {
            var actualResult = await _authService.GetUserPermission(0);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_Login()
        {
            LoginViewModel viewModel = new LoginViewModel
            {
                Email = "silencieux.le@gmail.com",
                Password = "213123123",
                UserAgent = "Chrome",
                UserHostAddress = "1.1.1.1"
            };
            var actualResult = await _authService.Login(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_Login_ShouldFail_NoInfo()
        {
            LoginViewModel viewModel = new LoginViewModel();
            var actualResult = await _authService.Login(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_Login_ShouldFail_NullModel()
        {
            var actualResult = await _authService.Login(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public void TestMethod_Logout()
        {

            var actualResult = _authService.Logout("0123");
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestMethod_Logout_ShouldFail()
        {
            var actualResult = _authService.Logout(null);
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public async Task TestMethod_CheckUserPermission()
        {
            var actualResult = await _authService.CheckUserPermission(1, "0123", "0123");
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async Task TestMethod_CheckUserPermission_ShouldFail()
        {
            var actualResult = await _authService.CheckUserPermission(0, null, null);
            Assert.IsNotNull(actualResult);
        }
        /* Token Handler */

        [TestMethod]
        public void TestMethod_GenerateToken()
        {
            var actualResult = _authService.GenerateToken(1, "username", "password", "123", "123", 0);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public void TestMethod_GenerateToken_ShouldFail()
        {
            var actualResult = _authService.GenerateToken(0, null, null, null, null, 0);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public void TestMethod_ValidateToken()
        {
            var actualResult = _authService.ValidateToken("0123");
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestMethod_ValidateToken_ShouldFail()
        {
            var actualResult =  _authService.ValidateToken(null);
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestMethod_RemoveToken()
        {
            var actualResult = _authService.RemoveToken("0123");
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestMethod_RemoveToken_ShouldFail()
        {
            var actualResult = _authService.RemoveToken(null);
            Assert.IsTrue(actualResult);
        }
    }
}
