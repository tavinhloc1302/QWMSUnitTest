using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;

namespace QWMSServer.Data.Services
{
    public interface IAdminService
    {
        /* Customer management block */
        Task<ResponseViewModel<CustomerViewModel>> GetAllCustomer();

        Task<ResponseViewModel<CustomerViewModel>> GetCustomerByCode(string code);

        Task<ResponseViewModel<CustomerViewModel>> SearchCustomer(string code);

        Task<ResponseViewModel<CustomerViewModel>> CreateNewCustomer(CustomerViewModel customerView);

        Task<ResponseViewModel<CustomerViewModel>> UpdateCustomer(CustomerViewModel customerView);

        Task<ResponseViewModel<CustomerViewModel>> DeleteCustomer(CustomerViewModel customerView);

        /* Driver management block */
        Task<ResponseViewModel<DriverViewModel>> GetAllDriver();

        Task<ResponseViewModel<DriverViewModel>> GetDriverByCode(string code);

        Task<ResponseViewModel<DriverViewModel>> SearchDriver(string code);

        Task<ResponseViewModel<DriverViewModel>> CreateNewDriver(DriverViewModel driverView);

        Task<ResponseViewModel<DriverViewModel>> UpdateDriver(DriverViewModel driverView);

        Task<ResponseViewModel<DriverViewModel>> DeleteDriver(DriverViewModel driverView);

        /* Carrier management block */
        Task<ResponseViewModel<CarrierVendorViewModel>> GetAllCarrier();

        Task<ResponseViewModel<CarrierVendorViewModel>> SearchCarrier(string code);

        Task<ResponseViewModel<CarrierVendorViewModel>> GetCarrierByCode(string code);

        Task<ResponseViewModel<CarrierVendorViewModel>> CreateNewCarrier(CarrierVendorViewModel carrierView);

        Task<ResponseViewModel<CarrierVendorViewModel>> UpdateCarrier(CarrierVendorViewModel carrierView);

        Task<ResponseViewModel<CarrierVendorViewModel>> DeleteCarrier(CarrierVendorViewModel carrierView);

		///* Login service */
  //      Task<List<SystemFunctionViewModel>> GetUserPermission(int userID);

  //      Task<ResponseViewModel<UserViewModel>> Login(string userName, string passWord);

        /* Material management block */
        Task<ResponseViewModel<MaterialViewModel>> GetAllMaterial();

        Task<ResponseViewModel<MaterialViewModel>> SearchMaterial(string code);

        Task<ResponseViewModel<MaterialViewModel>> GetMaterialByCode(string code);

        Task<ResponseViewModel<MaterialViewModel>> CreateNewMaterial(MaterialViewModel materialView);

        Task<ResponseViewModel<MaterialViewModel>> UpdateMaterial(MaterialViewModel materialView);

        Task<ResponseViewModel<MaterialViewModel>> DeleteMaterial(MaterialViewModel materialView);

        /* UnitType management block */
        Task<ResponseViewModel<UnitTypeViewModel>> GetAllUnitType();

        Task<ResponseViewModel<UnitTypeViewModel>> SearchUnitType(string code);

        Task<ResponseViewModel<UnitTypeViewModel>> GetUnitTypeByCode(string code);

        Task<ResponseViewModel<UnitTypeViewModel>> CreateNewUnitType(UnitTypeViewModel unitTypeView);

        Task<ResponseViewModel<UnitTypeViewModel>> UpdateUnitType(UnitTypeViewModel unitTypeView);

        Task<ResponseViewModel<UnitTypeViewModel>> DeleteUnitType(UnitTypeViewModel unitTypeView);

        /* Truck management block */
        Task<ResponseViewModel<TruckViewModel>> GetAllTruck();

        Task<ResponseViewModel<TruckViewModel>> TruckGetAllSuggestedDriver();

        Task<ResponseViewModel<TruckViewModel>> SearchTruck(string code);

        Task<ResponseViewModel<TruckViewModel>> GetTruckByCode(string code);

        Task<ResponseViewModel<TruckViewModel>> CreateNewTruck(TruckViewModel truckView);

        Task<ResponseViewModel<TruckViewModel>> UpdateTruck(TruckViewModel truckView);

        Task<ResponseViewModel<TruckViewModel>> DeleteTruck(TruckViewModel truckView);

        /* Truck Type management block */
        Task<ResponseViewModel<TruckTypeViewModel>> GetAllTruckType();

        Task<ResponseViewModel<TruckTypeViewModel>> SearchTruckType(string code);

        Task<ResponseViewModel<TruckTypeViewModel>> GetTruckTypeByCode(string code);

        Task<ResponseViewModel<TruckTypeViewModel>> CreateNewTruckType(TruckTypeViewModel truckTypeView);

        Task<ResponseViewModel<TruckTypeViewModel>> UpdateTruckType(TruckTypeViewModel truckTypeView);

        Task<ResponseViewModel<TruckTypeViewModel>> DeleteTruckType(TruckTypeViewModel truckTypeView);

        /* Loading Type management block */
        Task<ResponseViewModel<LoadingTypeViewModel>> GetAllLoadingType();

