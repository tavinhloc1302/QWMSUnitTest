using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Services
{
    public interface IAuthService
    {
        /* Login service */
        Task<ResponseViewModel<SystemFunctionViewModel>> GetUserPermission(int userID);
        Task<ResponseViewModel<UserViewModel>> Login(LoginViewModel loginViewModel);
        Task<bool> Logout(string tokenString);
        Task<bool> CheckUserPermission(int empID, string empRFIDCode, string APIName);

        /* Token Handler */
        TokenViewModel GenerateToken(int userID, string username, string password, string ip, string userAgent, long ticks);
        bool ValidateToken(string tokenID);
        bool RemoveToken(string tokenID);
    }
}
