using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Infrastructures
{
    public interface IDBContext
    {
        Database Database { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<ActivityLog> ActivityLog { get; set; }
        DbSet<Barrier> Barrier { get; set; }
        DbSet<BadgeReader> BadgeReader { get; set; }
        DbSet<Camera> Camera { get; set; }
        DbSet<CarrierVendor> Carrier { get; set; }
        DbSet<Company> Company { get; set; }
        DbSet<COMPort> COMPort { get; set; }
        DbSet<Customer> Customer { get; set; }
        DbSet<DeliveryOrder> DeliveryOrder { get; set; }
        DbSet<DeliveryOrderType> DeliveryOrderType { get; set; }
        DbSet<Driver> Driver { get; set; }
        DbSet<Employee> Employee { get; set; }
        DbSet<Employee_EmployeeGroup> Employee_EmployeeGroup { get; set; }
        DbSet<EmployeeGroup> EmployeeGroup { get; set; }
        DbSet<EmployeeGroup_SystemFunction> EmployeeGroup_SystemFunction { get; set; }
        DbSet<EmployeeRole> EmployeeRole { get; set; }
        DbSet<GatePass> GatePass { get; set; }
        DbSet<Lane> Lane { get; set; }
        DbSet<LaneType> LaneType { get; set; }
        DbSet<LoadingBay> LoadingBay { get; set; }
        DbSet<LoadingBayType> LoadingBayType { get; set; }
        DbSet<LoadingType> LoadingType { get; set; }
        DbSet<Material> Material { get; set; }
        DbSet<Order> Order { get; set; }
        DbSet<OrderItem> OrderItem { get; set; }
        DbSet<OrderMaterial> OrderMaterial { get; set; }
        DbSet<OrderType> OrderType { get; set; }
        DbSet<Plant> Plant { get; set; }
        DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        DbSet<PurchaseOrderType> PurchaseOrderType { get; set; }
        DbSet<QueueList> QueueList { get; set; }
        DbSet<ReWeightRecord> ReWeightRecord { get; set; }
        DbSet<RFIDCard> RFIDCard { get; set; }
        DbSet<SaleOrder> SaleOrder { get; set; }
        DbSet<Sensor> Sensor { get; set; }
        DbSet<State> State { get; set; }
        DbSet<StateRecord> StateRecord { get; set; }
        DbSet<SystemFunction> SystemFunction { get; set; }
        DbSet<Truck> Truck { get; set; }
        DbSet<TruckGroup> TruckAction { get; set; }
        DbSet<TruckType> TruckType { get; set; }
        DbSet<UnitType> UnitType { get; set; }
        DbSet<User> User { get; set; }
        DbSet<VoiceRecord> VoiceRecord { get; set; }
        DbSet<WarehouseType> WarehouseType { get; set; }
        DbSet<Warehouse> Warehouse { get; set; }
        DbSet<WeighBridge> WeighBridge { get; set; }
        DbSet<WeightRecord> WeightRecord { get; set; }
        DbSet<CustomerWarehouse> CustomerWarehouse { get; set; }
        DbSet<Token> Token { get; set; }
        DbSet<Constrain> Constrain { get; set; }
        DbSet<PrintHeader> PrintHeader { get; set; }
        DbSet<UserPassword> UserPassword { get; set; }
        DbSet<UserPC> UserPC { get; set; }
        DbSet<WeighbridgeConfiguration> WeighbridgeConfiguration { get; set; }
        Task<int> SaveChangesAsync();

        int SaveChanges();
        void Dispose();
    }
}
