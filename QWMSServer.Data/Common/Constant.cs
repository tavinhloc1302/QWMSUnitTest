using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Common
{
    public static class Constant
    {
        public static string tmpDirectory = System.IO.Path.GetTempPath();
        public static string DriverCapturePath = tmpDirectory + @"DriverCapturePath\";
        public static string TruckCapturePath = tmpDirectory + @"TruckCapturePath\";
        public static int DELIVERYORDER = 1;
        public static int PURCHASEORDER = 2;
        public static int INTERNALORDER = 3;

        public static int TRUCK = 1;
        public static int CONTAINER = 2;
        public static int PUMP = 3;
        public static int TRUCK_CONTAINER = 4;
        public static int TRUCKGROUP1X = 1;
        public static int TRUCKGROUP2X = 2;
        public static int TRUCKGROUP3X = 3;

        public static int NULLLANE = 1; // OK
        public static string TRUCK_INTERNAL = "Nội bộ";
        public static string TRASH_CAR = "Xe rác";

        public static string ALL = "Tất cả";

        public static int MAX_LOGIN_USER = 15;
    }

    public static class QueryIncludes
    {
        public static List<String> LANEFULLINCLUDES = new List<string> { "LoadingBay", "LoadingType", "TruckType" };
        public static List<String> DRIVERFULLINCLUDES = new List<string> { "carrierVendor" };
        public static List<String> MATERIALFULLINCLUDES = new List<string> { "unit" };
        public static List<String> CUSTOMERFULLINCUDES = new List<string> { "customerWarehouses" };
        public static List<String> CUSTOMERWAREHOUSEFULLINCUDES = new List<string> { "customer" };
        public static List<String> GATEPASSFULLINCLUDES = new List<string> { "loadingBay", "printEmployee", "truck.truckType", "Driver", "State", "Employee", "orders.DeliveryOrder.customer", "queueLists.Lane.LoadingBay.Warehouse.Plant.Company", "truckGroup", "Truck.CarrierVendor", "orders.orderMaterials.material.unit", "orders.orderType", "orders.purchaseOrder.carrierVendor", "weightRecords", "customer", "warehouse", "material", "RFIDCard"};
        public static List<String> QUEUELISTFULLINCLUDES = new List<string> { "truck", "lane", "gatePass", "gatePass.state" };
        public static List<String> USERFULLINCLUDES = new List<string> { "employee", "employee.employeeGroup.functionMaps.systemFunction", "employee.rfidCard", "userPasswords" }; //employees.groupMaps.employeeGroup.functionMaps.systemFunction
        public static List<String> EMPLOYEEFULLINCLUDES = new List<string> { "employeeGroup", "employeeGroup.functionMaps.systemFunction", "rfidCard", "users" }; //groupMaps.employeeGroup.functionMaps.systemFunction
        public static List<String> EMPLOYEEINCLUDES = new List<string> { "employeeGroup", "rfidCard", "users" }; //groupMaps.employeeGroup.functionMaps.systemFunction
        public static List<String> EMPLOYEEGROUPFULLINCLUDES = new List<string> { "functionMaps.systemFunction"};
        public static List<String> TRUCKFULLINCLUDES = new List<string> { "truckType", "loadingType", "carrierVendor", "driver" };
        public static List<String> PLANTINCLUDES = new List<String> { "company" };
        public static List<String> WAREHOUSEINCLUDES = new List<string> { "plant", "loadingBays" };
        public static List<String> LOADINGBAYINCLUDES = new List<string> { "warehouse", "lanes" };
        public static List<String> LANEINCLUDES = new List<string> { "loadingBay", "loadingType", "truckType", "loadingBay.warehouse" };
        public static List<String> ORDERFULLINCUDES = new List<string> { "gatePass.weightRecords", "orderType", "deliveryOrder.customer", "deliveryOrder.customerWarehouse", "purchaseOrder.carrierVendor", "orderMaterials.material.unit" };
        public static List<String> DOINCLUDES = new List<String> { "customerWarehouse", "customer", "carrierVendor", "deliveryOrderType", "order.plant", "saleOrder" };
        public static List<String> WBINCLUDES = new List<string> { "WBConfiguration" };
        public static List<String> PCFULLINCLUDES = new List<string> { "weighBridges", "weighBridges.WBConfiguration", "badgeReaders", "cameras", "controllers", "controllers.valueSensors", "controllers.statusSensors", "controllers.openBarriers", "controllers.closeBarriers", "controllers.hazardLights" };

        // Security App
        public static List<String> SECURITY_QUEUE_INCLUDES = new List<string> { "Lane.LoadingBay.Warehouse", "GatePass.orders.OrderType", "GatePass.Truck.TruckType", "GatePass.State", "GatePass.TruckGroup", "GatePass.RFIDCard", "GatePass.orders.deliveryOrder.customer", "GatePass.orders.purchaseOrder.carrierVendor", "GatePass.RFIDCard" };
        public static List<String> SECURITY_GATEPASS_INCLUDES = new List<string> { "queueLists", "Driver", "Truck", "TruckGroup", "Truck.TruckType","orders.orderType", "orders.orderMaterials", "orders.deliveryOrder.Customer", "orders.PurchaseOrder.carrierVendor", "RFIDCard", "queueLists.Lane.LoadingBay.Warehouse", "State", "material", "warehouse", "customer", "weightRecords" };

        // Queue App
        public static List<String> QUEUE_GATEPASS_ORDER_INCLUDES = new List<string> { "orders" };

        // Weight App
        public static List<String> WEIGHT_LANE_INCLUDES = new List<string> { "truckType", "loadingBay.warehouse" };
        public static List<String> WEIGHT_RECORD_INCLUDES = new List<string> { "employee", "weighBridge", "gatePass.truck", "gatePass.Driver", "gatePass.orders.orderMaterials", "gatePass.orders.deliveryOrder.customer", "gatePass.orders.purchaseOrder.carrierVendor" }; //, "gatePass.customer", "gatePass.material"

        public static List<String> ACCESSLOGFULLINCLUDES = new List<String> { "employee" };

        public static List<String> RFIDFULLINCLUDES = new List<String> { "employees", "gatePasses" };

        public static List<String> ORDERSHORTINCUDES = new List<string> { "deliveryOrder.customer", "purchaseOrder.carrierVendor"};

    }

    public static class ResponseCode
    {
        public const int SUCCESS = 0;

        public const int ERR_USER_NOT_EXSIT = 1;
        public const int ERR_INVALID_LOGIN = 2;
        public const int ERR_MAX_LOGIN_SUPPORT = 3;
        // Error Code OFF Security App
        public const int ERR_DB_CONNECTION_FAILED = 1001;
        public const int ERR_SEC_NOT_SUPPORT_CONDITION = 1002;
        public const int ERR_SEC_NOT_FOUND_RFID = 1003;
        public const int ERR_SEC_NOT_PERMITTED_REG = 1004;
        public const int ERR_SEC_WRONG_CONFIRMED_RFID = 1005;
        public const int ERR_SEC_NOT_PERMITTED_CHECK_IN = 1006;
        public const int ERR_SEC_NOT_PERMITTED_CHECK_OUT = 1007;
        public const int ERR_SEC_WRONG_BODY_REQUEST_FORMAT = 1008;
        public const int ERR_SEC_NOT_FOUND_GATEPASS = 1009;
        public const int ERR_SEC_NOT_SUPPORT_UPDATED_STATE = 1010;
        public const int ERR_SEC_UNKNOW = 1011;
        public const int ERR_SEC_GATEPASS_LACK_STATEID = 1012;
        public const int ERR_SEC_NOT_PERMIT_PASS_SECURITY_GATE = 1013;
        public const int ERR_QUE_NO_QUEUE_FOUND = 3001;
        public const int ERR_QUE_NO_GATEPASS_FOUND = 3002;
        public const int ERR_QUE_GATEPASS_WRONG_STATE = 3003;
        public const int ERR_QUE_WEIGH_NO_FOUND = 3004;
        public const int ERR_WEI_WEIGH_NOT_PERMITTED = 3005;
        public const int ERR_WEI_CALL_NOT_PERMITTED = 3006;
        public const int ERR_USER_PERMISSION = 3007;
		public const int ERR_NO_OBJECT_FOUND = 3008;
        public const int ERR_DB_FAIL_TO_SAVE = 3009;
        public const int ERR_WEI_NOT_FOUND_EMPTY_GATEPASS = 3010;
        public const int ERR_WEIGHTED_GATEPASS = 3011;

        // Error Login
        public const int ERR_LOGIN_WRONG_USERNAME_PASS = 4001;
        public const int ERR_PASS_DUPLICATE = 4002;
        public const int PASSWORD_CHANGE_REQUIRE = 4003;

        // Error Import DO, PO
        public const int ERR_IMPORT_CREATE_SO_FAIL = 5001;
        public const int ERR_IMPORT_DATA_NOT_SYNC = 5002;
        public const int ERR_IMPORT_DATA_HAVED_IN_DB = 5003;
        public const int ERR_IMPORT_DONT_HAVE_MATERIAL_IN_DB = 5004;
        public const int ERR_IMPORT_DONT_HAVE_VENDOR_IN_DB = 5005;
        public const int ERR_IMPORT_DONT_HAVE_PLANT_IN_DB = 5006;
        public const int ERR_IMPORT_EXCEPTION = 5007;
    }

    public static class ResponseText
    {
        public const string SUCCESS = "OK";

        // Error Code OFF Security App
        public const string ERR_DB_CONNECTION_FAILED_VI = "Lỗi: Không thể kết nối\r\ncơ sở dữ liệu";
        public const string ERR_DB_CONNECTION_FAILED_ENG = "Eror: Could NOT connect to\r\nDatabase";
        public const string ERR_SEC_NOT_SUPPORT_CONDITION = "Error: Security App not support input condition";
        public const string ERR_SEC_NOT_FOUND_RFID_VI = "Lỗi: Không tìm thấy thẻ RFID";
        public const string ERR_SEC_NOT_FOUND_RFID_ENG = "Error: Could found the RFID Tag";
        public const string ERR_SEC_NOT_PERMITTED_REG_VI = "Lỗi: Xe chưa được phép đăng ký ra/vào nhà máy";
        public const string ERR_SEC_NOT_PERMITTED_REG_ENG = "Error: Truck isn't yet registred for check in/out security gate";
        public const string ERR_SEC_WRONG_CONFIRMED_RFID_VI = "Lỗi: Sai thẻ bảo vệ";
        public const string ERR_SEC_WRONG_CONFIRMED_RFID_ENG = "Error: Wrong security officer tags";
        public const string ERR_SEC_NOT_PERMITTED_CHECKIN_VI = "Lỗi: Xe chưa được phép vào nhà máy";
        public const string ERR_SEC_NOT_PERMITTED_CHECKIN_ENG = "Error: Truck isn't yet permitted to check in security gate";
        public const string ERR_SEC_NOT_PERMITTED_CHECKOUT_VI = "Lỗi: Xe chưa được phép ra nhà máy";
        public const string ERR_SEC_NOT_PERMITTED_CHECKOUT_ENG = "Error: Truck isn't yet permitted to check out security gate";
        public const string ERR_SEC_WRONG_BODY_REQUEST_FORMAT = "Error: Wrong body for requesting to update SECURITY state ";
        public const string ERR_SEC_NOT_FOUND_GATEPASS_ENG = "Error: Not found GatePass";
        public const string ERR_SEC_NOT_FOUND_GATEPASS_VI = "Không tìm thấy\r\nGatePass";
        public const string ERR_SEC_NOT_SUPPORT_UPDATED_STATE = "Error: Security App not support to update the resquested state";
        public const string ERR_SEC_UNKNOW = "Error: Unknow error";
        public const string ERR_SEC_GATEPASS_LACK_STATEID = "Error: Lack of stateID on GatePass";
        public const string ERR_SEC_NOT_PERMIT_PASS_SECURITY_GATE_VI = "Lỗi: Xe chưa đước phép đi qua cổng bảo vệ";
        public const string ERR_SEC_NOT_PERMIT_PASS_SECURITY_GATE_ENG = "Error: Truck inn't yet permit to pass through security gate";
        public const string ERR_QUE_NO_QUEUE_FOUND_VI = "Lỗi: Không tìm thấy danh sách hàng đợi";
        public const string ERR_QUE_NO_GATEPASS_FOUND_VI = "Lỗi: Không tìm thấy GatePass với ID hiện tại";
        public const string ERR_QUE_GATEPASS_WRONG_STATE_VI = "Lỗi: GatePass không thực hiện đúng trạng thái";
        public const string ERR_QUE_WEIGH_NO_FOUND_VI = "Lỗi: Không tìm thấy thông tin cân hoặc xe chưa cân";
        public const string ERR_WEI_WEIGH_NOT_PERMITTED_VI = "Lỗi: Nhân viên không có quyền cân xe";
        public const string ERR_WEI_CALL_NOT_PERMITTED_VI = "Lỗi: Nhân viên không có quyền gọi xe";
        public const string ERR_USER_PERMISSION = "Lỗi: Nhân viên không có quyền thực hiện tác vụ";
		public const string ERR_USER_PERMISSION_VI = "Lỗi: Nhân viên không có quyền thực hiện tác vụ";
        public const string ERR_USER_NOT_EXSIT_VI = "Lỗi đăng nhập: User không tồn tại";
        public const string ERR_INVALID_LOGIN_VI = "Lỗi đăng nhập: email hoặc mật khẩu không đúng";
        public const string ERR_WEI_NOT_FOUND_EMPTY_GATEPASS_VI = "Lỗi: không tìm thấy gatepass rỗng";
        public const string ERR_WEIGHTED_GATEPASS = "Không thể chỉnh sửa thông tin khi xe đã cân.";

        // Error Login
        public const string ERR_LOGIN_WRONG_USERNAME_PASS = "Invalid username or password";

        // Admin Master Data
        public const string ERR_EMPTY_DATABASE = "Cơ sở dữ liệu\r\nrỗng";
        public const string ERR_LACK_INPUT = "Thiếu dữ liệu\r\nnhập vào";

        public const string ADD_CARRIER_SUCCESS = "Thêm nhà vận tải\r\nthành công";
        public const string ADD_CARRIER_FAIL = "Thêm nhà vận tải\r\nthất bại";
        public const string EDIT_CARRIER_SUCCESS = "Sửa nhà vận tải\r\nthành công";
        public const string EDIT_CARRIER_FAIL = "Sửa nhà vận tải\r\nthất bại";
        public const string DELETE_CARRIER_SUCCESS = "Xóa nhà vận tải\r\nthành công";
        public const string DELETE_CARRIER_FAIL = "Xóa nhà vận tải\r\nthất bại";

        public const string ADD_CUSTOMER_SUCCESS = "Thêm khách hàng\r\nthành công";
        public const string ADD_CUSTOMER_FAIL = "Thêm khách hàng\r\nthất bại";
        public const string EDIT_CUSTOMER_SUCCESS = "Sửa khách hàng\r\nthành công";
        public const string EDIT_CUSTOMER_FAIL = "Sửa khách hàng\r\nthất bại";
        public const string DELETE_CUSTOMER_SUCCESS = "Xóa khách hàng\r\nthành công";
        public const string DELETE_CUSTOMER_FAIL = "Xóa khách hàng\r\nthất bại";

        public const string ADD_CUSTOMERWAREHOUSE_SUCCESS = "Thêm kho khách hàng\r\nthành công";
        public const string ADD_CUSTOMERWAREHOUSE_FAIL = "Thêm kho khách hàng\r\nthất bại";
        public const string EDIT_CUSTOMERWAREHOUSE_SUCCESS = "Sửa kho khách hàng\r\nthành công";
        public const string EDIT_CUSTOMERWAREHOUSE_FAIL = "Sửa kho khách hàng\r\nthất bại";
        public const string DELETE_CUSTOMERWAREHOUSE_SUCCESS = "Xóa kho khách hàng\r\nthành công";
        public const string DELETE_CUSTOMERWAREHOUSE_FAIL = "Xóa kho khách hàng\r\nthất bại";

        public const string ADD_DRIVER_SUCCESS = "Thêm tài xế\r\nthành công";
        public const string ADD_DRIVER_FAIL = "Thêm tài xế\r\nthất bại";
        public const string EDIT_DRIVER_SUCCESS = "Sửa tài xế\r\nthành công";
        public const string EDIT_DRIVER_FAIL = "Sửa tài xế\r\nthất bại";
        public const string DELETE_DRIVER_SUCCESS = "Xóa tài xế\r\nthành công";
        public const string DELETE_DRIVER_FAIL = "Xóa tài xế\r\nthất bại";

        public const string ADD_EMPLOYEE_SUCCESS = "Thêm nhân viên\r\nthành công";
        public const string ADD_EMPLOYEE_FAIL = "Thêm nhân viên\r\nthất bại";
        public const string EDIT_EMPLOYEE_SUCCESS = "Sửa nhân viên\r\nthành công";
        public const string EDIT_EMPLOYEE_FAIL = "Sửa nhân viên\r\nthất bại";
        public const string DELETE_EMPLOYEE_SUCCESS = "Xóa nhân viên\r\nthành công";
        public const string DELETE_EMPLOYEE_FAIL = "Xóa nhân viên\r\nthất bại";

        public const string ADD_EMPLOYEEGROUP_SUCCESS = "Thêm nhóm nhân viên\r\nthành công";
        public const string ADD_EMPLOYEEGROUP_FAIL = "Thêm nhóm nhân viên\r\nthất bại";
        public const string EDIT_EMPLOYEEGROUP_SUCCESS = "Sửa nhóm nhân viên\r\nthành công";
        public const string EDIT_EMPLOYEEGROUP_FAIL = "Sửa nhóm nhân viên\r\nthất bại";
        public const string DELETE_EMPLOYEEGROUP_SUCCESS = "Xóa nhóm nhân viên\r\nthành công";
        public const string DELETE_EMPLOYEEGROUP_FAIL = "Xóa nhóm nhân viên\r\nthất bại";

        public const string ADD_USER_SUCCESS = "Thêm tài khoản\r\nthành công";
        public const string ADD_USER_FAIL = "Thêm tài khoản\r\nthất bại";
        public const string EDIT_USER_SUCCESS = "Sửa tài khoản\r\nthành công";
        public const string EDIT_USER_FAIL = "Sửa tài khoản\r\nthất bại";
        public const string DELETE_USER_SUCCESS = "Xóa tài khoản\r\nthành công";
        public const string DELETE_USER_FAIL = "Xóa tài khoản\r\nthất bại";

        public const string ADD_GROUPPERMISSION_SUCCESS = "Thêm phân quyền\r\nthành công";
        public const string ADD_GROUPPERMISSION_FAIL = "Thêm phân quyền\r\nthất bại";
        public const string EDIT_GROUPPERMISSION_SUCCESS = "Cập nhật phân quyền\r\nthành công";
        public const string EDIT_GROUPPERMISSION_FAIL = "Cập nhật phân quyền\r\nthất bại";
        public const string DELETE_GROUPPERMISSION_SUCCESS = "Xóa phân quyền\r\nthành công";
        public const string DELETE_GROUPPERMISSION_FAIL = "Xóa phân quyền\r\nthất bại";

        public const string ADD_MATERIAL_SUCCESS = "Thêm nguyên vật liệu\r\nthành công";
        public const string ADD_MATERIAL_FAIL = "Thêm nguyên vật liệu\r\nthất bại";
        public const string EDIT_MATERIAL_SUCCESS = "Sửa nguyên vật liệu\r\nthành công";
        public const string EDIT_MATERIAL_FAIL = "Sửa nguyên vật liệu\r\nthất bại";
        public const string DELETE_MATERIAL_SUCCESS = "Xóa nguyên vật liệu\r\nthành công";
        public const string DELETE_MATERIAL_FAIL = "Xóa nguyên vật liệu\r\nthất bại";

        public const string ADD_TRUCK_SUCCESS = "Thêm xe\r\nthành công";
        public const string ADD_TRUCK_FAIL = "Thêm xe\r\nthất bại";
        public const string EDIT_TRUCK_SUCCESS = "Sửa xe\r\nthành công";
        public const string EDIT_TRUCK_FAIL = "Sửa xe\r\nthất bại";
        public const string DELETE_TRUCK_SUCCESS = "Xóa xe\r\nthành công";
        public const string DELETE_TRUCK_FAIL = "Xóa xe\r\nthất bại";

        public const string ADD_WAREHOUSE_SUCCESS = "Thêm nhà kho\r\nthành công";
        public const string ADD_WAREHOUSE_FAIL = "Thêm nhà kho\r\nthất bại";
        public const string EDIT_WAREHOUSE_SUCCESS = "Sửa nhà kho\r\nthành công";
        public const string EDIT_WAREHOUSE_FAIL = "Sửa nhà kho\r\nthất bại";
        public const string DELETE_WAREHOUSE_SUCCESS = "Xóa nhà kho\r\nthành công";
        public const string DELETE_WAREHOUSE_FAIL = "Xóa nhà kho\r\nthất bại";

        public const string ADD_BADGEREADER_SUCCESS = "Thêm đầu đọc thẻ\r\nthành công";
        public const string ADD_BADGEREADER_FAIL = "Thêm đầu đọc thẻ\r\nthất bại";
        public const string EDIT_BADGEREADER_SUCCESS = "Cập nhât đầu đọc thẻ\r\nthành công";
        public const string EDIT_BADGEREADER_FAIL = "Cập nhât đầu đọc thẻ\r\nthất bại";
        public const string DELETE_BADGEREADER_SUCCESS = "Xóa đầu đọc thẻ\r\nthành công";
        public const string DELETE_BADGEREADER_FAIL = "Xóa đầu đọc thẻ\r\nthất bại";

        public const string ADD_CAMERA_SUCCESS = "Thêm camera\r\nthành công";
        public const string ADD_CAMERA_FAIL = "Thêm camera\r\nthất bại";
        public const string EDIT_CAMERA_SUCCESS = "Cập nhât camera\r\nthành công";
        public const string EDIT_CAMERA_FAIL = "Cập nhât camera\r\nthất bại";
        public const string DELETE_CAMERA_SUCCESS = "Xóa camera\r\nthành công";
        public const string DELETE_CAMERA_FAIL = "Xóa camera\r\nthất bại";

        public const string ADD_CONSTRAIN_SUCCESS = "Thêm ràng buộc\r\nthành công";
        public const string ADD_CONSTRAIN_FAIL = "Thêm ràng buộc\r\nthất bại";
        public const string EDIT_CONSTRAIN_SUCCESS = "Cập nhât ràng buộc\r\nthành công";
        public const string EDIT_CONSTRAIN_FAIL = "Cập nhât ràng buộc\r\nthất bại";
        public const string DELETE_CONSTRAIN_SUCCESS = "Xóa ràng buộc\r\nthành công";
        public const string DELETE_CONSTRAIN_FAIL = "Xóa ràng buộc\r\nthất bại";

        public const string ADD_PC_SUCCESS = "Thêm PC\r\nthành công";
        public const string ADD_PC_FAIL = "Thêm PC\r\nthất bại";
        public const string EDIT_PC_SUCCESS = "Cập nhât PC\r\nthành công";
        public const string EDIT_PC_FAIL = "Cập nhât PC\r\nthất bại";
        public const string DELETE_PC_SUCCESS = "Xóa PC\r\nthành công";
        public const string DELETE_PC_FAIL = "Xóa PC\r\nthất bại";

        public const string ADD_RFIDCARD_SUCCESS = "Thêm thẻ RFID\r\nthành công";
        public const string ADD_RFIDCARD_FAIL = "Thêm thẻ RFID\r\nthất bại";
        public const string EDIT_RFIDCARD_SUCCESS = "Cập nhât thẻ RFID\r\nthành công";
        public const string EDIT_RFIDCARD_FAIL = "Cập nhât thẻ RFID\r\nthất bại";
        public const string DELETE_RFIDCARD_SUCCESS = "Xóa thẻ RFID\r\nthành công";
        public const string DELETE_RFIDCARD_FAIL = "Xóa thẻ RFID\r\nthất bại";

        public const string ADD_GATEPASS_SUCCESS = "Thêm Gate-Pass\r\nthành công";
        public const string ADD_GATEPASS_FAIL = "Thêm Gate-Pass\r\nthất bại";
        public const string EDIT_GATEPASS_SUCCESS = "Cập nhât Gate-Pass\r\nthành công";
        public const string EDIT_GATEPASS_FAIL = "Cập nhât Gate-Pass\r\nthất bại";
        public const string DELETE_GATEPASS_SUCCESS = "Xóa Gate-Pass\r\nthành công";
        public const string DELETE_GATEPASS_FAIL = "Xóa Gate-Pass\r\nthất bại";

        public const string ADD_UNITTYPE_SUCCESS = "Thêm đơn vị\r\nthành công";
        public const string ADD_UNITTYPE_FAIL = "Thêm đơn vị\r\nthất bại";
        public const string EDIT_UNITTYPE_SUCCESS = "Sửa đơn vị\r\nthành công";
        public const string EDIT_UNITTYPE_FAIL = "Sửa đơn vị\r\nthất bại";
        public const string DELETE_UNITTYPE_SUCCESS = "Xóa đơn vị\r\nthành công";
        public const string DELETE_UNITTYPE_FAIL = "Xóa đơn vị\r\nthất bại";

        public const string ADD_TRUCKTYPE_SUCCESS = "Thêm loại xe\r\nthành công";
        public const string ADD_TRUCKTYPE_FAIL = "Thêm loại xe\r\nthất bại";
        public const string EDIT_TRUCKTYPE_SUCCESS = "Sửa loại xe\r\nthành công";
        public const string EDIT_TRUCKTYPE_FAIL = "Sửa loại xe\r\nthất bại";
        public const string DELETE_TRUCKTYPE_SUCCESS = "Xóa loại xe\r\nthành công";
        public const string DELETE_TRUCKTYPE_FAIL = "Xóa loại xe\r\nthất bại";

        public const string ADD_LOADINGTYPE_SUCCESS = "Thêm kiểu lấy hàng\r\nthành công";
        public const string ADD_LOADINGTYPE_FAIL = "Thêm kiểu lấy hàng\r\nthất bại";
        public const string EDIT_LOADINGTYPE_SUCCESS = "Sửa kiểu lấy hàng\r\nthành công";
        public const string EDIT_LOADINGTYPE_FAIL = "Sửa kiểu lấy hàng\r\nthất bại";
        public const string DELETE_LOADINGTYPE_SUCCESS = "Xóa kiểu lấy hàng\r\nthành công";
        public const string DELETE_LOADINGTYPE_FAIL = "Xóa kiểu lấy hàng\r\nthất bại";

        public const string ADD_EMPLOYEEROLE_SUCCESS = "Thêm chức vụ nhân viên\r\nthành công";
        public const string ADD_EMPLOYEEROLE_FAIL = "Thêm chức vụ nhân viên\r\nthất bại";
        public const string EDIT_EMPLOYEEROLE_SUCCESS = "Sửa chức vụ nhân viên\r\nthành công";
        public const string EDIT_EMPLOYEEROLE_FAIL = "Sửa chức vụ nhân viên\r\nthất bại";
        public const string DELETE_EMPLOYEEROLE_SUCCESS = "Xóa chức vụ nhân viên\r\nthành công";
        public const string DELETE_EMPLOYEEROLE_FAIL = "Xóa chức vụ nhân viên\r\nthất bại";

        public const string ADD_PLANT_SUCCESS = "Thêm nhà máy\r\nthành công";
        public const string ADD_PLANT_FAIL = "Thêm nhà máy\r\nthất bại";
        public const string EDIT_PLANT_SUCCESS = "Sửa nhà máy\r\nthành công";
        public const string EDIT_PLANT_FAIL = "Sửa nhà máy\r\nthất bại";
        public const string DELETE_PLANT_SUCCESS = "Xóa nhà máy\r\nthành công";
        public const string DELETE_PLANT_FAIL = "Xóa nhà máy\r\nthất bại";

        public const string ADD_COMPANY_SUCCESS = "Thêm công ty\r\nthành công";
        public const string ADD_COMPANY_FAIL = "Thêm công ty\r\nthất bại";
        public const string EDIT_COMPANY_SUCCESS = "Sửa công ty\r\nthành công";
        public const string EDIT_COMPANY_FAIL = "Sửa công ty\r\nthất bại";
        public const string DELETE_COMPANY_SUCCESS = "Xóa công ty\r\nthành công";
        public const string DELETE_COMPANY_FAIL = "Xóa công ty\r\nthất bại";

        public const string ADD_LOADINGBAY_SUCCESS = "Thêm khu lấy hàng\r\nthành công";
        public const string ADD_LOADINGBAY_FAIL = "Thêm khu lấy hàng\r\nthất bại";
        public const string EDIT_LOADINGBAY_SUCCESS = "Sửa khu lấy hàng\r\nthành công";
        public const string EDIT_LOADINGBAY_FAIL = "Sửa khu lấy hàng\r\nthất bại";
        public const string DELETE_LOADINGBAY_SUCCESS = "Xóa khu lấy hàng\r\nthành công";
        public const string DELETE_LOADINGBAY_FAIL = "Xóa khu lấy hàng\r\nthất bại";

        public const string ADD_LANE_SUCCESS = "Thêm làn xe\r\nthành công";
        public const string ADD_LANE_FAIL = "Thêm làn xe\r\nthất bại";
        public const string EDIT_LANE_SUCCESS = "Sửa làn xe\r\nthành công";
        public const string EDIT_LANE_FAIL = "Sửa làn xe\r\nthất bại";
        public const string DELETE_LANE_SUCCESS = "Xóa làn xe\r\nthành công";
        public const string DELETE_LANE_FAIL = "Xóa làn xe\r\nthất bại";

        public const string ADD_DO_SUCCESS = "Thêm DO\r\nthành công";
        public const string ADD_DO_FAIL = "Thêm DO\r\nthất bại";
        public const string EDIT_DO_SUCCESS = "Sửa DO\r\nthành công";
        public const string EDIT_DO_FAIL = "Sửa DO\r\nthất bại";
        public const string DELETE_DO_SUCCESS = "Xóa DO\r\nthành công";
        public const string DELETE_DO_FAIL = "Xóa DO\r\nthất bại";

        public const string ADD_PRINTHEADER_SUCCESS = "Thêm mẫu in\r\nthành công";
        public const string ADD_PRINTHEADER_FAIL = "Thêm mẫu in\r\nthất bại";
        public const string EDIT_PRINTHEADER_SUCCESS = "Sửa mẫu in\r\nthành công";
        public const string EDIT_PRINTHEADER_FAIL = "Sửa mẫu in\r\nthất bại";
        public const string DELETE_PRINTHEADER_SUCCESS = "Xóa mẫu in\r\nthành công";
        public const string DELETE_PRINTHEADER_FAIL = "Xóa mẫu in\r\nthất bại";

        public const string SEARCH_SUCCESS = "Tìm\r\nthành công: ";
        public const string SEARCH_GATEPASS_SUCCESS = "Tìm thành công\r\nGate-Pass ";
        public const string SEARCH_GATEPASS_FAIL = "Không tìm thấy Gate-Pass";
        public const string SEARCH_OBJECT = " bản ghi";
        public const string ZERO_OBJECT = "0";
        public const string DASH_STRING = "-";

        public const string ERR_CODE_DUPLICATE = "Mã\r\nđã được sử dụng";
        public const string ERR_SEARCH_FAIL = "Không tìm thấy\r\ndữ liệu";
        public const string ERR_SEARCH_KEYWORD_NULL = "Thiếu thông tin\r\ntìm kiếm";

        public const string INVALID_CARD = "Thẻ nhân viên\r\nkhông khớp";

        //User 
        public const string ERR_MAX_LOGIN = "Vượt số lượng user\r\nhỗ trợ";
        public const string ERR_INVALID_USER_NAME = "Tên tài khoản\r\nkhông tồn tại";
        public const string ERR_INVALID_PASSWORD = "Sai mật khẩu";
        public const string ERR_BLOCKED_USER_EXCEED_LOGIN = "Tài khoản bị khóa do vượt số lượt đăng nhập.\r\nVui lòng liên hệ Admin";
        public const string ERR_BLOCKED_USER_EXCEED_EXPIRE = "Tài khoản bị khóa do quá hạn đổi mật khẩu.\r\nVui lòng liên hệ Admin";
        public const string PASSWORD_CHANGE_SUCCESS = "Cập nhật mật khẩu\r\nthành công";
        public const string PASSWORD_CHANGE_FAIL = "Cập nhật mật khẩu\r\nthất bại";
        public const string PASSWORD_CHANGE_REQUIRE = "Tài khoản của bạn sắp hết hạn.\r\nVui lòng cập nhật mật khẩu";
        public const string EMPLOYEE_DELETED = "Nhân viên đã bị xóa.\r\nKhông thể thực hiên thao tác với User";

        public const string ERR_CONFIG_WB_TIMES_OVER_20 = "Số cấu hình tùy chỉnh\r\nđược vượt quá 20";
        public const string ERR_CONFIG_WB_NAME_NULL = "Tên cấu hình\r\nkhông được trống";
        public const string ERR_CONFIG_WB_DONT_CHANGE_DEFAULT = "Không lưu thay đổi\r\ncấu hình mặc định";

        // Error Import DO, PO
        public const string ERR_IMPORT_CREATE_SO_FAIL = "Tạo Sale Order\r\nthất bại";
        public const string ERR_IMPORT_DATA_NOT_SYNC = "Dữ liệu\r\nkhông đồng bộ";
        public const string ERR_IMPORT_DATA_HAVED_IN_DB = "Dữ liệu đã có\r\ntrong database";
        public const string ERR_IMPORT_DONT_HAVE_MATERIAL_IN_DB = "Không có hàng\r\ntrong database";
        public const string ERR_IMPORT_DONT_HAVE_VENDOR_IN_DB = "Không có nhà cung cấp\r\ntrong database";
        public const string ERR_IMPORT_DONT_HAVE_PLANT_IN_DB = "Không có nhà máy\r\ntrong database";
        public const string ERR_IMPORT_EXCEPTION = "Lỗi chưa xác định";
    }

    public static class GatepassState
    {
        public const int STATE_CREATE_INCOMPLETED = 1;
        public const int STATE_CREATE_COMPLETED = 2;
        public const int STATE_REGISTERED = 3;
        public const int STATE_CALLING_1 = 4;
        public const int STATE_CALLING_2 = 5;
        public const int STATE_CALLING_3 = 6;
        public const int STATE_IN_SECURITY_CHECK_IN = 7;
        public const int STATE_FINISH_SECURITY_CHECK_IN = 8;
        public const int STATE_PRE_WEIGHT_IN = 9;
        public const int STATE_IN_WEIGHT_IN = 10;
        public const int STATE_FINISH_WEIGHT_IN = 11;
        public const int STATE_IN_WAREHOUSE_CHECK_IN = 12;
        public const int STATE_FINISH_WAREHOUSE_CHECK_IN = 13;
        public const int STATE_IN_WAREHOUSE_CHECK_OUT = 14;
        public const int STATE_FINISH_WAREHOUSE_CHECK_OUT = 15;
        public const int STATE_IN_WEIGHT_OUT = 16;
        public const int STATE_FINISH_WEIGHT_OUT = 17;
        public const int STATE_IN_SECURITY_CHECK_OUT = 18;
        public const int STATE_FINISH_SECURITY_CHECK_OUT = 19;
    }

    public static class TruckGroups
    {
        public const string GROUP_1XXX = "TG001";
        public const string GROUP_2XXX = "TG002";
        public const string GROUP_3XXX = "TG003";
    }

    public static class APIList
    {
        public static int CreateRegisteredQueueItem = 3;
    }

    public static class LaneStatus
    {
        public const int FREE = 0;
        public const int OCCUPIED = 1;
    }

    public static class Crypt
    {
        public static String ToSha256(String data)
        {
            var byteData = Encoding.UTF8.GetBytes(data);
            SHA256Managed hasher = new SHA256Managed();
            byte[] hash = hasher.ComputeHash(byteData);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }

    public static class OrderTypeConst
    {
        public const int DELIVERYORDER = 0;
        public const int PURCHASEORDER = 1;
        public const int INTERNALORDER = 3;
        public const int OTHERSORDER = 4;
    }

    public static class ConstrainCategory
    {
        public const string USER_CONSTRAINS = "USER_CONSTRAINS";
        public const string WEIGHT_CONSTRAINS = "WEIGHT_CONSTRAINS";
        public const string PASS_CONSTRAINS = "PASS_CONSTRAINS";
        public const string PRINT_CONSTRAINS = "PRINT_CONSTRAINS";
    }

    public static class WeightType
    {
        public const int WEIGHT_ALL = 0;
        public const int WEIGHT_IN = 1;
        public const int WEIGHT_OUT = 2;
        public const int WEIGHT_INTERNAL = 3;
    }

    public static class WeightTimes
    {
        public const int WEIGHT_ALL = 0;
        public const int WEIGHT_FIRST = 1;
        public const int WEIGHT_SECOND = 2;
    }

    public static class ActionScreen
    {
        public const string SECURITY = "Bảo vệ";
        public const string QUEUE = "Đăng ký xe";
        public const string WEIGHT = "Cân xe";
        public const string WAREHOUSE = "Kho";
        public const string ADMIN = "Quản trị";

        public const string GATEPASSMNGNT = "Quản lý GatePass";
        public const string CUSTOMERMNGNT = "Quản lý khách hàng";
        public const string MATERIALMNGNT = "Quản lý hàng";
        public const string TRUCKMNGNT = "Quản lý xe";
        public const string DRIVERMNGNT = "Quản lý tài xế";
        public const string WAREHOUSEMNGNT = "Quản lý kho";
        public const string EMPLOYEEMNGNT = "Quản lý nhân viên";
        public const string EMPLOYEEGROUPMNGNT = "Quản lý nhóm";
        public const string CARRIERVENDORMNGNT = "Quản lý nhà vận tải";
        public const string CAMERACONFIG = "Cấu hình camera";
        public const string WBCONFIG = "Cấu hình cầu cân";
        public const string BADGECONFIG = "Cấu hình đầu đọc thẻ";
        public const string CONSTRAIN = "Cấu hình ràng buộc";
        public const string AUDITTRAILREPORT = "Báo cáo lịch sử thao tác";
        public const string EACHWEIGHTREPORT = "Báo cáo từng lần cân";
        public const string SUMMARIZEWEIGHTREPORT = "Báo cáo tổng hợp";
        public const string UNDONESECONDWEIGHTREPORT = "Báo cáo xe chưa cân lần 2";
        public const string WEIGHTCHANGEHISTORYREPORT = "Báo cáo lịch sử phiếu cân";
        public const string WEIGHTHISTORYREPORT = "Báo cáo lịch sử xe cân";
        public const string PRINTNUMBERREPORT = "Báo cáo số bản in phiếu cân";
    }

    public static class ActionType
    {
        public const string ADD = "Thêm mới";
        public const string UPDATE = "Chỉnh sửa";
        public const string DELETE = "Xóa";
        public const string SEARCH = "Tìm";
        public const string CHECKIN_SECURITY = "Checkin security";
        public const string CHECKOUT_SECURITY = "Checkout security";
        public const string CHECKIN_WAREHOUSE = "Checkin warehouse";
        public const string CHECKOUT_WAREHOUSE = "Checkout warehouse";
        public const string WEIGHT = "Cân";
        public const string PRINT = "In";
        public const string CHANGEPASSWORD = "Đổi mật khẩu";
    }

    public static class ActionTarget
    {
        public const string TRUCK = "Xe";
        public const string GATEPASS = "GatePass";
        public const string CUSTOMER = "Khách hàng";
        public const string WAREHOUSE = "Kho";
        public const string CARRIERVENDOR = "Nhà cung cấp";
        public const string DRIVER = "Tài xế";
        public const string EMPLOYEE = "Nhân viên";
        public const string MATERIAL = "Hàng";
        public const string CAMERA = "Camera";
        public const string WB = "Cầu cân";
        public const string BADGEREADER = "Đầu đọc thẻ";
        public const string WEIGHTVALUE = "Thông số cân";
        public const string PASSWORD = "Mật khẩu";
        public const string PRINTHEADER = "Phiếu in";
        public const string USER = "Tài khoản";
        public const string PERMISSION = "Phân quyền";
        public const string CHANGEPASSWORD = "Đổi mật khẩu";
        public const string REPORT = "Báo cáo";
    }

    public static class ActionResult
    {
        public const string SUCCESS = "Thành công";
        public const string FAIL = "Thất bại";
    }

    public static class ReportType
    {
        public const int FIRST_WEIGHT_ONLY = 1;
        public const int SECOND_WEIGHT_ONLY = 2;
        public const int BOTH_WEIGHT = 3;
        public const int FIRST_UNDONE_SECOND = 4;
        public const int WEIGHT_HISTORY = 5;
        public const int PRINTED_GATEPASS = 6;
        public const int WEIGHT_MODIFIED = 7;
        public const int ALL_WEIGHT = 8;
    }

    public static class Location
    {
        public const int SECURITY = 1;
        public const int QUEUE_LOGISTIC = 2;
        public const int WEIGHT_LOGISTIC = 3;
        public const int WAREHOUSE = 4;
        public const int ADMIN = 5;
    }

    public static class CameraUsage
    {
        public const int PLATENUMERA = 1;
        public const int PLATENUMERB = 2;
        public const int CONTAINERA = 3;
        public const int CONTAINERB = 4;
        public const int LOGISTIC = 5;
    }

    public static class ConstrainName
    {
        public const string WEIGHT_OVER = "WEIGHT_OVER";
        public const string WEIGHT_OVER_2 = "WEIGHT_OVER_2";
        public const string PASS_MIN_LENGHT = "PASS_MIN_LENGHT";
        public const string PASS_MAX_LENGHT = "PASS_MAX_LENGHT";
        public const string PASS_EXPIRED = "PASS_EXPIRED";
        public const string PASS_CHANGE = "PASS_CHANGE";
        public const string PASS_LOGIN_TIMES = "PASS_LOGIN_TIMES";
        public const string PASS_IS_INCLUDE_UPPERCASE = "PASS_IS_INCLUDE_UPPERCASE";
        public const string PASS_IS_INCLUDE_NUMBER = "PASS_IS_INCLUDE_NUMBER";
        public const string PASS_IS_INCLUDE_SPEC_CHAR = "PASS_IS_INCLUDE_SPEC_CHAR";
    }

    public static class PCFunction
    {
        public const string WEIGHT = "Weight";

    }
}
