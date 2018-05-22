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
        private Random _random = new Random();

        public AdminService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IDriverRepository driverRepository, ICarrierVendorRepository carrierRepository,
							IUserRepository userRepository, IMaterialRepository materialRepository, IUnitTypeRepository unitypeRepository, ITruckRepository truckRepository, 
                            ITruckTypeRepository truckTypeRepository, ILoadingTypeRepository loadingTypeRepository, IEmployeeRepository employeeRepository,
                            IEmployeeGroupRepository employeeGroupRepository, IEmployeeRoleRepository employeeRoleRepository, IPlantRepository plantRepository,
                            ICompanyRepository companyRepository, IWarehouseRepository warehouseRepository, ILoadingBayRepository loadingBayRepository,
                            ILaneRepository laneRepository)
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
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        /* Customer management block */
        #region
        public async Task<ResponseViewModel<CustomerViewModel>> GetAllCustomer()
        {
            var result = await _customerRepository.GetManyAsync(c => c.isDelete == false);
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
                    driver.carrierVendor = await _carrierRepository.GetAsync(ca => ca.code == driverView.carrierVendor.code);
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
                    carrierVendor.isDelete = false;
                    _carrierRepository.Add(carrierVendor);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetCarrierByCode(carrierVendor.code);
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

        /* Material management block */
        #region
        public async Task<ResponseViewModel<MaterialViewModel>> GetAllMaterial()
        {
            var result = await _materialRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.MATERIALFULLINCLUDES);
            ResponseViewModel<MaterialViewModel> responseViewModel = new ResponseViewModel<MaterialViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<Material>, IEnumerable<MaterialViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<MaterialViewModel>> SearchMaterial(string code)
        {
            ResponseViewModel<MaterialViewModel> responseViewModel = new ResponseViewModel<MaterialViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _materialRepository.GetManyAsync(c => (c.materialNameEn.Contains(code) || c.materialNameVi.Contains(code)) && c.isDelete == false, QueryIncludes.MATERIALFULLINCLUDES);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Material>, IEnumerable<MaterialViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Querry is Null";
                responseViewModel.responseData = Mapper.Map<Material, MaterialViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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

        /* Unit Type management block */
        #region
        public async Task<ResponseViewModel<UnitTypeViewModel>> GetAllUnitType()
        {
            var result = await _unittypeRepository.GetManyAsync(c => c.isDelete == false, null);
            ResponseViewModel<UnitTypeViewModel> responseViewModel = new ResponseViewModel<UnitTypeViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<UnitType>, IEnumerable<UnitTypeViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<UnitTypeViewModel>> SearchUnitType(string code)
        {
            ResponseViewModel<UnitTypeViewModel> responseViewModel = new ResponseViewModel<UnitTypeViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _unittypeRepository.GetManyAsync(c => (c.code.Contains(code) && c.isDelete == false), null);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<UnitType>, IEnumerable<UnitTypeViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<UnitType, UnitTypeViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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

        /* Truck management block */
        #region
        public async Task<ResponseViewModel<TruckViewModel>> GetAllTruck()
        {
            var result = await _truckRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.TRUCKFULLINCLUDES);
            ResponseViewModel<TruckViewModel> responseViewModel = new ResponseViewModel<TruckViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<Truck>, IEnumerable<TruckViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<TruckViewModel>> TruckGetAllSuggestedDriver()
        {
            var result = await _truckRepository.GetManyAsync(c => c.suggestDriverID != null && c.isDelete == false, QueryIncludes.TRUCKFULLINCLUDES);
            ResponseViewModel<TruckViewModel> responseViewModel = new ResponseViewModel<TruckViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<Truck>, IEnumerable<TruckViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<TruckViewModel>> SearchTruck(string code)
        {
            ResponseViewModel<TruckViewModel> responseViewModel = new ResponseViewModel<TruckViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _truckRepository.GetManyAsync(c => (c.plateNumber.Contains(code) && c.isDelete == false), QueryIncludes.TRUCKFULLINCLUDES);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Truck>, IEnumerable<TruckViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<Truck, TruckViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<TruckViewModel>> CreateNewTruck(TruckViewModel truckView)
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
                    truck.truckType = truckType;
                    truck.truckTypeID = truck.truckType.ID;
                    truck.loadingType = loadingType;
                    truck.loadingTypeID = truck.loadingType.ID;
                    truck.carrierVendor = carriervendor;
                    truck.carrierVendorID = carriervendor.ID;
                    if (truck.code == null)
                    {
                        Random r = new Random();
                        truck.code = r.Next().ToString();
                    }
                    _truckRepository.Add(truck);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetTruckByCode(truck.code);
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
                    truck.truckType=await _truckTypeRepository.GetAsync(tt => tt.code == truckView.truckType.code);
                    truck.truckTypeID = truck.truckType.ID;
                    truck.loadingType = await _loadingTypeRepository.GetAsync(lt => lt.code == truckView.loadingType.code);
                    truck.loadingTypeID = truck.loadingType.ID;
                    _truckRepository.Update(truck);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetTruckByCode(truck.code);
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

        /* Truck Type management block */
        #region
        public async Task<ResponseViewModel<TruckTypeViewModel>> GetAllTruckType()
        {
            var result = await _truckTypeRepository.GetManyAsync(c => c.isDelete == false, null);
            ResponseViewModel<TruckTypeViewModel> responseViewModel = new ResponseViewModel<TruckTypeViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<TruckType>, IEnumerable<TruckTypeViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<TruckTypeViewModel>> SearchTruckType(string code)
        {
            ResponseViewModel<TruckTypeViewModel> responseViewModel = new ResponseViewModel<TruckTypeViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _truckTypeRepository.GetManyAsync(c => ((c.code.Contains(code) || c.description.Contains(code)) && c.isDelete == false), null);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<TruckType>, IEnumerable<TruckTypeViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<TruckType, TruckTypeViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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

        /* Loading Type management block */
        #region
        public async Task<ResponseViewModel<LoadingTypeViewModel>> GetAllLoadingType()
        {
            var result = await _loadingTypeRepository.GetManyAsync(c => c.isDelete == false, null);
            ResponseViewModel<LoadingTypeViewModel> responseViewModel = new ResponseViewModel<LoadingTypeViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<LoadingType>, IEnumerable<LoadingTypeViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<LoadingTypeViewModel>> SearchLoadingType(string code)
        {
            ResponseViewModel<LoadingTypeViewModel> responseViewModel = new ResponseViewModel<LoadingTypeViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _loadingTypeRepository.GetManyAsync(c => ((c.code.Contains(code) || c.description.Contains(code)) && c.isDelete == false), null);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<LoadingType>, IEnumerable<LoadingTypeViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<LoadingType, LoadingTypeViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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

        /* Employee management block */
        #region
        public async Task<ResponseViewModel<EmployeeViewModel>> GetAllEmployee()
        {
            var result = await _employeeRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.EMPLOYEEFULLINCLUDES);
            ResponseViewModel<EmployeeViewModel> responseViewModel = new ResponseViewModel<EmployeeViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<EmployeeViewModel>> SearchEmployee(string code)
        {
            ResponseViewModel<EmployeeViewModel> responseViewModel = new ResponseViewModel<EmployeeViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _employeeRepository.GetManyAsync(c => ((c.firstName.Contains(code) || c.lastName.Contains(code)) && c.isDelete == false), null);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<Employee, EmployeeViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
            }
            return responseViewModel;
        }

        public async Task<ResponseViewModel<EmployeeViewModel>> CreateNewEmployee(EmployeeViewModel employeeView)
        {
            ResponseViewModel<EmployeeViewModel> responseViewModel = new ResponseViewModel<EmployeeViewModel>();
            if (employeeView != null)
            {
                Employee employee = Mapper.Map<EmployeeViewModel, Employee>(employeeView);
                if (employee != null)
                {
                    employee.isDelete = false;
                    if (employee.code == null)
                    {
                        Random r = new Random();
                        employee.code = r.Next().ToString();
                    }
                    _employeeRepository.Add(employee);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetEmployeeByCode(employee.code);
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

        public async Task<ResponseViewModel<EmployeeViewModel>> UpdateEmployee(EmployeeViewModel employeeView)
        {
            ResponseViewModel<EmployeeViewModel> responseViewModel = new ResponseViewModel<EmployeeViewModel>();
            if (employeeView != null)
            {
                Employee employee = Mapper.Map<EmployeeViewModel, Employee>(employeeView);
                if (employee != null)
                {
                    _employeeRepository.Update(employee);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetEmployeeByCode(employee.code);
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

        public async Task<ResponseViewModel<EmployeeViewModel>> DeleteEmployee(EmployeeViewModel employeeView)
        {
            ResponseViewModel<EmployeeViewModel> responseViewModel = new ResponseViewModel<EmployeeViewModel>();
            if (employeeView != null)
            {
                Employee employee = await _employeeRepository.GetAsync(dr => dr.ID.Equals(employeeView.ID));
                if (employee != null)
                {
                    employee.isDelete = true;
                    _employeeRepository.Update(employee);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetAllEmployee();
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

        /* Employee Group management block */
        #region
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> GetAllEmployeeGroup()
        {
            var result = await _employeeGroupRepository.GetManyAsync(c => c.isDelete == false, null);
            ResponseViewModel<EmployeeGroupViewModel> responseViewModel = new ResponseViewModel<EmployeeGroupViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<EmployeeGroup>, IEnumerable<EmployeeGroupViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<EmployeeGroupViewModel>> SearchEmployeeGroup(string code)
        {
            ResponseViewModel<EmployeeGroupViewModel> responseViewModel = new ResponseViewModel<EmployeeGroupViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _employeeGroupRepository.GetManyAsync(c => ((c.code.Contains(code) || c.description.Contains(code)) && c.isDelete == false), null);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<EmployeeGroup>, IEnumerable<EmployeeGroupViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<EmployeeGroup, EmployeeGroupViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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

        /* User management block */
        #region
        public async Task<ResponseViewModel<UserViewModel>> GetAllUser()
        {
            var result = await _userRepository.GetManyAsync(c => c.isDelete == false, null);
            ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<UserViewModel>> SearchUser(string code)
        {
            ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _userRepository.GetManyAsync(c => ((c.Code.Contains(code) || c.username.Contains(code)) && c.isDelete == false), null);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                var result = await _userRepository.GetAsync(c => c.Code.Equals(code), null);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<User, UserViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    if (user.Code == null)
                    {
                        Random r = new Random();
                        user.Code = r.Next().ToString();
                    }
                    _userRepository.Add(user);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetUserByCode(user.Code);
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

        public async Task<ResponseViewModel<UserViewModel>> UpdateUser(UserViewModel userView)
        {
            ResponseViewModel<UserViewModel> responseViewModel = new ResponseViewModel<UserViewModel>();
            if (userView != null)
            {
                User user = Mapper.Map<UserViewModel, User>(userView);
                if (user != null)
                {
                    _userRepository.Update(user);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetUserByCode(user.Code);
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

        /* Employee Role management block */
        #region
        public async Task<ResponseViewModel<EmployeeRoleViewModel>> GetAllEmployeeRole()
        {
            var result = await _employeeRoleRepository.GetManyAsync(c => c.isDelete == false, null);
            ResponseViewModel<EmployeeRoleViewModel> responseViewModel = new ResponseViewModel<EmployeeRoleViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<EmployeeRole>, IEnumerable<EmployeeRoleViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<EmployeeRoleViewModel>> SearchEmployeeRole(string code)
        {
            ResponseViewModel<EmployeeRoleViewModel> responseViewModel = new ResponseViewModel<EmployeeRoleViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _employeeRoleRepository.GetManyAsync(c => ((c.Code.Contains(code) || c.description.Contains(code)) && c.isDelete == false), null);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<EmployeeRole>, IEnumerable<EmployeeRoleViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<EmployeeRole, EmployeeRoleViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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

        /* Plant management block */
        #region
        public async Task<ResponseViewModel<PlantViewModel>> GetAllPlant()
        {
            var result = await _plantRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.PLANTINCLUDES);
            ResponseViewModel<PlantViewModel> responseViewModel = new ResponseViewModel<PlantViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<Plant>, IEnumerable<PlantViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<PlantViewModel>> SearchPlant(string code)
        {
            ResponseViewModel<PlantViewModel> responseViewModel = new ResponseViewModel<PlantViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _plantRepository.GetManyAsync(c => ((c.nameVi.Contains(code) || c.nameEn.Contains(code)) && c.isDelete == false), QueryIncludes.PLANTINCLUDES);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Plant>, IEnumerable<PlantViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<Plant, PlantViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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

        /* Company management block */
        #region
        public async Task<ResponseViewModel<CompanyViewModel>> GetAllCompany()
        {
            var result = await _companyRepository.GetManyAsync(c => c.isDelete == false, null);
            ResponseViewModel<CompanyViewModel> responseViewModel = new ResponseViewModel<CompanyViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<CompanyViewModel>> SearchCompany(string code)
        {
            ResponseViewModel<CompanyViewModel> responseViewModel = new ResponseViewModel<CompanyViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _companyRepository.GetManyAsync(c => ((c.nameVi.Contains(code) || c.code.Contains(code)) && c.isDelete == false), null);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<Company, CompanyViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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

        /* Warehouse management block */
        #region
        public async Task<ResponseViewModel<WarehouseViewModel>> GetAllWarehouse()
        {
            var result = await _warehouseRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.WAREHOUSEINCLUDES);
            ResponseViewModel<WarehouseViewModel> responseViewModel = new ResponseViewModel<WarehouseViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<Warehouse>, IEnumerable<WarehouseViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<WarehouseViewModel>> SearchWarehouse(string code)
        {
            ResponseViewModel<WarehouseViewModel> responseViewModel = new ResponseViewModel<WarehouseViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _warehouseRepository.GetManyAsync(c => ((c.nameVi.Contains(code) || c.code.Contains(code)) && c.isDelete == false), QueryIncludes.WAREHOUSEINCLUDES);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Warehouse>, IEnumerable<WarehouseViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<Warehouse, WarehouseViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    warehouse.plant = await _plantRepository.GetAsync(pl => pl.code == warehouseView.plant.code);
                    warehouse.plantID = warehouse.plant.ID;
                    if (warehouse.code == null)
                    {
                        Random r = new Random();
                        warehouse.code = r.Next().ToString();
                    }
                    _warehouseRepository.Add(warehouse);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetWarehouseByCode(warehouse.code);
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

        public async Task<ResponseViewModel<WarehouseViewModel>> UpdateWarehouse(WarehouseViewModel warehouseView)
        {
            ResponseViewModel<WarehouseViewModel> responseViewModel = new ResponseViewModel<WarehouseViewModel>();
            if (warehouseView != null)
            {
                Warehouse warehouse = Mapper.Map<WarehouseViewModel, Warehouse>(warehouseView);
                if (warehouse != null)
                {
                    warehouse.plant = await _plantRepository.GetAsync(pl => pl.code == warehouseView.plant.code);
                    warehouse.plantID = warehouse.plant.ID;
                    _warehouseRepository.Update(warehouse);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetWarehouseByCode(warehouse.code);
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

        /* Loading Bay management block */
        #region
        public async Task<ResponseViewModel<LoadingBayViewModel>> GetAllLoadingBay()
        {
            var result = await _loadingBayRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.LOADINGBAYINCLUDES);
            ResponseViewModel<LoadingBayViewModel> responseViewModel = new ResponseViewModel<LoadingBayViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<LoadingBay>, IEnumerable<LoadingBayViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<LoadingBayViewModel>> SearchLoadingBay(string code)
        {
            ResponseViewModel<LoadingBayViewModel> responseViewModel = new ResponseViewModel<LoadingBayViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _loadingBayRepository.GetManyAsync(c => ((c.nameVi.Contains(code) || c.code.Contains(code)) && c.isDelete == false), QueryIncludes.LOADINGBAYINCLUDES);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<LoadingBay>, IEnumerable<LoadingBayViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<LoadingBay, LoadingBayViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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

        /* Lane management block */
        #region
        public async Task<ResponseViewModel<LaneViewModel>> GetAllLane()
        {
            var result = await _laneRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.LANEINCLUDES);
            ResponseViewModel<LaneViewModel> responseViewModel = new ResponseViewModel<LaneViewModel>();
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<Lane>, IEnumerable<LaneViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<LaneViewModel>> SearchLane(string code)
        {
            ResponseViewModel<LaneViewModel> responseViewModel = new ResponseViewModel<LaneViewModel>();
            responseViewModel.responseData = null;
            responseViewModel.responseDatas = null;
            if (code != null)
            {
                var result = await _laneRepository.GetManyAsync(c => ((c.nameVi.Contains(code) || c.code.Contains(code)) && c.isDelete == false), QueryIncludes.LANEINCLUDES);
                if (result == null)
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseDatas = Mapper.Map<IEnumerable<Lane>, IEnumerable<LaneViewModel>>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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
                    responseViewModel.errorText = "Query is Null";
                responseViewModel.responseData = Mapper.Map<Lane, LaneViewModel>(result);
            }
            else
            {
                responseViewModel.errorText = "Code is Null";
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

        //public async Task<List<SystemFunctionViewModel>> GetUserPermission(int userID)
        //{
        //ResponseViewModel<UserViewModel> response;
        //User queryUserResult;
        //UserViewModel userView;

        //if (_unitOfWork.Exists() == false)
        //{
        //    response = ResponseConstructor<UserViewModel>.ConstructData(ResponseCode.ERR_DB_CONNECTION_FAILED, null);
        //}
        //else
        //{
        //    // Find user
        //    queryUserResult = await _userRepository.GetAsync(u => u.isDelete == false &&
        //                                                        u.username == userName &&
        //                                                        u.password ==passWord);
        //    if (queryUserResult == null)
        //    {
        //        // Not Found User
        //        response = ResponseConstructor<UserViewModel>.ConstructData(ResponseCode.ERR_LOGIN_WRONG_USERNAME_PASS, null);
        //    }
        //    else
        //    {
        //        userView = Mapper.Map<User, UserViewModel>(queryUserResult);
        //        response = ResponseConstructor<UserViewModel>.ConstructData(ResponseCode.SUCCESS, userView);
        //    }
        //}

        //return response;
        //}

        //public async Task<ResponseViewModel<UserViewModel>> Login(string userName, string passWord)
        //{
        //    ResponseViewModel<UserViewModel> ret = new ResponseViewModel<UserViewModel>();
        //    ret.errorCode = ResponseCode.SUCCESS;

        //    return ret;
        //}
    }
}
