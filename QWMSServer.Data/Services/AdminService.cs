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
        private readonly IMaterialRepository _materialRepository;
        private readonly IUnitTypeRepository _unittypeRepository;
        private readonly ITruckRepository _truckRepository;
        private readonly ITruckTypeRepository _truckTypeRepository;
        private readonly ILoadingTypeRepository _loadingTypeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeGroupRepository _employeeGroupRepository;
        private readonly IEmployeeRoleRepository _employeeRoleRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly ILoadingBayRepository _loadingBayRepository;
        private readonly ILaneRepository _laneRepository;
        private readonly IRFIDCardRepository _rdifCardRepository;
        private readonly ICameraRepository _cameraRepository;
        private readonly IConstrainRepository _constrainRepository;
        private readonly IDeliveryOrderRepository _doRepository;
        private readonly ICustomerWarehouseRepository _customerWarehouseRepository;
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IWeighBridgeRepository _weighBridgeRepository;
        private readonly IPrintHeaderRepository _printHeaderRepository;
        private readonly IUserPasswordRepository _userPasswordRepository;
        private readonly ISystemFunctionRepository _systemFunctionRepository;
        private readonly IEmployeeGroup_SystemFunctionRepository _employeeGroup_SystemFunctionRepository;
        private readonly IUserPCRepository _userPCRepository;
        private readonly IBadgeReaderRepository _badgeReaderRepository;
        private readonly IWeightRecordRepository _weightRecordRepository;
        private Random _random = new Random();

        public AdminService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IDriverRepository driverRepository, ICarrierVendorRepository carrierRepository,
							IUserRepository userRepository, IMaterialRepository materialRepository, IUnitTypeRepository unitypeRepository, ITruckRepository truckRepository, 
                            ITruckTypeRepository truckTypeRepository, ILoadingTypeRepository loadingTypeRepository, IEmployeeRepository employeeRepository,
                            IEmployeeGroupRepository employeeGroupRepository, IEmployeeRoleRepository employeeRoleRepository, IPlantRepository plantRepository,
                            ICompanyRepository companyRepository, IWarehouseRepository warehouseRepository, ILoadingBayRepository loadingBayRepository,
                            ILaneRepository laneRepository, IRFIDCardRepository rdifCardRepository, ICameraRepository cameraRepository, IConstrainRepository constrainRepository,
                            IDeliveryOrderRepository doRepository, ICustomerWarehouseRepository customerWarehouseRepository,
                            ISaleOrderRepository saleOrderRepository, IOrderRepository orderRepository, IWeighBridgeRepository weighBridgeRepository, IPrintHeaderRepository printHeaderRepository,
                            IUserPasswordRepository userPasswordRepository, ISystemFunctionRepository systemFunctionRepository, IEmployeeGroup_SystemFunctionRepository employeeGroup_SystemFunctionRepository,
                            IUserPCRepository userPCRepository, IBadgeReaderRepository badgeReaderRepository, IWeightRecordRepository weightRecordRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _driverRepository = driverRepository;
            _carrierRepository = carrierRepository;
            _userRepository = userRepository;
            _truckRepository = truckRepository;
            _materialRepository = materialRepository;
            _unittypeRepository = unitypeRepository;
            _truckTypeRepository = truckTypeRepository;
            _loadingTypeRepository = loadingTypeRepository;
            _employeeRepository = employeeRepository;
            _employeeGroupRepository = employeeGroupRepository;
            _employeeRoleRepository = employeeRoleRepository;
            _plantRepository = plantRepository;
            _companyRepository = companyRepository;
            _warehouseRepository = warehouseRepository;
            _loadingBayRepository = loadingBayRepository;
            _laneRepository = laneRepository;
            _rdifCardRepository = rdifCardRepository;
            _cameraRepository = cameraRepository;
            _constrainRepository = constrainRepository;
            _doRepository = doRepository;
            _customerWarehouseRepository = customerWarehouseRepository;
            _saleOrderRepository= saleOrderRepository;
            _orderRepository = orderRepository;
            _weighBridgeRepository = weighBridgeRepository;
            _printHeaderRepository = printHeaderRepository;
            _userPasswordRepository = userPasswordRepository;
            _systemFunctionRepository = systemFunctionRepository;
            _employeeGroup_SystemFunctionRepository = employeeGroup_SystemFunctionRepository;
            _userPCRepository = userPCRepository;
            _badgeReaderRepository = badgeReaderRepository;
            _weightRecordRepository = weightRecordRepository;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        /* Customer management block */
        #region
        public async Task<ResponseViewModel<CustomerViewModel>> GetAllCustomer()
        {
            try
            {
                var result = await _customerRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.CUSTOMERFULLINCUDES);
                var customerView = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(result);
                ResponseViewModel<CustomerViewModel> responseViewModel = new ResponseViewModel<CustomerViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<CustomerViewModel> responseViewModel = new ResponseViewModel<CustomerViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<CustomerViewModel>> SearchCustomer(string code)
        {
            ResponseViewModel<CustomerViewModel> responseViewModel = new ResponseViewModel<CustomerViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _customerRepository.GetManyAsync(c => (c.code.Contains(code) || c.nameVi.Contains(code) || c.nameEn.Contains(code) || c.shortName.Contains(code)) && c.isDelete == false, QueryIncludes.CUSTOMERFULLINCUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
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
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<Customer, CustomerViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
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
                        responseViewModel.errorText = Common.ResponseText.ADD_CUSTOMER_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_CUSTOMER_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_CUSTOMER_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CustomerViewModel>> DeleteCustomer(CustomerViewModel customerView)
        {
            ResponseViewModel<CustomerViewModel> responseViewModel = new ResponseViewModel<CustomerViewModel>();
            if (customerView != null)
            {
                Customer customer = await _customerRepository.GetAsync(cs => cs.code.Equals(customerView.code), QueryIncludes.CUSTOMERFULLINCUDES);
                if (customer != null)
                {
                    customer.isDelete = true;
                    //foreach (var item in customer.customerWarehouses.ToArray())
                    //{
                    //    item.isDelete = false;
                    //}
                    
                    _customerRepository.Update(customer);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllCustomer();
                        responseViewModel.errorText = Common.ResponseText.DELETE_CUSTOMER_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_CUSTOMER_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_CUSTOMER_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
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
                        responseViewModel.errorText = Common.ResponseText.EDIT_CUSTOMER_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_CUSTOMER_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_CUSTOMER_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }
        #endregion

        /* Customer Warehouse management block */
        #region
        public async Task<ResponseViewModel<CustomerWarehouseViewModel>> GetAllCustomerWarehouse()
        {
            try
            {
                var result = await _customerWarehouseRepository.GetManyAsync(c => c.isDelete == false);
                var customerView = Mapper.Map<IEnumerable<CustomerWarehouse>, IEnumerable<CustomerWarehouseViewModel>>(result);
                ResponseViewModel<CustomerWarehouseViewModel> responseViewModel = new ResponseViewModel<CustomerWarehouseViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<CustomerWarehouse>, IEnumerable<CustomerWarehouseViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<CustomerWarehouseViewModel> responseViewModel = new ResponseViewModel<CustomerWarehouseViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<CustomerWarehouseViewModel>> SearchCustomerWarehouse(string code)
        {
            ResponseViewModel<CustomerWarehouseViewModel> responseViewModel = new ResponseViewModel<CustomerWarehouseViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _customerWarehouseRepository.GetManyAsync(c => (c.code.Contains(code) || c.warehouseName.Contains(code)) && c.isDelete == false, QueryIncludes.CUSTOMERWAREHOUSEFULLINCUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<CustomerWarehouse>, IEnumerable<CustomerWarehouseViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<CustomerWarehouseViewModel>> GetCustomerWarehouseByCustomerID(int customerID)
        {
            ResponseViewModel<CustomerWarehouseViewModel> responseViewModel = new ResponseViewModel<CustomerWarehouseViewModel>();
            try
            {
                var resultCustomer = await _customerRepository.GetAsync(cs => cs.ID == customerID && cs.isDelete == false, QueryIncludes.CUSTOMERFULLINCUDES);
                if (resultCustomer == null)
                {
                    return responseViewModel = ResponseConstructor<CustomerWarehouseViewModel>.ConstructEnumerableData(ResponseCode.ERR_NO_OBJECT_FOUND, Common.ResponseText.ERR_SEARCH_FAIL, null);
                }
                else
                {
                    var result = await _customerWarehouseRepository.GetManyAsync(c => c.customerID == customerID && c.isDelete == false, QueryIncludes.CUSTOMERWAREHOUSEFULLINCUDES);
                    if (result == null)
                        return responseViewModel = ResponseConstructor<CustomerWarehouseViewModel>.ConstructEnumerableData(ResponseCode.ERR_NO_OBJECT_FOUND, Common.ResponseText.ERR_SEARCH_FAIL, null);
                    return responseViewModel = ResponseConstructor<CustomerWarehouseViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, Mapper.Map<IEnumerable<CustomerWarehouse>, IEnumerable<CustomerWarehouseViewModel>>(result));
                }
            }
            catch
            {
                return responseViewModel = ResponseConstructor<CustomerWarehouseViewModel>.ConstructEnumerableData(ResponseCode.ERR_NO_OBJECT_FOUND, Common.ResponseText.ERR_SEARCH_FAIL, null);
            }
        }

        public async Task<ResponseViewModel<CustomerWarehouseViewModel>> GetCustomerWarehouseByCode(string code)
        {
            ResponseViewModel<CustomerWarehouseViewModel> responseViewModel = new ResponseViewModel<CustomerWarehouseViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _customerWarehouseRepository.GetAsync(c => c.code.Equals(code), null);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<CustomerWarehouse, CustomerWarehouseViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<CustomerWarehouseViewModel>> CreateNewCustomerWarehouse(CustomerWarehouseViewModel customerWarehouseView)
        {
            ResponseViewModel<CustomerWarehouseViewModel> responseViewModel = new ResponseViewModel<CustomerWarehouseViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (customerWarehouseView != null)
            {
                CustomerWarehouse customerWarehouse = Mapper.Map<CustomerWarehouseViewModel, CustomerWarehouse>(customerWarehouseView);
                if (customerWarehouse != null)
                {
                    customerWarehouse.customer = await _customerRepository.GetAsync(cs => cs.code == customerWarehouseView.customer.code && cs.isDelete == false);
                    customerWarehouse.customerID = customerWarehouse.customer.ID;
                    customerWarehouse.isDelete = false;
                    _customerWarehouseRepository.Add(customerWarehouse);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetCustomerWarehouseByCode(customerWarehouse.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_CUSTOMERWAREHOUSE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_CUSTOMERWAREHOUSE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_CUSTOMERWAREHOUSE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CustomerWarehouseViewModel>> DeleteCustomerWarehouse(CustomerWarehouseViewModel customerWarehouseView)
        {
            ResponseViewModel<CustomerWarehouseViewModel> responseViewModel = new ResponseViewModel<CustomerWarehouseViewModel>();
            if (customerWarehouseView != null)
            {
                CustomerWarehouse customerWarehouse = await _customerWarehouseRepository.GetAsync(cs => cs.code.Equals(customerWarehouseView.code));
                if (customerWarehouse != null)
                {
                    customerWarehouse.isDelete = true;
                    _customerWarehouseRepository.Update(customerWarehouse);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllCustomerWarehouse();
                        responseViewModel.errorText = Common.ResponseText.DELETE_CUSTOMERWAREHOUSE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_CUSTOMERWAREHOUSE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_CUSTOMERWAREHOUSE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CustomerWarehouseViewModel>> UpdateCustomerWarehouse(CustomerWarehouseViewModel customerWarehouseView)
        {
            ResponseViewModel<CustomerWarehouseViewModel> responseViewModel = new ResponseViewModel<CustomerWarehouseViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (customerWarehouseView != null)
            {
                CustomerWarehouse customerWarehouse = Mapper.Map<CustomerWarehouseViewModel, CustomerWarehouse>(customerWarehouseView);
                if (customerWarehouse != null)
                {
                    customerWarehouse.customer = await _customerRepository.GetAsync(cs => cs.code == customerWarehouseView.customer.code && cs.isDelete == false);
                    customerWarehouse.customerID = customerWarehouse.customer.ID;
                    _customerWarehouseRepository.Update(customerWarehouse);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetCustomerWarehouseByCode(customerWarehouse.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_CUSTOMERWAREHOUSE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_CUSTOMERWAREHOUSE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_CUSTOMERWAREHOUSE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }
        #endregion

        /* Driver management block */
        #region
        public async Task<ResponseViewModel<DriverViewModel>> GetAllDriver()
        {
            try
            {
                var result = await _driverRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.DRIVERFULLINCLUDES);
                var driverView = Mapper.Map<IEnumerable<Driver>, IEnumerable<DriverViewModel>>(result);
                ResponseViewModel<DriverViewModel> responseViewModel = new ResponseViewModel<DriverViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Driver>, IEnumerable<DriverViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<DriverViewModel> responseViewModel = new ResponseViewModel<DriverViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<DriverViewModel>> GetDriverByCode(string code)
        {
            ResponseViewModel<DriverViewModel> responseViewModel = new ResponseViewModel<DriverViewModel>();
            if (code != null)
            {
                var result = await _driverRepository.GetAsync(c => c.code.Equals(code), QueryIncludes.DRIVERFULLINCLUDES);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<Driver, DriverViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
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
                var result = await _driverRepository.GetManyAsync(c => (c.code.Contains(code) || c.nameVi.Contains(code)) && c.isDelete == false, QueryIncludes.DRIVERFULLINCLUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Driver>, IEnumerable<DriverViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
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
                    driver.carrierVendor = await _carrierRepository.GetAsync(ca => ca.code == driverView.carrierVendor.code);
                    _driverRepository.Add(driver);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetDriverByCode(driver.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_DRIVER_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_DRIVER_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_DRIVER_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<DriverViewModel>> DeleteDriver(DriverViewModel driverView)
        {
            ResponseViewModel<DriverViewModel> responseViewModel = new ResponseViewModel<DriverViewModel>();
            if (driverView != null)
            {
                Driver driver = await _driverRepository.GetAsync(dr => dr.ID.Equals(driverView.ID));
                if (driver != null)
                {
                    driver.isDelete = true;
                    _driverRepository.Update(driver);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllDriver();
                        responseViewModel.errorText = Common.ResponseText.DELETE_DRIVER_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_DRIVER_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_DRIVER_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
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
                        responseViewModel.errorText = Common.ResponseText.EDIT_DRIVER_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_DRIVER_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_DRIVER_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }
        #endregion

        /* Carrier management block */
        #region
        public async Task<ResponseViewModel<CarrierVendorViewModel>> GetAllCarrier()
        {
            try
            {
                var result = await _carrierRepository.GetManyAsync(c => c.isDelete == false, null);
                ResponseViewModel<CarrierVendorViewModel> responseViewModel = new ResponseViewModel<CarrierVendorViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<CarrierVendor>, IEnumerable<CarrierVendorViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<CarrierVendorViewModel> responseViewModel = new ResponseViewModel<CarrierVendorViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<CarrierVendorViewModel>> SearchCarrier(string code)
        {
            ResponseViewModel<CarrierVendorViewModel> responseViewModel = new ResponseViewModel<CarrierVendorViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _carrierRepository.GetManyAsync(c => (c.code.Contains(code) || c.nameVi.Contains(code) || c.nameEn.Contains(code) || c.shortName.Contains(code)) && c.isDelete == false, null);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<CarrierVendor>, IEnumerable<CarrierVendorViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
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
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<CarrierVendor, CarrierVendorViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
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
                    carrierVendor.isDelete = false;
                    _carrierRepository.Add(carrierVendor);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetCarrierByCode(carrierVendor.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_CARRIER_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_CARRIER_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_CARRIER_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
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
                        responseViewModel.errorText = Common.ResponseText.EDIT_CARRIER_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_CARRIER_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_CARRIER_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CarrierVendorViewModel>> DeleteCarrier(CarrierVendorViewModel carrierView)
        {
            ResponseViewModel<CarrierVendorViewModel> responseViewModel = new ResponseViewModel<CarrierVendorViewModel>();
            if (carrierView != null)
            {
                CarrierVendor carrierVendor = await _carrierRepository.GetAsync(dr => dr.ID.Equals(carrierView.ID));
                if (carrierVendor != null)
                {
                    carrierVendor.isDelete = true;
                    _carrierRepository.Update(carrierVendor);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllCarrier();
                        responseViewModel.errorText = Common.ResponseText.DELETE_CARRIER_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_CARRIER_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_CARRIER_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion

        /* Material management block */
        #region
        public async Task<ResponseViewModel<MaterialViewModel>> GetAllMaterial()
        {
            try
            {
                var result = await _materialRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.MATERIALFULLINCLUDES);
                ResponseViewModel<MaterialViewModel> responseViewModel = new ResponseViewModel<MaterialViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Material>, IEnumerable<MaterialViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<MaterialViewModel> responseViewModel = new ResponseViewModel<MaterialViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<MaterialViewModel>> SearchMaterial(string code)
        {
            ResponseViewModel<MaterialViewModel> responseViewModel = new ResponseViewModel<MaterialViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _materialRepository.GetManyAsync(c => (c.code.Contains(code) || c.materialNameEn.Contains(code) || c.materialNameVi.Contains(code)) && c.isDelete == false, QueryIncludes.MATERIALFULLINCLUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Material>, IEnumerable<MaterialViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<MaterialViewModel>> GetMaterialByCode(string code)
        {
            ResponseViewModel<MaterialViewModel> responseViewModel = new ResponseViewModel<MaterialViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _materialRepository.GetAsync(c => c.code.Equals(code), QueryIncludes.MATERIALFULLINCLUDES);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<Material, MaterialViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<MaterialViewModel>> CreateNewMaterial(MaterialViewModel materialView)
        {
            ResponseViewModel<MaterialViewModel> responseViewModel = new ResponseViewModel<MaterialViewModel>();
            if (materialView != null)
            {
                Material material = Mapper.Map<MaterialViewModel, Material>(materialView);
                if (material != null)
                {
                    material.isDelete = false;
                    UnitType unit = await _unittypeRepository.GetAsync(un => un.ID == material.unit.ID && un.isDelete == false);
                    if (unit != null)
                    {
                        material.unit = unit;
                    }
                    _materialRepository.Add(material);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetMaterialByCode(material.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_MATERIAL_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_MATERIAL_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_MATERIAL_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<MaterialViewModel>> UpdateMaterial(MaterialViewModel materialView)
        {
            ResponseViewModel<MaterialViewModel> responseViewModel = new ResponseViewModel<MaterialViewModel>();
            if (materialView != null)
            {
                Material material = Mapper.Map<MaterialViewModel, Material>(materialView);
                if (material != null)
                {
                    UnitType unit = await _unittypeRepository.GetAsync(un => un.ID == material.unit.ID && un.isDelete == false);
                    if (unit != null)
                    {
                        material.unit = unit;
                    }
                    else
                    {
                        UnitTypeViewModel unitTypeView = new UnitTypeViewModel();
                        unitTypeView.code = material.unit.code;
                        await this.CreateNewUnitType(unitTypeView);
                        unit = await _unittypeRepository.GetAsync(un => un.code == unitTypeView.code && un.isDelete == false);
                        material.unit = unit;
                        material.unitID = unit.ID;
                    }
                    _materialRepository.Update(material);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetMaterialByCode(material.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_MATERIAL_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_MATERIAL_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_MATERIAL_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<MaterialViewModel>> DeleteMaterial(MaterialViewModel materialView)
        {
            ResponseViewModel<MaterialViewModel> responseViewModel = new ResponseViewModel<MaterialViewModel>();
            if (materialView != null)
            {
                Material material = await _materialRepository.GetAsync(dr => dr.ID.Equals(materialView.ID));
                if (material != null)
                {
                    material.isDelete = true;
                    _materialRepository.Update(material);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllMaterial();
                        responseViewModel.errorText = Common.ResponseText.DELETE_MATERIAL_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_MATERIAL_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_MATERIAL_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion

        /* Unit Type management block */
        #region
        public async Task<ResponseViewModel<UnitTypeViewModel>> GetAllUnitType()
        {
            try
            {
                var result = await _unittypeRepository.GetManyAsync(c => c.isDelete == false, null);
                ResponseViewModel<UnitTypeViewModel> responseViewModel = new ResponseViewModel<UnitTypeViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<UnitType>, IEnumerable<UnitTypeViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<UnitTypeViewModel> responseViewModel = new ResponseViewModel<UnitTypeViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<UnitTypeViewModel>> SearchUnitType(string code)
        {
            ResponseViewModel<UnitTypeViewModel> responseViewModel = new ResponseViewModel<UnitTypeViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _unittypeRepository.GetManyAsync(c => (c.code.Contains(code) && c.isDelete == false), null);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<UnitType>, IEnumerable<UnitTypeViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<UnitTypeViewModel>> GetUnitTypeByCode(string code)
        {
            ResponseViewModel<UnitTypeViewModel> responseViewModel = new ResponseViewModel<UnitTypeViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _unittypeRepository.GetAsync(c => c.code.Equals(code), null);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<UnitType, UnitTypeViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<UnitTypeViewModel>> CreateNewUnitType(UnitTypeViewModel unitTypeView)
        {
            ResponseViewModel<UnitTypeViewModel> responseViewModel = new ResponseViewModel<UnitTypeViewModel>();
            if (unitTypeView != null)
            {
                UnitType unitType = Mapper.Map<UnitTypeViewModel, UnitType>(unitTypeView);
                if (unitType != null)
                {
                    unitType.isDelete = false;
                    _unittypeRepository.Add(unitType);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetUnitTypeByCode(unitType.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_UNITTYPE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_UNITTYPE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_UNITTYPE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<UnitTypeViewModel>> UpdateUnitType(UnitTypeViewModel unitTypeView)
        {
            ResponseViewModel<UnitTypeViewModel> responseViewModel = new ResponseViewModel<UnitTypeViewModel>();
            if (unitTypeView != null)
            {
                UnitType unitType = Mapper.Map<UnitTypeViewModel, UnitType>(unitTypeView);
                if (unitType != null)
                {
                    _unittypeRepository.Update(unitType);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetUnitTypeByCode(unitType.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_UNITTYPE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_UNITTYPE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_UNITTYPE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<UnitTypeViewModel>> DeleteUnitType(UnitTypeViewModel unitTypeView)
        {
            ResponseViewModel<UnitTypeViewModel> responseViewModel = new ResponseViewModel<UnitTypeViewModel>();
            if (unitTypeView != null)
            {
                UnitType unitType = await _unittypeRepository.GetAsync(dr => dr.ID.Equals(unitTypeView.ID));
                if (unitType != null)
                {
                    unitType.isDelete = true;
                    _unittypeRepository.Update(unitType);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllUnitType();
                        responseViewModel.errorText = Common.ResponseText.DELETE_UNITTYPE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_UNITTYPE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_UNITTYPE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion

        /* Truck management block */
        #region
        public async Task<ResponseViewModel<TruckViewModel>> GetAllTruck()
        {
            try
            {
                var result = await _truckRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.TRUCKFULLINCLUDES);
                ResponseViewModel<TruckViewModel> responseViewModel = new ResponseViewModel<TruckViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Truck>, IEnumerable<TruckViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<TruckViewModel> responseViewModel = new ResponseViewModel<TruckViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<TruckViewModel>> TruckGetAllSuggestedDriver()
        {
            try
            {
                var result = await _truckRepository.GetManyAsync(c => c.suggestDriverID != null && c.isDelete == false, QueryIncludes.TRUCKFULLINCLUDES);
                ResponseViewModel<TruckViewModel> responseViewModel = new ResponseViewModel<TruckViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Truck>, IEnumerable<TruckViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<TruckViewModel> responseViewModel = new ResponseViewModel<TruckViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<TruckViewModel>> SearchTruck(string code)
        {
            ResponseViewModel<TruckViewModel> responseViewModel = new ResponseViewModel<TruckViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _truckRepository.GetManyAsync(c => (c.plateNumber.Contains(code) || c.code.Contains(code)) && c.isDelete == false, QueryIncludes.TRUCKFULLINCLUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Truck>, IEnumerable<TruckViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<TruckViewModel>> GetTruckByCode(string code)
        {
            ResponseViewModel<TruckViewModel> responseViewModel = new ResponseViewModel<TruckViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _truckRepository.GetAsync(c => c.code.Equals(code), QueryIncludes.TRUCKFULLINCLUDES);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<Truck, TruckViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<TruckViewModel>> CreateNewTruck(TruckViewModel truckView)
        {
            try
            {
                ResponseViewModel<TruckViewModel> responseViewModel = new ResponseViewModel<TruckViewModel>();
                if (truckView != null)
                {
                    Truck truck = Mapper.Map<TruckViewModel, Truck>(truckView);
                    if (truck != null)
                    {
                        truck.isDelete = false;
                        TruckType truckType = await _truckTypeRepository.GetAsync(tt => tt.ID == truckView.truckType.ID && tt.isDelete == false);
                        LoadingType loadingType = await _loadingTypeRepository.GetAsync(lt => lt.ID == truckView.loadingType.ID && lt.isDelete == false);
                        CarrierVendor carriervendor = await _carrierRepository.GetAsync(cr => cr.code == truckView.carrierVendor.code && cr.isDelete == false);
                        Driver driver = await _driverRepository.GetAsync( dr => dr.ID == truckView.suggestDriverID && dr.isDelete == false);
                        truck.truckType = truckType;
                        truck.truckTypeID = truck.truckType.ID;
                        truck.loadingType = loadingType;
                        truck.loadingTypeID = truck.loadingType.ID;
                        truck.carrierVendor = carriervendor;
                        truck.carrierVendorID = carriervendor.ID;
                        truck.driver = driver;
                        truck.suggestDriverID = driver.ID;
                        if (truck.code == null)
                        {
                            Random r = new Random();
                            truck.code = r.Next().ToString();
                        }
                        _truckRepository.Add(truck);
                        if (await this.SaveChangesAsync())
                        {
                            responseViewModel = await this.GetTruckByCode(truck.code);
                            responseViewModel.errorText = Common.ResponseText.ADD_TRUCK_SUCCESS;
                        }
                        else
                        {
                            responseViewModel.errorText = Common.ResponseText.ADD_TRUCK_FAIL;
                        }
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_TRUCK_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
                }
                return responseViewModel;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public async Task<ResponseViewModel<TruckViewModel>> UpdateTruck(TruckViewModel truckView)
        {
            ResponseViewModel<TruckViewModel> responseViewModel = new ResponseViewModel<TruckViewModel>();
            if (truckView != null)
            {
                Truck truck = Mapper.Map<TruckViewModel, Truck>(truckView);
                if (truck != null)
                {
                    truck.carrierVendor = await _carrierRepository.GetAsync(ca => ca.code == truckView.carrierVendor.code);
                    truck.carrierVendorID = truck.carrierVendor.ID;
                    truck.truckType=await _truckTypeRepository.GetAsync(tt => tt.ID == truckView.truckType.ID);
                    truck.truckTypeID = truck.truckType.ID;
                    truck.loadingType = await _loadingTypeRepository.GetAsync(lt => lt.ID == truckView.loadingType.ID);
                    truck.loadingTypeID = truck.loadingType.ID;
                    truck.driver = await _driverRepository.GetAsync(dr => dr.ID == truckView.suggestDriverID);
                    truck.suggestDriverID = truck.driver.ID;
                    _truckRepository.Update(truck);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetTruckByCode(truck.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_TRUCK_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_TRUCK_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_TRUCK_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<TruckViewModel>> DeleteTruck(TruckViewModel truckView)
        {
            ResponseViewModel<TruckViewModel> responseViewModel = new ResponseViewModel<TruckViewModel>();
            if (truckView != null)
            {
                Truck truck = await _truckRepository.GetAsync(dr => dr.ID.Equals(truckView.ID));
                if (truck != null)
                {
                    truck.isDelete = true;
                    _truckRepository.Update(truck);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllTruck();
                        responseViewModel.errorText = Common.ResponseText.DELETE_TRUCK_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_TRUCK_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_TRUCK_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion

        /* Truck Type management block */
        #region
        public async Task<ResponseViewModel<TruckTypeViewModel>> GetAllTruckType()
        {
            try
            {
                var result = await _truckTypeRepository.GetManyAsync(c => c.isDelete == false, null);
                ResponseViewModel<TruckTypeViewModel> responseViewModel = new ResponseViewModel<TruckTypeViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<TruckType>, IEnumerable<TruckTypeViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<TruckTypeViewModel> responseViewModel = new ResponseViewModel<TruckTypeViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<TruckTypeViewModel>> SearchTruckType(string code)
        {
            ResponseViewModel<TruckTypeViewModel> responseViewModel = new ResponseViewModel<TruckTypeViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _truckTypeRepository.GetManyAsync(c => ((c.code.Contains(code) || c.description.Contains(code)) && c.isDelete == false), null);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<TruckType>, IEnumerable<TruckTypeViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<TruckTypeViewModel>> GetTruckTypeByCode(string code)
        {
            ResponseViewModel<TruckTypeViewModel> responseViewModel = new ResponseViewModel<TruckTypeViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _truckTypeRepository.GetAsync(c => c.code.Equals(code), null);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<TruckType, TruckTypeViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<TruckTypeViewModel>> CreateNewTruckType(TruckTypeViewModel truckTypeView)
        {
            ResponseViewModel<TruckTypeViewModel> responseViewModel = new ResponseViewModel<TruckTypeViewModel>();
            if (truckTypeView != null)
            {
                TruckType truckType = Mapper.Map<TruckTypeViewModel, TruckType>(truckTypeView);
                if (truckType != null)
                {
                    truckType.isDelete = false;
                    _truckTypeRepository.Add(truckType);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetTruckTypeByCode(truckType.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_TRUCKTYPE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_TRUCKTYPE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_TRUCKTYPE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<TruckTypeViewModel>> UpdateTruckType(TruckTypeViewModel truckTypeView)
        {
            ResponseViewModel<TruckTypeViewModel> responseViewModel = new ResponseViewModel<TruckTypeViewModel>();
            if (truckTypeView != null)
            {
                TruckType truckType = Mapper.Map<TruckTypeViewModel, TruckType>(truckTypeView);
                if (truckType != null)
                {
                    _truckTypeRepository.Update(truckType);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetTruckTypeByCode(truckType.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_TRUCKTYPE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_TRUCKTYPE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_TRUCKTYPE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<TruckTypeViewModel>> DeleteTruckType(TruckTypeViewModel truckTypeView)
        {
            ResponseViewModel<TruckTypeViewModel> responseViewModel = new ResponseViewModel<TruckTypeViewModel>();
            if (truckTypeView != null)
            {
                TruckType truckType = await _truckTypeRepository.GetAsync(dr => dr.ID.Equals(truckTypeView.ID));
                if (truckType != null)
                {
                    truckType.isDelete = true;
                    _truckTypeRepository.Update(truckType);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllTruckType();
                        responseViewModel.errorText = Common.ResponseText.DELETE_TRUCKTYPE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_TRUCKTYPE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_TRUCKTYPE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion

        /* Loading Type management block */
        #region
        public async Task<ResponseViewModel<LoadingTypeViewModel>> GetAllLoadingType()
        {
            try
            {
                var result = await _loadingTypeRepository.GetManyAsync(c => c.isDelete == false, null);
                ResponseViewModel<LoadingTypeViewModel> responseViewModel = new ResponseViewModel<LoadingTypeViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<LoadingType>, IEnumerable<LoadingTypeViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<LoadingTypeViewModel> responseViewModel = new ResponseViewModel<LoadingTypeViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<LoadingTypeViewModel>> SearchLoadingType(string code)
        {
            ResponseViewModel<LoadingTypeViewModel> responseViewModel = new ResponseViewModel<LoadingTypeViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _loadingTypeRepository.GetManyAsync(c => ((c.code.Contains(code) || c.description.Contains(code)) && c.isDelete == false), null);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<LoadingType>, IEnumerable<LoadingTypeViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<LoadingTypeViewModel>> GetLoadingTypeByCode(string code)
        {
            ResponseViewModel<LoadingTypeViewModel> responseViewModel = new ResponseViewModel<LoadingTypeViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _loadingTypeRepository.GetAsync(c => c.code.Equals(code), null);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<LoadingType, LoadingTypeViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<LoadingTypeViewModel>> CreateNewLoadingType(LoadingTypeViewModel loadingTypeView)
        {
            ResponseViewModel<LoadingTypeViewModel> responseViewModel = new ResponseViewModel<LoadingTypeViewModel>();
            if (loadingTypeView != null)
            {
                LoadingType loadingType = Mapper.Map<LoadingTypeViewModel, LoadingType>(loadingTypeView);
                if (loadingType != null)
                {
                    loadingType.isDelete = false;
                    _loadingTypeRepository.Add(loadingType);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetLoadingTypeByCode(loadingType.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_LOADINGTYPE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_LOADINGTYPE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_LOADINGTYPE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<LoadingTypeViewModel>> UpdateLoadingType(LoadingTypeViewModel loadingTypeView)
        {
            ResponseViewModel<LoadingTypeViewModel> responseViewModel = new ResponseViewModel<LoadingTypeViewModel>();
            if (loadingTypeView != null)
            {
                LoadingType loadingType = Mapper.Map<LoadingTypeViewModel, LoadingType>(loadingTypeView);
                if (loadingType != null)
                {
                    _loadingTypeRepository.Update(loadingType);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetLoadingTypeByCode(loadingType.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_LOADINGTYPE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_LOADINGTYPE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_LOADINGTYPE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<LoadingTypeViewModel>> DeleteLoadingType(LoadingTypeViewModel loadingTypeView)
        {
            ResponseViewModel<LoadingTypeViewModel> responseViewModel = new ResponseViewModel<LoadingTypeViewModel>();
            if (loadingTypeView != null)
            {
                LoadingType loadingType = await _loadingTypeRepository.GetAsync(dr => dr.ID.Equals(loadingTypeView.ID));
                if (loadingType != null)
                {
                    loadingType.isDelete = true;
                    _loadingTypeRepository.Update(loadingType);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllLoadingType();
                        responseViewModel.errorText = Common.ResponseText.DELETE_LOADINGTYPE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_LOADINGTYPE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_LOADINGTYPE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion

        /* Employee management block */
        #region
        public async Task<ResponseViewModel<EmployeeViewModel>> GetAllEmployee()
        {
            try
            {
                var result = await _employeeRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.EMPLOYEEINCLUDES);
                ResponseViewModel<EmployeeViewModel> responseViewModel = new ResponseViewModel<EmployeeViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<EmployeeViewModel> responseViewModel = new ResponseViewModel<EmployeeViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<EmployeeViewModel>> SearchEmployee(string code)
        {
            ResponseViewModel<EmployeeViewModel> responseViewModel = new ResponseViewModel<EmployeeViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _employeeRepository.GetManyAsync(c => ((c.firstName.Contains(code) || c.lastName.Contains(code) || c.code.Contains(code)) && c.isDelete == false), QueryIncludes.EMPLOYEEINCLUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<EmployeeViewModel>> GetEmployeeByCode(string code)
        {
            ResponseViewModel<EmployeeViewModel> responseViewModel = new ResponseViewModel<EmployeeViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _employeeRepository.GetAsync(c => c.code.Equals(code), null);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<Employee, EmployeeViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<EmployeeViewModel>> CreateNewEmployee(EmployeeViewModel employeeView)
        {
            try
            {
                ResponseViewModel<EmployeeViewModel> responseViewModel = new ResponseViewModel<EmployeeViewModel>();
                if (employeeView != null)
                {
                    Employee employee = Mapper.Map<EmployeeViewModel, Employee>(employeeView);
                    if (employee != null)
                    {
                        if (await _employeeRepository.GetAsync(em => em.rfidCard.code.Equals(employeeView.rfidCard.code) && em.isDelete == false) != null)
                        {
                            responseViewModel.errorText = Common.ResponseText.ERR_CODE_DUPLICATE;
                        }
                        else
                        {
                            employee.isDelete = false;
                            RFIDCard card = await _rdifCardRepository.GetAsync(cr => cr.code.Equals(employeeView.rfidCard.code), null);
                            card.status = 1;
                            employee.rfidCard = card;
                            employee.RFIDCardID = card.ID;
                            employee.employeeGroup = await _employeeGroupRepository.GetAsync(emg => emg.ID == employee.employeeGroup.ID, null);
                            employee.EmployeeGroupID = employee.employeeGroup.ID;
                            if (employee.code == null)
                            {
                                Random r = new Random();
                                employee.code = r.Next().ToString();
                            }
                            _rdifCardRepository.Update(card);
                            _employeeRepository.Add(employee);
                            if (await this.SaveChangesAsync())
                            {
                                responseViewModel = await this.GetEmployeeByCode(employee.code);
                                responseViewModel.errorText = Common.ResponseText.ADD_EMPLOYEE_SUCCESS;
                            }
                            else
                            {
                                responseViewModel.errorText = Common.ResponseText.ADD_EMPLOYEE_FAIL;
                            }
                        }
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_EMPLOYEE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
                }
                return responseViewModel;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public async Task<ResponseViewModel<EmployeeViewModel>> UpdateEmployee(EmployeeViewModel employeeView)
        {
            ResponseViewModel<EmployeeViewModel> responseViewModel = new ResponseViewModel<EmployeeViewModel>();
            if (employeeView != null)
            {
                try
                {
                    Employee curEmployee = await _employeeRepository.GetAsync(emp => emp.ID.Equals(employeeView.ID), QueryIncludes.EMPLOYEEINCLUDES);
                    curEmployee.firstName = employeeView.firstName;
                    curEmployee.lastName = employeeView.lastName;
                    if (curEmployee.rfidCard.code != employeeView.rfidCard.code)
                    {
                        // Update old card
                        RFIDCard oldCard = curEmployee.rfidCard;
                        oldCard.status = 0;
                        _rdifCardRepository.Update(oldCard);

                        // Update new card
                        RFIDCard newCard = await _rdifCardRepository.GetAsync(cr => cr.code.Equals(employeeView.rfidCard.code), null);
                        newCard.status = 1;
                        curEmployee.rfidCard = newCard;
                        _rdifCardRepository.Update(newCard);
                        curEmployee.RFIDCardID = newCard.ID;
                    }
                    else
                    {

                    }
                    //employee.rfidCard = await _rdifCardRepository.GetAsync(cr => cr.code.Equals(employeeView.rfidCard.code), null);
                    curEmployee.employeeGroup = await _employeeGroupRepository.GetAsync(emg => emg.ID == employeeView.employeeGroup.ID, null);
                    curEmployee.EmployeeGroupID = curEmployee.employeeGroup.ID;
                    _employeeRepository.Update(curEmployee);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetEmployeeByCode(curEmployee.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_EMPLOYEE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_EMPLOYEE_FAIL;
                    }
                }
                catch (Exception ex)
                {

                }
                //}
                //else
                //{
                //    responseViewModel.errorText = Common.ResponseText.ERR_EDIT_FAIL;
                //}
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<EmployeeViewModel>> DeleteEmployee(EmployeeViewModel employeeView)
        {
            ResponseViewModel<EmployeeViewModel> responseViewModel = new ResponseViewModel<EmployeeViewModel>();
            if (employeeView != null)
            {
                Employee employee = await _employeeRepository.GetAsync(dr => dr.ID.Equals(employeeView.ID), QueryIncludes.EMPLOYEEINCLUDES);
                if (employee != null)
                {
                    // Change RFID card
                    RFIDCard rfidCard = employee.rfidCard;
                    rfidCard.status = 0;
                    _rdifCardRepository.Update(rfidCard);
                    // Change user
                    foreach (var user in employee.users)
                    {
                        user.isActive = false;
                        user.isDelete = true;
                        user.isBlock = true;
                        _userRepository.Update(user);
                    }
                    // Change employee
                    employee.isDelete = true;
                    _employeeRepository.Update(employee);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllEmployee();
                        responseViewModel.errorText = Common.ResponseText.DELETE_EMPLOYEE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_EMPLOYEE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_EMPLOYEE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion

        /* Employee Group management block */
        #region
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> GetAllEmployeeGroup()
        {
            try
            {
                var result = await _employeeGroupRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.EMPLOYEEGROUPFULLINCLUDES);
                ResponseViewModel<EmployeeGroupViewModel> responseViewModel = new ResponseViewModel<EmployeeGroupViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<EmployeeGroup>, IEnumerable<EmployeeGroupViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<EmployeeGroupViewModel> responseViewModel = new ResponseViewModel<EmployeeGroupViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<EmployeeGroupViewModel>> SearchEmployeeGroup(string code)
        {
            ResponseViewModel<EmployeeGroupViewModel> responseViewModel = new ResponseViewModel<EmployeeGroupViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _employeeGroupRepository.GetManyAsync(c => ((c.code.Contains(code) || c.description.Contains(code)) && c.isDelete == false), null);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<EmployeeGroup>, IEnumerable<EmployeeGroupViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<EmployeeGroupViewModel>> GetEmployeeGroupByCode(string code)
        {
            ResponseViewModel<EmployeeGroupViewModel> responseViewModel = new ResponseViewModel<EmployeeGroupViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _employeeGroupRepository.GetAsync(c => c.code.Equals(code), null);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<EmployeeGroup, EmployeeGroupViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<EmployeeGroupViewModel>> CreateNewEmployeeGroup(EmployeeGroupViewModel employeeGroupView)
        {
            ResponseViewModel<EmployeeGroupViewModel> responseViewModel = new ResponseViewModel<EmployeeGroupViewModel>();
            if (employeeGroupView != null)
            {
                EmployeeGroup employeeGroup = Mapper.Map<EmployeeGroupViewModel, EmployeeGroup>(employeeGroupView);
                if (employeeGroup != null)
                {
                    employeeGroup.isDelete = false;
                    if (employeeGroup.code == null)
                    {
                        Random r = new Random();
                        employeeGroup.code = r.Next().ToString();
                    }
                    _employeeGroupRepository.Add(employeeGroup);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetEmployeeGroupByCode(employeeGroup.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_EMPLOYEEGROUP_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_EMPLOYEEGROUP_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_EMPLOYEEGROUP_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<EmployeeGroupViewModel>> UpdateEmployeeGroup(EmployeeGroupViewModel employeeGroupView)
        {
            ResponseViewModel<EmployeeGroupViewModel> responseViewModel = new ResponseViewModel<EmployeeGroupViewModel>();
            if (employeeGroupView != null)
            {
                EmployeeGroup employeeGroup = Mapper.Map<EmployeeGroupViewModel, EmployeeGroup>(employeeGroupView);
                if (employeeGroup != null)
                {
                    _employeeGroupRepository.Update(employeeGroup);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetEmployeeGroupByCode(employeeGroup.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_EMPLOYEEGROUP_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_EMPLOYEEGROUP_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_EMPLOYEEGROUP_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<EmployeeGroupViewModel>> DeleteEmployeeGroup(EmployeeGroupViewModel employeeGroupView)
        {
            ResponseViewModel<EmployeeGroupViewModel> responseViewModel = new ResponseViewModel<EmployeeGroupViewModel>();
            if (employeeGroupView != null)
            {
                EmployeeGroup employeeGroup = await _employeeGroupRepository.GetAsync(dr => dr.ID.Equals(employeeGroupView.ID));
                if (employeeGroup != null)
                {
                    employeeGroup.isDelete = true;
                    _employeeGroupRepository.Update(employeeGroup);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllEmployeeGroup();
                        responseViewModel.errorText = Common.ResponseText.DELETE_EMPLOYEEGROUP_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_EMPLOYEEGROUP_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_EMPLOYEEGROUP_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }
        #endregion

        /* User management block */
        #region
        public async Task<ResponseViewModel<UserViewModel>> GetAllUser()
        {
            try
            {
                var result = await _userRepository.GetManyAsync(c => c.isDelete == false, null);
                ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<UserViewModel>> GetUserByEmployeeID(int employeeID)
        {
            ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
            try
            {
                var result = await _userRepository.GetManyAsync(c => c.employeeID == employeeID && c.isDelete == false, QueryIncludes.USERFULLINCLUDES);
                if (result == null)
                    return responseViewModel = ResponseConstructor<UserViewModel>.ConstructEnumerableData(ResponseCode.ERR_NO_OBJECT_FOUND, Common.ResponseText.ERR_SEARCH_FAIL, null);
                return responseViewModel = ResponseConstructor<UserViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(result));
            }
            catch
            {
                return responseViewModel = ResponseConstructor<UserViewModel>.ConstructEnumerableData(ResponseCode.ERR_NO_OBJECT_FOUND, Common.ResponseText.ERR_SEARCH_FAIL, null);
            }
        }

        public async Task<string> CreateUserName(string userName)
        {
            ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
            try
            {
                var result = await _userRepository.GetManyAsync(c => c.username.Contains(userName));
                if (result != null && result.Count() > 0)
                {
                    userName += result.Count().ToString();
                    return userName;
                }
                return userName;
            }
            catch
            {
                return "";
            }
        }

        public async Task<ResponseViewModel<UserViewModel>> SearchUser(string code)
        {
            ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _userRepository.GetManyAsync(c => ((c.Code.Contains(code) || c.username.Contains(code)) && c.isDelete == false), QueryIncludes.USERFULLINCLUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<UserViewModel>> GetUserByCode(string code)
        {
            ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _userRepository.GetAsync(c => c.Code.Equals(code), QueryIncludes.USERFULLINCLUDES);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<User, UserViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<UserViewModel>> CreateNewUser(UserViewModel userView)
        {
            ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
            if (userView != null)
            {
                User user = Mapper.Map<UserViewModel, User>(userView);
                if (user != null)
                {
                    user.isDelete = false;
                    user.isActive = false;
                    if (user.Code == null || user.Code.Trim() == "")
                    {
                        var u = await _userRepository.GetAsync(c => c.Code.Equals(user.Code), null);
                        do
                        {
                            Random r = new Random();
                            user.Code = r.Next().ToString();
                            u =  await _userRepository.GetAsync(c => c.Code.Equals(user.Code), null);
                        } while(u != null);
                    }
                    Employee curentEmployee = await _employeeRepository.GetAsync(emp => emp.code == user.employee.code && emp.isDelete == false);
                    if(curentEmployee != null && curentEmployee.isDelete == false)
                    {
                        user.employee = curentEmployee;
                        user.employeeID = user.employee.ID;
                        user.password = Crypt.ToSha256(user.password);
                        UserPassword userPassword = new UserPassword();
                        userPassword.passwordString = user.password;
                        userPassword.userID = user.ID;
                        userPassword.createDate = DateTime.Now;
                        _userPasswordRepository.Add(userPassword);
                        _userRepository.Add(user);
                        if (await this.SaveChangesAsync())
                        {
                            responseViewModel = await this.GetUserByCode(user.Code);
                            responseViewModel.errorText = Common.ResponseText.ADD_USER_SUCCESS;
                        }
                        else
                        {
                            responseViewModel.errorText = Common.ResponseText.ADD_USER_FAIL;
                        }
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EMPLOYEE_DELETED;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_USER_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<UserViewModel>> UpdateUserPassword(UserViewModel userView)
        {
            ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
            try
            {
                if (userView != null)
                {
                    var user = await _userRepository.GetAsync(u => u.ID == userView.ID && u.isDelete == false, QueryIncludes.USERFULLINCLUDES);
                    var tmpPass = user.userPasswords;
                    if (tmpPass != null && tmpPass.Count() > 0)
                    {
                        var tmp = 0;
                        var constrains = await this.GetAllConstrain();
                        var maxChanges = constrains.responseDatas.Where(cs => cs.name == ConstrainName.PASS_CHANGE).ToList().FirstOrDefault().value;
                        int.TryParse(maxChanges.ToString(), out tmp);
                        var passwordList = tmpPass.OrderByDescending(o => o.createDate).Take(tmp);
                        foreach (var pass in passwordList)
                        {
                            if (Crypt.ToSha256(userView.password) == pass.passwordString)
                            {
                                responseViewModel.errorText = "Mật khẩu không được trùng với " + maxChanges + " mật khẩu gần nhất";
                                responseViewModel.errorCode = ResponseCode.ERR_PASS_DUPLICATE;
                                return responseViewModel;
                            }
                        }
                    }
                    // Create new password in password table
                    UserPassword userPassword = new UserPassword();
                    userPassword.passwordString = Crypt.ToSha256(userView.password);
                    userPassword.userID = userView.ID;
                    userPassword.createDate = DateTime.Now;
                    // Update current pasword of User
                    user.password = Crypt.ToSha256(userView.password);
                    // Save changes
                    _userPasswordRepository.Add(userPassword);
                    _userRepository.Update(user);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetUserByCode(user.Code);
                        responseViewModel.errorText = Common.ResponseText.PASSWORD_CHANGE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.PASSWORD_CHANGE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
                }
                return responseViewModel;
            }
            catch (Exception e)
            {
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<UserViewModel>> UpdateUser(UserViewModel userView)
        {
            ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
            try
            {
                if (userView != null)
                {
                    var tmpPass = await _userPasswordRepository.GetManyAsync(u => u.userID == userView.ID);
                    if (tmpPass != null && tmpPass.Count() > 0 && userView.password != tmpPass.Last().passwordString)
                    {
                        var tmp = 0;
                        var constrains = await this.GetAllConstrain();
                        var maxChanges = constrains.responseDatas.Where(cs => cs.name == ConstrainName.PASS_CHANGE).ToList().FirstOrDefault().value;
                        int.TryParse(maxChanges.ToString(), out tmp);
                        var passwordList = tmpPass.OrderByDescending(o => o.createDate).Take(tmp);
                        foreach (var pass in passwordList)
                        {
                            if (Crypt.ToSha256(userView.password) == pass.passwordString)
                            {
                                responseViewModel.errorText = "Mật khẩu không được trùng với " + maxChanges + " mật khẩu gần nhất";
                                responseViewModel.errorCode = ResponseCode.ERR_PASS_DUPLICATE;
                                return responseViewModel;
                            }
                        }
                        UserPassword userPassword = new UserPassword();
                        userPassword.passwordString = Crypt.ToSha256(userView.password);
                        userPassword.userID = userView.ID;
                        //userPassword.user = await _userRepository.GetAsync(u => u.ID == userView.ID, null);
                        userPassword.createDate = DateTime.Now;
                        _userPasswordRepository.Add(userPassword);
                    }
                    var tuser = Mapper.Map<UserViewModel, User>(userView);
                    tuser.employee = await _employeeRepository.GetAsync(emp => emp.code == tuser.employee.code && emp.isDelete == false);
                    if (tuser.employee != null && tuser.employee.isDelete == false)
                    {
                        tuser.employeeID = tuser.employee.ID;
                        tuser.isActive = false;
                        tuser.password = userView.password; // Client side present has password already
                        _userRepository.Update(tuser);
                        if (await this.SaveChangesAsync())
                        {
                            responseViewModel = await this.GetUserByCode(tuser.Code);
                            responseViewModel.errorText = Common.ResponseText.EDIT_USER_SUCCESS;
                        }
                        else
                        {
                            responseViewModel.errorText = Common.ResponseText.EDIT_USER_FAIL;
                        }
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EMPLOYEE_DELETED;
                    }

                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
                }
                return responseViewModel;
            }
            catch (Exception e)
            {
                return responseViewModel;
            }

        }

        public async Task<ResponseViewModel<UserViewModel>> DeleteUser(UserViewModel userView)
        {
            ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
            if (userView != null)
            {
                User user = await _userRepository.GetAsync(dr => dr.ID.Equals(userView.ID));
                if (user != null)
                {
                    user.isDelete = true;
                    _userRepository.Update(user);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllUser();
                        responseViewModel.errorText = Common.ResponseText.DELETE_USER_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_USER_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_USER_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }
        #endregion

        /* Employee Role management block */
        #region
        public async Task<ResponseViewModel<EmployeeRoleViewModel>> GetAllEmployeeRole()
        {
            try
            {
                var result = await _employeeRoleRepository.GetManyAsync(c => c.isDelete == false, null);
                ResponseViewModel<EmployeeRoleViewModel> responseViewModel = new ResponseViewModel<EmployeeRoleViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<EmployeeRole>, IEnumerable<EmployeeRoleViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<EmployeeRoleViewModel> responseViewModel = new ResponseViewModel<EmployeeRoleViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<EmployeeRoleViewModel>> SearchEmployeeRole(string code)
        {
            ResponseViewModel<EmployeeRoleViewModel> responseViewModel = new ResponseViewModel<EmployeeRoleViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _employeeRoleRepository.GetManyAsync(c => ((c.Code.Contains(code) || c.description.Contains(code)) && c.isDelete == false), null);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<EmployeeRole>, IEnumerable<EmployeeRoleViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<EmployeeRoleViewModel>> GetEmployeeRoleByCode(string code)
        {
            ResponseViewModel<EmployeeRoleViewModel> responseViewModel = new ResponseViewModel<EmployeeRoleViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _employeeRoleRepository.GetAsync(c => c.Code.Equals(code), null);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<EmployeeRole, EmployeeRoleViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<EmployeeRoleViewModel>> CreateNewEmployeeRole(EmployeeRoleViewModel employeeRoleView)
        {
            ResponseViewModel<EmployeeRoleViewModel> responseViewModel = new ResponseViewModel<EmployeeRoleViewModel>();
            if (employeeRoleView != null)
            {
                EmployeeRole employeeRole = Mapper.Map<EmployeeRoleViewModel, EmployeeRole>(employeeRoleView);
                if (employeeRole != null)
                {
                    employeeRole.isDelete = false;
                    if (employeeRole.Code == null)
                    {
                        Random r = new Random();
                        employeeRole.Code = r.Next().ToString();
                    }
                    _employeeRoleRepository.Add(employeeRole);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetEmployeeRoleByCode(employeeRole.Code);
                        responseViewModel.errorText = Common.ResponseText.ADD_EMPLOYEEROLE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_EMPLOYEEROLE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_EMPLOYEEROLE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<EmployeeRoleViewModel>> UpdateEmployeeRole(EmployeeRoleViewModel employeeRoleView)
        {
            ResponseViewModel<EmployeeRoleViewModel> responseViewModel = new ResponseViewModel<EmployeeRoleViewModel>();
            if (employeeRoleView != null)
            {
                EmployeeRole employeeRole = Mapper.Map<EmployeeRoleViewModel, EmployeeRole>(employeeRoleView);
                if (employeeRole != null)
                {
                    _employeeRoleRepository.Update(employeeRole);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetEmployeeRoleByCode(employeeRole.Code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_EMPLOYEEROLE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_EMPLOYEEROLE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_EMPLOYEEROLE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<EmployeeRoleViewModel>> DeleteEmployeeRole(EmployeeRoleViewModel employeeRoleView)
        {
            ResponseViewModel<EmployeeRoleViewModel> responseViewModel = new ResponseViewModel<EmployeeRoleViewModel>();
            if (employeeRoleView != null)
            {
                EmployeeRole employeeRole = await _employeeRoleRepository.GetAsync(dr => dr.ID.Equals(employeeRoleView.ID));
                if (employeeRole != null)
                {
                    employeeRole.isDelete = true;
                    _employeeRoleRepository.Update(employeeRole);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllEmployeeRole();
                        responseViewModel.errorText = Common.ResponseText.DELETE_EMPLOYEEROLE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_EMPLOYEEROLE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_EMPLOYEEROLE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion

        /* Plant management block */
        #region
        public async Task<ResponseViewModel<PlantViewModel>> GetAllPlant()
        {
            try
            {
                var result = await _plantRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.PLANTINCLUDES);
                ResponseViewModel<PlantViewModel> responseViewModel = new ResponseViewModel<PlantViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Plant>, IEnumerable<PlantViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<PlantViewModel> responseViewModel = new ResponseViewModel<PlantViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<PlantViewModel>> SearchPlant(string code)
        {
            ResponseViewModel<PlantViewModel> responseViewModel = new ResponseViewModel<PlantViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _plantRepository.GetManyAsync(c => ((c.nameVi.Contains(code) || c.nameEn.Contains(code)) && c.isDelete == false), QueryIncludes.PLANTINCLUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Plant>, IEnumerable<PlantViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<PlantViewModel>> GetPlantByCode(string code)
        {
            ResponseViewModel<PlantViewModel> responseViewModel = new ResponseViewModel<PlantViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _plantRepository.GetAsync(c => c.code.Equals(code), QueryIncludes.PLANTINCLUDES);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<Plant, PlantViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<PlantViewModel>> CreateNewPlant(PlantViewModel plantView)
        {
            ResponseViewModel<PlantViewModel> responseViewModel = new ResponseViewModel<PlantViewModel>();
            if (plantView != null)
            {
                Plant plant = Mapper.Map<PlantViewModel, Plant>(plantView);
                if (plant != null)
                {
                    plant.isDelete = false;
                    plant.company = await _companyRepository.GetAsync(ca => ca.code == plantView.company.code);
                    plant.companyID = plant.company.ID;
                    if (plant.code == null)
                    {
                        Random r = new Random();
                        plant.code = r.Next().ToString();
                    }
                    _plantRepository.Add(plant);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetPlantByCode(plant.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_PLANT_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_PLANT_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_PLANT_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<PlantViewModel>> UpdatePlant(PlantViewModel plantView)
        {
            ResponseViewModel<PlantViewModel> responseViewModel = new ResponseViewModel<PlantViewModel>();
            if (plantView != null)
            {
                Plant plant = Mapper.Map<PlantViewModel, Plant>(plantView);
                if (plant != null)
                {
                    plant.company = await _companyRepository.GetAsync(ca => ca.code == plantView.company.code);
                    plant.companyID = plant.company.ID;
                    _plantRepository.Update(plant);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetPlantByCode(plant.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_PLANT_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_PLANT_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_PLANT_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<PlantViewModel>> DeletePlant(PlantViewModel plantView)
        {
            ResponseViewModel<PlantViewModel> responseViewModel = new ResponseViewModel<PlantViewModel>();
            if (plantView != null)
            {
                Plant plant = await _plantRepository.GetAsync(dr => dr.ID.Equals(plantView.ID));
                if (plant != null)
                {
                    plant.isDelete = true;
                    _plantRepository.Update(plant);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllPlant();
                        responseViewModel.errorText = Common.ResponseText.DELETE_PLANT_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_PLANT_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_PLANT_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion

        /* Company management block */
        #region
        public async Task<ResponseViewModel<CompanyViewModel>> GetAllCompany()
        {
            try
            {
                var result = await _companyRepository.GetManyAsync(c => c.isDelete == false, null);
                ResponseViewModel<CompanyViewModel> responseViewModel = new ResponseViewModel<CompanyViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<CompanyViewModel> responseViewModel = new ResponseViewModel<CompanyViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<CompanyViewModel>> SearchCompany(string code)
        {
            ResponseViewModel<CompanyViewModel> responseViewModel = new ResponseViewModel<CompanyViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _companyRepository.GetManyAsync(c => ((c.nameVi.Contains(code) || c.code.Contains(code)) && c.isDelete == false), null);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<CompanyViewModel>> GetCompanyByCode(string code)
        {
            ResponseViewModel<CompanyViewModel> responseViewModel = new ResponseViewModel<CompanyViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _companyRepository.GetAsync(c => c.code.Equals(code), null);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<Company, CompanyViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CompanyViewModel>> CreateNewCompany(CompanyViewModel companyView)
        {
            ResponseViewModel<CompanyViewModel> responseViewModel = new ResponseViewModel<CompanyViewModel>();
            if (companyView != null)
            {
                Company company = Mapper.Map<CompanyViewModel, Company>(companyView);
                if (company != null)
                {
                    company.isDelete = false;
                    if (company.code == null)
                    {
                        Random r = new Random();
                        company.code = r.Next().ToString();
                    }
                    _companyRepository.Add(company);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetCompanyByCode(company.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_COMPANY_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_COMPANY_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_COMPANY_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CompanyViewModel>> UpdateCompany(CompanyViewModel companyView)
        {
            ResponseViewModel<CompanyViewModel> responseViewModel = new ResponseViewModel<CompanyViewModel>();
            if (companyView != null)
            {
                Company company = Mapper.Map<CompanyViewModel, Company>(companyView);
                if (company != null)
                {
                    _companyRepository.Update(company);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetCompanyByCode(company.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_COMPANY_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_COMPANY_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_COMPANY_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CompanyViewModel>> DeleteCompany(CompanyViewModel companyView)
        {
            ResponseViewModel<CompanyViewModel> responseViewModel = new ResponseViewModel<CompanyViewModel>();
            if (companyView != null)
            {
                Company company = await _companyRepository.GetAsync(dr => dr.ID.Equals(companyView.ID));
                if (company != null)
                {
                    company.isDelete = true;
                    _companyRepository.Update(company);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllCompany();
                        responseViewModel.errorText = Common.ResponseText.DELETE_COMPANY_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_COMPANY_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_COMPANY_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion

        /* Warehouse management block */
        #region
        public async Task<ResponseViewModel<WarehouseViewModel>> GetAllWarehouse()
        {
            try
            {
                var result = await _warehouseRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.WAREHOUSEINCLUDES);
                ResponseViewModel<WarehouseViewModel> responseViewModel = new ResponseViewModel<WarehouseViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Warehouse>, IEnumerable<WarehouseViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<WarehouseViewModel> responseViewModel = new ResponseViewModel<WarehouseViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<WarehouseViewModel>> SearchWarehouse(string code)
        {
            ResponseViewModel<WarehouseViewModel> responseViewModel = new ResponseViewModel<WarehouseViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _warehouseRepository.GetManyAsync(c => ((c.nameVi.Contains(code) || c.code.Contains(code)) && c.isDelete == false), QueryIncludes.WAREHOUSEINCLUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Warehouse>, IEnumerable<WarehouseViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<WarehouseViewModel>> GetWarehouseByCode(string code)
        {
            ResponseViewModel<WarehouseViewModel> responseViewModel = new ResponseViewModel<WarehouseViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _warehouseRepository.GetAsync(c => c.code.Equals(code), QueryIncludes.WAREHOUSEINCLUDES);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<Warehouse, WarehouseViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<WarehouseViewModel>> CreateNewWarehouse(WarehouseViewModel warehouseView)
        {
            ResponseViewModel<WarehouseViewModel> responseViewModel = new ResponseViewModel<WarehouseViewModel>();
            if (warehouseView != null)
            {
                Warehouse warehouse = Mapper.Map<WarehouseViewModel, Warehouse>(warehouseView);
                if (warehouse != null)
                {
                    warehouse.isDelete = false;
                    //warehouse.plant = await _plantRepository.GetAsync(pl => pl.code == warehouseView.plant.code);
                    //warehouse.plantID = warehouse.plant.ID;
                    if (warehouse.code == null)
                    {
                        Random r = new Random();
                        warehouse.code = r.Next().ToString();
                    }
                    _warehouseRepository.Add(warehouse);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetWarehouseByCode(warehouse.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_WAREHOUSE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_WAREHOUSE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_WAREHOUSE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<WarehouseViewModel>> UpdateWarehouse(WarehouseViewModel warehouseView)
        {
            ResponseViewModel<WarehouseViewModel> responseViewModel = new ResponseViewModel<WarehouseViewModel>();
            if (warehouseView != null)
            {
                Warehouse warehouse = Mapper.Map<WarehouseViewModel, Warehouse>(warehouseView);
                if (warehouse != null)
                {
                    //warehouse.plant = await _plantRepository.GetAsync(pl => pl.code == warehouseView.plant.code);
                    //warehouse.plantID = warehouse.plant.ID;
                    _warehouseRepository.Update(warehouse);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetWarehouseByCode(warehouse.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_WAREHOUSE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_WAREHOUSE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_WAREHOUSE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<WarehouseViewModel>> DeleteWarehouse(WarehouseViewModel warehouseView)
        {
            ResponseViewModel<WarehouseViewModel> responseViewModel = new ResponseViewModel<WarehouseViewModel>();
            if (warehouseView != null)
            {
                Warehouse warehouse = await _warehouseRepository.GetAsync(dr => dr.ID.Equals(warehouseView.ID));
                if (warehouse != null)
                {
                    warehouse.isDelete = true;
                    _warehouseRepository.Update(warehouse);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllWarehouse();
                        responseViewModel.errorText = Common.ResponseText.DELETE_WAREHOUSE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_WAREHOUSE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_WAREHOUSE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion

        /* Loading Bay management block */
        #region
        public async Task<ResponseViewModel<LoadingBayViewModel>> GetAllLoadingBay()
        {
            try
            {
                var result = await _loadingBayRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.LOADINGBAYINCLUDES);
                ResponseViewModel<LoadingBayViewModel> responseViewModel = new ResponseViewModel<LoadingBayViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<LoadingBay>, IEnumerable<LoadingBayViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<LoadingBayViewModel> responseViewModel = new ResponseViewModel<LoadingBayViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<LoadingBayViewModel>> SearchLoadingBay(string code)
        {
            ResponseViewModel<LoadingBayViewModel> responseViewModel = new ResponseViewModel<LoadingBayViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _loadingBayRepository.GetManyAsync(c => ((c.nameVi.Contains(code) || c.code.Contains(code)) && c.isDelete == false), QueryIncludes.LOADINGBAYINCLUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<LoadingBay>, IEnumerable<LoadingBayViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<LoadingBayViewModel>> GetLoadingBayByCode(string code)
        {
            ResponseViewModel<LoadingBayViewModel> responseViewModel = new ResponseViewModel<LoadingBayViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _loadingBayRepository.GetAsync(c => c.code.Equals(code), QueryIncludes.LOADINGBAYINCLUDES);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<LoadingBay, LoadingBayViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<LoadingBayViewModel>> CreateNewLoadingBay(LoadingBayViewModel loadingBayView)
        {
            ResponseViewModel<LoadingBayViewModel> responseViewModel = new ResponseViewModel<LoadingBayViewModel>();
            if (loadingBayView != null)
            {
                LoadingBay loadingBay = Mapper.Map<LoadingBayViewModel, LoadingBay>(loadingBayView);
                if (loadingBay != null)
                {
                    loadingBay.isDelete = false;
                    loadingBay.warehouse = await _warehouseRepository.GetAsync(pl => pl.code == loadingBayView.warehouse.code);
                    loadingBay.warehouseID = loadingBay.warehouse.ID;
                    if (loadingBay.code == null)
                    {
                        Random r = new Random();
                        loadingBay.code = r.Next().ToString();
                    }
                    _loadingBayRepository.Add(loadingBay);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetLoadingBayByCode(loadingBay.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_LOADINGBAY_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_LOADINGBAY_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_LOADINGBAY_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<LoadingBayViewModel>> UpdateLoadingBay(LoadingBayViewModel loadingBayView)
        {
            ResponseViewModel<LoadingBayViewModel> responseViewModel = new ResponseViewModel<LoadingBayViewModel>();
            if (loadingBayView != null)
            {
                LoadingBay loadingBay = Mapper.Map<LoadingBayViewModel, LoadingBay>(loadingBayView);
                if (loadingBay != null)
                {
                    loadingBay.warehouse = await _warehouseRepository.GetAsync(pl => pl.code == loadingBayView.warehouse.code);
                    loadingBay.warehouseID = loadingBay.warehouse.ID;
                    _loadingBayRepository.Update(loadingBay);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetLoadingBayByCode(loadingBay.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_LOADINGBAY_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_LOADINGBAY_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_LOADINGBAY_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<LoadingBayViewModel>> DeleteLoadingBay(LoadingBayViewModel loadingBayView)
        {
            ResponseViewModel<LoadingBayViewModel> responseViewModel = new ResponseViewModel<LoadingBayViewModel>();
            if (loadingBayView != null)
            {
                LoadingBay loadingBay = await _loadingBayRepository.GetAsync(dr => dr.ID.Equals(loadingBayView.ID));
                if (loadingBay != null)
                {
                    loadingBay.isDelete = true;
                    _loadingBayRepository.Update(loadingBay);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllLoadingBay();
                        responseViewModel.errorText = Common.ResponseText.DELETE_LOADINGBAY_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_LOADINGBAY_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_LOADINGBAY_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion

        /* Lane management block */
        #region
        public async Task<ResponseViewModel<LaneViewModel>> GetAllLane()
        {
            try
            {
                var result = await _laneRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.LANEINCLUDES);
                ResponseViewModel<LaneViewModel> responseViewModel = new ResponseViewModel<LaneViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Lane>, IEnumerable<LaneViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<LaneViewModel> responseViewModel = new ResponseViewModel<LaneViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<LaneViewModel>> SearchLane(string code)
        {
            ResponseViewModel<LaneViewModel> responseViewModel = new ResponseViewModel<LaneViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _laneRepository.GetManyAsync(c => ((c.nameVi.Contains(code) || c.code.Contains(code)) && c.isDelete == false), QueryIncludes.LANEINCLUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Lane>, IEnumerable<LaneViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<LaneViewModel>> GetLaneByCode(string code)
        {
            ResponseViewModel<LaneViewModel> responseViewModel = new ResponseViewModel<LaneViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _laneRepository.GetAsync(c => c.code.Equals(code), QueryIncludes.LANEINCLUDES);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<Lane, LaneViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<LaneViewModel>> CreateNewLane(LaneViewModel laneView)
        {
            ResponseViewModel<LaneViewModel> responseViewModel = new ResponseViewModel<LaneViewModel>();
            if (laneView != null)
            {
                Lane lane = Mapper.Map<LaneViewModel, Lane>(laneView);
                if (lane != null)
                {
                    lane.isDelete = false;
                    lane.loadingBay = await _loadingBayRepository.GetAsync(lb => lb.code == laneView.loadingBay.code);
                    lane.loadingType = await _loadingTypeRepository.GetAsync(lt => lt.code == laneView.loadingType.code);
                    lane.truckType = await _truckTypeRepository.GetAsync(tt => tt.code == laneView.truckType.code);
                    if (lane.code == null)
                    {
                        Random r = new Random();
                        lane.code = r.Next().ToString();
                    }
                    _laneRepository.Add(lane);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetLaneByCode(lane.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_LANE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_LANE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_LANE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<LaneViewModel>> UpdateLane(LaneViewModel laneView)
        {
            ResponseViewModel<LaneViewModel> responseViewModel = new ResponseViewModel<LaneViewModel>();
            if (laneView != null)
            {
                Lane lane = Mapper.Map<LaneViewModel, Lane>(laneView);
                if (lane != null)
                {
                    lane.loadingBay = await _loadingBayRepository.GetAsync(lb => lb.code == laneView.loadingBay.code);
                    lane.loadingBayID = lane.loadingBay.ID;
                    lane.loadingType = await _loadingTypeRepository.GetAsync(lt => lt.code == laneView.loadingType.code);
                    lane.loadingTypeID = lane.loadingType.ID;
                    lane.truckType = await _truckTypeRepository.GetAsync(tt => tt.code == laneView.truckType.code);
                    lane.truckTypeID = lane.truckType.ID;
                    _laneRepository.Update(lane);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetLaneByCode(lane.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_LANE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_LANE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_LANE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<LaneViewModel>> DeleteLane(LaneViewModel laneView)
        {
            ResponseViewModel<LaneViewModel> responseViewModel = new ResponseViewModel<LaneViewModel>();
            if (laneView != null)
            {
                Lane lane = await _laneRepository.GetAsync(dr => dr.ID.Equals(laneView.ID));
                if (lane != null)
                {
                    lane.isDelete = true;
                    _laneRepository.Update(lane);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllLane();
                        responseViewModel.errorText = Common.ResponseText.DELETE_LANE_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_LANE_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_LANE_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        #endregion



        /* Device control */
        /* Camera */
        #region
        public async Task<ResponseViewModel<Camera>> GetAllCamera()
        {
            ResponseViewModel<Camera> responseViewModel = new ResponseViewModel<Camera>();
            try
            {
                var result = await _cameraRepository.GetManyAsync(c => c.isDelete == false, null);
                responseViewModel.responseDatas = result;
                return responseViewModel;
            }
            catch (Exception e)
            {
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<Camera>> GetCameraByCode(string Code)
        {
            ResponseViewModel<Camera> responseViewModel = new ResponseViewModel<Camera>();
            try
            {
                var result = await _cameraRepository.GetAsync(c => c.Code.Equals(Code) && c.isDelete == false, null);
                responseViewModel.responseData = result;
                return responseViewModel;
            }
            catch (Exception e)
            {
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<Camera>> CreateNewCamera(Camera CameraView)
        {
            ResponseViewModel<Camera> responseViewModel = new ResponseViewModel<Camera>();
            if (CameraView != null)
            {
                CameraView.isDelete = false;
                if (CameraView.Code == null)
                {
                    Random r = new Random();
                    CameraView.Code = r.Next().ToString();
                }
                _cameraRepository.Add(CameraView);
                if (await this.SaveChangesAsync())
                {
                    responseViewModel = await GetCameraByCode(CameraView.Code);
                    responseViewModel.errorText = Common.ResponseText.ADD_CAMERA_SUCCESS;
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_CAMERA_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ADD_CAMERA_FAIL;
			}
            return responseViewModel;
        }
        #endregion

        /* DO management block */
        #region
        public async Task<ResponseViewModel<DeliveryOrderViewModel>> GetAllDO()
        {
            try
            {
                var result = await _doRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.DOINCLUDES);
                ResponseViewModel<DeliveryOrderViewModel> responseViewModel = new ResponseViewModel<DeliveryOrderViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<DeliveryOrder>, IEnumerable<DeliveryOrderViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<DeliveryOrderViewModel> responseViewModel = new ResponseViewModel<DeliveryOrderViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<DeliveryOrderViewModel>> SearchDO(string code)
        {
            ResponseViewModel<DeliveryOrderViewModel> responseViewModel = new ResponseViewModel<DeliveryOrderViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _doRepository.GetManyAsync(c => (c.customer.nameVi.Contains(code) && c.isDelete == false), QueryIncludes.DOINCLUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<DeliveryOrder>, IEnumerable<DeliveryOrderViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;

        }

        public async Task<ResponseViewModel<DeliveryOrderViewModel>> GetDOByCode(string code)
        {
            ResponseViewModel<DeliveryOrderViewModel> responseViewModel = new ResponseViewModel<DeliveryOrderViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _doRepository.GetAsync(c => c.code.Equals(code), QueryIncludes.DOINCLUDES);
                if (result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = Mapper.Map<DeliveryOrder, DeliveryOrderViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<Camera>> UpdateCamera(Camera CameraView)
        {
            ResponseViewModel<Camera> responseViewModel = new ResponseViewModel<Camera>();
            if (CameraView != null)
            {
                _cameraRepository.Update(CameraView);
                if (await this.SaveChangesAsync())
                {
                    responseViewModel = await GetCameraByCode(CameraView.Code);
                    responseViewModel.errorText = Common.ResponseText.EDIT_CAMERA_SUCCESS;
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_CAMERA_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.EDIT_CAMERA_FAIL;
            }
            return responseViewModel;
        }
		
        public async Task<ResponseViewModel<DeliveryOrderViewModel>> CreateNewDO(DeliveryOrderViewModel DOView)
        {
            ResponseViewModel<DeliveryOrderViewModel> responseViewModel = new ResponseViewModel<DeliveryOrderViewModel>();
            if (DOView != null)
            {
                DeliveryOrder deliveryOrder = Mapper.Map<DeliveryOrderViewModel, DeliveryOrder>(DOView);
                Order order = new Order();
                if (deliveryOrder != null)
                {
                    deliveryOrder.isDelete = false;
                    //deliveryOrder.customerWarehouse = await _customerWarehouseRepository.GetAsync(cw => cw.ID == DOView.customerWarehouse.ID);
                    deliveryOrder.customer = await _customerRepository.GetAsync(cs => cs.code == DOView.customer.code);
                    deliveryOrder.customerID = deliveryOrder.customer.ID;
                    deliveryOrder.carrierVendor = await _carrierRepository.GetAsync(cr => cr.code == DOView.carrierVendor.code);
                    deliveryOrder.carrierVendorID = deliveryOrder.carrierVendor.ID;
                    order.code = deliveryOrder.code;
                    if (deliveryOrder.code == null)
                    {
                        Random r = new Random();
                        deliveryOrder.code = r.Next().ToString();
                    }
                    _doRepository.Add(deliveryOrder);
                    //order.doID = deliveryOrder.ID;
                    _orderRepository.Add(order);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetDOByCode(deliveryOrder.code);
                        responseViewModel.errorText = Common.ResponseText.ADD_DO_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_DO_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_DO_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<Camera>> DeleteCamera(Camera CameraView)
        {
            ResponseViewModel<Camera> responseViewModel = new ResponseViewModel<Camera>();
            if (CameraView != null)
            {
                CameraView.isDelete = true;
                _cameraRepository.Update(CameraView);
                if (await this.SaveChangesAsync())
                {
                    responseViewModel = await GetCameraByCode(CameraView.Code);
                    responseViewModel.errorText = Common.ResponseText.DELETE_CAMERA_SUCCESS;
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_CAMERA_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }
		
        public async Task<ResponseViewModel<DeliveryOrderViewModel>> UpdateDO(DeliveryOrderViewModel DOView)
        {
            ResponseViewModel<DeliveryOrderViewModel> responseViewModel = new ResponseViewModel<DeliveryOrderViewModel>();
            if (DOView != null)
            {
                DeliveryOrder deliveryOrder = Mapper.Map<DeliveryOrderViewModel, DeliveryOrder>(DOView);
                if (deliveryOrder != null)
                {
                    deliveryOrder.customerWarehouse = await _customerWarehouseRepository.GetAsync(cw => cw.ID == DOView.customerWarehouse.ID);
                    deliveryOrder.customer = await _customerRepository.GetAsync(cs => cs.ID == DOView.customer.ID);
                    deliveryOrder.carrierVendor = await _carrierRepository.GetAsync(cr => cr.ID == DOView.carrierVendor.ID);
                    //deliveryOrder.loadingBayID = deliveryOrder.loadingBay.ID;
                    //deliveryOrder.loadingTypeID = deliveryOrder.loadingType.ID;
                    //deliveryOrder.truckTypeID = deliveryOrder.truckType.ID;
                    _doRepository.Update(deliveryOrder);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetDOByCode(deliveryOrder.code);
                        responseViewModel.errorText = Common.ResponseText.EDIT_DO_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_DO_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_DO_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<DeliveryOrderViewModel>> DeleteDO(DeliveryOrderViewModel DOView)
        {
            ResponseViewModel<DeliveryOrderViewModel> responseViewModel = new ResponseViewModel<DeliveryOrderViewModel>();
            if (DOView != null)
            {
                DeliveryOrder deliveryOrder = await _doRepository.GetAsync(dr => dr.ID.Equals(DOView.ID));
                if (deliveryOrder != null)
                {
                    deliveryOrder.isDelete = true;
                    _doRepository.Update(deliveryOrder);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllDO();
                        responseViewModel.errorText = Common.ResponseText.DELETE_DO_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.DELETE_DO_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.DELETE_DO_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }
        #endregion


        /* Constrain management */
        #region
        public async Task<ResponseViewModel<Constrain>> GetAllConstrain()
        {
            ResponseViewModel<Constrain> responseViewModel = new ResponseViewModel<Constrain>();
            try
            {
                var result = await _constrainRepository.GetManyAsync( c => true, null);
                responseViewModel.responseDatas = result;
                return responseViewModel;
            }
            catch (Exception e)
            {
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<Constrain>> UpdateConstrain(Constrain ConstrainView)
        {
            ResponseViewModel<Constrain> responseViewModel = new ResponseViewModel<Constrain>();
            try
            {
                if (ConstrainView != null)
                {
                    var ctmp = await _constrainRepository.GetAsync(ct => ct.name == ConstrainView.name);
                    ctmp.value = ConstrainView.value;
                    ctmp.svalue = ConstrainView.svalue;
                    ctmp.description = ConstrainView.description;
                    _constrainRepository.Update(ctmp);
                    if (await this.SaveChangesAsync())
                    {
                        //responseViewModel = await GetAllConstrain();
                        responseViewModel.errorText = Common.ResponseText.EDIT_CONSTRAIN_SUCCESS;
                        responseViewModel.errorCode = ResponseCode.SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_CONSTRAIN_FAIL;
                        responseViewModel.errorCode = ResponseCode.ERR_DB_FAIL_TO_SAVE;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
                    responseViewModel.errorCode = ResponseCode.ERR_DB_FAIL_TO_SAVE;
                }
                return responseViewModel;
            }
            catch (Exception e)
            {
                responseViewModel.errorText = Common.ResponseText.EDIT_CONSTRAIN_FAIL;
                responseViewModel.errorCode = ResponseCode.ERR_SEC_UNKNOW;
                return responseViewModel;
            }

        }

        public async Task<ResponseViewModel<PrintHeader>> UpdatePrintHeader(PrintHeader printHeader)
        {
            ResponseViewModel<PrintHeader> responseViewModel = new ResponseViewModel<PrintHeader>();
            try
            {
                if (printHeader != null)
                {
                    var ctmp = await _printHeaderRepository.GetByIdAsync(1);
                    ctmp.companyName = printHeader.companyName;
                    ctmp.companyName2 = printHeader.companyName2;
                    ctmp.phoneNo = printHeader.phoneNo;
                    ctmp.faxNo = printHeader.faxNo;
                    ctmp.address = printHeader.address;
                    _printHeaderRepository.Update(ctmp);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_PRINTHEADER_SUCCESS;
                        responseViewModel.errorCode = ResponseCode.SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_PRINTHEADER_FAIL;
                        responseViewModel.errorCode = ResponseCode.ERR_DB_FAIL_TO_SAVE;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
                    responseViewModel.errorCode = ResponseCode.ERR_DB_FAIL_TO_SAVE;
                }
                return responseViewModel;
            }
            catch (Exception e)
            {
                responseViewModel.errorText = Common.ResponseText.EDIT_PRINTHEADER_FAIL;
                responseViewModel.errorCode = ResponseCode.ERR_SEC_UNKNOW;
                return responseViewModel;
            }

        }


        public async Task<ResponseViewModel<Constrain>> GetConstrainByCategory(string category)
        {
            ResponseViewModel<Constrain> responseViewModel = new ResponseViewModel<Constrain>();
            try
            {
                var result = await _constrainRepository.GetAsync(c => c.category.Equals(category), null);
                if(result == null)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseData = result;
                return responseViewModel;
            }
            catch (Exception e)
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                return responseViewModel;
            }
        }

        #endregion

        /* SO management block */
        #region
        public async Task<ResponseViewModel<SaleOrderViewModel>> GetAllSO()
        {
            try
            {
                var result = await _saleOrderRepository.GetManyAsync(c => c.isDelete == false, null);
                ResponseViewModel<SaleOrderViewModel> responseViewModel = new ResponseViewModel<SaleOrderViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<SaleOrder>, IEnumerable<SaleOrderViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<SaleOrderViewModel> responseViewModel = new ResponseViewModel<SaleOrderViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        #endregion

        /* RFID */
        #region
        public async Task<ResponseViewModel<RFIDCardViewModel>> CreateNewRFID(RFIDCardViewModel rFIDCardV)
        {
            ResponseViewModel<RFIDCardViewModel> responseViewModel = new ResponseViewModel<RFIDCardViewModel>();
            if (rFIDCardV != null)
            {
                RFIDCard rFID = Mapper.Map<RFIDCardViewModel, RFIDCard>(rFIDCardV);
                if (rFID != null)
                {
                    rFID.isDelete = false;
                    rFID.status = 0;
                    if (rFID.code == null)
                    {
                        Random r = new Random();
                        rFID.code = r.Next().ToString();
                    }
                    _rdifCardRepository.Add(rFID);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_RFIDCARD_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.ADD_RFIDCARD_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.ADD_RFIDCARD_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<RFIDCardViewModel>> UpdateRFIDCardStatus(RFIDCardViewModel rFIDCardView)
        {
            ResponseViewModel<RFIDCardViewModel> responseViewModel = new ResponseViewModel<RFIDCardViewModel>();
            if (rFIDCardView != null)
            {
                RFIDCard rFIDCard = await _rdifCardRepository.GetAsync(c => c.code == rFIDCardView.code);
                rFIDCard.status = rFIDCardView.status;
                if (rFIDCard != null)
                {
                    _rdifCardRepository.Update(rFIDCard);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_RFIDCARD_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_RFIDCARD_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_RFIDCARD_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_LACK_INPUT;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<RFIDCardViewModel>> GetAllRFID()
        {
            ResponseViewModel<RFIDCardViewModel> responseViewModel = new ResponseViewModel<RFIDCardViewModel>();
            try
            {
                var result = await _rdifCardRepository.GetManyAsync(c => true, QueryIncludes.RFIDFULLINCLUDES);
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<RFIDCard>, IEnumerable<RFIDCardViewModel>>(result);
                return responseViewModel;
            }
            catch (Exception e)
            {
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<RFIDCardViewModel>> SearchRFID(string code)
        {
            ResponseViewModel<RFIDCardViewModel> responseViewModel = new ResponseViewModel<RFIDCardViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _rdifCardRepository.GetManyAsync(c => c.code.Contains(code)  && c.isDelete == false, QueryIncludes.RFIDFULLINCLUDES);
                if (result.Count() == 0)
                    responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_FAIL;
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<RFIDCard>, IEnumerable<RFIDCardViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.ERR_SEARCH_KEYWORD_NULL;
            }
            return responseViewModel;
        }

        #endregion

        /* WB */
        public async Task<ResponseViewModel<WeighBridge>> GetAllWB()
        {
            try
            {
                var result = await _weighBridgeRepository.GetManyAsync(c => c.isDelete == false);
                ResponseViewModel<WeighBridge> responseViewModel = new ResponseViewModel<WeighBridge>();
                responseViewModel.responseDatas = result;
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<WeighBridge> responseViewModel = new ResponseViewModel<WeighBridge>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<SystemFunctionViewModel>> GetAllSystemFunction()
        {
            try
            {
                var result = await _systemFunctionRepository.GetManyAsync(c => c.isDelete == false, null);
                ResponseViewModel<SystemFunctionViewModel> responseViewModel = new ResponseViewModel<SystemFunctionViewModel>();
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<SystemFunction>, IEnumerable<SystemFunctionViewModel>>(result);
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<SystemFunctionViewModel> responseViewModel = new ResponseViewModel<SystemFunctionViewModel>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<EmployeeGroupViewModel>> UpdateEmployeeGroupFunction(EmployeeGroupViewModel employeeGroupViewModel)
        {
            try
            {
                //Delete all function in group
                int deleteWithGroupID = employeeGroupViewModel.ID;
                var result = await _employeeGroup_SystemFunctionRepository.GetManyAsync(c => c.employeeGroupID == deleteWithGroupID, null);
                for (int i = 0; i < result.Count(); i++)
                {
                    _employeeGroup_SystemFunctionRepository.Delete(result.ElementAt(i));
                }
                await this.SaveChangesAsync();

                //Add list function base on employeeGroupViewModel
                for (int i = 0; i < employeeGroupViewModel.functionMaps.Count; i++)
                {
                    EmployeeGroup_SystemFunction newGroupFunction = new EmployeeGroup_SystemFunction();
                    newGroupFunction.employeeGroupID = deleteWithGroupID;
                    newGroupFunction.systemFunctionID = employeeGroupViewModel.functionMaps.ElementAt(i).systemFunctionID;
                    _employeeGroup_SystemFunctionRepository.Add(newGroupFunction);
                }
                if (await this.SaveChangesAsync())
                {
                    ResponseViewModel<EmployeeGroupViewModel> responseViewModel = new ResponseViewModel<EmployeeGroupViewModel>();
                    responseViewModel.errorText = Common.ResponseText.EDIT_GROUPPERMISSION_SUCCESS;
                    return responseViewModel;
                }
                else
                {
                    ResponseViewModel<EmployeeGroupViewModel> responseViewModel = new ResponseViewModel<EmployeeGroupViewModel>();
                    responseViewModel.errorText = Common.ResponseText.EDIT_GROUPPERMISSION_FAIL;
                    return responseViewModel;
                }
            }
            catch
            {
                ResponseViewModel<EmployeeGroupViewModel> responseViewModel = new ResponseViewModel<EmployeeGroupViewModel>();
                responseViewModel.errorText = Common.ResponseText.EDIT_GROUPPERMISSION_FAIL;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<WeighBridge>> UpdateWeighBridge(WeighBridge weighBridge)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel<UserPC>> GetAllUserPC()
        {
            try
            {
                var result = await _userPCRepository.GetManyAsync(c => true, QueryIncludes.PCFULLINCLUDES);
                ResponseViewModel<UserPC> responseViewModel = new ResponseViewModel<UserPC>();
                responseViewModel.responseDatas = result;
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<UserPC> responseViewModel = new ResponseViewModel<UserPC>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<UserPC>> GetUserPCByIP(string IP)
        {
            try
            {
                var result = await _userPCRepository.GetManyAsync(c => c.IPAddress.Equals(IP), QueryIncludes.PCFULLINCLUDES);
                ResponseViewModel<UserPC> responseViewModel = new ResponseViewModel<UserPC>();
                responseViewModel.responseDatas = result;
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<UserPC> responseViewModel = new ResponseViewModel<UserPC>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<UserPC>> UpdateUserPC(UserPC userPC)
        {
            ResponseViewModel<UserPC> responseViewModel = new ResponseViewModel<UserPC>();
                if (userPC != null)
                {
                    _userPCRepository.Update(userPC);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetUserPCByIP(userPC.IPAddress);
                        responseViewModel.errorText = Common.ResponseText.EDIT_PC_SUCCESS;
                    }
                    else
                    {
                        responseViewModel.errorText = Common.ResponseText.EDIT_PC_FAIL;
                    }
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_PC_FAIL;
                }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<BadgeReader>> GetBadgeReaderByCode(string code)
        {
            try
            {
                var result = await _badgeReaderRepository.GetManyAsync(c => c.Code.Equals(code));
                ResponseViewModel<BadgeReader> responseViewModel = new ResponseViewModel<BadgeReader>();
                responseViewModel.responseDatas = result;
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<BadgeReader> responseViewModel = new ResponseViewModel<BadgeReader>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<BadgeReader>> UpdateBadgeReader(BadgeReader badgeReader)
        {
            ResponseViewModel<BadgeReader> responseViewModel = new ResponseViewModel<BadgeReader>();
            if (badgeReader != null)
            {
                var result = await _badgeReaderRepository.GetAsync(b => b.Code == badgeReader.Code);
                result.ipAddress = badgeReader.ipAddress;
                result.port = badgeReader.port;
                _badgeReaderRepository.Update(result);
                if (await this.SaveChangesAsync())
                {
                    responseViewModel = await this.GetBadgeReaderByCode(badgeReader.Code);
                    responseViewModel.errorText = Common.ResponseText.EDIT_BADGEREADER_SUCCESS;
                }
                else
                {
                    responseViewModel.errorText = Common.ResponseText.EDIT_BADGEREADER_FAIL;
                }
            }
            else
            {
                responseViewModel.errorText = Common.ResponseText.EDIT_BADGEREADER_FAIL;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<GenericResponseModel>> AddWeightCode(string code)
        {
            ResponseViewModel<GenericResponseModel> responseViewModel = new ResponseViewModel<GenericResponseModel>();
            var weightRecords = await _weightRecordRepository.GetManyAsync(wt => wt.gatePass.code.Equals(code) && wt.isDelete == false && wt.isSuccess == true);
            if(weightRecords != null && weightRecords.Count() > 1)
            {
                foreach (var weight in weightRecords)
                {
                    weight.code = DateTime.Now.ToString("yyMMddHHmmss");
                    _weightRecordRepository.Update(weight);
                }
                if(await this.SaveChangesAsync())
                {
                    responseViewModel.booleanResponse = true;
                }
                else
                {
                    responseViewModel.booleanResponse = false;
                }
            }
            else
            {
                responseViewModel.booleanResponse = false;
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<UserPC>> GetAllWBPC()
        {
            try
            {
                var result = await _userPCRepository.GetManyAsync(c => c.Function.Equals(PCFunction.WEIGHT));
                ResponseViewModel<UserPC> responseViewModel = new ResponseViewModel<UserPC>();
                responseViewModel.responseDatas = result;
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<UserPC> responseViewModel = new ResponseViewModel<UserPC>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }
    }
}
