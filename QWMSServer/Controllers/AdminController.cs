using QWMSServer.Data.Services;
using QWMSServer.Filter;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace QWMSServer.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminController : ApiController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /* Customer API */
        //[AuthenticateRequire]
        [HttpGet]
        [Route("Customer/GetAll", Name ="CustomerGetAll")]
        public async Task<ResponseViewModel<CustomerViewModel>> GetAllCustomer()
        {
            return await _adminService.GetAllCustomer();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Customer/SearchByCode/code={code}", Name ="CustomerSearchByCode")]
        public async Task<ResponseViewModel<CustomerViewModel>> SearchCustomer(string code)
        {
            return await _adminService.SearchCustomer(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Customer/Add", Name = "CustomerAddNew")]
        public async Task<ResponseViewModel<CustomerViewModel>> CreateNewCustomer([FromBody]CustomerViewModel customerView)
        {
            return await _adminService.CreateNewCustomer(customerView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Customer/Update", Name = "CustomerUpdate")]
        public async Task<ResponseViewModel<CustomerViewModel>> UpdateCustomer([FromBody]CustomerViewModel customerView)
        {
            return await _adminService.UpdateCustomer(customerView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Customer/Delete", Name = "CustomerDelte")]
        public async Task<ResponseViewModel<CustomerViewModel>> DeleteCustomer([FromBody]CustomerViewModel customerView)
        {
            return await _adminService.DeleteCustomer(customerView);
        }


        /* Driver API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("Driver/GetAll", Name = "DriverGetAll")]
        public async Task<ResponseViewModel<DriverViewModel>> GetAllDriver()
        {
            return await _adminService.GetAllDriver();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Driver/SearchByCode/code={code}", Name = "DriverSearchByCode")]
        public async Task<ResponseViewModel<DriverViewModel>> SearchDriver(string code)
        {
            return await _adminService.SearchDriver(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Driver/Add", Name = "DriverAddNew")]
        public async Task<ResponseViewModel<DriverViewModel>> CreateNewDriver([FromBody]DriverViewModel driverView)
        {
            return await _adminService.CreateNewDriver(driverView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Driver/Update", Name = "DriverUpdate")]
        public async Task<ResponseViewModel<DriverViewModel>> UpdateDriver([FromBody]DriverViewModel driverView)
        {
            return await _adminService.UpdateDriver(driverView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Driver/Delete", Name = "DriverDelte")]
        public async Task<ResponseViewModel<DriverViewModel>> DeleteDriver([FromBody]DriverViewModel driverView)
        {
            return await _adminService.DeleteDriver(driverView);
        }

        /* Carrier API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("Carrier/GetAll", Name = "CarrierGetAll")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> GetAllCarrier()
        {
            return await _adminService.GetAllCarrier();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Carrier/SearchByCode/code={code}", Name = "CarrierSearchByCode")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> SearchCarrier(string code)
        {
            return await _adminService.SearchCarrier(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Carrier/Add", Name = "CarrierAddNew")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> CreateNewCarrier([FromBody]CarrierVendorViewModel carrierView)
        {
            return await _adminService.CreateNewCarrier(carrierView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Carrier/Update", Name = "CarrierUpdate")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> UpdateCarrier([FromBody]CarrierVendorViewModel carrierView)
        {
            return await _adminService.UpdateCarrier(carrierView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Carrier/Delete", Name = "CarrierDelte")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> DeleteCarrier([FromBody]CarrierVendorViewModel carrierView)
        {
            return await _adminService.DeleteCarrier(carrierView);
        }
		
        /* Material API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("Material/GetAll", Name = "MaterialGetAll")]
        public async Task<ResponseViewModel<MaterialViewModel>> GetAllMaterial()
        {
            return await _adminService.GetAllMaterial();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Material/SearchByCode/code={code}", Name = "MaterialSearchByCode")]
        public async Task<ResponseViewModel<MaterialViewModel>> SearchMaterial(string code)
        {
            return await _adminService.SearchMaterial(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Material/Add", Name = "MaterialAddNew")]
        public async Task<ResponseViewModel<MaterialViewModel>> CreateNewMaterial([FromBody]MaterialViewModel materialView)
        {
            return await _adminService.CreateNewMaterial(materialView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Material/Update", Name = "MaterialUpdate")]
        public async Task<ResponseViewModel<MaterialViewModel>> UpdateMaterial([FromBody]MaterialViewModel materialView)
        {
            return await _adminService.UpdateMaterial(materialView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Material/Delete", Name = "MaterialDelte")]
        public async Task<ResponseViewModel<MaterialViewModel>> DeleteMaterial([FromBody]MaterialViewModel materialView)
        {
            return await _adminService.DeleteMaterial(materialView);
        }

        /* UnitType API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("UnitType/GetAll", Name = "UnitTypeGetAll")]
        public async Task<ResponseViewModel<UnitTypeViewModel>> GetAllUnitType()
        {
            return await _adminService.GetAllUnitType();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("UnitType/SearchByCode/code={code}", Name = "UnitTypeSearchByCode")]
        public async Task<ResponseViewModel<UnitTypeViewModel>> SearchUnitType(string code)
        {
            return await _adminService.SearchUnitType(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("UnitType/Add", Name = "UnitTypeAddNew")]
        public async Task<ResponseViewModel<UnitTypeViewModel>> CreateNewUnitType([FromBody]UnitTypeViewModel unitTypeView)
        {
            return await _adminService.CreateNewUnitType(unitTypeView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("UnitType/Update", Name = "UnitTypeUpdate")]
        public async Task<ResponseViewModel<UnitTypeViewModel>> UpdateUnitType([FromBody]UnitTypeViewModel unitTypeView)
        {
            return await _adminService.UpdateUnitType(unitTypeView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("UnitType/Delete", Name = "UnitTypeDelte")]
        public async Task<ResponseViewModel<UnitTypeViewModel>> DeleteUnitType([FromBody]UnitTypeViewModel unitTypeView)
        {
            return await _adminService.DeleteUnitType(unitTypeView);
        }

        /* Truck API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("Truck/GetAll", Name = "TruckGetAll")]
        public async Task<ResponseViewModel<TruckViewModel>> GetAllTruck()
        {
            return await _adminService.GetAllTruck();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Truck/GetAllSuggestedDriver", Name = "TruckGetAllSuggestedDriver")]
        public async Task<ResponseViewModel<TruckViewModel>> TruckGetAllSuggestedDriver()
        {
            return await _adminService.TruckGetAllSuggestedDriver();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Truck/SearchByCode/code={code}", Name = "TruckSearchByCode")]
        public async Task<ResponseViewModel<TruckViewModel>> SearchTruck(string code)
        {
            return await _adminService.SearchTruck(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Truck/Add", Name = "TruckAddNew")]
        public async Task<ResponseViewModel<TruckViewModel>> CreateNewTruck([FromBody]TruckViewModel truckView)
        {
            return await _adminService.CreateNewTruck(truckView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Truck/Update", Name = "TruckUpdate")]
        public async Task<ResponseViewModel<TruckViewModel>> UpdateTruck([FromBody]TruckViewModel truckView)
        {
            return await _adminService.UpdateTruck(truckView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Truck/Delete", Name = "TruckDelete")]
        public async Task<ResponseViewModel<TruckViewModel>> DeleteTruck([FromBody]TruckViewModel truckView)
        {
            return await _adminService.DeleteTruck(truckView);
        }

        /* Truck Type API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("TruckType/GetAll", Name = "TruckTypeGetAll")]
        public async Task<ResponseViewModel<TruckTypeViewModel>> GetAllTruckType()
        {
            return await _adminService.GetAllTruckType();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("TruckType/SearchByCode/code={code}", Name = "TruckTypeSearchByCode")]
        public async Task<ResponseViewModel<TruckTypeViewModel>> SearchTruckType(string code)
        {
            return await _adminService.SearchTruckType(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("TruckType/Add", Name = "TruckTypeAddNew")]
        public async Task<ResponseViewModel<TruckTypeViewModel>> CreateNewTruckType([FromBody]TruckTypeViewModel truckTypeView)
        {
            return await _adminService.CreateNewTruckType(truckTypeView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("TruckType/Update", Name = "TruckTypeUpdate")]
        public async Task<ResponseViewModel<TruckTypeViewModel>> UpdateTruckType([FromBody]TruckTypeViewModel truckTypeView)
        {
            return await _adminService.UpdateTruckType(truckTypeView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("TruckType/Delete", Name = "TruckTypeDelete")]
        public async Task<ResponseViewModel<TruckTypeViewModel>> DeleteTruckType([FromBody]TruckTypeViewModel truckTypeView)
        {
            return await _adminService.DeleteTruckType(truckTypeView);
        }

        /* Loading Type API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("LoadingType/GetAll", Name = "LoadingTypeGetAll")]
        public async Task<ResponseViewModel<LoadingTypeViewModel>> GetAllLoadingType()
        {
            return await _adminService.GetAllLoadingType();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("LoadingType/SearchByCode/code={code}", Name = "LoadingTypeSearchByCode")]
        public async Task<ResponseViewModel<LoadingTypeViewModel>> SearchLoadingType(string code)
        {
            return await _adminService.SearchLoadingType(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("LoadingType/Add", Name = "LoadingTypeAddNew")]
        public async Task<ResponseViewModel<LoadingTypeViewModel>> CreateNewLoadingType([FromBody]LoadingTypeViewModel loadingTypeView)
        {
            return await _adminService.CreateNewLoadingType(loadingTypeView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("LoadingType/Update", Name = "LoadingTypeUpdate")]
        public async Task<ResponseViewModel<LoadingTypeViewModel>> UpdateLoadingType([FromBody]LoadingTypeViewModel loadingTypeView)
        {
            return await _adminService.UpdateLoadingType(loadingTypeView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("LoadingType/Delete", Name = "LoadingTypeDelete")]
        public async Task<ResponseViewModel<LoadingTypeViewModel>> DeleteLoadingType([FromBody]LoadingTypeViewModel loadingTypeView)
        {
            return await _adminService.DeleteLoadingType(loadingTypeView);
        }

        /* Employee API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("Employee/GetAll", Name = "EmployeeGetAll")]
        public async Task<ResponseViewModel<EmployeeViewModel>> GetAllEmployee()
        {
            return await _adminService.GetAllEmployee();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Employee/SearchByCode/code={code}", Name = "EmployeeSearchByCode")]
        public async Task<ResponseViewModel<EmployeeViewModel>> SearchEmployee(string code)
        {
            return await _adminService.SearchEmployee(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Employee/Add", Name = "EmployeeAddNew")]
        public async Task<ResponseViewModel<EmployeeViewModel>> CreateNewEmployee([FromBody]EmployeeViewModel employeeView)
        {
            return await _adminService.CreateNewEmployee(employeeView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Employee/Update", Name = "EmployeeUpdate")]
        public async Task<ResponseViewModel<EmployeeViewModel>> UpdateEmployee([FromBody]EmployeeViewModel employeeView)
        {
            return await _adminService.UpdateEmployee(employeeView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Employee/Delete", Name = "EmployeeDelete")]
        public async Task<ResponseViewModel<EmployeeViewModel>> DeleteEmployee([FromBody]EmployeeViewModel employeeView)
        {
            return await _adminService.DeleteEmployee(employeeView);
        }

        /* Employee Group API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("EmployeeGroup/GetAll", Name = "EmployeeGroupGetAll")]
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> GetAllEmployeeGroup()
        {
            return await _adminService.GetAllEmployeeGroup();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("EmployeeGroup/SearchByCode/code={code}", Name = "EmployeeGroupSearchByCode")]
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> SearchEmployeeGroup(string code)
        {
            return await _adminService.SearchEmployeeGroup(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("EmployeeGroup/Add", Name = "EmployeeGroupAddNew")]
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> CreateNewEmployeeGroup([FromBody]EmployeeGroupViewModel employeeGroupView)
        {
            return await _adminService.CreateNewEmployeeGroup(employeeGroupView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("EmployeeGroup/Update", Name = "EmployeeGroupUpdate")]
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> UpdateEmployeeGroup([FromBody]EmployeeGroupViewModel employeeGroupView)
        {
            return await _adminService.UpdateEmployeeGroup(employeeGroupView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("EmployeeGroup/Delete", Name = "EmployeeGroupDelete")]
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> DeleteEmployeeGroup([FromBody]EmployeeGroupViewModel employeeGroupView)
        {
            return await _adminService.DeleteEmployeeGroup(employeeGroupView);
        }

        /* User API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("User/GetAll", Name = "UserGetAll")]
        public async Task<ResponseViewModel<UserViewModel>> GetAllUser()
        {
            return await _adminService.GetAllUser();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("User/GetByEmployeeID/ID={employeeID}", Name = "UserGetByEmployeeID")]
        public async Task<ResponseViewModel<UserViewModel>> GetUserByEmployeeID(int employeeID)
        {
            return await _adminService.GetUserByEmployeeID(employeeID);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("User/SearchByCode/code={code}", Name = "UserSearchByCode")]
        public async Task<ResponseViewModel<UserViewModel>> SearchUser(string code)
        {
            return await _adminService.SearchUser(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("User/Add", Name = "UserAddNew")]
        public async Task<ResponseViewModel<UserViewModel>> CreateNewUser([FromBody]UserViewModel userView)
        {
            return await _adminService.CreateNewUser(userView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("User/Update", Name = "UserUpdate")]
        public async Task<ResponseViewModel<UserViewModel>> UpdateUser([FromBody]UserViewModel userView)
        {
            return await _adminService.UpdateUser(userView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("User/Delete", Name = "UserDelete")]
        public async Task<ResponseViewModel<UserViewModel>> DeleteUser([FromBody]UserViewModel userView)
        {
            return await _adminService.DeleteUser(userView);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("User/CreateUserName/{userName}", Name = "CreateUserName")]
        public async Task<string> CreateUserName(string userName)
        {
            return await _adminService.CreateUserName(userName);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("User/UpdateUserPassword", Name = "UpdateUserPassword")]
        public async Task<ResponseViewModel<UserViewModel>> UpdateUserPassword([FromBody]UserViewModel userView)
        {
            return await _adminService.UpdateUserPassword(userView);
        }
        
        /* Employee Role API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("EmployeeRole/GetAll", Name = "EmployeeRoleGetAll")]
        public async Task<ResponseViewModel<EmployeeRoleViewModel>> GetAllEmployeeRole()
        {
            return await _adminService.GetAllEmployeeRole();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("EmployeeRole/SearchByCode/code={code}", Name = "EmployeeRoleSearchByCode")]
        public async Task<ResponseViewModel<EmployeeRoleViewModel>> SearchEmployeeRole(string code)
        {
            return await _adminService.SearchEmployeeRole(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("EmployeeRole/Add", Name = "EmployeeRoleAddNew")]
        public async Task<ResponseViewModel<EmployeeRoleViewModel>> CreateNewEmployeeRole([FromBody]EmployeeRoleViewModel employeeRoleView)
        {
            return await _adminService.CreateNewEmployeeRole(employeeRoleView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("EmployeeRole/Update", Name = "EmployeeRoleUpdate")]
        public async Task<ResponseViewModel<EmployeeRoleViewModel>> UpdateEmployeeRole([FromBody]EmployeeRoleViewModel employeeRoleView)
        {
            return await _adminService.UpdateEmployeeRole(employeeRoleView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("EmployeeRole/Delete", Name = "EmployeeRoleDelete")]
        public async Task<ResponseViewModel<EmployeeRoleViewModel>> DeleteEmployeeRole([FromBody]EmployeeRoleViewModel employeeRoleView)
        {
            return await _adminService.DeleteEmployeeRole(employeeRoleView);
        }

        /* Plant API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("Plant/GetAll", Name = "PlantGetAll")]
        public async Task<ResponseViewModel<PlantViewModel>> GetAllPlant()
        {
            return await _adminService.GetAllPlant();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Plant/SearchByCode/code={code}", Name = "PlantSearchByCode")]
        public async Task<ResponseViewModel<PlantViewModel>> SearchPlant(string code)
        {
            return await _adminService.SearchPlant(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Plant/Add", Name = "PlantAddNew")]
        public async Task<ResponseViewModel<PlantViewModel>> CreateNewPlant([FromBody]PlantViewModel plantView)
        {
            return await _adminService.CreateNewPlant(plantView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Plant/Update", Name = "PlantUpdate")]
        public async Task<ResponseViewModel<PlantViewModel>> UpdatePlant([FromBody]PlantViewModel plantView)
        {
            return await _adminService.UpdatePlant(plantView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Plant/Delete", Name = "PlantDelete")]
        public async Task<ResponseViewModel<PlantViewModel>> DeletePlant([FromBody]PlantViewModel plantView)
        {
            return await _adminService.DeletePlant(plantView);
        }

        /* Company API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("Company/GetAll", Name = "CompanyGetAll")]
        public async Task<ResponseViewModel<CompanyViewModel>> GetAllCompany()
        {
            return await _adminService.GetAllCompany();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Company/SearchByCode/code={code}", Name = "CompanySearchByCode")]
        public async Task<ResponseViewModel<CompanyViewModel>> SearchCompany(string code)
        {
            return await _adminService.SearchCompany(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Company/Add", Name = "CompanyAddNew")]
        public async Task<ResponseViewModel<CompanyViewModel>> CreateNewCompany([FromBody]CompanyViewModel companyView)
        {
            return await _adminService.CreateNewCompany(companyView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Company/Update", Name = "CompanyUpdate")]
        public async Task<ResponseViewModel<CompanyViewModel>> UpdateCompany([FromBody]CompanyViewModel companyView)
        {
            return await _adminService.UpdateCompany(companyView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Company/Delete", Name = "CompanyDelete")]
        public async Task<ResponseViewModel<CompanyViewModel>> DeleteCompany([FromBody]CompanyViewModel companyView)
        {
            return await _adminService.DeleteCompany(companyView);
        }

        /* Warehouse API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("Warehouse/GetAll", Name = "WarehouseGetAll")]
        public async Task<ResponseViewModel<WarehouseViewModel>> GetAllWarehouse()
        {
            return await _adminService.GetAllWarehouse();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Warehouse/SearchByCode/code={code}", Name = "WarehouseSearchByCode")]
        public async Task<ResponseViewModel<WarehouseViewModel>> SearchWarehouse(string code)
        {
            return await _adminService.SearchWarehouse(code);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Warehouse/GetByCode/code={code}", Name = "GetWareHouseByCode")]
        public async Task<ResponseViewModel<WarehouseViewModel>> GetWarehouseByCode(string code)
        {
            return await _adminService.GetWarehouseByCode(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Warehouse/Add", Name = "WarehouseAddNew")]
        public async Task<ResponseViewModel<WarehouseViewModel>> CreateNewWarehouse([FromBody]WarehouseViewModel warehouseView)
        {
            return await _adminService.CreateNewWarehouse(warehouseView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Warehouse/Update", Name = "WarehouseUpdate")]
        public async Task<ResponseViewModel<WarehouseViewModel>> UpdateWarehouse([FromBody]WarehouseViewModel warehouseView)
        {
            return await _adminService.UpdateWarehouse(warehouseView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Warehouse/Delete", Name = "WarehouseDelete")]
        public async Task<ResponseViewModel<WarehouseViewModel>> DeleteWarehouse([FromBody]WarehouseViewModel warehouseView)
        {
            return await _adminService.DeleteWarehouse(warehouseView);
        }

        /* Loading Bay API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("LoadingBay/GetAll", Name = "LoadingBayGetAll")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> GetAllLoadingBay()
        {
            return await _adminService.GetAllLoadingBay();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("LoadingBay/SearchByCode/code={code}", Name = "LoadingBaySearchByCode")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> SearchLoadingBay(string code)
        {
            return await _adminService.SearchLoadingBay(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("LoadingBay/Add", Name = "LoadingBayAddNew")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> CreateNewLoadingBay([FromBody]LoadingBayViewModel loadingBayView)
        {
            return await _adminService.CreateNewLoadingBay(loadingBayView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("LoadingBay/Update", Name = "LoadingBayUpdate")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> UpdateLoadingBay([FromBody]LoadingBayViewModel loadingBayView)
        {
            return await _adminService.UpdateLoadingBay(loadingBayView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("LoadingBay/Delete", Name = "LoadingBayDelete")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> DeleteLoadingBay([FromBody]LoadingBayViewModel loadingBayView)
        {
            return await _adminService.DeleteLoadingBay(loadingBayView);
        }
		
		[AuthenticateRequire]
        [HttpGet]
        [Route("LoadingBay/GetLoadingBayByCode/code={code}", Name = "GetLoadingBayByCode")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> GetLoadingBayByCode(string code)
        {
            return await _adminService.GetLoadingBayByCode(code);
        }

        /* Lane API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("Lane/GetAll", Name = "LaneGetAll")]
        public async Task<ResponseViewModel<LaneViewModel>> GetAllLane()
        {
            return await _adminService.GetAllLane();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Lane/SearchByCode/code={code}", Name = "LaneSearchByCode")]
        public async Task<ResponseViewModel<LaneViewModel>> SearchLane(string code)
        {
            return await _adminService.SearchLane(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Lane/Add", Name = "LaneAddNew")]
        public async Task<ResponseViewModel<LaneViewModel>> CreateNewLane([FromBody]LaneViewModel laneView)
        {
            return await _adminService.CreateNewLane(laneView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Lane/Update", Name = "LaneUpdate")]
        public async Task<ResponseViewModel<LaneViewModel>> UpdateLane([FromBody]LaneViewModel laneView)
        {
            return await _adminService.UpdateLane(laneView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Lane/Delete", Name = "LaneDelete")]
        public async Task<ResponseViewModel<LaneViewModel>> DeleteLane([FromBody]LaneViewModel laneView)
        {
            return await _adminService.DeleteLane(laneView);
		}


        /* Device_Camera API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("Camera/GetAll", Name = "CameraGetAll")]
        public async Task<ResponseViewModel<Camera>> GetAllCamera()
        {
            return await _adminService.GetAllCamera();
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Camera/Add", Name = "CameraAddNew")]
        public async Task<ResponseViewModel<Camera>> CreateNewCamera([FromBody]Camera CameraView)
        {
            return await _adminService.CreateNewCamera(CameraView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Camera/Update", Name = "CameraUpdate")]
        public async Task<ResponseViewModel<Camera>> UpdateCamera([FromBody]Camera CameraView)
        {
            return await _adminService.UpdateCamera(CameraView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Camera/Delete", Name = "CameraDelete")]
        public async Task<ResponseViewModel<Camera>> DeleteCamera([FromBody]Camera CameraView)
        {
            return await _adminService.DeleteCamera(CameraView);
        }

        /* Constrain API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("Constrain/GetAll", Name = "ConstrainGetAll")]
        public async Task<ResponseViewModel<Constrain>> GetAllConstrain()
        {
            return await _adminService.GetAllConstrain();
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Constrain/Get/{category}", Name = "ConstrainGetByCategory")]
        public async Task<ResponseViewModel<Constrain>> ConstrainGetByCategory(string category)
        {
            return await _adminService.GetConstrainByCategory(category);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Constrain/Update", Name = "ConstrainUpdate")]
        public async Task<ResponseViewModel<Constrain>> UpdateConstrain([FromBody]Constrain ConstrainView)
        {
            return await _adminService.UpdateConstrain(ConstrainView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Constrain/UpdatePrintHeader", Name = "UpdatePrintHeader")]
        public async Task<ResponseViewModel<PrintHeader>> UpdatePrintHeader([FromBody]PrintHeader printHeader)
        {
            return await _adminService.UpdatePrintHeader(printHeader);
        }


        /* DO API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("DO/GetAll", Name = "DOGetAll")]
        public async Task<ResponseViewModel<DeliveryOrderViewModel>> GetAllDO()
        {
            return await _adminService.GetAllDO();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("DO/SearchByCode/code={code}", Name = "DOSearchByCode")]
        public async Task<ResponseViewModel<DeliveryOrderViewModel>> SearchDO(string code)
        {
            return await _adminService.SearchDO(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("DO/Add", Name = "DOAddNew")]
        public async Task<ResponseViewModel<DeliveryOrderViewModel>> CreateNewDO([FromBody]DeliveryOrderViewModel DOView)
        {
            return await _adminService.CreateNewDO(DOView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("DO/Update", Name = "DOUpdate")]
        public async Task<ResponseViewModel<DeliveryOrderViewModel>> UpdateDO([FromBody]DeliveryOrderViewModel DOView)
        {
            return await _adminService.UpdateDO(DOView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("DO/Delete", Name = "DODelete")]
        public async Task<ResponseViewModel<DeliveryOrderViewModel>> DeleteDO([FromBody]DeliveryOrderViewModel DOView)
        {
            return await _adminService.DeleteDO(DOView);
        }

        /* SO API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("SO/GetAll", Name = "SOGetAll")]
        public async Task<ResponseViewModel<SaleOrderViewModel>> GetAllSO()
        {
            return await _adminService.GetAllSO();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("WB/GetAll", Name = "WBGetAll")]
        public async Task<ResponseViewModel<WeighBridge>> GetAllWB()
        {
            return await _adminService.GetAllWB();
        }

        /* SystemFunction API */
        [AuthenticateRequire]
        [HttpGet]
        [Route("SystemFunction/GetAll", Name = "GetAllSystemFunction")]
        public async Task<ResponseViewModel<SystemFunctionViewModel>> GetAllSystemFunction()
        {
            return await _adminService.GetAllSystemFunction();
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("EmployeeGroup/UpdateEmployeeGroupFunction", Name = "UpdateEmployeeGroupFunction")]
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> UpdateEmployeeGroupFunction([FromBody]EmployeeGroupViewModel employeeGroupViewModel)
        {
            return await _adminService.UpdateEmployeeGroupFunction(employeeGroupViewModel);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("RFID/GetAllRFID", Name = "GetAllRFID")]
        public async Task<ResponseViewModel<RFIDCardViewModel>> GetAllRFID()
        {
            return await _adminService.GetAllRFID();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("RFID/SearchRFID/{code}", Name = "SearchRFID")]
        public async Task<ResponseViewModel<RFIDCardViewModel>> SearchRFID(string code)
        {
            return await _adminService.SearchRFID(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("RFID/CreateNewRFID", Name = "CreateNewRFID")]
        public async Task<ResponseViewModel<RFIDCardViewModel>> CreateNewRFID([FromBody]RFIDCardViewModel rFIDCardView)
        {
            return await _adminService.CreateNewRFID(rFIDCardView);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("RFID/UpdateRFIDCardStatus", Name = "UpdateRFIDCardStatus")]
        public async Task<ResponseViewModel<RFIDCardViewModel>> UpdateRFIDCardStatus([FromBody]RFIDCardViewModel rFIDCardView)
        {
            return await _adminService.UpdateRFIDCardStatus(rFIDCardView);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("UPC/GetAllUserPC/", Name = "GetAllUserPC")]
        public async Task<ResponseViewModel<UserPC>> GetAllUserPC()
        {
            return await _adminService.GetAllUserPC();
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("UPC/GetUserPCByIP/{IP}", Name = "GetUserPCByIP")]
        public async Task<ResponseViewModel<UserPC>> GetUserPCByIP(string IP)
        {
            return await _adminService.GetUserPCByIP(IP);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("UPC/UpdateUserPC", Name = "UpdateUserPC")]
        public async Task<ResponseViewModel<UserPC>> UpdateUserPC([FromBody]UserPC userPC)
        {
            return await _adminService.UpdateUserPC(userPC);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("BadgeReader/GetBadgeReaderByCode/{code}", Name = "GetBadgeReaderByCode")]
        public async Task<ResponseViewModel<BadgeReader>> GetBadgeReaderByCode(string code)
        {
            return await _adminService.GetBadgeReaderByCode(code);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("BadgeReader/UpdateBadgeReader", Name = "UpdateBadgeReader")]
        public async Task<ResponseViewModel<BadgeReader>> UpdateBadgeReader([FromBody]BadgeReader badgeReader)
        {
            return await _adminService.UpdateBadgeReader(badgeReader);
        }
    }
}