        Task<ResponseViewModel<LoadingTypeViewModel>> SearchLoadingType(string code);

        Task<ResponseViewModel<LoadingTypeViewModel>> GetLoadingTypeByCode(string code);

        Task<ResponseViewModel<LoadingTypeViewModel>> CreateNewLoadingType(LoadingTypeViewModel loadingTypeView);

        Task<ResponseViewModel<LoadingTypeViewModel>> UpdateLoadingType(LoadingTypeViewModel loadingTypeView);

        Task<ResponseViewModel<LoadingTypeViewModel>> DeleteLoadingType(LoadingTypeViewModel loadingTypeView);

        /* Employee management block */
        Task<ResponseViewModel<EmployeeViewModel>> GetAllEmployee();

        Task<ResponseViewModel<EmployeeViewModel>> SearchEmployee(string code);

        Task<ResponseViewModel<EmployeeViewModel>> GetEmployeeByCode(string code);

        Task<ResponseViewModel<EmployeeViewModel>> CreateNewEmployee(EmployeeViewModel employeeView);

        Task<ResponseViewModel<EmployeeViewModel>> UpdateEmployee(EmployeeViewModel employeeView);

        Task<ResponseViewModel<EmployeeViewModel>> DeleteEmployee(EmployeeViewModel employeeView);

        /* Employee management block */
        Task<ResponseViewModel<EmployeeGroupViewModel>> GetAllEmployeeGroup();

        Task<ResponseViewModel<EmployeeGroupViewModel>> SearchEmployeeGroup(string code);

        Task<ResponseViewModel<EmployeeGroupViewModel>> GetEmployeeGroupByCode(string code);

        Task<ResponseViewModel<EmployeeGroupViewModel>> CreateNewEmployeeGroup(EmployeeGroupViewModel employeeGroupView);

        Task<ResponseViewModel<EmployeeGroupViewModel>> UpdateEmployeeGroup(EmployeeGroupViewModel employeeGroupView);

        Task<ResponseViewModel<EmployeeGroupViewModel>> DeleteEmployeeGroup(EmployeeGroupViewModel employeeGroupView);

        /* User management block */
        Task<ResponseViewModel<UserViewModel>> GetAllUser();

        Task<ResponseViewModel<UserViewModel>> GetUserByEmployeeID(int employeeID);

        Task<ResponseViewModel<UserViewModel>> SearchUser(string code);

        Task<ResponseViewModel<UserViewModel>> GetUserByCode(string code);

        Task<ResponseViewModel<UserViewModel>> CreateNewUser(UserViewModel userView);

        Task<ResponseViewModel<UserViewModel>> UpdateUser(UserViewModel userView);

        Task<ResponseViewModel<UserViewModel>> DeleteUser(UserViewModel userView);

        Task<string> CreateUserName(string userName);

        Task<ResponseViewModel<UserViewModel>> UpdateUserPassword(UserViewModel userView);

        /* Employee Role management block */
        Task<ResponseViewModel<EmployeeRoleViewModel>> GetAllEmployeeRole();

        Task<ResponseViewModel<EmployeeRoleViewModel>> SearchEmployeeRole(string code);

        Task<ResponseViewModel<EmployeeRoleViewModel>> GetEmployeeRoleByCode(string code);

        Task<ResponseViewModel<EmployeeRoleViewModel>> CreateNewEmployeeRole(EmployeeRoleViewModel employeeRoleView);

        Task<ResponseViewModel<EmployeeRoleViewModel>> UpdateEmployeeRole(EmployeeRoleViewModel employeeRoleView);

        Task<ResponseViewModel<EmployeeRoleViewModel>> DeleteEmployeeRole(EmployeeRoleViewModel employeeRoleView);

        /* Plant management block */
        Task<ResponseViewModel<PlantViewModel>> GetAllPlant();

        Task<ResponseViewModel<PlantViewModel>> SearchPlant(string code);

        Task<ResponseViewModel<PlantViewModel>> GetPlantByCode(string code);

        Task<ResponseViewModel<PlantViewModel>> CreateNewPlant(PlantViewModel plantView);

        Task<ResponseViewModel<PlantViewModel>> UpdatePlant(PlantViewModel plantView);

        Task<ResponseViewModel<PlantViewModel>> DeletePlant(PlantViewModel plantView);

        /* Company management block */
        Task<ResponseViewModel<CompanyViewModel>> GetAllCompany();

        Task<ResponseViewModel<CompanyViewModel>> SearchCompany(string code);

        Task<ResponseViewModel<CompanyViewModel>> GetCompanyByCode(string code);

        Task<ResponseViewModel<CompanyViewModel>> CreateNewCompany(CompanyViewModel companyView);

        Task<ResponseViewModel<CompanyViewModel>> UpdateCompany(CompanyViewModel companyView);

        Task<ResponseViewModel<CompanyViewModel>> DeleteCompany(CompanyViewModel companyView);

        /* Warehouse management block */
        Task<ResponseViewModel<WarehouseViewModel>> GetAllWarehouse();

        Task<ResponseViewModel<WarehouseViewModel>> SearchWarehouse(string code);

