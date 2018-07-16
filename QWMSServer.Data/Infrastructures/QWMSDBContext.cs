using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Infrastructures
{
    public class QWMSDBContext : DbContext, IDBContext
    {
        //public virtual DbSet<Products> Products { get; set; }
        //public virtual DbSet<ProductTypes> ProductTypes { get; set; }
        public virtual DbSet<ActivityLog> ActivityLog { get; set; }
        public virtual DbSet<Barrier> Barrier { get; set; }
        public virtual DbSet<BadgeReader> BadgeReader { get; set; }
        public virtual DbSet<Camera> Camera { get; set; }
        public virtual DbSet<CarrierVendor> Carrier { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<COMPort> COMPort { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<DeliveryOrder> DeliveryOrder { get; set; }
        public virtual DbSet<DeliveryOrderType> DeliveryOrderType { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Employee_EmployeeGroup> Employee_EmployeeGroup { get; set; }
        public virtual DbSet<EmployeeGroup> EmployeeGroup { get; set; }
        public virtual DbSet<EmployeeGroup_SystemFunction> EmployeeGroup_SystemFunction { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRole { get; set; }
        public virtual DbSet<GatePass> GatePass { get; set; }
        public virtual DbSet<Lane> Lane { get; set; }
        public virtual DbSet<LaneType> LaneType { get; set; }
        public virtual DbSet<LoadingBay> LoadingBay { get; set; }
        public virtual DbSet<LoadingBayType> LoadingBayType { get; set; }
        public virtual DbSet<LoadingType> LoadingType { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<OrderMaterial> OrderMaterial { get; set; }
        public virtual DbSet<OrderType> OrderType { get; set; }
        public virtual DbSet<Plant> Plant { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<PurchaseOrderType> PurchaseOrderType { get; set; }
        public virtual DbSet<QueueList> QueueList { get; set; }
        public virtual DbSet<ReWeightRecord> ReWeightRecord { get; set; }
        public virtual DbSet<RFIDCard> RFIDCard { get; set; }
        public virtual DbSet<SaleOrder> SaleOrder { get; set; }
        public virtual DbSet<Sensor> Sensor { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<StateRecord> StateRecord { get; set; }
        public virtual DbSet<SystemFunction> SystemFunction { get; set; }
        public virtual DbSet<Truck> Truck { get; set; }
        public virtual DbSet<TruckGroup> TruckAction { get; set; }
        public virtual DbSet<TruckType> TruckType { get; set; }
        public virtual DbSet<UnitType> UnitType { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VoiceRecord> VoiceRecord { get; set; }
        public virtual DbSet<WarehouseType> WarehouseType { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<WeighBridge> WeighBridge { get; set; }
        public virtual DbSet<WeightRecord> WeightRecord { get; set; }
        public virtual DbSet<CustomerWarehouse> CustomerWarehouse { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<PrintHeader> PrintHeader { get; set; }
        public virtual DbSet<UserPassword> UserPassword { get; set; }
        public virtual DbSet<Constrain> Constrain { get; set; }
        public virtual DbSet<UserPC> UserPC { get; set; }
        public virtual DbSet<WeighbridgeConfiguration> WeighbridgeConfiguration { get; set; }

        public QWMSDBContext() : base("name=QWMSDBConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}
