using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Common
{
    public static class Constant
    {
        public static string DriverCapturePath = "D:/";
        public static string TruckCapturePath = "D:/";
        public static int DELIVERYORDER = 1;
        public static int PURCHASEORDER = 2;

        public static int TRUCK = 1;
        public static int CONTAINER = 2;
        public static int PUMP = 3;
        public static int TRUCK_CONTAINER = 4;

        public static int TRUCKGROUP1X = 1;
        public static int TRUCKGROUP2X = 2;
        public static int TRUCKGROUP3X = 3;

        public static int NULLLANE = 65; // change
    }

    public static class QueryIncludes
    {
        public static List<String> LANEFULLINCLUDES = new List<string>{ "LoadingBay", "LoadingType", "TruckType" };
        public static List<String> DRIVERFULLINCLUDES = new List<string> { "carrierVendor" };
        public static List<String> CUSTOMERFULLINCUDES = new List<string> { "customerWarehouses" };
        public static List<String> GATEPASSFULLINCLUDES = new List<string> { "Truck.TruckType", "Driver", "State", "Employee", "orders.DeliveryOrder", "queueLists.Lane.LoadingBay.Wareshouse.Plant.Company", "truckGroup" };
        public static List<String> QUEUELISTFULLINCLUDES = new List<string> { "truck", "lane", "gatePass" };
        public static List<String> USERFULLINCLUDES = new List<string> { "employees.groupMaps.employeeGroup.functionMaps.systemFunction" };
        public static List<String> EMPLOYEEFULLINCLUDES = new List<string> { "groupMaps.employeeGroup.functionMaps.systemFunction" };
        // Security App
        public static List<String> SECURITY_QUEUE_INCLUDES = new List<string> { "Lane.LoadingBay.Wareshouse", "GatePass.orders.OrderType", "GatePass.Truck.TruckType", "GatePass.State" };
        public static List<String> SECURITY_GATEPASS_INCLUDES = new List<string> { "queueLists", "State" };
    }

    public static class ResponseCode
    {
        public const int SUCCESS = 0;

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
    }

    public static class ResponseText
    {
        public const string SUCCESS = "OK";

        // Error Code OFF Security App
        public const string ERR_DB_CONNECTION_FAILED_VI = "Lỗi: Không thể kết nối đến cơ sở dữ liệu";
        public const string ERR_DB_CONNECTION_FAILED_ENG = "Eror: Could NOT connect to Database";
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
        public const string ERR_SEC_NOT_FOUND_GATEPASS_VI = "Lỗi: Không tìm thấy GatePass";
        public const string ERR_SEC_NOT_SUPPORT_UPDATED_STATE = "Error: Security App not support to update the resquested state";
        public const string ERR_SEC_UNKNOW = "Error: Unknow error";
        public const string ERR_SEC_GATEPASS_LACK_STATEID = "Error: Lack of stateID on GatePass";
        public const string ERR_SEC_NOT_PERMIT_PASS_SECURITY_GATE_VI = "Lỗi: Xe chưa đước phép đi qua cổng bảo vệ";
        public const string ERR_SEC_NOT_PERMIT_PASS_SECURITY_GATE_ENG = "Error: Truck inn't yet permit to pass through security gate";
        public const string ERR_QUE_NO_QUEUE_FOUND_VI = "Lỗi: Không tìm thấy danh sách hàng đợi";
        public const string ERR_QUE_NO_GATEPASS_FOUND_VI = "Lỗi: Không tìm thấy GatePass với ID hiện tại";
        public const string ERR_QUE_GATEPASS_WRONG_STATE_VI = "Lỗi: GatePass không thực hiện đúng trạng thái";
        public const string ERR_QUE_WEIGH_NO_FOUND = "Lỗi: Không tìm thấy thông tin cân hoặc xe chưa cân";
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
        public const string GROUP_1XXX = "1xxx";
        public const string GROUP_2XXX = "2xxx";
        public const string GROUP_3XXX = "3xxx";
    }

    public static class APIList
    {
        public static int CreateRegisteredQueueItem = 3;
    }
}
