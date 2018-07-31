using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.DeviceService
{
    public class DeviceStatus
    {
        // Badge Reader status
        public const int STATUS_CODE_OK = 0;
        public const int STATUS_CODE_ERR_CONNECT = 1;
        public const int STATUS_CODE_ERR_AUTHEN = 2;
        public const int STATUS_CODE_ERR_DEVICE = 3;
        public const int STATUS_CODE_ERR_OPEN_CTRL = 4;
        public const int STATUS_CODE_ERR_CLOSE_CTRL = 5;
        public const int STATUS_CODE_ERR_CTRLS = 6;
        public const int STATUS_CODE_ERR_VALUE_CTRL = 7;
        public const int STATUS_CODE_ERR_STATUS_CTRL = 8;
        public const string STATUS_TEXT_OK = "OK";
        public const string STATUS_TEXT_ERR_CONNECT = "Connect Failed";
        public const string STATUS_TEXT_ERR_AUTHEN = "Authen Failed";
        public const string STATUS_TEXT_ERR_DEVICE = "Device Error";
        public const string STATUS_TEXT_ERR_OPEN_CTRL = "Opened Controller Error";
        public const string STATUS_TEXT_ERR_CLOSE_CTRL = "Closed Controller Error";
        public const string STATUS_TEXT_ERR_CTRLS = "Controllers Error";
        public const string STATUS_TEXT_ERR_VALUE_CTRL = "Value Controller Error";
        public const string STATUS_TEXT_ERR_STATUS_CTRL = "Status Controller Error";
    }
}
