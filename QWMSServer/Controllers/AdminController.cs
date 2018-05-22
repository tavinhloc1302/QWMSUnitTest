using QWMSServer.Data.Services;
using QWMSServer.Filter;
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

        [HttpGet]
        [Route("Customer/SearchByCode/code={code}", Name ="CustomerSearchByCode")]
        public async Task<ResponseViewModel<CustomerViewModel>> SearchCustomer(string code)
        {
            return await _adminService.SearchCustomer(code);
        }

        [HttpPost]
        [Route("Customer/Add", Name = "CustomerAddNew")]
        public async Task<ResponseViewModel<CustomerViewModel>> CreateNewCustomer([FromBody]CustomerViewModel customerView)
        {
            return await _adminService.CreateNewCustomer(customerView);
        }

        [HttpPost]
        [Route("Customer/Update", Name = "CustomerUpdate")]
        public async Task<ResponseViewModel<CustomerViewModel>> UpdateCustomer([FromBody]CustomerViewModel customerView)
        {
            return await _adminService.UpdateCustomer(customerView);
        }

        [HttpPost]
        [Route("Customer/Delete", Name = "CustomerDelte")]
        public async Task<ResponseViewModel<CustomerViewModel>> DeleteCustomer([FromBody]CustomerViewModel customerView)
        {
            return await _adminService.DeleteCustomer(customerView);
        }


        /* Driver API */
        [HttpGet]
        [Route("Driver/GetAll", Name = "DriverGetAll")]
        public async Task<ResponseViewModel<DriverViewModel>> GetAllDriver()
        {
            return await _adminService.GetAllDriver();
        }

        [HttpGet]
        [Route("Driver/SearchByCode/code={code}", Name = "DriverSearchByCode")]
        public async Task<ResponseViewModel<DriverViewModel>> SearchDriver(string code)
        {
            return await _adminService.SearchDriver(code);
        }

        [HttpPost]
        [Route("Driver/Add", Name = "DriverAddNew")]
        public async Task<ResponseViewModel<DriverViewModel>> CreateNewDriver([FromBody]DriverViewModel driverView)
        {
            return await _adminService.CreateNewDriver(driverView);
        }

        [HttpPost]
        [Route("Driver/Update", Name = "DriverUpdate")]
        public async Task<ResponseViewModel<DriverViewModel>> UpdateDriver([FromBody]DriverViewModel driverView)
        {
            return await _adminService.UpdateDriver(driverView);
        }

        [HttpPost]
        [Route("Driver/Delete", Name = "DriverDelte")]
        public async Task<ResponseViewModel<DriverViewModel>> DeleteDriver([FromBody]DriverViewModel driverView)
        {
            return await _adminService.DeleteDriver(driverView);
        }

        /* Carrier API */
        [HttpGet]
        [Route("Carrier/GetAll", Name = "CarrierGetAll")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> GetAllCarrier()
        {
            return await _adminService.GetAllCarrier();
        }

        [HttpGet]
        [Route("Carrier/SearchByCode/code={code}", Name = "CarrierSearchByCode")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> SearchCarrier(string code)
        {
            return await _adminService.SearchCarrier(code);
        }

        [HttpPost]
        [Route("Carrier/Add", Name = "CarrierAddNew")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> CreateNewCarrier([FromBody]CarrierVendorViewModel carrierView)
        {
            return await _adminService.CreateNewCarrier(carrierView);
        }

        [HttpPost]
        [Route("Carrier/Update", Name = "CarrierUpdate")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> UpdateCarrier([FromBody]CarrierVendorViewModel carrierView)
        {
            return await _adminService.UpdateCarrier(carrierView);
        }

        [HttpPost]
        [Route("Carrier/Delete", Name = "CarrierDelte")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> DeleteCarrier([FromBody]CarrierVendorViewModel carrierView)
        {
            return await _adminService.DeleteCarrier(carrierView);
        }
		
        /* Material API */
        [HttpGet]
        [Route("Material/GetAll", Name = "MaterialGetAll")]
        public async Task<ResponseViewModel<MaterialViewModel>> GetAllMaterial()
        {
            return await _adminService.GetAllMaterial();
        }

        [HttpGet]
        [Route("Material/SearchByCode/code={code}", Name = "MaterialSearchByCode")]
        public async Task<ResponseViewModel<MaterialViewModel>> SearchMaterial(string code)
        {
            return await _adminService.SearchMaterial(code);
        }

        [HttpPost]
        [Route("Material/Add", Name = "MaterialAddNew")]
        public async Task<ResponseViewModel<MaterialViewModel>> CreateNewMaterial([FromBody]MaterialViewModel materialView)
        {
            return await _adminService.CreateNewMaterial(materialView);
        }

        [HttpPost]
        [Route("Material/Update", Name = "MaterialUpdate")]
        public async Task<ResponseViewModel<MaterialViewModel>> UpdateMaterial([FromBody]MaterialViewModel materialView)
        {
            return await _adminService.UpdateMaterial(materialView);
        }

        [HttpPost]
        [Route("Material/Delete", Name = "MaterialDelte")]
        public async Task<ResponseViewModel<MaterialViewModel>> DeleteMaterial([FromBody]MaterialViewModel materialView)
        {
            return await _adminService.DeleteMaterial(materialView);
        }

        /* UnitType API */
        [HttpGet]
        [Route("UnitType/GetAll", Name = "UnitTypeGetAll")]
        public async Task<ResponseViewModel<UnitTypeViewModel>> GetAllUnitType()
        {
            return await _adminService.GetAllUnitType();
        }

        [HttpGet]
        [Route("UnitType/SearchByCode/code={code}", Name = "UnitTypeSearchByCode")]
        public async Task<ResponseViewModel<UnitTypeViewModel>> SearchUnitType(string code)
        {
            return await _adminService.SearchUnitType(code);
        }

        [HttpPost]
        [Route("UnitType/Add", Name = "UnitTypeAddNew")]
        public async Task<ResponseViewModel<UnitTypeViewModel>> CreateNewUnitType([FromBody]UnitTypeViewModel unitTypeView)
        {
            return await _adminService.CreateNewUnitType(unitTypeView);
        }

        [HttpPost]
        [Route("UnitType/Update", Name = "UnitTypeUpdate")]
        public async Task<ResponseViewModel<UnitTypeViewModel>> UpdateUnitType([FromBody]UnitTypeViewModel unitTypeView)
        {
            return await _adminService.UpdateUnitType(unitTypeView);
        }

        [HttpPost]
        [Route("UnitType/Delete", Name = "UnitTypeDelte")]
        public async Task<ResponseViewModel<UnitTypeViewModel>> DeleteUnitType([FromBody]UnitTypeViewModel unitTypeView)
        {
            return await _adminService.DeleteUnitType(unitTypeView);
        }

        /* Truck API */
        [HttpGet]
        [Route("Truck/GetAll", Name = "TruckGetAll")]
        public async Task<ResponseViewModel<TruckViewModel>> GetAllTruck()
        {
            return await _adminService.GetAllTruck();
        }

        [HttpGet]
        [Route("Truck/GetAllSuggestedDriver", Name = "TruckGetAllSuggestedDriver")]
        public async Task<ResponseViewModel<TruckViewModel>> TruckGetAllSuggestedDriver()
        {
            return await _adminService.TruckGetAllSuggestedDriver();
        }

        [HttpGet]
        [Route("Truck/SearchByCode/code={code}", Name = "TruckSearchByCode")]
        public async Task<ResponseViewModel<TruckViewModel>> SearchTruck(string code)
        {
            return await _adminService.SearchTruck(code);
        }

        [HttpPost]
        [Route("Truck/Add", Name = "TruckAddNew")]
        public async Task<ResponseViewModel<TruckViewModel>> CreateNewTruck([FromBody]TruckViewModel truckView)
        {
            return await _adminService.CreateNewTruck(truckView);
        }

        [HttpPost]
        [Route("Truck/Update", Name = "TruckUpdate")]
        public async Task<ResponseViewModel<TruckViewModel>> UpdateTruck([FromBody]TruckViewModel truckView)
        {
            return await _adminService.UpdateTruck(truckView);
        }

        [HttpPost]
        [Route("Truck/Delete", Name = "TruckDelete")]
        public async Task<ResponseViewModel<TruckViewModel>> DeleteTruck([FromBody]TruckViewModel truckView)
        {
            return await _adminService.DeleteTruck(truckView);
        }

        /* Truck Type API */
        [HttpGet]
        [Route("TruckType/GetAll", Name = "TruckTypeGetAll")]
        public async Task<ResponseViewModel<TruckTypeViewModel>> GetAllTruckType()
        {
            return await _adminService.GetAllTruckType();
        }

        [HttpGet]
        [Route("TruckType/SearchByCode/code={code}", Name = "TruckTypeSearchByCode")]
        public async Task<ResponseViewModel<TruckTypeViewModel>> SearchTruckType(string code)
        {
            return await _adminService.SearchTruckType(code);
        }

        [HttpPost]
        [Route("TruckType/Add", Name = "TruckTypeAddNew")]
        public async Task<ResponseViewModel<TruckTypeViewModel>> CreateNewTruckType([FromBody]TruckTypeViewModel truckTypeView)
        {
            return await _adminService.CreateNewTruckType(truckTypeView);
        }

        [HttpPost]
        [Route("TruckType/Update", Name = "TruckTypeUpdate")]
        public async Task<ResponseViewModel<TruckTypeViewModel>> UpdateTruckType([FromBody]TruckTypeViewModel truckTypeView)
        {
            return await _adminService.UpdateTruckType(truckTypeView);
        }

        [HttpPost]
        [Route("TruckType/Delete", Name = "TruckTypeDelete")]
        public async Task<ResponseViewModel<TruckTypeViewModel>> DeleteTruckType([FromBody]TruckTypeViewModel truckTypeView)
        {
            return await _adminService.DeleteTruckType(truckTypeView);
        }

        /* Loading Type API */
        [HttpGet]
        [Route("LoadingType/GetAll", Name = "LoadingTypeGetAll")]
        public async Task<ResponseViewModel<LoadingTypeViewModel>> GetAllLoadingType()
        {
            return await _adminService.GetAllLoadingType();
        }

        [HttpGet]
        [Route("LoadingType/SearchByCode/code={code}", Name = "LoadingTypeSearchByCode")]
        public async Task<ResponseViewModel<LoadingTypeViewModel>> SearchLoadingType(string code)
        {
            return await _adminService.SearchLoadingType(code);
        }

        [HttpPost]
        [Route("LoadingType/Add", Name = "LoadingTypeAddNew")]
        public async Task<ResponseViewModel<LoadingTypeViewModel>> CreateNewLoadingType([FromBody]LoadingTypeViewModel loadingTypeView)
        {
            return await _adminService.CreateNewLoadingType(loadingTypeView);
        }

        [HttpPost]
        [Route("LoadingType/Update", Name = "LoadingTypeUpdate")]
        public async Task<ResponseViewModel<LoadingTypeViewModel>> UpdateLoadingType([FromBody]LoadingTypeViewModel loadingTypeView)
        {
            return await _adminService.UpdateLoadingType(loadingTypeView);
        }

        [HttpPost]
        [Route("LoadingType/Delete", Name = "LoadingTypeDelete")]
        public async Task<ResponseViewModel<LoadingTypeViewModel>> DeleteLoadingType([FromBody]LoadingTypeViewModel loadingTypeView)
        {
            return await _adminService.DeleteLoadingType(loadingTypeView);
        }

        /* Employee API */
        [HttpGet]
        [Route("Employee/GetAll", Name = "EmployeeGetAll")]
        public async Task<ResponseViewModel<EmployeeViewModel>> GetAllEmployee()
        {
            return await _adminService.GetAllEmployee();
        }

        [HttpGet]
        [Route("Employee/SearchByCode/code={code}", Name = "EmployeeSearchByCode")]
        public async Task<ResponseViewModel<EmployeeViewModel>> SearchEmployee(string code)
        {
            return await _adminService.SearchEmployee(code);
        }

        [HttpPost]
        [Route("Employee/Add", Name = "EmployeeAddNew")]
        public async Task<ResponseViewModel<EmployeeViewModel>> CreateNewEmployee([FromBody]EmployeeViewModel employeeView)
        {
            return await _adminService.CreateNewEmployee(employeeView);
        }

        [HttpPost]
        [Route("Employee/Update", Name = "EmployeeUpdate")]
        public async Task<ResponseViewModel<EmployeeViewModel>> UpdateEmployee([FromBody]EmployeeViewModel employeeView)
        {
            return await _adminService.UpdateEmployee(employeeView);
        }

        [HttpPost]
        [Route("Employee/Delete", Name = "EmployeeDelete")]
        public async Task<ResponseViewModel<EmployeeViewModel>> DeleteEmployee([FromBody]EmployeeViewModel employeeView)
        {
            return await _adminService.DeleteEmployee(employeeView);
        }

        /* Employee Group API */
        [HttpGet]
        [Route("EmployeeGroup/GetAll", Name = "EmployeeGroupGetAll")]
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> GetAllEmployeeGroup()
        {
            return await _adminService.GetAllEmployeeGroup();
        }

        [HttpGet]
        [Route("EmployeeGroup/SearchByCode/code={code}", Name = "EmployeeGroupSearchByCode")]
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> SearchEmployeeGroup(string code)
        {
            return await _adminService.SearchEmployeeGroup(code);
        }

        [HttpPost]
        [Route("EmployeeGroup/Add", Name = "EmployeeGroupAddNew")]
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> CreateNewEmployeeGroup([FromBody]EmployeeGroupViewModel employeeGroupView)
        {
            return await _adminService.CreateNewEmployeeGroup(employeeGroupView);
        }

        [HttpPost]
        [Route("EmployeeGroup/Update", Name = "EmployeeGroupUpdate")]
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> UpdateEmployeeGroup([FromBody]EmployeeGroupViewModel employeeGroupView)
        {
            return await _adminService.UpdateEmployeeGroup(employeeGroupView);
        }

        [HttpPost]
        [Route("EmployeeGroup/Delete", Name = "EmployeeGroupDelete")]
        public async Task<ResponseViewModel<EmployeeGroupViewModel>> DeleteEmployeeGroup([FromBody]EmployeeGroupViewModel employeeGroupView)
        {
            return await _adminService.DeleteEmployeeGroup(employeeGroupView);
        }

        /* User API */
        [HttpGet]
        [Route("User/GetAll", Name = "UserGetAll")]
        public async Task<ResponseViewModel<UserViewModel>> GetAllUser()
        {
            return await _adminService.GetAllUser();
        }

        [HttpGet]
        [Route("User/SearchByCode/code={code}", Name = "UserSearchByCode")]
        public async Task<ResponseViewModel<UserViewModel>> SearchUser(string code)
        {
            return await _adminService.SearchUser(code);
        }

        [HttpPost]
        [Route("User/Add", Name = "UserAddNew")]
        public async Task<ResponseViewModel<UserViewModel>> CreateNewUser([FromBody]UserViewModel userView)
        {
            return await _adminService.CreateNewUser(userView);
        }

        [HttpPost]
        [Route("User/Update", Name = "UserUpdate")]
        public async Task<ResponseViewModel<UserViewModel>> UpdateUser([FromBody]UserViewModel userView)
        {
            return await _adminService.UpdateUser(userView);
        }

        [HttpPost]
        [Route("User/Delete", Name = "UserDelete")]
        public async Task<ResponseViewModel<UserViewModel>> DeleteUser([FromBody]UserViewModel userView)
        {
            return await _adminService.DeleteUser(userView);
        }
		
        /* Employee Role API */
        [HttpGet]
        [Route("EmployeeRole/GetAll", Name = "EmployeeRoleGetAll")]
        public async Task<ResponseViewModel<EmployeeRoleViewModel>> GetAllEmployeeRole()
        {
            return await _adminService.GetAllEmployeeRole();
        }

        [HttpGet]
        [Route("EmployeeRole/SearchByCode/code={code}", Name = "EmployeeRoleSearchByCode")]
        public async Task<ResponseViewModel<EmployeeRoleViewModel>> SearchEmployeeRole(string code)
        {
            return await _adminService.SearchEmployeeRole(code);
        }

        [HttpPost]
        [Route("EmployeeRole/Add", Name = "EmployeeRoleAddNew")]
        public async Task<ResponseViewModel<EmployeeRoleViewModel>> CreateNewEmployeeRole([FromBody]EmployeeRoleViewModel employeeRoleView)
        {
            return await _adminService.CreateNewEmployeeRole(employeeRoleView);
        }

        [HttpPost]
        [Route("EmployeeRole/Update", Name = "EmployeeRoleUpdate")]
        public async Task<ResponseViewModel<EmployeeRoleViewModel>> UpdateEmployeeRole([FromBody]EmployeeRoleViewModel employeeRoleView)
        {
            return await _adminService.UpdateEmployeeRole(employeeRoleView);
        }

        [HttpPost]
        [Route("EmployeeRole/Delete", Name = "EmployeeRoleDelete")]
        public async Task<ResponseViewModel<EmployeeRoleViewModel>> DeleteEmployeeRole([FromBody]EmployeeRoleViewModel employeeRoleView)
        {
            return await _adminService.DeleteEmployeeRole(employeeRoleView);
        }

        /* Plant API */
        [HttpGet]
        [Route("Plant/GetAll", Name = "PlantGetAll")]
        public async Task<ResponseViewModel<PlantViewModel>> GetAllPlant()
        {
            return await _adminService.GetAllPlant();
        }

        [HttpGet]
        [Route("Plant/SearchByCode/code={code}", Name = "PlantSearchByCode")]
        public async Task<ResponseViewModel<PlantViewModel>> SearchPlant(string code)
        {
            return await _adminService.SearchPlant(code);
        }

        [HttpPost]
        [Route("Plant/Add", Name = "PlantAddNew")]
        public async Task<ResponseViewModel<PlantViewModel>> CreateNewPlant([FromBody]PlantViewModel plantView)
        {
            return await _adminService.CreateNewPlant(plantView);
        }

        [HttpPost]
        [Route("Plant/Update", Name = "PlantUpdate")]
        public async Task<ResponseViewModel<PlantViewModel>> UpdatePlant([FromBody]PlantViewModel plantView)
        {
            return await _adminService.UpdatePlant(plantView);
        }

        [HttpPost]
        [Route("Plant/Delete", Name = "PlantDelete")]
        public async Task<ResponseViewModel<PlantViewModel>> DeletePlant([FromBody]PlantViewModel plantView)
        {
            return await _adminService.DeletePlant(plantView);
        }

        /* Company API */
        [HttpGet]
        [Route("Company/GetAll", Name = "CompanyGetAll")]
        public async Task<ResponseViewModel<CompanyViewModel>> GetAllCompany()
        {
            return await _adminService.GetAllCompany();
        }

        [HttpGet]
        [Route("Company/SearchByCode/code={code}", Name = "CompanySearchByCode")]
        public async Task<ResponseViewModel<CompanyViewModel>> SearchCompany(string code)
        {
            return await _adminService.SearchCompany(code);
        }

        [HttpPost]
        [Route("Company/Add", Name = "CompanyAddNew")]
        public async Task<ResponseViewModel<CompanyViewModel>> CreateNewCompany([FromBody]CompanyViewModel companyView)
        {
            return await _adminService.CreateNewCompany(companyView);
        }

        [HttpPost]
        [Route("Company/Update", Name = "CompanyUpdate")]
        public async Task<ResponseViewModel<CompanyViewModel>> UpdateCompany([FromBody]CompanyViewModel companyView)
        {
            return await _adminService.UpdateCompany(companyView);
        }

        [HttpPost]
        [Route("Company/Delete", Name = "CompanyDelete")]
        public async Task<ResponseViewModel<CompanyViewModel>> DeleteCompany([FromBody]CompanyViewModel companyView)
        {
            return await _adminService.DeleteCompany(companyView);
        }

        /* Warehouse API */
        [HttpGet]
        [Route("Warehouse/GetAll", Name = "WarehouseGetAll")]
        public async Task<ResponseViewModel<WarehouseViewModel>> GetAllWarehouse()
        {
            return await _adminService.GetAllWarehouse();
        }

        [HttpGet]
        [Route("Warehouse/SearchByCode/code={code}", Name = "WarehouseSearchByCode")]
        public async Task<ResponseViewModel<WarehouseViewModel>> SearchWarehouse(string code)
        {
            return await _adminService.SearchWarehouse(code);
        }

        [HttpGet]
        [Route("Warehouse/GetByCode/code={code}", Name = "GetWareHouseByCode")]
        public async Task<ResponseViewModel<WarehouseViewModel>> GetWarehouseByCode(string code)
        {
            return await _adminService.GetWarehouseByCode(code);
        }

        [HttpPost]
        [Route("Warehouse/Add", Name = "WarehouseAddNew")]
        public async Task<ResponseViewModel<WarehouseViewModel>> CreateNewWarehouse([FromBody]WarehouseViewModel warehouseView)
        {
            return await _adminService.CreateNewWarehouse(warehouseView);
        }

        [HttpPost]
        [Route("Warehouse/Update", Name = "WarehouseUpdate")]
        public async Task<ResponseViewModel<WarehouseViewModel>> UpdateWarehouse([FromBody]WarehouseViewModel warehouseView)
        {
            return await _adminService.UpdateWarehouse(warehouseView);
        }

        [HttpPost]
        [Route("Warehouse/Delete", Name = "WarehouseDelete")]
        public async Task<ResponseViewModel<WarehouseViewModel>> DeleteWarehouse([FromBody]WarehouseViewModel warehouseView)
        {
            return await _adminService.DeleteWarehouse(warehouseView);
        }

        /* Loading Bay API */
        [HttpGet]
        [Route("LoadingBay/GetAll", Name = "LoadingBayGetAll")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> GetAllLoadingBay()
        {
            return await _adminService.GetAllLoadingBay();
        }

        [HttpGet]
        [Route("LoadingBay/SearchByCode/code={code}", Name = "LoadingBaySearchByCode")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> SearchLoadingBay(string code)
        {
            return await _adminService.SearchLoadingBay(code);
        }

        [HttpPost]
        [Route("LoadingBay/Add", Name = "LoadingBayAddNew")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> CreateNewLoadingBay([FromBody]LoadingBayViewModel loadingBayView)
        {
            return await _adminService.CreateNewLoadingBay(loadingBayView);
        }

        [HttpPost]
        [Route("LoadingBay/Update", Name = "LoadingBayUpdate")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> UpdateLoadingBay([FromBody]LoadingBayViewModel loadingBayView)
        {
            return await _adminService.UpdateLoadingBay(loadingBayView);
        }

        [HttpPost]
        [Route("LoadingBay/Delete", Name = "LoadingBayDelete")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> DeleteLoadingBay([FromBody]LoadingBayViewModel loadingBayView)
        {
            return await _adminService.DeleteLoadingBay(loadingBayView);
        }
		
		[HttpGet]
        [Route("LoadingBay/GetLoadingBayByCode/code={code}", Name = "GetLoadingBayByCode")]
        public async Task<ResponseViewModel<LoadingBayViewModel>> GetLoadingBayByCode(string code)
        {
            return await _adminService.GetLoadingBayByCode(code);
        }

        /* Lane API */
        [HttpGet]
        [Route("Lane/GetAll", Name = "LaneGetAll")]
        public async Task<ResponseViewModel<LaneViewModel>> GetAllLane()
        {
            return await _adminService.GetAllLane();
        }

        [HttpGet]
        [Route("Lane/SearchByCode/code={code}", Name = "LaneSearchByCode")]
        public async Task<ResponseViewModel<LaneViewModel>> SearchLane(string code)
        {
            return await _adminService.SearchLane(code);
        }

        [HttpPost]
        [Route("Lane/Add", Name = "LaneAddNew")]
        public async Task<ResponseViewModel<LaneViewModel>> CreateNewLane([FromBody]LaneViewModel laneView)
        {
            return await _adminService.CreateNewLane(laneView);
        }

        [HttpPost]
        [Route("Lane/Update", Name = "LaneUpdate")]
        public async Task<ResponseViewModel<LaneViewModel>> UpdateLane([FromBody]LaneViewModel laneView)
        {
            return await _adminService.UpdateLane(laneView);
        }

        [HttpPost]
        [Route("Lane/Delete", Name = "LaneDelete")]
        public async Task<ResponseViewModel<LaneViewModel>> DeleteLane([FromBody]LaneViewModel laneView)
        {
            return await _adminService.DeleteLane(laneView);
		}
        
    }
}
