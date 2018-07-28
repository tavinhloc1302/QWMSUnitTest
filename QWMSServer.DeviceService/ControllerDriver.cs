using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace QWMSServer.DeviceService
{
    public class ControllerDriver
    {
        private string _ipAddr;
        private int _port;
        private string _protocol;
        private string _getPath;
        private string _setPath;
        private string _setParam;
        private string _user;
        private string _pass;
        private HttpClient _client;
        // Storage variable
        public string _code;
        private ControllerPort _portsValue;
        private int _status;
        private string _errMsg;
        public DateTime _upDateTime;

        public ControllerDriver(string code)
        {
            _code = code;
            _portsValue = new ControllerPort();
            _upDateTime = DateTime.Now;
        }
        public ControllerDriver(string code,
                                string protocol, string ipAddr, int port,
                                string getPath, string setPath, string setParam,
                                string user, string pass)
        {
            _code = code;
            _protocol = protocol;
            _ipAddr = ipAddr;
            _port = port;
            _getPath = getPath;
            _setPath = setPath;
            _setParam = setParam;
            _user = user;
            _pass = pass;
            _portsValue = new ControllerPort();
            _upDateTime = DateTime.Now;
        }
        public void UpdateInfo(string code,
                                string protocol, string ipAddr, int port,
                                string getPath, string setPath, string setParam,
                                string user, string pass)
        {
            _code = code;
            _protocol = protocol;
            _ipAddr = ipAddr;
            _port = port;
            _getPath = getPath;
            _setPath = setPath;
            _setParam = setParam;
            _user = user;
            _pass = pass;
        }

        public bool Equal(ControllerDriver obj)
        {
            bool ret;

            if (obj == null)
            {
                ret = false;
            }
            else
            {
                if (this._ipAddr.Equals(obj._ipAddr) && this._port == obj._port)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            return ret;
        }

        private void Authenticate()
        {
            if (_client == null)
            {
                _client = new HttpClient();
            }
            var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_user}:{_pass}"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64String);
            var reqUrl = _protocol + "://" + _ipAddr + ":" + _port;
            var response = _client.PostAsync(reqUrl, null);
            try
            {
                var result = response.Result;
            }
            catch (Exception ex) { }
            return;
        }

        // Return
        //    STATUS_CODE_OK
        //    STATUS_CODE_ERR_AUTHEN
        //    STATUS_CODE_ERR_CONNECT
        //    STATUS_CODE_ERR_DEVICE
        private int RequestStatus(out string errMsg, out ControllerPort ports)
        {
            string reqUrl;
            Task<HttpResponseMessage> responseTask;
            MemoryStream result;
            XmlSerializer serializer;
            int ret;

            reqUrl = _protocol + "://" + _ipAddr + ":" + _port + "/" + _getPath;
            Authenticate();
            responseTask = _client.GetAsync(reqUrl);
            // Waiting for Response task
            try
            {
                responseTask.Wait();

                // Status OK
                if (responseTask.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ret = DeviceStatus.STATUS_CODE_OK;
                    errMsg = DeviceStatus.STATUS_TEXT_OK;

                    // Get Ports status
                    result = (MemoryStream)responseTask.Result.Content.ReadAsStreamAsync().Result;
                    serializer = new XmlSerializer(typeof(ControllerPort));
                    ports = (ControllerPort)serializer.Deserialize(result);
                }
                // Status Authen Failed
                else if (responseTask.Result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ret = DeviceStatus.STATUS_CODE_ERR_AUTHEN;
                    errMsg = DeviceStatus.STATUS_TEXT_ERR_AUTHEN;
                    ports = null;
                }
                // Status Device Error
                else
                {
                    ret = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                    errMsg = DeviceStatus.STATUS_TEXT_ERR_DEVICE;
                    ports = null;
                }
            }
            catch (Exception)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CONNECT;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CONNECT;
                ports = null;
            }

            return ret;
        }

        // Return
        //    STATUS_CODE_OK
        //    STATUS_CODE_ERR_AUTHEN
        //    STATUS_CODE_ERR_CONNECT
        //    STATUS_CODE_ERR_DEVICE
        public int GetStatusSync(out string errMsg)
        {
            ControllerPort portsStatus;
            int ret;
            Ping pinger;
            PingReply reply;

            // Default
            ret = DeviceStatus.STATUS_CODE_OK;
            errMsg = DeviceStatus.STATUS_TEXT_OK;
            portsStatus = null;

            // Check conection by ping cmd
            pinger = new Ping();
            try
            {
                reply = pinger.Send(_ipAddr);
                if (reply.Status == IPStatus.Success)
                {
                    ret = DeviceStatus.STATUS_CODE_OK;
                    errMsg = DeviceStatus.STATUS_TEXT_OK;
                }
                else
                {
                    ret = DeviceStatus.STATUS_CODE_ERR_CONNECT;
                    errMsg = DeviceStatus.STATUS_TEXT_ERR_CONNECT;
                }
            }
            catch
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CONNECT;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CONNECT;
            }

            // Check Status
            if (ret == DeviceStatus.STATUS_CODE_OK)
            {
                ret = RequestStatus(out errMsg, out portsStatus);
            }

            // Upate port value
            _portsValue.Update(portsStatus);
            _status = ret;
            _errMsg = errMsg;
            _upDateTime = DateTime.Now;

            return ret;
        }

        public int GetStatusLocal(out string errMsg)
        {
            errMsg = _errMsg;
            return _status;
        }

        public void SetPort(int port, bool value)
        {
            ControllerPort portsStatus;
            string errMsg;
            int ret;

            // Get current status
            ret = RequestStatus(out errMsg, out portsStatus);

            if (ret == DeviceStatus.STATUS_CODE_OK && 
                portsStatus.GetPort(port) != value)
            {
                InvertPort(port);
            }
            return;
        }

        public bool GetPortSync(int port)
        {
            ControllerPort portsStatus;
            string errMsg;
            int status;
            bool ret;

            // Default
            ret = false;

            // Request Port status of smart controller
            status = RequestStatus(out errMsg, out portsStatus);

            if (status == DeviceStatus.STATUS_CODE_OK)
            {
                ret = portsStatus.GetPort(port);
            }

            return ret;
        }

        public bool GetPortLocal(int port)
        {
            if(_portsValue != null)
            {
                return _portsValue.GetPort(port);
            }
            else
            {
                return false;
            }
        }

        public void InvertPort(int port)
        {
            string reqUrl;
            Task<HttpResponseMessage> responseTask;

            // Update Port value
            reqUrl = _protocol + "://" + _ipAddr + ":" + _port + "/" + _setPath + "?" + _setParam + "=" + port;
            Authenticate();
            responseTask = _client.GetAsync(reqUrl);

            // Waiting for Response task
            try
            {
                responseTask.Wait();
            }
            catch (Exception) { }

            return;
        }

        public DateTime GetUpdateTime()
        {
            return _upDateTime;
        }
    }

    [XmlRoot("response")]
    public class ControllerPort
    {
        [XmlElement("led0")]
        public bool led0 { get; set; }

        [XmlElement("led1")]
        public bool led1 { get; set; }

        [XmlElement("led2")]
        public bool led2 { get; set; }

        [XmlElement("led3")]
        public bool led3 { get; set; }

        [XmlElement("led4")]
        public bool led4 { get; set; }

        [XmlElement("led5")]
        public bool led5 { get; set; }

        [XmlElement("led6")]
        public bool led6 { get; set; }

        [XmlElement("led7")]
        public bool led7 { get; set; }

        [XmlElement("led8")]
        public bool led8 { get; set; }

        public void Update(ControllerPort value)
        {
            if(value != null)
            {
                this.led0 = value.led0;
                this.led1 = value.led1;
                this.led2 = value.led2;
                this.led3 = value.led3;
                this.led4 = value.led4;
                this.led5 = value.led5;
                this.led6 = value.led6;
                this.led7 = value.led7;
            }
            else
            {
                this.led0 = this.led1 = this.led2 =
                    this.led3 = this.led4 = this.led5 =
                    this.led6 = this.led7 = this.led8 = false;
            }
        }

        public bool GetPort(int portID)
        {
            bool ret;
            switch (portID)
            {
                case 0:
                    ret = led0;
                    break;
                case 1:
                    ret = led1;
                    break;
                case 2:
                    ret = led2;
                    break;
                case 3:
                    ret = led3;
                    break;
                case 4:
                    ret = led4;
                    break;
                case 5:
                    ret = led5;
                    break;
                case 6:
                    ret = led6;
                    break;
                case 7:
                    ret = led7;
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }
    }
}
