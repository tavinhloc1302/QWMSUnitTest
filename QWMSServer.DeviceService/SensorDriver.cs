using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.DeviceService
{
    public class SensorDriver
    {
        private ControllerDriver _valueCtrl;
        private int _valuePortId;
        private ControllerDriver _statusCtrl;
        private int _statusPortId;
        public string _code;

        public SensorDriver(string code)
        {
            _code = code;
        }
        public SensorDriver(string code,
                      ControllerDriver valueCtrl, int valuePortId,
                      ControllerDriver statusCtrl, int statusPortId)
        {
            _code = code;
            _valueCtrl = valueCtrl;
            _valuePortId = valuePortId;
            _statusCtrl = statusCtrl;
            _statusPortId = statusPortId;
        }

        public void UpdateInfo(string code,
                      ControllerDriver valueCtrl, int valuePortId,
                      ControllerDriver statusCtrl, int statusPortId)
        {
            _code = code;
            _valueCtrl = valueCtrl;
            _valuePortId = valuePortId;
            _statusCtrl = statusCtrl;
            _statusPortId = statusPortId;
        }

        // Return
        //    STATUS_CODE_ERR_CTRLS
        //    STATUS_CODE_ERR_STATUS_CTRL
        //    STATUS_CODE_ERR_VALUE_CTRL
        //    STATUS_CODE_ERR_DEVICE
        //    STATUS_CODE_OK
        public int GetStatusSync(out string errMsg)
        {
            int ret, valueCtrlStt, statusCtrlStt, deviceStt ;
            string tmp;
            bool statusPort;

            valueCtrlStt = statusCtrlStt = deviceStt = DeviceStatus.STATUS_CODE_OK;
            // Check status controller
            if (_statusCtrl == null)
            {
                valueCtrlStt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
            }
            if (_statusCtrl.GetStatusSync(out tmp) != DeviceStatus.STATUS_CODE_OK)
            {
                valueCtrlStt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
            }
            else
            {
                valueCtrlStt = DeviceStatus.STATUS_CODE_OK;
            }

            // Checkout status port
            if (valueCtrlStt == DeviceStatus.STATUS_CODE_OK)
            {
                statusPort = _statusCtrl.GetPortSync(_statusPortId);
                if (statusPort == STATUS_PORT_ERR)
                {
                    deviceStt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                }
            }

            // Check value controller
            if (_valueCtrl == null)
            {
                valueCtrlStt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
            }
            else if (_valueCtrl.GetStatusSync(out tmp) != DeviceStatus.STATUS_CODE_OK)
            {
                valueCtrlStt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
            }
            else
            {
                valueCtrlStt = DeviceStatus.STATUS_CODE_OK;
            }

            // Judgment
            if(statusCtrlStt == DeviceStatus.STATUS_CODE_ERR_DEVICE &&
                valueCtrlStt == DeviceStatus.STATUS_CODE_ERR_DEVICE)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CTRLS;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CTRLS;
            }
            else if(statusCtrlStt == DeviceStatus.STATUS_CODE_ERR_DEVICE)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_STATUS_CTRL;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_STATUS_CTRL;
            }
            else if(valueCtrlStt == DeviceStatus.STATUS_CODE_ERR_DEVICE)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_VALUE_CTRL;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_VALUE_CTRL;
            }
            else if(deviceStt == DeviceStatus.STATUS_CODE_ERR_DEVICE)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_DEVICE;
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
        //    STATUS_CODE_ERR_STATUS_CTRL
        //    STATUS_CODE_ERR_VALUE_CTRL
        //    STATUS_CODE_ERR_DEVICE
        //    STATUS_CODE_OK
        public int GetStatusLocal(out string errMsg)
        {
            int ret, valueCtrlStt, statusCtrlStt, deviceStt;
            string tmp;
            bool statusPort;

            valueCtrlStt = statusCtrlStt = deviceStt = DeviceStatus.STATUS_CODE_OK;
            // Check status controller
            if (_statusCtrl == null)
            {
                valueCtrlStt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
            }
            if (_statusCtrl.GetStatusLocal(out tmp) != DeviceStatus.STATUS_CODE_OK)
            {
                valueCtrlStt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
            }
            else
            {
                valueCtrlStt = DeviceStatus.STATUS_CODE_OK;
            }

            // Checkout status port
            if (valueCtrlStt == DeviceStatus.STATUS_CODE_OK)
            {
                statusPort = _statusCtrl.GetPortLocal(_statusPortId);
                if (statusPort == STATUS_PORT_ERR)
                {
                    deviceStt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                }
            }

            // Check value controller
            if (_valueCtrl == null)
            {
                valueCtrlStt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
            }
            else if (_valueCtrl.GetStatusLocal(out tmp) != DeviceStatus.STATUS_CODE_OK)
            {
                valueCtrlStt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
            }
            else
            {
                valueCtrlStt = DeviceStatus.STATUS_CODE_OK;
            }

            // Judgment
            if (statusCtrlStt == DeviceStatus.STATUS_CODE_ERR_DEVICE &&
                valueCtrlStt == DeviceStatus.STATUS_CODE_ERR_DEVICE)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CTRLS;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CTRLS;
            }
            else if (statusCtrlStt == DeviceStatus.STATUS_CODE_ERR_DEVICE)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_STATUS_CTRL;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_STATUS_CTRL;
            }
            else if (valueCtrlStt == DeviceStatus.STATUS_CODE_ERR_DEVICE)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_VALUE_CTRL;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_VALUE_CTRL;
            }
            else if (deviceStt == DeviceStatus.STATUS_CODE_ERR_DEVICE)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_DEVICE;
            }
            else
            {
                ret = DeviceStatus.STATUS_CODE_OK;
                errMsg = DeviceStatus.STATUS_TEXT_OK;
            }

            return ret;
        }

        public bool GetValueSync()
        {
            bool ret;
            if(_valueCtrl != null)
            {
                ret = _valueCtrl.GetPortSync(_valuePortId);
            }
            else
            {
                ret = false;
            }
            
            return ret;
        }

        public bool GetValueLocal()
        {
            bool ret;
            if(_valueCtrl != null)
            {
                ret = _valueCtrl.GetPortLocal(_valuePortId);
            }
            else
            {
                ret = false;
            }
            
            return ret;
        }

        public const bool STATUS_PORT_OK = true;
        public const bool STATUS_PORT_ERR = false;
        public const bool OBJECT_PRESENCE = true;
        public const bool NO_OBJECT_PRESENCE = false;
    }
}
