using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;
using AutoMapper;
using QWMSServer.Data.Common;

namespace QWMSServer.Data.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ICarrierVendorRepository _carrierRepository;
        private readonly IUserRepository _userRepository;
        private Random _random = new Random();

        public AdminService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IDriverRepository driverRepository, ICarrierVendorRepository carrierRepository,
                            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _driverRepository = driverRepository;
            _carrierRepository = carrierRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        /* Customer management block */
        #region
        public async Task<ResponseViewModel<CustomerViewModel>> GetAllCustomer()
        {
            var result = await _customerRepository.GetManyAsync( c => c.isDelete == false);
            var customerView = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(result);
            ResponseViewModel<CustomerViewModel> responseViewModel = new ResponseViewModel<CustomerViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CustomerViewModel>> SearchCustomer(string code)
        {
            ResponseViewModel<CustomerViewModel> responseViewModel = new ResponseViewModel<CustomerViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _customerRepository.GetManyAsync(c => (c.nameVi.Contains(code) || c.nameEn.Contains(code) || c.shortName.Contains(code)) && c.isDelete == false, QueryIncludes.CUSTOMERFULLINCUDES);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<CustomerViewModel>> GetCustomerByCode(string code)
        {
            ResponseViewModel<CustomerViewModel> responseViewModel = new ResponseViewModel<CustomerViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _customerRepository.GetAsync(c => c.code.Equals(code), null);
                if (result == null)
                    responseViewModel.errorText = "Querry is Null";
                responseViewModel.responseData = Mapper.Map<Customer, CustomerViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<CustomerViewModel>> CreateNewCustomer(CustomerViewModel customerView)
        {
            ResponseViewModel<CustomerViewModel> responseViewModel = new ResponseViewModel<CustomerViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (customerView != null)
            {
                Customer customer = Mapper.Map<CustomerViewModel, Customer>(customerView);
                if (customer != null)
                {
                    customer.isDelete = false;
                    _customerRepository.Add(customer);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetCustomerByCode(customer.code);
                        responseViewModel.errorText = "Save Success";
                    }
                    else
                    {
                        responseViewModel.errorText = "Save Fail";
                    }
                }
                else
                {
                    responseViewModel.errorText = "Cannot map";
                }
            }
            else
            {
                responseViewModel.errorText = "Input is null";
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CustomerViewModel>> DeleteCustomer(CustomerViewModel customerView)
        {
            ResponseViewModel<CustomerViewModel> responseViewModel = new ResponseViewModel<CustomerViewModel>();
            if (customerView != null)
            {
                Customer customer = await _customerRepository.GetAsync(cs => cs.code.Equals(customerView.code));
                if (customer != null)
                {
                    customer.isDelete = true;
                    _customerRepository.Update(customer);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllCustomer();
                    }
                    else
                    {
                        responseViewModel.errorText = "Save Fail";
                    }
                }
                else
                {
                    responseViewModel.errorText = "Cannot map";
                }
            }
            else
            {
                responseViewModel.errorText = "Input is null";
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CustomerViewModel>> UpdateCustomer(CustomerViewModel customerView)
        {
            ResponseViewModel<CustomerViewModel> responseViewModel = new ResponseViewModel<CustomerViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (customerView != null)
            {
                Customer customer = Mapper.Map<CustomerViewModel, Customer>(customerView);
                if (customer != null)
                {
                    _customerRepository.Update(customer);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetCustomerByCode(customer.code);
                        responseViewModel.errorText = "Save Success";
                    }
                    else
                    {
                        responseViewModel.errorText = "Save Fail";
                    }
                }
                else
                {
                    responseViewModel.errorText = "Cannot map";
                }
            }
            else
            {
                responseViewModel.errorText = "Input is null";
            }
            return responseViewModel;
        }
        #endregion

        /* Driver management block */
        #region
        public async Task<ResponseViewModel<DriverViewModel>> GetAllDriver()
        {
            var result = await _driverRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.DRIVERFULLINCLUDES);
            var driverView = Mapper.Map<IEnumerable<Driver>, IEnumerable<DriverViewModel>>(result);
            ResponseViewModel<DriverViewModel> responseViewModel = new ResponseViewModel<DriverViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<Driver>, IEnumerable<DriverViewModel>>(result);
            return responseViewModel;
        }
        
        public async Task<ResponseViewModel<DriverViewModel>> GetDriverByCode(string code)
        {
            ResponseViewModel<DriverViewModel> responseViewModel = new ResponseViewModel<DriverViewModel>();
            if (code != null)
            {
                var result = await _driverRepository.GetAsync(c => c.code.Equals(code), QueryIncludes.DRIVERFULLINCLUDES);
                if (result == null)
                    responseViewModel.errorText = "Querry is Null";
                responseViewModel.responseData = Mapper.Map<Driver, DriverViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "INput Code is null";
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<DriverViewModel>> SearchDriver(string code)
        {
            ResponseViewModel<DriverViewModel> responseViewModel = new ResponseViewModel<DriverViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _driverRepository.GetManyAsync(c => c.nameVi.Contains(code) && c.isDelete == false, QueryIncludes.DRIVERFULLINCLUDES);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Driver>, IEnumerable<DriverViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<DriverViewModel>> CreateNewDriver(DriverViewModel driverView)
        {
            ResponseViewModel<DriverViewModel> responseViewModel = new ResponseViewModel<DriverViewModel>();
            if (driverView != null)
            {
                Driver driver = Mapper.Map<DriverViewModel, Driver>(driverView);
                if (driver != null)
                {
                    driver.isDelete = false;
                    driver.carrierVendor = await _carrierRepository.GetAsync( ca => ca.code == driverView.carrierVendor.code);
                    _driverRepository.Add(driver);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetDriverByCode(driver.code);
                        responseViewModel.errorText = "Save Success";
                    }
                    else
                    {
                        responseViewModel.errorText = "Save Fail";
                    }
                }
                else
                {
                    responseViewModel.errorText = "Cannot Map";
                }
            }
            else
            {
                responseViewModel.errorText = "Input is null";
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<DriverViewModel>> DeleteDriver(DriverViewModel driverView)
        {
            ResponseViewModel<DriverViewModel> responseViewModel = new ResponseViewModel<DriverViewModel>();
            if (driverView != null)
            {
                Driver driver = await _driverRepository.GetAsync(dr => dr.code.Equals(driverView.code));
                if (driver != null)
                {
                    driver.isDelete = true;
                    _driverRepository.Update(driver);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllDriver();
                    }
                    else
                    {
                        responseViewModel.errorText = "Save fail";
                    }
                }
                else
                {
                    responseViewModel.errorText = "Mapper fail";
                }
            }
            else
            {
                responseViewModel.errorText = "Input is null";
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<DriverViewModel>> UpdateDriver(DriverViewModel driverView)
        {
            ResponseViewModel<DriverViewModel> responseViewModel = new ResponseViewModel<DriverViewModel>();
            if (driverView != null)
            {
                Driver driver = Mapper.Map<DriverViewModel, Driver>(driverView);
                if (driver != null)
                {
                    driver.carrierVendor = await _carrierRepository.GetAsync(ca => ca.code == driverView.carrierVendor.code);
                    driver.carrierVendorID = driver.carrierVendor.ID;
                    _driverRepository.Update(driver);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetDriverByCode(driver.code);
                        responseViewModel.errorText = "Save Success";
                    }
                    else
                    {
                        responseViewModel.errorText = "Save fail";
                    }
                }
                else
                {
                    responseViewModel.errorText = "Mapper fail";
                }
            }
            else
            {
                responseViewModel.errorText = "Input is null";
            }
            return responseViewModel;
        }
        #endregion

        /* Carrier management block */
        #region
        public async Task<ResponseViewModel<CarrierVendorViewModel>> GetAllCarrier()
        {
            var result = await _carrierRepository.GetManyAsync(c => c.isDelete == false, null);
            ResponseViewModel<CarrierVendorViewModel> responseViewModel = new ResponseViewModel<CarrierVendorViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<CarrierVendor>, IEnumerable<CarrierVendorViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CarrierVendorViewModel>> SearchCarrier(string code)
        {
            ResponseViewModel<CarrierVendorViewModel> responseViewModel = new ResponseViewModel<CarrierVendorViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _carrierRepository.GetManyAsync(c => (c.nameVi.Contains(code) || c.nameEn.Contains(code) || c.shortName.Contains(code)) && c.isDelete == false, null);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<CarrierVendor>, IEnumerable<CarrierVendorViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<CarrierVendorViewModel>> GetCarrierByCode(string code)
        {
            ResponseViewModel<CarrierVendorViewModel> responseViewModel = new ResponseViewModel<CarrierVendorViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _carrierRepository.GetAsync(c => c.code.Equals(code), null);
                if (result == null)
                    responseViewModel.errorText = "Querry is Null";
                responseViewModel.responseData = Mapper.Map<CarrierVendor, CarrierVendorViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CarrierVendorViewModel>> CreateNewCarrier(CarrierVendorViewModel carrierView)
        {
            ResponseViewModel<CarrierVendorViewModel> responseViewModel = new ResponseViewModel<CarrierVendorViewModel>();
            if (carrierView != null)
            {
                CarrierVendor carrierVendor = Mapper.Map<CarrierVendorViewModel, CarrierVendor>(carrierView);
                if (carrierVendor != null)
                {
                    //string code = "5101001895";
                    //while ((await this.GetCarrierByCode(code)).responseData != null)
                    //{
                    //    code = "5101" + _random.Next(999999).ToString();
                    //}
                    //carrierVendor.code = code;
                    carrierVendor.isDelete = false;
                    _carrierRepository.Add(carrierVendor);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetCarrierByCode(carrierVendor.code);
                    }
                    else
                    {
                        responseViewModel.errorText = "Save Fail";
                    }
                }
                else
                {
                    responseViewModel.errorText = "Cannot map";
                }
            }
            else
            {
                responseViewModel.errorText = "Input is null";
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CarrierVendorViewModel>> UpdateCarrier(CarrierVendorViewModel carrierView)
        {
            ResponseViewModel<CarrierVendorViewModel> responseViewModel = new ResponseViewModel<CarrierVendorViewModel>();
            if (carrierView != null)
            {
                CarrierVendor carrierVendor = Mapper.Map<CarrierVendorViewModel, CarrierVendor>(carrierView);
                if (carrierVendor != null)
                {
                    _carrierRepository.Update(carrierVendor);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetCarrierByCode(carrierVendor.code);
                    }
                    else
                    {
                        responseViewModel.errorText = "Save Fail";
                    }
                }
                else
                {
                    responseViewModel.errorText = "Cannot map";
                }
            }
            else
            {
                responseViewModel.errorText = "Input is null";
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CarrierVendorViewModel>> DeleteCarrier(CarrierVendorViewModel carrierView)
        {
            ResponseViewModel<CarrierVendorViewModel> responseViewModel = new ResponseViewModel<CarrierVendorViewModel>();
            if (carrierView != null)
            {
                CarrierVendor carrierVendor = Mapper.Map<CarrierVendorViewModel, CarrierVendor>(carrierView);
                carrierVendor.isDelete = true;
                if (carrierVendor != null)
                {
                    _carrierRepository.Update(carrierVendor);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllCarrier();
                    }
                    else
                    {
                        responseViewModel.errorText = "Save Fail";
                    }
                }
                else
                {
                    responseViewModel.errorText = "Cannot map";
                }
            }
            else
            {
                responseViewModel.errorText = "Input is null";
            }
            return responseViewModel;
        }

        #endregion


        /* User Group Management */
        public async Task<List<SystemFunctionViewModel>> GetUserPermission(int userID)
        {
            Dictionary<string, SystemFunctionViewModel> systemFunctionViewModels = new Dictionary<string, SystemFunctionViewModel>();
            var user = await _userRepository.GetAsync(us => us.ID == userID, QueryIncludes.USERFULLINCLUDES);
            if (user==null)
            {
                return null;
            }
            else
            {
                var employee = user.employees.FirstOrDefault();
                foreach (var group in employee.groupMaps)
                {
                    foreach (var function in group.employeeGroup.functionMaps)
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
                }
            }
            return systemFunctionViewModels.Values.ToList();
        }
    }

}
