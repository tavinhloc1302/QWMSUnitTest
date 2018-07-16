using AutoMapper;
using QWMSServer.Data.Common;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAdminService _adminService;
        private readonly IEmployeeRepository _employeeRepository;
        private const string _alg = "HmacSHA256";
        private const string _salt = "hl2lBkZTMrTcWlZwDPiK";

        public AuthService(IUnitOfWork unitOfWork, ITokenRepository tokenRepository, IUserRepository userRepository, IEmployeeRepository employeeRepository, IAdminService adminService)
        {
            _unitOfWork = unitOfWork;
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _adminService = adminService;
        }

        public TokenViewModel GenerateToken(int userID, string username, string password, string ip, string userAgent, long ticks)
        {
            string hash = string.Join(":", new string[] { username, ip, userAgent, ticks.ToString() });
            string hashLeft = "";
            string hashRight = "";

            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(GetHashedPassword(password));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { username, ticks.ToString() });
            }
            string tokenString = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
            var issuedOn = DateTime.Now;

            var token = new Token
            {
                UserId = userID,
                TokenString = tokenString,
                IssuedOn = issuedOn,
                ExpiresIn = 86400
            };
            _tokenRepository.Add(token);

            _unitOfWork.SaveChanges();
            return Mapper.Map<Token, TokenViewModel>(token);
        }

        public bool RemoveToken(string tokenString)
        {
            _tokenRepository.Delete( tk => tk.TokenString == tokenString);
            return _unitOfWork.SaveChanges();
        }

        public bool ValidateToken(string tokenString)
        {
            var token = _tokenRepository.Get(tk => tk.TokenString == tokenString);
            if (token != null && token.ExpiresOn > DateTime.Now)
                return true;
            return false;
        }

        private string GetHashedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, _salt });
            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
                return Convert.ToBase64String(hmac.Hash);
            }
        }

        public async Task<ResponseViewModel<SystemFunctionViewModel>> GetUserPermission(int userID)
        {
            ResponseViewModel<SystemFunctionViewModel> response = new ResponseViewModel<SystemFunctionViewModel>();
            Dictionary<string, SystemFunctionViewModel> systemFunctionViewModels = new Dictionary<string, SystemFunctionViewModel>();
            var user = await _userRepository.GetAsync(us => us.ID == userID, QueryIncludes.USERFULLINCLUDES);
            if (user == null)
            {
                return response = ResponseConstructor<SystemFunctionViewModel>.ConstructData(ResponseCode.ERR_USER_NOT_EXSIT, null);
            }
            else
            {
                foreach (var function in user.employee.employeeGroup.functionMaps)
                {
                    try
                    {
                        systemFunctionViewModels.Add(function.systemFunction.Code, Mapper.Map<SystemFunction, SystemFunctionViewModel>(function.systemFunction));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                response = ResponseConstructor<SystemFunctionViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, systemFunctionViewModels.Values.ToList());
            }
            return response;
        }

        public async Task<ResponseViewModel<UserViewModel>> Login(LoginViewModel loginViewModel)
        {
            var constrains = await _adminService.GetAllConstrain();
            var maxLoginTimes = constrains.responseDatas.Where(cs => cs.name == ConstrainName.PASS_LOGIN_TIMES).ToList().FirstOrDefault().value;
            var expireddays = constrains.responseDatas.Where(cs => cs.name == ConstrainName.PASS_EXPIRED).ToList().FirstOrDefault().value;
            ResponseViewModel<UserViewModel> response = new ResponseViewModel<UserViewModel>();
            // Check number of online user
            var count = await _userRepository.GetManyAsync(u => u.isActive == true && u.isDelete == false, null);
            if(count.Count() >= Constant.MAX_LOGIN_USER)
            {
                return response = ResponseConstructor<UserViewModel>.ConstructData(ResponseCode.ERR_MAX_LOGIN_SUPPORT, ResponseText.ERR_MAX_LOGIN, null);
            }

            var user = await _userRepository.GetAsync(u => u.username == loginViewModel.Email && u.isDelete == false, QueryIncludes.USERFULLINCLUDES);
            // Check if user exist
            if (user == null)
            {
                return response = ResponseConstructor<UserViewModel>.ConstructData(ResponseCode.ERR_USER_NOT_EXSIT, ResponseText.ERR_INVALID_USER_NAME, null);
            }

            if(user.isBlock == true)
            {
                return response = ResponseConstructor<UserViewModel>.ConstructData(ResponseCode.ERR_INVALID_LOGIN, ResponseText.ERR_BLOCKED_USER_EXCEED_LOGIN, null);
            }

            // Check if password is valid
            //var userPassword = user.userPasswords.Where(p => p.isUsing == true).FirstOrDefault();
            var haspass = Crypt.ToSha256(loginViewModel.Password);
            if (user.password != haspass)
            {
                user.loginTimes += 1;
                if (user.loginTimes > maxLoginTimes)
                {
                    user.isBlock = true;
                    _userRepository.Update(user);
                    await _unitOfWork.SaveChangesAsync();
                    return response = ResponseConstructor<UserViewModel>.ConstructData(ResponseCode.ERR_INVALID_LOGIN, ResponseText.ERR_BLOCKED_USER_EXCEED_LOGIN, null);
                }
                _userRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();
                return response = ResponseConstructor<UserViewModel>.ConstructData(ResponseCode.ERR_INVALID_LOGIN, ResponseText.ERR_INVALID_PASSWORD, null);
            }

            if(DateTime.Now.Subtract(user.userPasswords.Last().createDate).TotalDays > expireddays)
            {
                user.isBlock = true;
                _userRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();
                return response = ResponseConstructor<UserViewModel>.ConstructData(ResponseCode.ERR_INVALID_LOGIN, ResponseText.ERR_BLOCKED_USER_EXCEED_EXPIRE, null);
            }else if (expireddays - DateTime.Now.Subtract(user.userPasswords.Last().createDate).TotalDays < 7)
            {
                response.errorCode = ResponseCode.PASSWORD_CHANGE_REQUIRE;
                response.errorText = ResponseText.PASSWORD_CHANGE_REQUIRE;
            }
            else
            {
                response.errorCode = ResponseCode.SUCCESS;
            }

            user.loginTimes = 0;
            user.isActive = true;
            user.loginTime = DateTime.Now;
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            _tokenRepository.Delete(tk => tk.UserId == user.ID);
            _unitOfWork.SaveChanges();

            var token = this.GenerateToken(user.ID, user.username, user.password, loginViewModel.UserHostAddress,
                                            loginViewModel.UserAgent, DateTime.Now.Ticks);
            UserViewModel viewModel = Mapper.Map<User, UserViewModel>(user);
            viewModel.token = token;
            response.responseData = viewModel;
            //response = ResponseConstructor<UserViewModel>.ConstructData(ResponseCode.SUCCESS, viewModel);

            return response;
        }

        public async Task<bool> Logout(string tokenString)
        {
            var curToken = _tokenRepository.Get(tk => tk.TokenString.Equals(tokenString));
            var curUser = await _userRepository.GetAsync(u => u.ID == curToken.UserId);
            curUser.isActive = false;
            _userRepository.Update(curUser);
            await _unitOfWork.SaveChangesAsync();
            return this.RemoveToken(tokenString);
        }


        public async Task<bool> CheckUserPermission(int empID, string empRFIDCode, string APIName)
        {
            //var employee = await _employeeRepository.GetAsync(emp => emp.rfidCard.code.Equals(empRFIDCode) && emp.isDelete == false, QueryIncludes.EMPLOYEEFULLINCLUDES);
            //if (employee == null)
            //    return false;
            //if (employee.ID != empID)
            //    return false;
            //bool found = false;
            //foreach (var group in employee.groupMaps)
            //{
            //    foreach (var function in group.employeeGroup.functionMaps)
            //    {
            //        if (function.systemFunction.API.Equals(APIName))
            //            found = true;
            //    }
            //}
            //if (found == false)
            //    return false;
            return true;
        }

    }
}
