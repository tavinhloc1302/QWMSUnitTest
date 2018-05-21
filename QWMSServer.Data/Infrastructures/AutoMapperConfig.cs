using AutoMapper;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;

namespace QWMSServer.Data.Infrastructures
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Lane, LaneViewModel>().ReverseMap();
                config.CreateMap<Customer, CustomerViewModel>().ReverseMap();
				config.CreateMap<Driver, DriverViewModel>().ReverseMap();
                config.CreateMap<CarrierVendor, CarrierVendorViewModel>().ReverseMap();
                config.CreateMap<CustomerWarehouse, CustomerWarehouseViewModel>().ReverseMap();
                config.CreateMap<GatePass, GatePassViewModel>().ReverseMap();
                config.CreateMap<LoadingBay, LoadingBayViewModel>().ReverseMap();
                config.CreateMap<OrderType, OrderTypeViewModel>().ReverseMap();
                config.CreateMap<TruckType, TruckTypeViewModel>().ReverseMap();
                config.CreateMap<State, StateViewModel>().ReverseMap();
                config.CreateMap<Lane, LaneViewModel>().ReverseMap();
                config.CreateMap<Warehouse, WarehouseViewModel>().ReverseMap();
                config.CreateMap<Plant, PlantViewModel>().ReverseMap();
                config.CreateMap<Truck, TruckViewModel>().ReverseMap();
                config.CreateMap<QueueList, QueueListViewModel>().ReverseMap();
                config.CreateMap<Order, OrderViewModel>().ReverseMap();
                config.CreateMap<OrderMaterial, OrderMaterialViewModel>().ReverseMap();
                config.CreateMap<Material, MaterialViewModel>().ReverseMap();
                config.CreateMap<DeliveryOrder, DOViewModel>().ReverseMap();
                config.CreateMap<PurchaseOrder, POViewModel>().ReverseMap();
                config.CreateMap<Employee, EmployeeViewModel>().ReverseMap();
                config.CreateMap<TruckGroup, TruckGroupViewModel>().ReverseMap();
                config.CreateMap<Token, TokenViewModel>().ReverseMap();
                config.CreateMap<EmployeeGroup, EmployeeGroupViewModel>().ReverseMap();
                config.CreateMap<User, UserViewModel>().ReverseMap();
                config.CreateMap<RFIDCard, RFIDCardViewModel>().ReverseMap();
            });
        }
    }
}
