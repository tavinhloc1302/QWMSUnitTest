﻿using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using QWMSServer.Tests.Dummy;

namespace QWMSServer.Tests.ServiceTest
{
    [TestClass]
    public class AdminServiceTest
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
        private readonly AdminService _adminService;

        public AdminServiceTest()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Configure();

            _unitOfWork = new UnitOfWorkTest();
            _customerRepository = new CustomerRepositoryTest();
            _driverRepository = new DriverRepositoryTest();
            _carrierRepository = new CarrierVendorRepositoryTest();
            _userRepository = new UserRepositoryTest();
            _materialRepository = new MaterialRepositoryTest();
            _unittypeRepository = new UnitTypeRepositoryTest();
            _truckRepository = new TruckRepositoryTest();
            _loadingTypeRepository = new LoadingTypeRepositoryTest();
            _employeeRepository = new EmployeeRepositoryTest();
            _employeeGroupRepository = new EmployeeGroupRepositoryTest();
            _plantRepository = new PlantRepositoryTest();
            _companyRepository = new CompanyRepositoryTest();
            _warehouseRepository = new WarehouseRepositoryTest();
            _loadingBayRepository = new LoadingBayRepositoryTest();
            _laneRepository = new LaneRepositoryTest();
            _employeeRoleRepository = new EmployeeRoleRepositoryTest();
            _truckTypeRepository = new TruckTypeRepositoryTest();

