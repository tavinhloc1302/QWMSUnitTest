using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.DeviceService
{
    public class BarrierDriver
    {
        private ControllerDriver _openController;
        private int _openPort;
        private ControllerDriver _closeController;
        private int _closePort;
        public string _code;

        public BarrierDriver(string code)
        {
            _code = code;
        }
        public BarrierDriver(string code,
                      ControllerDriver openCtrl, int openPort,
                      ControllerDriver closeCtrl, int closePort)
        {
            _code = code;
            _openController = openCtrl;
            _openPort = openPort;
            _closeController = closeCtrl;
            _closePort = closePort;
        }

        public void UpdateInfo(string code,
                      ControllerDriver openCtrl, int openPort,
                      ControllerDriver closeCtrl, int closePort)
        {
            _code = code;
            _openController = openCtrl;
            _openPort = openPort;
            _closeController = closeCtrl;
            _closePort = closePort;
        }

        // Return
        //    STATUS_CODE_OK
        //    STATUS_CODE_ERR_OPEN_CTRL
        //    STATUS_CODE_ERR_CLOSE_CTRL
        //    STATUS_CODE_ERR_CTRLS
        public int GetStatusSync(out string errMsg)
        {
            int ret, openCtrlStt, closeCtrlStt;
            string tmp;

            // Check status open controller
            if (_openController == null)
            {
                openCtrlStt = DeviceStatus.STATUS_CODE_ERR_OPEN_CTRL;
            }
            if (_openController.GetStatusSync(out tmp) != DeviceStatus.STATUS_CODE_OK)
            {
                openCtrlStt = DeviceStatus.STATUS_CODE_ERR_OPEN_CTRL;
            }
            else
            {
                openCtrlStt = DeviceStatus.STATUS_CODE_OK;
            }

            // Check close controller
            if (_closeController == null)
            {
                closeCtrlStt = DeviceStatus.STATUS_CODE_ERR_CLOSE_CTRL;
            }
            else if (_closeController.GetStatusSync(out tmp) != DeviceStatus.STATUS_CODE_OK)
            {
                closeCtrlStt = DeviceStatus.STATUS_CODE_ERR_CLOSE_CTRL;
            }
            else
            {
                closeCtrlStt = DeviceStatus.STATUS_CODE_OK;
            }

            // Judgement
            if(openCtrlStt != DeviceStatus.STATUS_CODE_OK && 
                closeCtrlStt != DeviceStatus.STATUS_CODE_OK)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CTRLS;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CTRLS;
            }
            else if(openCtrlStt != DeviceStatus.STATUS_CODE_OK)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_OPEN_CTRL;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_OPEN_CTRL;
            }
            else
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CLOSE_CTRL;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CLOSE_CTRL;
            }

            return ret;
        }

        // Return
        //    STATUS_CODE_OK
        //    STATUS_CODE_ERR_OPEN_CTRL
        //    STATUS_CODE_ERR_CLOSE_CTRL
        //    STATUS_CODE_ERR_CTRLS
        public int GetStatusLocal(out string errMsg)
        {
            int ret, openCtrlStt, closeCtrlStt;
            string tmp;

            // Check status open controller
            if (_openController == null)
            {
                openCtrlStt = DeviceStatus.STATUS_CODE_ERR_OPEN_CTRL;
            }
            if (_openController.GetStatusLocal(out tmp) != DeviceStatus.STATUS_CODE_OK)
            {
                openCtrlStt = DeviceStatus.STATUS_CODE_ERR_OPEN_CTRL;
            }
            else
            {
                openCtrlStt = DeviceStatus.STATUS_CODE_OK;
            }

            // Check close controller
            if (_closeController == null)
            {
                closeCtrlStt = DeviceStatus.STATUS_CODE_ERR_CLOSE_CTRL;
            }
            else if (_closeController.GetStatusLocal(out tmp) != DeviceStatus.STATUS_CODE_OK)
            {
                closeCtrlStt = DeviceStatus.STATUS_CODE_ERR_CLOSE_CTRL;
            }
            else
            {
                closeCtrlStt = DeviceStatus.STATUS_CODE_OK;
            }

            // Judgement
            if (openCtrlStt != DeviceStatus.STATUS_CODE_OK && 
                closeCtrlStt != DeviceStatus.STATUS_CODE_OK)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CTRLS;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CTRLS;
            }
            else if (openCtrlStt != DeviceStatus.STATUS_CODE_OK)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_OPEN_CTRL;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_OPEN_CTRL;
            }
            else
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CLOSE_CTRL;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CLOSE_CTRL;
            }

            return ret;
        }

        public bool GetOpenPortSync()
        {
            bool ret;
            if (_openController != null)
            {
                if(_openController.GetPortSync(_openPort))
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

        public bool GetOpenPortLocal()
        {
            bool ret;
            if (_openController != null)
            {
                if(_openController.GetPortLocal(_openPort))
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

        public bool GetClosePortSync()
        {
            bool ret;
            if (_closeController != null)
            {
                if(_closeController.GetPortSync(_closePort))
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

        public bool GetClosePortLocal()
        {
            bool ret;
            if (_closeController != null)
            {
                if(_closeController.GetPortLocal(_closePort))
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

        public void SetOpenPort(bool value)
        {
            if(_openController != null)
            {
                _openController.SetPort(_openPort, value);
            }
        }

        public void SetClosePort(bool value)
        {
            if (_closeController != null)
            {
                _closeController.SetPort(_closePort, value);
            }
        }

        public const bool STATUS_PORT_HIGHT = true;
        public const bool STATUS_PORT_LOW = false;
    }
}
