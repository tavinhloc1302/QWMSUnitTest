using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.DeviceService
{
    public class HazardLightDriver
    {
        private ControllerDriver _controller;
        private int _port;
        public string _code;

        public HazardLightDriver(string code)
        {
            _code = code;
        }
        public HazardLightDriver(string code,
                      ControllerDriver ctrl, int port)
        {
            _code = code;
            _controller = ctrl;
            _port = port;
        }

        public void UpdateInfo(string code,
                      ControllerDriver ctrl, int port)
        {
            _code = code;
            _controller = ctrl;
            _port = port;
        }

        // Return
        //    STATUS_CODE_ERR_CTRLS
        //    STATUS_CODE_OK
        public int GetStatusSync(out string errMsg)
        {
            int ret;
            string tmp;

            // Check status controller
            if (_controller == null)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CTRLS;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CTRLS;
            }
            if (_controller.GetStatusSync(out tmp) != DeviceStatus.STATUS_CODE_OK)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CTRLS;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CTRLS;
            }
            else
            {
                ret = DeviceStatus.STATUS_CODE_OK;
                errMsg = DeviceStatus.STATUS_TEXT_OK;
            }

            return ret;
        }

        // Return
        //    STATUS_CODE_ERR_CTRLS
        //    STATUS_CODE_OK
        public int GetStatusLocal(out string errMsg)
        {
            int ret;
            string tmp;

            // Check status controller
            if (_controller == null)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CTRLS;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CTRLS;
            }
            if (_controller.GetStatusLocal(out tmp) != DeviceStatus.STATUS_CODE_OK)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CTRLS;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CTRLS;
            }
            else
            {
                ret = DeviceStatus.STATUS_CODE_OK;
                errMsg = DeviceStatus.STATUS_TEXT_OK;
            }

            return ret;
        }

        public bool GetPortSync()
        {
            bool ret;
            if (_controller != null)
            {
                if (_controller.GetPortSync(_port))
                {
                    ret = STATUS_PORT_HIGHT;
                }
                else
                {
                    ret = STATUS_PORT_LOW;
                }
            }
            else
            {
                ret = STATUS_PORT_LOW;
            }

            return ret;
        }

        public bool GetPortLocal()
        {
            bool ret;
            if (_controller != null)
            {
                if (_controller.GetPortLocal(_port))
                {
                    ret = STATUS_PORT_HIGHT;
                }
                else
                {
                    ret = STATUS_PORT_LOW;
                }
            }
            else
            {
                ret = STATUS_PORT_LOW;
            }

            return ret;
        }

        public void SetPort(bool value)
        {
            if (_controller != null)
            {
                _controller.SetPort(_port, value);
            }
        }

        public const bool STATUS_PORT_HIGHT = true;
        public const bool STATUS_PORT_LOW = false;
    }
}
