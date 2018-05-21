using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using QWMSServer.Data.Repository;
using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;

namespace QWMSServer.Data.Infrastructures
{
    public class DependencyConfig
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<QWMSDBContext>().As<IDBContext>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            // Repositories
            builder.RegisterType<ProductsRepository>().As<IProductsRepository>().InstancePerRequest();
            builder.RegisterType<AccessLogRepository>().As<IAccessLogRepository>().InstancePerRequest();
            builder.RegisterType<BarrierRepository>().As<IBarrierRepository>().InstancePerRequest();
            builder.RegisterType<CameraRepository>().As<ICameraRepository>().InstancePerRequest();
            builder.RegisterType<CarrierVendorRepository>().As<ICarrierVendorRepository>().InstancePerRequest();
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>().InstancePerRequest();
            builder.RegisterType<COMPortRepository>().As<ICOMPortRepository>().InstancePerRequest();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerRequest();
            builder.RegisterType<DeliveryOrderRepository>().As<IDeliveryOrderRepository>().InstancePerRequest();
            builder.RegisterType<DeliveryOrderTypeRepository>().As<IDeliveryOrderTypeRepository>().InstancePerRequest();
            builder.RegisterType<DriverRepository>().As<IDriverRepository>().InstancePerRequest();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerRequest();
            builder.RegisterType<Employee_EmployeeGroupRepository>().As<IEmployee_EmployeeGroupRepository>().InstancePerRequest();
            builder.RegisterType<EmployeeGroupRepository>().As<IEmployeeGroupRepository>().InstancePerRequest();
            builder.RegisterType<EmployeeGroup_SystemFunctionRepository>().As<IEmployeeGroup_SystemFunctionRepository>().InstancePerRequest();
            builder.RegisterType<EmployeeRoleRepository>().As<IEmployeeRoleRepository>().InstancePerRequest();
            builder.RegisterType<GatePassRepository>().As<IGatePassRepository>().InstancePerRequest();
            builder.RegisterType<LaneRepository>().As<ILaneRepository>().InstancePerRequest();
            builder.RegisterType<LaneTypeRepository>().As<ILaneTypeRepository>().InstancePerRequest();
            builder.RegisterType<LoadingBayRepository>().As<ILoadingBayRepository>().InstancePerRequest();
            builder.RegisterType<LoadingBayTypeRepository>().As<ILoadingBayTypeRepository>().InstancePerRequest();
            builder.RegisterType<LoadingTypeRepository>().As<ILoadingTypeRepository>().InstancePerRequest();
            builder.RegisterType<MaterialRepository>().As<IMaterialRepository>().InstancePerRequest();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerRequest();
            builder.RegisterType<OrderItemRepository>().As<IOrderItemRepository>().InstancePerRequest();
            builder.RegisterType<OrderMaterialRepository>().As<IOrderMaterialRepository>().InstancePerRequest();
            builder.RegisterType<OrderTypeRepository>().As<IOrderTypeRepository>().InstancePerRequest();
            builder.RegisterType<PlantRepository>().As<IPlantRepository>().InstancePerRequest();
            builder.RegisterType<PurchaseOrderRepository>().As<IPurchaseOrderRepository>().InstancePerRequest();
            builder.RegisterType<PurchaseOrderTypeRepository>().As<IPurchaseOrderTypeRepository>().InstancePerRequest();
            builder.RegisterType<QueueListRepository>().As<IQueueListRepository>().InstancePerRequest();
            builder.RegisterType<ReWeightRecordRepository>().As<IReWeightRecordRepository>().InstancePerRequest();
            builder.RegisterType<RFIDCardRepository>().As<IRFIDCardRepository>().InstancePerRequest();
            builder.RegisterType<SaleOrderRepository>().As<ISaleOrderRepository>().InstancePerRequest();
            builder.RegisterType<SensorRepository>().As<ISensorRepository>().InstancePerRequest();
            builder.RegisterType<StateRepository>().As<IStateRepository>().InstancePerRequest();
            builder.RegisterType<StateRecordRepository>().As<IStateRecordRepository>().InstancePerRequest();
            builder.RegisterType<SystemFunctionRepository>().As<ISystemFunctionRepository>().InstancePerRequest();
            builder.RegisterType<TruckRepository>().As<ITruckRepository>().InstancePerRequest();
            builder.RegisterType<TruckActionRepository>().As<ITruckActionRepository>().InstancePerRequest();
            builder.RegisterType<TruckTypeRepository>().As<ITruckTypeRepository>().InstancePerRequest();
            builder.RegisterType<UnitTypeRepository>().As<IUnitTypeRepository>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<VoiceRecordRepository>().As<IVoiceRecordRepository>().InstancePerRequest();
            builder.RegisterType<WarehouseTypeRepository>().As<IWarehouseTypeRepository>().InstancePerRequest();
            builder.RegisterType<WarehouseRepository>().As<IWarehouseRepository>().InstancePerRequest();
            builder.RegisterType<WeighBridgeRepository>().As<IWeighBridgeRepository>().InstancePerRequest();
            builder.RegisterType<WeightRecordRepository>().As<IWeightRecordRepository>().InstancePerRequest();
            builder.RegisterType<CustomerWarehouseRepository>().As<ICustomerWarehouseRepository>().InstancePerRequest();
            builder.RegisterType<TokenRepository>().As<ITokenRepository>().InstancePerRequest();

            // Services
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerRequest();
            builder.RegisterType<AdminService>().As<IAdminService>().InstancePerRequest();
            builder.RegisterType<SecurityServices>().As<ISecurityServicecs>().InstancePerRequest();
            builder.RegisterType<QueueService>().As<IQueueService>().InstancePerRequest();
            builder.RegisterType<WeightService>().As<IWeightService>().InstancePerRequest();
            builder.RegisterType<WarehouseService>().As<IWarehouseService>().InstancePerRequest();
            builder.RegisterType<CommonService>().As<ICommonService>().InstancePerRequest();
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerRequest();
        }
    }
}