        Task<ResponseViewModel<WarehouseViewModel>> GetWarehouseByCode(string code);

        Task<ResponseViewModel<WarehouseViewModel>> CreateNewWarehouse(WarehouseViewModel warehouseView);

        Task<ResponseViewModel<WarehouseViewModel>> UpdateWarehouse(WarehouseViewModel warehouseView);

        Task<ResponseViewModel<WarehouseViewModel>> DeleteWarehouse(WarehouseViewModel warehouseView);

        /* Loading Bay management block */
        Task<ResponseViewModel<LoadingBayViewModel>> GetAllLoadingBay();

        Task<ResponseViewModel<LoadingBayViewModel>> SearchLoadingBay(string code);

        Task<ResponseViewModel<LoadingBayViewModel>> GetLoadingBayByCode(string code);

        Task<ResponseViewModel<LoadingBayViewModel>> CreateNewLoadingBay(LoadingBayViewModel loadingBayView);

        Task<ResponseViewModel<LoadingBayViewModel>> UpdateLoadingBay(LoadingBayViewModel loadingBayView);

        Task<ResponseViewModel<LoadingBayViewModel>> DeleteLoadingBay(LoadingBayViewModel loadingBayView);

        /* Lane management block */
        Task<ResponseViewModel<LaneViewModel>> GetAllLane();

        Task<ResponseViewModel<LaneViewModel>> SearchLane(string code);

        Task<ResponseViewModel<LaneViewModel>> GetLaneByCode(string code);

        Task<ResponseViewModel<LaneViewModel>> CreateNewLane(LaneViewModel laneView);

        Task<ResponseViewModel<LaneViewModel>> UpdateLane(LaneViewModel laneView);

        Task<ResponseViewModel<LaneViewModel>> DeleteLane(LaneViewModel laneView);


        /* DO management block */
        Task<ResponseViewModel<DeliveryOrderViewModel>> GetAllDO();

        Task<ResponseViewModel<DeliveryOrderViewModel>> SearchDO(string code);

        Task<ResponseViewModel<DeliveryOrderViewModel>> GetDOByCode(string code);

        Task<ResponseViewModel<DeliveryOrderViewModel>> CreateNewDO(DeliveryOrderViewModel DOView);

        Task<ResponseViewModel<DeliveryOrderViewModel>> UpdateDO(DeliveryOrderViewModel DOView);

        Task<ResponseViewModel<DeliveryOrderViewModel>> DeleteDO(DeliveryOrderViewModel DOView);

        /* SO management block */
        Task<ResponseViewModel<SaleOrderViewModel>> GetAllSO();

        /* Constrain */
        #region
        Task<ResponseViewModel<Constrain>> GetAllConstrain();

        Task<ResponseViewModel<Constrain>> UpdateConstrain(Constrain ConstrainView);

        Task<ResponseViewModel<Constrain>> GetConstrainByCategory(string category);

        Task<ResponseViewModel<PrintHeader>> UpdatePrintHeader(PrintHeader printHeader);
        #endregion

        /* Permission */
        #region
        Task<ResponseViewModel<SystemFunctionViewModel>> GetAllSystemFunction();

        Task<ResponseViewModel<EmployeeGroupViewModel>> UpdateEmployeeGroupFunction(EmployeeGroupViewModel employeeGroupViewModel);
        #endregion

        /* Device management */
        #region

        /* RFID card */
        #region
        Task<ResponseViewModel<RFIDCardViewModel>> UpdateRFIDCardStatus(RFIDCardViewModel rFIDCardView);

        Task<ResponseViewModel<RFIDCardViewModel>> CreateNewRFID(RFIDCardViewModel rFIDCardV);

        Task<ResponseViewModel<RFIDCardViewModel>> GetAllRFID();

        Task<ResponseViewModel<RFIDCardViewModel>> SearchRFID(string code);
        #endregion

        /* Weigh Bridge */
        #region
        Task<ResponseViewModel<WeighBridge>> GetAllWB();
        Task<ResponseViewModel<WeighBridge>> UpdateWeighBridge(WeighBridge weighBridge);
        #endregion

        /* Camera management block */
        #region
        Task<ResponseViewModel<Camera>> GetAllCamera();

        Task<ResponseViewModel<Camera>> CreateNewCamera(Camera CameraView);

        Task<ResponseViewModel<Camera>> UpdateCamera(Camera CameraView);

        Task<ResponseViewModel<Camera>> DeleteCamera(Camera CameraView);
        #endregion

        /* User PC */
        #region
        Task<ResponseViewModel<UserPC>> GetAllUserPC();
        Task<ResponseViewModel<UserPC>> GetUserPCByIP(string IP);
        Task<ResponseViewModel<UserPC>> UpdateUserPC(UserPC userPC);
        #endregion

        /* Badge Reader */
        #region
        Task<ResponseViewModel<BadgeReader>> GetBadgeReaderByCode(string code);
        Task<ResponseViewModel<BadgeReader>> UpdateBadgeReader(BadgeReader badgeReader);
        #endregion
        #endregion
    }
}
