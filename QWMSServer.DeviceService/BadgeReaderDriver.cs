using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.DeviceService
{
    public class BadgeReaderDriver
    {
        // RFID Reader Service
        public string _code;
        public int _status;
        public string _uid;
        public DateTime _updatedStatus;
        public DateTime _updatedUID;

        // RFID Reader Info
        public string _readerIP;
        public int _readerPort;
        TcpClient _tcpClient;

        // Sycn
        public string _lock = "lock";

        void Default()
        {
            _code = "";
            _status = DeviceStatus.STATUS_CODE_ERR_DEVICE;
            _uid = "";
            _updatedStatus = DateTime.Now;
            _updatedUID = _updatedStatus;
        }

        // Contructor
        public BadgeReaderDriver()
        {
            Default();
        }

        public BadgeReaderDriver(string readerIP, int readerPort)
        {
            Default();
            _readerIP = readerIP;
            _readerPort = readerPort;
            _tcpClient = null;
        }

        public BadgeReaderDriver(string code, string readerIP, int readerPort)
        {
            Default();
            _readerIP = readerIP;
            _readerPort = readerPort;
            _code = code;
            _tcpClient = null;
        }

        public void UpdateInfo(string readerIP, int readerPort)
        {
            _readerIP = readerIP;
            _readerPort = readerPort;
            _tcpClient = null;
        }

        // API for connecting with Badge Reader
        // Return:
        //    STATUS_CODE_OK
        //    STATUS_CODE_ERR_CONNECT
        public int connectToReader()
        {
            int ret;

            if (_tcpClient == null)
            {
                _tcpClient = new TcpClient();
            }
            else
            {
                try
                {
                    _tcpClient.Close();
                }
                catch(Exception ex) { }
                _tcpClient = new TcpClient();
            }

            // Connecto to Badge;
            try
            {
                if(_readerIP != null && _readerPort != 0)
                {
                    _tcpClient.Connect(_readerIP, _readerPort);
                    if (_tcpClient.Connected)
                    {
                        ret = DeviceStatus.STATUS_CODE_OK;
                    }
                    else
                    {
                        ret = DeviceStatus.STATUS_CODE_ERR_CONNECT;
                    }
                }
                else
                {
                    ret = DeviceStatus.STATUS_CODE_ERR_CONNECT;
                }
            }
            catch (Exception ex)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CONNECT;
            }
            return ret;
        }

        // API for closing connection with Badge Reader
        public void closeConnection()
        {
            if (_tcpClient != null)
            {
                _tcpClient.Close();
                _tcpClient = null;
            }
        }

        // API for Geting Status of Badge Reader
        // Return:
        //    STATUS_CODE_OK
        //    STATUS_CODE_ERR_CONNECT
        //    STATUS_CODE_ERR_DEVICE
        public int getBadgeStatus(out string errMsg)
        {
            Ping pinger;
            PingReply reply;
            // Return value
            int serviceStatus, pingStatus;
            int ret;

            // 1. Check conection by ping cmd
            pinger = new Ping();
            try
            {
                reply = pinger.Send(_readerIP);
                if (reply.Status == IPStatus.Success)
                {
                    pingStatus = DeviceStatus.STATUS_CODE_OK;
                }
                else
                {
                    pingStatus = DeviceStatus.STATUS_CODE_ERR_CONNECT;
                }
            }
            catch
            {
                pingStatus = DeviceStatus.STATUS_CODE_ERR_CONNECT;
            }

            // 2. Check Service
            if(IsConnected(_tcpClient))
            {
                serviceStatus = DeviceStatus.STATUS_CODE_OK;
            }
            else
            {
                lock(_lock)
                {
                    serviceStatus = connectToReader();
                }
            }
            //serviceStatus = STATUS_CODE_OK;
            // 3. Judgement
            if (serviceStatus == DeviceStatus.STATUS_CODE_OK)
            {
                ret = DeviceStatus.STATUS_CODE_OK;
                errMsg = DeviceStatus.STATUS_TEXT_OK;
            }
            else if(pingStatus == DeviceStatus.STATUS_CODE_OK)
            {
                ret = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_DEVICE;
            }
            else
            {
                ret = DeviceStatus.STATUS_CODE_ERR_CONNECT;
                errMsg = DeviceStatus.STATUS_TEXT_ERR_CONNECT;
            }

            // Update status
            _status = ret;
            _updatedStatus = DateTime.Now;

            return ret;
        }

        public int ReadUID(out byte[] uidByte, out string uidStr, int timeOut)
        {
            int readLen;
            uidByte = new byte[20];
            if(_tcpClient != null && _tcpClient.Connected)
            {
                try
                {
                    if (timeOut == 0)
                    {
                        _tcpClient.GetStream().ReadTimeout = -1;
                    }
                    else
                    {
                        _tcpClient.GetStream().ReadTimeout = timeOut;
                    }
                    readLen = _tcpClient.GetStream().Read(uidByte, 0, 20);
                }
                catch (Exception ex)
                {
                    readLen = 0;
                    //if(ex.InnerException.Equals(InnerException.))
                }

                if (readLen > 10)
                {
                    uidStr = Encoding.ASCII.GetString(uidByte, 0, 10);
                }
                else
                {
                    readLen = 0;
                    uidStr = "";
                }

                // Update uid
                _uid = uidStr;
                _updatedUID = DateTime.Now;
            }
            else
            {
                uidStr = "";
                readLen = 0;

                // Update uid
                _uid = uidStr;
                _updatedUID = DateTime.Now;

                // Waitting for _client # null
                System.Threading.Thread.Sleep(timeOut/10);
            }

            return readLen;
        }

        public int UpdateState(out byte[] uidByte, out string uidStr, int timeOut)
        {
            int readLen;
            uidByte = new byte[20];
            int stt;

            // Default status
            stt = _status;
            uidStr = "";


            // Re-connect
            if(!IsConnected(_tcpClient) || stt == DeviceStatus.STATUS_CODE_ERR_DEVICE)
            {
                // Reconnect
                stt = connectToReader();
            }

            // Get UID
            if (stt == DeviceStatus.STATUS_CODE_OK)
            {
                try
                {
                    if (timeOut == 0)
                    {
                        _tcpClient.GetStream().ReadTimeout = -1;
                    }
                    else
                    {
                        _tcpClient.GetStream().ReadTimeout = timeOut;
                    }
                    readLen = _tcpClient.GetStream().Read(uidByte, 0, 20);
                }
                catch (Exception ex)
                {
                    readLen = 0;
                    if(ex.InnerException.GetType() == typeof(System.Net.Sockets.SocketException))
                    {
                        if(((System.Net.Sockets.SocketException)ex.InnerException).SocketErrorCode != SocketError.TimedOut)
                        {
                            stt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                        }
                    }
                    else
                    {
                        stt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                    }
                }

                if (readLen > 10 && stt == DeviceStatus.STATUS_CODE_OK)
                {
                    uidStr = Encoding.ASCII.GetString(uidByte, 0, 10);
                }
                else
                {
                    uidStr = "";
                }
            }
            else
            {
                uidStr = "";
            }

            // Update state
            _status = stt;
            _updatedStatus = DateTime.Now;
            if (uidStr != "")
            {
                _uid = uidStr;
                _updatedUID = _updatedStatus;
            }

            return _status;
        }

        public bool IsConnected(TcpClient tcpClient)
        {
            bool ret;

            byte[] dump = new byte[] { 1, 1, 1, 1 };
            
            try
            {
                if (tcpClient != null && tcpClient.Connected)
                {
                    tcpClient.GetStream().WriteTimeout = 1000;
                    tcpClient.GetStream().Write(dump, 0, 4);
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                ret = false;
            }

            return ret;
        }

        public void ClearUID()
        {
            _uid = "";
            _updatedUID = DateTime.Now;
        }

    }
}
