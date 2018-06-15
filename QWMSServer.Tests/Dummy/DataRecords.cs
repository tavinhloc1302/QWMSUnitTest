using QWMSServer.Data.Common;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public static class DataRecords
    {
        public static CarrierVendor CARRIER_VENDOR_NORMAL = new CarrierVendor()
        {
            ID = 1,
            addressEn = "Address in English",
            addressVi = "Address in Vietnamese",
            code = "0123",
            contactPerson = "Galvin Nguyen",
            department = "Sky",
            isDelete = false,
            nameEn = "Sky Rider 1",
            nameVi = "Sky Rider 1",
            shortName = "SR1",
            taxCode = "0123",
            telNo = "0123456789"
        };
        public static CarrierVendor CARRIER_VENDOR_NORMAL_2 = new CarrierVendor()
        {
            ID = 2,
            addressEn = "Address in English",
            addressVi = "Address in Vietnamese",
            code = "3210",
            contactPerson = "Galvin Nguyen",
            department = "Sky",
            isDelete = false,
            nameEn = "Sky Rider 2",
            nameVi = "Sky Rider 2",
            shortName = "SR2",
            taxCode = "0123",
            telNo = "98765432100123456789"
        };

        public static Company COMPANY_NORMAL = new Company()
        {
            code = "0123",
            ID = 1,
            isDelete = false,
            nameEn = "Sky Rider 1",
            nameVi = "Sky Rider 1",

        };
        public static Company COMPANY_NORMAL_2 = new Company()
        {
            code = "3210",
            ID = 2,
            isDelete = false,
            nameEn = "Sky Rider 2",
            nameVi = "Sky Rider 2",
        };

        public static LoadingBay LOADING_BAY_NORMAL = new LoadingBay()
        { ID = 1, code = "1111", nameVi = "Bay 1 VI", nameEn = "Bay 1", warehouseID = null, warehouse = null, isDelete = false, };
        public static LoadingBay LOADING_BAY_NORMAL_2 = new LoadingBay()
        { ID = 2, code = "2222", nameVi = "Bay 2 VI", nameEn = "Bay 2", warehouseID = null, warehouse = null, isDelete = false, };
        public static LoadingBay LOADING_BAY_DELETED = new LoadingBay()
        { ID = 3, code = "3333", nameVi = "Bay 3 VI", nameEn = "Bay 3", warehouseID = null, warehouse = null, isDelete = true, };

        public static TruckType TRUCK_TYPE_TRUCK = new TruckType()
        { ID = Constant.TRUCK, code = "1111", description = "Truck", isDelete = false };
        public static TruckType TRUCK_TYPE_CONTAINER = new TruckType()
        { ID = Constant.CONTAINER, code = "2222", description = "Container", isDelete = false };
        public static TruckType TRUCK_TYPE_PUMP = new TruckType()
        { ID = Constant.PUMP, code = "3333", description = "Punp", isDelete = false };
        public static TruckType TRUCK_TYPE_TRUCK_CONTAINER = new TruckType()
        { ID = Constant.TRUCK_CONTAINER, code = "4444", description = "Truck container", isDelete = false };

        public static Truck TRUCK_NORMAL = new Truck()
        { ID = 1, code = "A001", plateNumber = "1111", weightValueRegistWithCalofig = 0.71111F, carrierVendorID = 1, truckLenght = 20.1111F, truckHeight = 2.51111F, truckWidth = 2.1111F, containerLenght = 15.1111F, containerWidth = 2.1111F, containerHeight = 2.1111F, truckNetWeight = 2.1111F, weightValueRegistWithTransportDepartment = 3.1111F, totalWeight = 4.1111F, expireYear = 2025, truckTypeID = 1, loadingTypeID = 1, KPI = 1111, isDelete = false };
        public static Truck TRUCK_DELETED = new Truck()
        { ID = 2, code = "A002", plateNumber = "2222", weightValueRegistWithCalofig = 0.72222F, carrierVendorID = 2, truckLenght = 20.2222F, truckHeight = 2.52222F, truckWidth = 2.2222F, containerLenght = 15.2222F, containerWidth = 2.2222F, containerHeight = 2.2222F, truckNetWeight = 2.2222F, weightValueRegistWithTransportDepartment = 3.2222F, totalWeight = 4.2222F, expireYear = 2025, truckTypeID = 1, loadingTypeID = 1, KPI = 2222, isDelete = true };
        public static Truck TRUCK_DELETED_TYPE = new Truck()
        { ID = 3, code = "A003", plateNumber = "3333", weightValueRegistWithCalofig = 0.73333F, carrierVendorID = 3, truckLenght = 20.3333F, truckHeight = 2.53333F, truckWidth = 2.3333F, containerLenght = 15.3333F, containerWidth = 2.3333F, containerHeight = 2.3333F, truckNetWeight = 2.3333F, weightValueRegistWithTransportDepartment = 3.3333F, totalWeight = 4.3333F, expireYear = 2025, truckTypeID = 2, loadingTypeID = 2, KPI = 3333, isDelete = false };
        public static Truck TRUCK_NORMAL_2 = new Truck()
        { ID = 4, code = "A004", plateNumber = "4444", weightValueRegistWithCalofig = 0.71111F, carrierVendorID = 1, truckLenght = 20.1111F, truckHeight = 2.51111F, truckWidth = 2.1111F, containerLenght = 15.1111F, containerWidth = 2.1111F, containerHeight = 2.1111F, truckNetWeight = 2.1111F, weightValueRegistWithTransportDepartment = 3.1111F, totalWeight = 4.1111F, expireYear = 2025, truckTypeID = 1, loadingTypeID = 1, KPI = 1111, isDelete = false };

        public static RFIDCard RFID_CARD_NORMAL = new RFIDCard()
        { code = "0123", ID = 1, isDelete = false, status = 1 };
        public static RFIDCard RFID_CARD_NORMAL_2 = new RFIDCard()
        { code = "3210", ID = 2, isDelete = false, status = 1 };
        public static RFIDCard RFID_CARD_DELETED = new RFIDCard()
        { code = "3456", ID = 3, isDelete = true, status = 1 };

        public static Employee EMPLOYEE_NORMAL = new Employee()
        { ID = 1, code = "0123", isDelete = false, firstName = "Van Hoang", lastName = "Dinh", rfidCard = RFID_CARD_NORMAL, RFIDCardID = RFID_CARD_NORMAL.ID, userID = null, user = null, };
        public static Employee EMPLOYEE_DELTED = new Employee()
        { ID = 1, code = "1234", isDelete = true, firstName = "Van Dang", lastName = "Huynh", rfidCard = RFID_CARD_NORMAL, RFIDCardID = RFID_CARD_NORMAL.ID, userID = null, user = null, };

        public static LoadingType LOADING_TYPE_NORMAL = new LoadingType()
        { ID = 1, code = "1111", description = "1111 Desc", isDelete = false };
        public static LoadingType LOADING_TYPE_DELETED = new LoadingType()
        { ID = 1, code = "2222", description = "2222 Desc", isDelete = true };

        public static Driver DRIVER_NORMAL = new Driver()
        { code = "1234", carrierVendor = null, carrierVendorID = 1, ID = 1, isDelete = false, nameEn = "Sky Rider 1", nameVi = "Sky Rider 1", phoneNo = "0123456789", remark = "Remark", nameViNoSign = "Address in Vietnamese", driverLicenseNo = "0123456789", IDNo = "0123456789" };
        public static Driver DRIVER_DELETED = new Driver()
        { code = "2345", carrierVendor = null, carrierVendorID = 2, ID = 2, isDelete = true, nameEn = "Sky Rider 2", nameVi = "Sky Rider 2", phoneNo = "9876543210", remark = "Remark", nameViNoSign = "Address in Vietnamese", driverLicenseNo = "9876543210", IDNo = "9876543210" };
        public static Driver DRIVER_NORMAL_2 = new Driver()
        { code = "3456", carrierVendor = null, carrierVendorID = 1, ID = 3, isDelete = false, nameEn = "Sky Rider 1", nameVi = "Sky Rider 1", phoneNo = "0123456789", remark = "Remark", nameViNoSign = "Address in Vietnamese", driverLicenseNo = "0123456789", IDNo = "0123456789" };

        public static GatePass GATE_PASS_NORMAL = new GatePass()
        { createDate = DateTime.Now, driver = DRIVER_NORMAL, driverCamCapturePath = "Driver Path 1", driverID = 1, employeeID = EMPLOYEE_NORMAL.ID, employee = EMPLOYEE_NORMAL, code = "0123", ID = 1, isDelete = false, enterTime = DateTime.Now, leaveTime = DateTime.Now, loadingBayID = 1, orders = null, queueLists = null, RFIDCardID = RFID_CARD_NORMAL.ID, RFIDCard = RFID_CARD_NORMAL, stateID = 1, truckID = TRUCK_NORMAL.ID, truck = TRUCK_NORMAL, truckGroupID = 1, truckTyeID = TRUCK_TYPE_TRUCK.ID };
        public static GatePass GATE_PASS_DELETED = new GatePass()
        { createDate = DateTime.Now, driverID = DRIVER_NORMAL_2.ID, driver = DRIVER_NORMAL_2, driverCamCapturePath = "Driver Path 2", employeeID = EMPLOYEE_NORMAL.ID, employee = EMPLOYEE_NORMAL, code = "3210", ID = 2, isDelete = true, enterTime = DateTime.Now, leaveTime = DateTime.Now, loadingBayID = 2, orders = null, queueLists = null, RFIDCardID = RFID_CARD_NORMAL_2.ID, RFIDCard = RFID_CARD_NORMAL_2, stateID = 2, truckID = TRUCK_NORMAL_2.ID, truck = TRUCK_NORMAL_2, truckGroupID = 2, truckTyeID = TRUCK_TYPE_TRUCK.ID };
        public static GatePass GATE_PASS_FOR_UPDATE = new GatePass()
        { createDate = DateTime.Now, driverID = DRIVER_NORMAL.ID, driver = DRIVER_NORMAL, driverCamCapturePath = "Driver Path Update", employeeID = EMPLOYEE_NORMAL.ID, employee = EMPLOYEE_NORMAL, code = "3456", ID = 4, isDelete = false, enterTime = DateTime.Now, leaveTime = DateTime.Now, loadingBayID = 1, orders = null, queueLists = null, RFIDCardID = RFID_CARD_NORMAL.ID, RFIDCard = RFID_CARD_NORMAL, stateID = 1, truckID = TRUCK_NORMAL.ID, truck = TRUCK_NORMAL, truckGroupID = 1, truckTyeID = TRUCK_TYPE_TRUCK.ID };
        public static GatePass GATE_PASS_1ST_ORDER_DELI_TYPE_NOT_PUMP = new GatePass()
        { createDate = DateTime.Now, driver = DRIVER_NORMAL, driverCamCapturePath = "Driver Path 1", driverID = 1, employeeID = EMPLOYEE_NORMAL.ID, employee = EMPLOYEE_NORMAL, code = "1000", ID = 1000, isDelete = false, enterTime = DateTime.Now, leaveTime = DateTime.Now, loadingBayID = 1, orders = null, queueLists = null, RFIDCardID = RFID_CARD_NORMAL.ID, RFIDCard = RFID_CARD_NORMAL, stateID = 1, truckID = TRUCK_NORMAL.ID, truck = TRUCK_NORMAL, truckGroupID = 1, truckTyeID = TRUCK_TYPE_TRUCK.ID };
        public static GatePass GATE_PASS_1ST_ORDER_DELI_TYPE_PUMP = new GatePass()
        { createDate = DateTime.Now, driver = DRIVER_NORMAL, driverCamCapturePath = "Driver Path 1", driverID = 1, employeeID = EMPLOYEE_NORMAL.ID, employee = EMPLOYEE_NORMAL, code = "1001", ID = 1001, isDelete = false, enterTime = DateTime.Now, leaveTime = DateTime.Now, loadingBayID = 1, orders = null, queueLists = null, RFIDCardID = RFID_CARD_NORMAL.ID, RFIDCard = RFID_CARD_NORMAL, stateID = 1, truckID = TRUCK_NORMAL.ID, truck = TRUCK_NORMAL, truckGroupID = 1, truckTyeID = TRUCK_TYPE_PUMP.ID };
        public static GatePass GATE_PASS_1ST_ORDER_PURCHASE = new GatePass()
        { createDate = DateTime.Now, driver = DRIVER_NORMAL, driverCamCapturePath = "Driver Path 1", driverID = 1, employeeID = EMPLOYEE_NORMAL.ID, employee = EMPLOYEE_NORMAL, code = "1010", ID = 1010, isDelete = false, enterTime = DateTime.Now, leaveTime = DateTime.Now, loadingBayID = 1, orders = null, queueLists = null, RFIDCardID = RFID_CARD_NORMAL.ID, RFIDCard = RFID_CARD_NORMAL, stateID = 1, truckID = TRUCK_NORMAL.ID, truck = TRUCK_NORMAL, truckGroupID = 1, truckTyeID = TRUCK_TYPE_TRUCK.ID };
        public static GatePass GATE_PASS_1ST_ORDER_TYPE_OTHER = new GatePass()
        { createDate = DateTime.Now, driver = DRIVER_NORMAL, driverCamCapturePath = "Driver Path 1", driverID = 1, employeeID = EMPLOYEE_NORMAL.ID, employee = EMPLOYEE_NORMAL, code = "1100", ID = 1100, isDelete = false, enterTime = DateTime.Now, leaveTime = DateTime.Now, loadingBayID = 1, orders = null, queueLists = null, RFIDCardID = RFID_CARD_NORMAL.ID, RFIDCard = RFID_CARD_NORMAL, stateID = 1, truckID = TRUCK_NORMAL.ID, truck = TRUCK_NORMAL, truckGroupID = 1, truckTyeID = TRUCK_TYPE_TRUCK.ID };

        public static Lane LANE_NORMAL = new Lane()
        { ID = 1, code = "1", nameVi = "Lane 1 EN", nameEn = "Lane 1 VI", loadingBayID = 1, truckTypeID = 1, loadingTypeID = 1, minCapacity = 1, maxCapactity = 10, isDelete = false, status = 1, usingStatus = 0 };
        public static Lane LANE_DELETED = new Lane()
        { ID = 2, code = "2", nameVi = "Lane 2 EN", nameEn = "Lane 2 VI", loadingBayID = 2, truckTypeID = 2, loadingTypeID = 2, minCapacity = 2, maxCapactity = 20, isDelete = true, status = 0, usingStatus = 0 };

        public static QueueList QUEUE_LIST_NORMAL = new QueueList()
        { estimateTime = 1, code = "1234", ID = 1, isDelete = false, gatePassID = GATE_PASS_NORMAL.ID, gatePass = GATE_PASS_NORMAL, laneID = LANE_NORMAL.ID, lane = LANE_NORMAL, truckID = TRUCK_NORMAL.ID, truck = TRUCK_NORMAL, queueTime = DateTime.Now, queueOrder = 1 };
        public static QueueList QUEUE_LIST_NORMAL_2 = new QueueList()
        { estimateTime = 1, code = "2345", ID = 2, isDelete = false, gatePassID = GATE_PASS_1ST_ORDER_TYPE_OTHER.ID, gatePass = GATE_PASS_1ST_ORDER_TYPE_OTHER, laneID = LANE_NORMAL.ID, lane = LANE_NORMAL, truckID = TRUCK_NORMAL.ID, truck = TRUCK_NORMAL, queueTime = DateTime.Now, queueOrder = 2 };
        public static QueueList QUEUE_LIST_DELETED = new QueueList()
        { estimateTime = 1, code = "3456", ID = 3, isDelete = true, gatePassID = GATE_PASS_NORMAL.ID, gatePass = GATE_PASS_NORMAL, laneID = LANE_NORMAL.ID, lane = LANE_NORMAL, truckID = TRUCK_NORMAL.ID, truck = TRUCK_NORMAL, queueTime = DateTime.Now, queueOrder = 3 };

        public static OrderType ORDER_TYPE_DELIVERY = new OrderType()
        { ID = OrderTypeConst.DELIVERYORDER, code = "DELIVERY", description = "Delivery", isDelete = false };
        public static OrderType ORDER_TYPE_PURCHASE = new OrderType()
        { ID = OrderTypeConst.PURCHASEORDER, code = "PURCHASE", description = "Purchase", isDelete = false };
        public static OrderType ORDER_TYPE_INTERNAL = new OrderType()
        { ID = OrderTypeConst.INTERNALORDER, code = "INTERNAL", description = "Internal", isDelete = false };
        public static OrderType ORDER_TYPE_OTHER = new OrderType()
        { ID = OrderTypeConst.OTHERSORDER, code = "OTHER", description = "Other", isDelete = false };

        public static Order ORDER_NORMAL_DELI = new Order()
        { ID = 1, code = "1234", orderTypeID = ORDER_TYPE_DELIVERY.ID, orderType = ORDER_TYPE_DELIVERY, grossWeight = 10, gatePassID = GATE_PASS_NORMAL.ID, gatePass = GATE_PASS_NORMAL, plantID = null, plant = null, doID = null, deliveryOrder = null, poID = null, purchaseOrder = null, isDelete = false, };
        public static Order ORDER_NORMAL_PURCHASE = new Order()
        { ID = 2, code = "2345", orderTypeID = ORDER_TYPE_PURCHASE.ID, orderType = ORDER_TYPE_PURCHASE, grossWeight = 20, gatePassID = GATE_PASS_NORMAL.ID, gatePass = GATE_PASS_NORMAL, plantID = null, plant = null, doID = null, deliveryOrder = null, poID = null, purchaseOrder = null, isDelete = false, };
        public static Order ORDER_NORMAL_TYPE_OTHER = new Order()
        { ID = 3, code = "3456", orderTypeID = ORDER_TYPE_OTHER.ID, orderType = ORDER_TYPE_OTHER, grossWeight = 30, gatePassID = GATE_PASS_NORMAL.ID, gatePass = GATE_PASS_NORMAL, plantID = null, plant = null, doID = null, deliveryOrder = null, poID = null, purchaseOrder = null, isDelete = false, };

        public static Customer CUSTOMER_NORMAL = new Customer()
        { ID = 1, code = "1111", nameVi = "KH 1", nameEn = "Cus 1", shortName = "K 1", invoiceAddressVi = "Ho Chi Minh", invoiceAddressEn = "HCMC", taxCode = "Tax 1", contactPerson = "Contact 1", telNo = "0908832000", faxNo = "11111111", email = "cus1@yopmail.com", isDelete = false };
        public static Customer CUSTOMER_DELETED = new Customer()
        { ID = 2, code = "2222", nameVi = "KH 2", nameEn = "Cus 2", shortName = "K 2", invoiceAddressVi = "Ho Chi Minh", invoiceAddressEn = "HCMC", taxCode = "Tax 2", contactPerson = "Contact 2", telNo = "0908832000", faxNo = "22222222", email = "cus2@yopmail.com", isDelete = true };

        public static DeliveryOrderType DELIVERY_ORDER_TYPE_NORMAL = new DeliveryOrderType()
        { ID = 1, code = "1111", description = "Normal", isDelete = false };
        public static DeliveryOrderType DELIVERY_ORDER_TYPE_DELETED = new DeliveryOrderType()
        { ID = 2, code = "2222", description = "Deleted", isDelete = true };

        //public static DeliveryOrderType DELIVERY_ORDER_NORMAL = new DeliveryOrder()
        //{ ID = 1, code = "1111", doNumber = "DO Number 1", createDate = DateTime.Now, soNumber = "SO Number 1", customerID = CUSTOMER_NORMAL.ID, customer = CUSTOMER_NORMAL, carrierVendorID = 1, carrierVendor = 1, remark = 1, sloc = 1, doTypeID = 1, deliveryOrderType = 1, customerWarehouseID = 1, isDelete = 1, customerWarehouse = 1 };

        static DataRecords()
        {
            GATE_PASS_1ST_ORDER_DELI_TYPE_NOT_PUMP.orders = new List<Order>() { ORDER_NORMAL_DELI, ORDER_NORMAL_PURCHASE };
            GATE_PASS_1ST_ORDER_DELI_TYPE_PUMP.orders = new List<Order>() { ORDER_NORMAL_DELI, ORDER_NORMAL_PURCHASE };
            GATE_PASS_1ST_ORDER_PURCHASE.orders = new List<Order>() { ORDER_NORMAL_PURCHASE, ORDER_NORMAL_DELI };
            GATE_PASS_1ST_ORDER_TYPE_OTHER.orders = new List<Order>() { ORDER_NORMAL_TYPE_OTHER, ORDER_NORMAL_DELI, ORDER_NORMAL_PURCHASE };
            GATE_PASS_FOR_UPDATE.orders = new List<Order>() { ORDER_NORMAL_TYPE_OTHER, ORDER_NORMAL_DELI, ORDER_NORMAL_PURCHASE };
        }
    }
}
