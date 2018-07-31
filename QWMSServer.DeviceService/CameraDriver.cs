using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.DeviceService
{
    public class CameraDriver
    {
        public string _code;
        public int _status;
        public DateTime _upDateTime;

        public CameraDriver(string code)
        {
            _code = code;
            _status = DeviceStatus.STATUS_CODE_ERR_DEVICE;
            _upDateTime = DateTime.Now;
        }

        public void UpdateStatus(int status)
        {
            _status = status;
            _upDateTime = DateTime.Now;
        }

        public void UpdateInfo(string code)
        {
            _code = code;
        }

        public void ClearStatus()
        {
            _status = DeviceStatus.STATUS_CODE_ERR_DEVICE;
            _upDateTime = DateTime.Now;
        }
    }
}