            _adminService = new AdminService(
                _unitOfWork, _customerRepository, _driverRepository, _carrierRepository, _userRepository,
                _materialRepository, _unittypeRepository, _truckRepository, _truckTypeRepository, _loadingTypeRepository,
                _employeeRepository, _employeeGroupRepository, _employeeRoleRepository, _plantRepository,
                _companyRepository, _warehouseRepository, _loadingBayRepository, _laneRepository);
        }

        [TestMethod]
        public async Task TestMethod_SaveChangesAsync()
        {
            var actualResult = await _adminService.SaveChangesAsync();
            Assert.IsTrue(actualResult);
        }

        // ---------------------------------------------> Begin CreateNewCarrier test cases

        [TestMethod]
        public async Task TestMethod_CreateNewCarrier()
        {
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 1;
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel() { code = "0123" };
            var actualResult = await _adminService.CreateNewCarrier(carrierView);
            CarrierVendorRepositoryTest.FLAG_GET_ASYNC = 0;
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCarrier_NoCode()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel();
            var actualResult = await _adminService.CreateNewCarrier(carrierView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCarrier_NullModel()
        {
            var actualResult = await _adminService.CreateNewCarrier(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewCarrier test cases

        // ---------------------------------------------> Begin CreateNewCustomer test cases

        [TestMethod]
        public async Task TestMethod_CreateNewCustomer()
        {
            CustomerViewModel customerView = new CustomerViewModel() { code = "0123" };
            var actualResult = await _adminService.CreateNewCustomer(customerView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCustomer_NoCode()
        {
            CustomerViewModel customerView = new CustomerViewModel();
            var actualResult = await _adminService.CreateNewCustomer(customerView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCustomer_NullModel()
        {
            var actualResult = await _adminService.CreateNewCustomer(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewCarrier test cases

        // ---------------------------------------------> Begin CreateNewDriver test cases

        [TestMethod]
        public async Task TestMethod_CreateNewDriver()
        {
            DriverViewModel driverView = new DriverViewModel() { code = "0123" };
            var actualResult = await _adminService.CreateNewDriver(driverView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewDriver_NoCode()
        {
            DriverViewModel driverView = new DriverViewModel();
            var actualResult = await _adminService.CreateNewDriver(driverView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewDriver_NullModel()
        {
            var actualResult = await _adminService.CreateNewDriver(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewCarrier test cases

        // ---------------------------------------------> Begin DeleteCarrier test cases

        [TestMethod]
        public async Task TestMethod_DeleteCarrier()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel();
            var actualResult = await _adminService.DeleteCarrier(carrierView);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCarrier_NullModel()
        {
            var actualResult = await _adminService.DeleteCarrier(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End DeleteCarrier test cases

        // ---------------------------------------------> Begin DeleteCustomer test cases

        [TestMethod]
        public async Task TestMethod_DeleteCustomer()
        {
            CustomerViewModel customerView = new CustomerViewModel();
            var actualResult = await _adminService.DeleteCustomer(customerView);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCustomer_NullModel()
        {
            var actualResult = await _adminService.DeleteCustomer(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End DeleteCustomer test cases

        // ---------------------------------------------> Begin DeleteDriver test cases
        [TestMethod]
        public async Task TestMethod_DeleteDriver()
        {
            DriverViewModel driverView = new DriverViewModel() { code = "0123" };
            var actualResult = await _adminService.DeleteDriver(driverView);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_DeleteDriver_NoCode()
        {
            DriverViewModel driverView = new DriverViewModel();
            var actualResult = await _adminService.DeleteDriver(driverView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteDriver_NullModel()
        {
            var actualResult = await _adminService.DeleteDriver(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End DeleteCustomer test cases

        // ---------------------------------------------> Begin GetAllCarrier test cases

        [TestMethod]
        public async Task TestMethod_GetAllCarrier()
        {
            var actualResult = await _adminService.GetAllCarrier();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End DeleteCustomer test cases

        // ---------------------------------------------> Begin GetAllCustomer test cases

        [TestMethod]
        public async Task TestMethod_GetAllCustomer()
        {
            var actualResult = await _adminService.GetAllCustomer();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End GetAllCustomer test cases

        // ---------------------------------------------> Begin GetAllDriver test cases

        [TestMethod]
        public async Task TestMethod_GetAllDriver()
        {
            var actualResult = await _adminService.GetAllDriver();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End GetAllDriver test cases

        // ---------------------------------------------> Begin GetCarrierByCode test cases

        [TestMethod]
        public async Task TestMethod_GetCarrierByCode()
        {
            var actualResult = await _adminService.GetCarrierByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetCarrierByCode_NoCode()
        {
            var actualResult = await _adminService.GetCarrierByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End GetCarrierByCode test cases

        // ---------------------------------------------> Begin GetCustomerByCode test cases

        [TestMethod]
        public async Task TestMethod_GetCustomerByCode()
        {
            var actualResult = await _adminService.GetCustomerByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetCustomerByCode_NoCode()
        {
            var actualResult = await _adminService.GetCustomerByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End GetCustomerByCode test cases

        // ---------------------------------------------> Begin GetDriverByCode test cases

        [TestMethod]
        public async Task TestMethod_GetDriverByCode()
        {
            var actualResult = await _adminService.GetDriverByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetDriverByCode_NoCode()
        {
            var actualResult = await _adminService.GetDriverByCode(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End GetDriverByCode test cases

        // ---------------------------------------------> Begin SearchCarrier test cases

        [TestMethod]
        public async Task TestMethod_SearchCarrier()
        {
            var actualResult = await _adminService.SearchCarrier("0123");
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchCarrier_NoCode()
        {
            var actualResult = await _adminService.SearchCarrier(null);
            Assert.IsNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End SearchCarrier test cases

        // ---------------------------------------------> Begin SearchCustomer test cases

        [TestMethod]
        public async Task TestMethod_SearchCustomer()
        {
            var actualResult = await _adminService.SearchCustomer("0123");
            var expectedResult = new ResponseViewModel<CustomerViewModel>();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchCustomer_NoCode()
        {
            var actualResult = await _adminService.SearchCustomer(null);
            var expectedResult = new ResponseViewModel<CustomerViewModel>();
            Assert.IsNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End SearchCustomer test cases

        // ---------------------------------------------> Begin SearchDriver test cases

        [TestMethod]
        public async Task TestMethod_SearchDriver()
        {
            var actualResult = await _adminService.SearchDriver("0123");
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchDriver_NoCode()
        {
            var actualResult = await _adminService.SearchDriver(null);
            Assert.IsNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End SearchDriver test cases

        // ---------------------------------------------> Begin UpdateCarrier test cases

        [TestMethod]
        public async Task TestMethod_UpdateCarrier()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel() { code = "0123" };
            var actualResult = await _adminService.UpdateCarrier(carrierView);
            Assert.IsNotNull(actualResult.responseData);
        }

        public async Task TestMethod_UpdateCarrier_NullModel()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel() { code = "0123" };
            var actualResult = await _adminService.UpdateCarrier(null);
            Assert.IsNull(actualResult.responseData);
        }

        public async Task TestMethod_UpdateCarrier_NoCode()
        {
            CarrierVendorViewModel carrierView = new CarrierVendorViewModel();
            var actualResult = await _adminService.UpdateCarrier(carrierView);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End UpdateCarrier test cases

        // ---------------------------------------------> Begin UpdateCustomer test case

        [TestMethod]
        public async Task TestMethod_UpdateCustomer()
        {
            CustomerViewModel customerView = new CustomerViewModel() { code = "0123" };
            var actualResult = await _adminService.UpdateCustomer(customerView);
            Assert.IsNotNull(actualResult.responseData);
        }

        public async Task TestMethod_UpdateCustomer_NullModel()
        {
            var actualResult = await _adminService.UpdateCustomer(null);
            Assert.IsNull(actualResult.responseData);
        }

        public async Task TestMethod_UpdateCustomer_NoCode()
        {
            CustomerViewModel customerView = new CustomerViewModel();
            var actualResult = await _adminService.UpdateCustomer(customerView);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End UpdateCustomer test cases

        // ---------------------------------------------> Begin UpdateDriver test cases

        [TestMethod]
        public async Task TestMethod_UpdateDriver()
        {
            DriverViewModel driverView = new DriverViewModel() { code = "0123", carrierVendor = new CarrierVendorViewModel { code = "0123" } };
            var actualResult = await _adminService.UpdateDriver(driverView);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateDriver_NoCode()
        {
            DriverViewModel driverView = new DriverViewModel();
            var actualResult = await _adminService.UpdateDriver(driverView);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateDriver_NullModel()
        {
            var actualResult = await _adminService.UpdateDriver(null);
            Assert.IsNull(actualResult.responseData);
        }

        // ---------------------------------------------> End UpdateDriver test cases

        // ---------------------------------------------> Begin GetAllMaterial test cases

        [TestMethod]
        public async Task TestMethod_GetAllMaterial()
        {
            var actualResult = await _adminService.GetAllMaterial();
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End GetAllMaterial test cases

        // ---------------------------------------------> Begin SearchMaterial test cases

        [TestMethod]
        public async Task TestMethod_SearchMaterial()
        {
            var actualResult = await _adminService.SearchMaterial("0123");
            Assert.IsNotNull(actualResult.responseDatas);
        }

        [TestMethod]
        public async Task TestMethod_SearchMaterial_NoCode()
        {
            var actualResult = await _adminService.SearchMaterial(null);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End SearchMaterial test cases

        // ---------------------------------------------> Begin GetMaterialByCode test cases

        [TestMethod]
        public async Task TestMethod_GetMaterialByCode()
        {
            var actualResult = await _adminService.GetMaterialByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetMaterialByCode_NoCode()
        {
            var actualResult = await _adminService.GetMaterialByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        // ---------------------------------------------> End SearchMaterial test cases

        // ---------------------------------------------> Begin CreateNewMaterial test cases

        [TestMethod]
        public async Task TestMethod_CreateNewMaterial()
        {
            MaterialViewModel viewModel = new MaterialViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewMaterial(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewMaterial_NoCode()
        {
            MaterialViewModel viewModel = new MaterialViewModel();
            var actualResult = await _adminService.CreateNewMaterial(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewMaterial_NullModel()
        {
            var actualResult = await _adminService.CreateNewMaterial(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewMaterial test cases

        // ---------------------------------------------> Begin UpdateMaterial test cases

        [TestMethod]
        public async Task TestMethod_UpdateMaterial()
        {
            MaterialViewModel viewModel = new MaterialViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateMaterial(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateMaterial_NoCode()
        {
            MaterialViewModel viewModel = new MaterialViewModel();
            var actualResult = await _adminService.UpdateMaterial(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateMaterial_NullModel()
        {
            var actualResult = await _adminService.UpdateMaterial(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        // ---------------------------------------------> End UpdateMaterial test cases

        // ---------------------------------------------> Begin DeleteMaterial test cases

        [TestMethod]
        public async Task TestMethod_DeleteMaterial()
        {
            MaterialViewModel viewModel = new MaterialViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteMaterial(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteMaterial_NoCode()
        {
            MaterialViewModel viewModel = new MaterialViewModel();
            var actualResult = await _adminService.DeleteMaterial(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteMaterial_NullModel()
        {
            var actualResult = await _adminService.DeleteMaterial(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        // ---------------------------------------------> End UpdateMaterial test cases

        // ---------------------------------------------> Begin GetAllUnitType test cases

        [TestMethod]
        public async Task TestMethod_GetAllUnitType()
        {
            MaterialViewModel viewModel = new MaterialViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteMaterial(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        // ---------------------------------------------> End GetAllUnitType test cases

        // ---------------------------------------------> Begin SearchUnitType test cases

        [TestMethod]
        public async Task TestMethod_SearchUnitType()
        {
            var actualResult = await _adminService.SearchUnitType("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchUnitType_NoCode()
        {
            var actualResult = await _adminService.SearchUnitType(null);
            Assert.IsNotNull(actualResult.responseDatas);
        }

        // ---------------------------------------------> End SearchUnitType test cases

        // ---------------------------------------------> Begin GetUnitTypeByCode test cases

        [TestMethod]
        public async Task TestMethod_GetUnitTypeByCode()
        {
            var actualResult = await _adminService.GetUnitTypeByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetUnitTypeByCode_NoCode()
        {
            var actualResult = await _adminService.GetUnitTypeByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        // ---------------------------------------------> End GetUnitTypeByCode test cases

        // ---------------------------------------------> Begin CreateNewUnitType test cases

        [TestMethod]
        public async Task TestMethod_CreateNewUnitType()
        {
            UnitTypeViewModel viewModel = new UnitTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewUnitType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewUnitType_NoCode()
        {
            UnitTypeViewModel viewModel = new UnitTypeViewModel();
            var actualResult = await _adminService.CreateNewUnitType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewUnitType_NullModel()
        {
            var actualResult = await _adminService.CreateNewUnitType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        // ---------------------------------------------> End CreateNewUnitType test cases

        [TestMethod]
        public async Task TestMethod_UpdateUnitType()
        {
            UnitTypeViewModel viewModel = new UnitTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateUnitType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateUnitType_NoCode()
        {
            UnitTypeViewModel viewModel = new UnitTypeViewModel();
            var actualResult = await _adminService.UpdateUnitType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateUnitType_NullModel()
        {
            var actualResult = await _adminService.UpdateUnitType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUnitType()
        {
            UnitTypeViewModel viewModel = new UnitTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteUnitType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUnitType_NoCode()
        {
            UnitTypeViewModel viewModel = new UnitTypeViewModel();
            var actualResult = await _adminService.DeleteUnitType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUnitType_NullModel()
        {
            var actualResult = await _adminService.DeleteUnitType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllTruck()
        {
            var actualResult = await _adminService.GetAllTruck();
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchTruck()
        {
            var actualResult = await _adminService.SearchTruck("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchTruck_NoCode()
        {
            var actualResult = await _adminService.SearchTruck(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetTruckByCode()
        {
            var actualResult = await _adminService.GetTruckByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetTruckByCode_NoCode()
        {
            var actualResult = await _adminService.GetTruckByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewTruck()
        {
            TruckViewModel viewModel = new TruckViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewTruck(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewTruck_NoCode()
        {
            TruckViewModel viewModel = new TruckViewModel();
            var actualResult = await _adminService.CreateNewTruck(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewTruck_NoModel()
        {
            var actualResult = await _adminService.CreateNewTruck(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateTruck()
        {
            TruckViewModel viewModel = new TruckViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateTruck(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateTruck_NoCode()
        {
            TruckViewModel viewModel = new TruckViewModel();
            var actualResult = await _adminService.UpdateTruck(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateTruck_NoModel()
        {
            var actualResult = await _adminService.UpdateTruck(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruck()
        {
            TruckViewModel viewModel = new TruckViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteTruck(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruck_NoCode()
        {
            TruckViewModel viewModel = new TruckViewModel();
            var actualResult = await _adminService.DeleteTruck(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruck_NoModel()
        {
            var actualResult = await _adminService.DeleteTruck(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllTruckType()
        {
            var actualResult = await _adminService.GetAllTruckType();
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchTruckType()
        {
            var actualResult = await _adminService.SearchTruckType("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchTruckType_NoCode()
        {
            var actualResult = await _adminService.SearchTruckType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetTruckTypeByCode()
        {
            var actualResult = await _adminService.GetTruckTypeByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetTruckTypeByCode_NoCode()
        {
            var actualResult = await _adminService.GetTruckTypeByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewTruckType()
        {
            TruckTypeViewModel viewModel = new TruckTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewTruckType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewTruckType_NoCode()
        {
            TruckTypeViewModel viewModel = new TruckTypeViewModel();
            var actualResult = await _adminService.CreateNewTruckType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewTruckType_NoModel()
        {
            var actualResult = await _adminService.CreateNewTruckType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateTruckType()
        {
            TruckTypeViewModel viewModel = new TruckTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateTruckType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateTruckType_NoCode()
        {
            TruckTypeViewModel viewModel = new TruckTypeViewModel();
            var actualResult = await _adminService.UpdateTruckType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateTruckType_NoModel()
        {
            var actualResult = await _adminService.UpdateTruckType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruckType()
        {
            TruckTypeViewModel viewModel = new TruckTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteTruckType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruckType_NoCode()
        {
            TruckTypeViewModel viewModel = new TruckTypeViewModel();
            var actualResult = await _adminService.DeleteTruckType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteTruckType_NoModel()
        {
            var actualResult = await _adminService.DeleteTruckType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllLoadingType()
        {
            var actualResult = await _adminService.GetAllLoadingType();
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchLoadingType()
        {
            var actualResult = await _adminService.SearchLoadingType("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchLoadingType_NoCode()
        {
            var actualResult = await _adminService.SearchLoadingType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetLoadingTypeByCode()
        {
            var actualResult = await _adminService.GetLoadingTypeByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetLoadingTypeByCode_NoCode()
        {
            var actualResult = await _adminService.GetLoadingTypeByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLoadingType()
        {
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewLoadingType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLoadingType_NoCode()
        {
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel();
            var actualResult = await _adminService.CreateNewLoadingType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLoadingType_NoModel()
        {
            var actualResult = await _adminService.CreateNewLoadingType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLoadingType()
        {
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateLoadingType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLoadingType_NoCode()
        {
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel();
            var actualResult = await _adminService.UpdateLoadingType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLoadingType_NoModel()
        {
            var actualResult = await _adminService.UpdateLoadingType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingType()
        {
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteLoadingType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingType_NoCode()
        {
            LoadingTypeViewModel viewModel = new LoadingTypeViewModel();
            var actualResult = await _adminService.DeleteLoadingType(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingType_NoModel()
        {
            var actualResult = await _adminService.DeleteLoadingType(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllEmployee()
        {
            var actualResult = await _adminService.GetAllEmployee();
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchEmployee()
        {
            var actualResult = await _adminService.SearchEmployee("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchEmployee_NoCode()
        {
            var actualResult = await _adminService.SearchEmployee(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetEmployeeByCode()
        {
            var actualResult = await _adminService.GetEmployeeByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetEmployeeByCode_NoCode()
        {
            var actualResult = await _adminService.GetEmployeeByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployee()
        {
            EmployeeViewModel viewModel = new EmployeeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewEmployee(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployee_NoCode()
        {
            EmployeeViewModel viewModel = new EmployeeViewModel();
            var actualResult = await _adminService.CreateNewEmployee(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployee_NoModel()
        {
            var actualResult = await _adminService.CreateNewEmployee(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployee()
        {
            EmployeeViewModel viewModel = new EmployeeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateEmployee(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployee_NoCode()
        {
            EmployeeViewModel viewModel = new EmployeeViewModel();
            var actualResult = await _adminService.UpdateEmployee(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployee_NoModel()
        {
            var actualResult = await _adminService.UpdateEmployee(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployee()
        {
            EmployeeViewModel viewModel = new EmployeeViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteEmployee(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployee_NoCode()
        {
            EmployeeViewModel viewModel = new EmployeeViewModel();
            var actualResult = await _adminService.DeleteEmployee(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployee_NoModel()
        {
            var actualResult = await _adminService.DeleteEmployee(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllEmployeeGroup()
        {
            var actualResult = await _adminService.GetAllEmployeeGroup();
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchEmployeeGroup()
        {
            var actualResult = await _adminService.SearchEmployeeGroup("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchEmployeeGroup_NoCode()
        {
            var actualResult = await _adminService.SearchEmployeeGroup(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetEmployeeGroupByCode()
        {
            var actualResult = await _adminService.GetEmployeeGroupByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetEmployeeGroupByCode_NoCode()
        {
            var actualResult = await _adminService.GetEmployeeGroupByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployeeGroup()
        {
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewEmployeeGroup(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployeeGroup_NoCode()
        {
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel();
            var actualResult = await _adminService.CreateNewEmployeeGroup(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployeeGroup_NoModel()
        {
            var actualResult = await _adminService.CreateNewEmployeeGroup(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployeeGroup()
        {
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateEmployeeGroup(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployeeGroup_NoCode()
        {
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel();
            var actualResult = await _adminService.UpdateEmployeeGroup(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployeeGroup_NoModel()
        {
            var actualResult = await _adminService.UpdateEmployeeGroup(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeGroup()
        {
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteEmployeeGroup(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeGroup_NoCode()
        {
            EmployeeGroupViewModel viewModel = new EmployeeGroupViewModel();
            var actualResult = await _adminService.DeleteEmployeeGroup(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeGroup_NoModel()
        {
            var actualResult = await _adminService.DeleteEmployeeGroup(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllUser()
        {
            var actualResult = await _adminService.GetAllUser();
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchUser()
        {
            var actualResult = await _adminService.SearchUser("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchUser_NoCode()
        {
            var actualResult = await _adminService.SearchUser(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetUserByCode()
        {
            var actualResult = await _adminService.GetUserByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetUserByCode_NoCode()
        {
            var actualResult = await _adminService.GetUserByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }
        
        [TestMethod]
        public async Task TestMethod_CreateNewUser()
        {
            UserViewModel viewModel = new UserViewModel
            {
                Code = "0123"
            };
            var actualResult = await _adminService.CreateNewUser(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewUser_NoCode()
        {
            UserViewModel viewModel = new UserViewModel();
            var actualResult = await _adminService.CreateNewUser(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewUser_NoModel()
        {
            var actualResult = await _adminService.CreateNewUser(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateUser()
        {
            UserViewModel viewModel = new UserViewModel
            {
                Code = "0123"
            };
            var actualResult = await _adminService.UpdateUser(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateUser_NoCode()
        {
            UserViewModel viewModel = new UserViewModel();
            var actualResult = await _adminService.UpdateUser(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateUser_NoModel()
        {
            var actualResult = await _adminService.UpdateUser(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUser()
        {
            UserViewModel viewModel = new UserViewModel
            {
                Code = "0123"
            };
            var actualResult = await _adminService.DeleteUser(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUser_NoCode()
        {
            UserViewModel viewModel = new UserViewModel();
            var actualResult = await _adminService.DeleteUser(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteUser_NoModel()
        {
            var actualResult = await _adminService.DeleteUser(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllEmployeeRole()
        {
            var actualResult = await _adminService.GetAllEmployeeRole();
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchEmployeeRole()
        {
            var actualResult = await _adminService.SearchEmployeeRole("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchEmployeeRole_NoCode()
        {
            var actualResult = await _adminService.SearchEmployeeRole(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetEmployeeRoleByCode()
        {
            var actualResult = await _adminService.GetEmployeeRoleByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetEmployeeRoleByCode_NoCode()
        {
            var actualResult = await _adminService.GetEmployeeRoleByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployeeRole()
        {
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel
            {
                Code = "0123"
            };
            var actualResult = await _adminService.CreateNewEmployeeRole(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployeeRole_NoCode()
        {
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel();
            var actualResult = await _adminService.CreateNewEmployeeRole(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewEmployeeRole_NoModel()
        {
            var actualResult = await _adminService.CreateNewEmployeeRole(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployeeRole()
        {
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel
            {
                Code = "0123"
            };
            var actualResult = await _adminService.UpdateEmployeeRole(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployeeRole_NoCode()
        {
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel();
            var actualResult = await _adminService.UpdateEmployeeRole(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateEmployeeRole_NoModel()
        {
            var actualResult = await _adminService.UpdateEmployeeRole(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeRole()
        {
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel
            {
                Code = "0123"
            };
            var actualResult = await _adminService.DeleteEmployeeRole(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeRole_NoCode()
        {
            EmployeeRoleViewModel viewModel = new EmployeeRoleViewModel();
            var actualResult = await _adminService.DeleteEmployeeRole(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteEmployeeRole_NoModel()
        {
            var actualResult = await _adminService.DeleteEmployeeRole(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllPlant()
        {
            var actualResult = await _adminService.GetAllPlant();
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchPlant()
        {
            var actualResult = await _adminService.SearchPlant("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchPlant_NoCode()
        {
            var actualResult = await _adminService.SearchPlant(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetPlantByCode()
        {
            var actualResult = await _adminService.GetPlantByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetPlantByCode_NoCode()
        {
            var actualResult = await _adminService.GetPlantByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewPlant()
        {
            PlantViewModel viewModel = new PlantViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewPlant(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewPlant_NoCode()
        {
            PlantViewModel viewModel = new PlantViewModel();
            var actualResult = await _adminService.CreateNewPlant(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewPlant_NoModel()
        {
            var actualResult = await _adminService.CreateNewPlant(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdatePlant()
        {
            PlantViewModel viewModel = new PlantViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdatePlant(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdatePlant_NoCode()
        {
            PlantViewModel viewModel = new PlantViewModel();
            var actualResult = await _adminService.UpdatePlant(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdatePlant_NoModel()
        {
            var actualResult = await _adminService.UpdatePlant(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeletePlant()
        {
            PlantViewModel viewModel = new PlantViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeletePlant(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeletePlant_NoCode()
        {
            PlantViewModel viewModel = new PlantViewModel();
            var actualResult = await _adminService.DeletePlant(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeletePlant_NoModel()
        {
            var actualResult = await _adminService.DeletePlant(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllCompany()
        {
            var actualResult = await _adminService.GetAllCompany();
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchCompany()
        {
            var actualResult = await _adminService.SearchCompany("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchCompany_NoCode()
        {
            var actualResult = await _adminService.SearchCompany(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetCompanyByCode()
        {
            var actualResult = await _adminService.GetCompanyByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetCompanyByCode_NoCode()
        {
            var actualResult = await _adminService.GetCompanyByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCompany()
        {
            CompanyViewModel viewModel = new CompanyViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewCompany(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCompany_NoCode()
        {
            CompanyViewModel viewModel = new CompanyViewModel();
            var actualResult = await _adminService.CreateNewCompany(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewCompany_NoModel()
        {
            var actualResult = await _adminService.CreateNewCompany(null);
            Assert.IsNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateCompany()
        {
            CompanyViewModel viewModel = new CompanyViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateCompany(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateCompany_NoCode()
        {
            CompanyViewModel viewModel = new CompanyViewModel();
            var actualResult = await _adminService.UpdateCompany(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateCompany_NoModel()
        {
            var actualResult = await _adminService.UpdateCompany(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCompany()
        {
            CompanyViewModel viewModel = new CompanyViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteCompany(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCompany_NoCode()
        {
            CompanyViewModel viewModel = new CompanyViewModel();
            var actualResult = await _adminService.DeleteCompany(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteCompany_NoModel()
        {
            var actualResult = await _adminService.DeleteCompany(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllWarehouse()
        {
            var actualResult = await _adminService.GetAllWarehouse();
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchWarehouse()
        {
            var actualResult = await _adminService.SearchWarehouse("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchWarehouse_NoCode()
        {
            var actualResult = await _adminService.SearchWarehouse(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetWarehouseByCode()
        {
            var actualResult = await _adminService.GetWarehouseByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }
        
        [TestMethod]
        public async Task TestMethod_GetWarehouseByCode_NoCode()
        {
            var actualResult = await _adminService.GetWarehouseByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewWarehouse()
        {
            WarehouseViewModel viewModel = new WarehouseViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewWarehouse(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewWarehouse_NoCode()
        {
            WarehouseViewModel viewModel = new WarehouseViewModel();
            var actualResult = await _adminService.CreateNewWarehouse(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewWarehouse_NoModel()
        {
            var actualResult = await _adminService.CreateNewWarehouse(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateWarehouse()
        {
            WarehouseViewModel viewModel = new WarehouseViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateWarehouse(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateWarehouse_NoCode()
        {
            WarehouseViewModel viewModel = new WarehouseViewModel();
            var actualResult = await _adminService.UpdateWarehouse(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateWarehouse_NoModel()
        {
            var actualResult = await _adminService.UpdateWarehouse(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteWarehouse()
        {
            WarehouseViewModel viewModel = new WarehouseViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteWarehouse(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteWarehouse_NoCode()
        {
            WarehouseViewModel viewModel = new WarehouseViewModel();
            var actualResult = await _adminService.DeleteWarehouse(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteWarehouse_NoModel()
        {
            var actualResult = await _adminService.DeleteWarehouse(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllLoadingBay()
        {
            var actualResult = await _adminService.GetAllLoadingBay();
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchLoadingBay()
        {
            var actualResult = await _adminService.SearchLoadingBay("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchLoadingBay_NoCode()
        {
            var actualResult = await _adminService.SearchLoadingBay(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetLoadingBayByCode()
        {
            var actualResult = await _adminService.GetLoadingBayByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetLoadingBayByCode_NoCode()
        {
            var actualResult = await _adminService.GetLoadingBayByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLoadingBay()
        {
            LoadingBayViewModel viewModel = new LoadingBayViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewLoadingBay(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLoadingBay_NoCode()
        {
            LoadingBayViewModel viewModel = new LoadingBayViewModel();
            var actualResult = await _adminService.CreateNewLoadingBay(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLoadingBay_NoModel()
        {
            var actualResult = await _adminService.CreateNewLoadingBay(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLoadingBay()
        {
            LoadingBayViewModel viewModel = new LoadingBayViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateLoadingBay(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLoadingBay_NoCode()
        {
            LoadingBayViewModel viewModel = new LoadingBayViewModel();
            var actualResult = await _adminService.UpdateLoadingBay(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLoadingBay_NoModel()
        {
            var actualResult = await _adminService.UpdateLoadingBay(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingBay()
        {
            LoadingBayViewModel viewModel = new LoadingBayViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteLoadingBay(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingBay_NoCode()
        {
            LoadingBayViewModel viewModel = new LoadingBayViewModel();
            var actualResult = await _adminService.DeleteLoadingBay(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLoadingBay_NoModel()
        {
            var actualResult = await _adminService.DeleteLoadingBay(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetAllLane()
        {
            var actualResult = await _adminService.GetAllLane();
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchLane()
        {
            var actualResult = await _adminService.SearchLane("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_SearchLane_NoCode()
        {
            var actualResult = await _adminService.SearchLane(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetLaneByCode()
        {
            var actualResult = await _adminService.GetLaneByCode("0123");
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_GetLaneByCode_NoCode()
        {
            var actualResult = await _adminService.GetLaneByCode(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLane()
        {
            LaneViewModel viewModel = new LaneViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.CreateNewLane(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLane_NoCode()
        {
            LaneViewModel viewModel = new LaneViewModel();
            var actualResult = await _adminService.CreateNewLane(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_CreateNewLane_NoModel()
        {
            var actualResult = await _adminService.CreateNewLane(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLane()
        {
            LaneViewModel viewModel = new LaneViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.UpdateLane(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLane_NoCode()
        {
            LaneViewModel viewModel = new LaneViewModel();
            var actualResult = await _adminService.UpdateLane(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_UpdateLane_NoModel()
        {
            var actualResult = await _adminService.UpdateLane(null);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLane()
        {
            LaneViewModel viewModel = new LaneViewModel
            {
                code = "0123"
            };
            var actualResult = await _adminService.DeleteLane(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLane_NoCode()
        {
            LaneViewModel viewModel = new LaneViewModel();
            var actualResult = await _adminService.DeleteLane(viewModel);
            Assert.IsNotNull(actualResult.responseData);
        }

        [TestMethod]
        public async Task TestMethod_DeleteLane_NoModel()
        {
            var actualResult = await _adminService.DeleteLane(null);
            Assert.IsNotNull(actualResult.responseData);
        }
    }
}
