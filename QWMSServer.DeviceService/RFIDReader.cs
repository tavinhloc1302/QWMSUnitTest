using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.DeviceService
{
    public class RFIDReader
    {
        // RFID Reader Service
        public string _code;
        public bool _status;
        public string _uid;
        public DateTime _updatedStatus;
        public DateTime _updatedUID;

        // RFID Reader Info
        public string _readerIP;
        public int _readerPort;
        TcpClient _tcpClient;

        void Default()
        {
            _code = "";
            _status = false;
            _uid = "";
            _updatedStatus = DateTime.Now;
            _updatedUID = _updatedStatus;
        }

        // Contructor
        public RFIDReader()
        {
            Default();
        }
        public RFIDReader(string readerIP, int readerPort)
        {
            Default();
            _readerIP = readerIP;
            _readerPort = readerPort;
            _tcpClient = null;
        }

        public RFIDReader(string code, string readerIP, int readerPort)
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

        // Private API for connecting with Badge Reader
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
                        ret = STATUS_CODE_OK;
                    }
                    else
                    {
                        ret = STATUS_CODE_ERR_CONNECT;
                    }
                }
                else
                {
                    ret = STATUS_CODE_ERR_CONNECT;
                }
            }
            catch (Exception ex)
            {
                ret = STATUS_CODE_ERR_CONNECT;
            }
            return ret;
        }

        // Private API for closing connection with Badge Reader
        public void closeConnection()
        {
            if (_tcpClient != null)
            {
                _tcpClient.Close();
                _tcpClient = null;
            }
        }

        // API for Geting Status of Badge Reader
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
                    pingStatus = STATUS_CODE_OK;
                }
                else
                {
                    pingStatus = STATUS_CODE_ERR_CONNECT;
                }
            }
            catch
            {
                pingStatus = STATUS_CODE_ERR_CONNECT;
            }

            // 2. Check Service
            if(IsConnected(_tcpClient))
            {
                serviceStatus = STATUS_CODE_OK;
            }
            else
            {
                serviceStatus = connectToReader();
            }

            // 3. Judgement
            if(serviceStatus == STATUS_CODE_OK)
            {
                ret = STATUS_CODE_OK;
                errMsg = STATUS_TEXT_OK;
            }
            else if(pingStatus == STATUS_CODE_OK)
            {
                ret = STATUS_CODE_ERR_DEVICE;
                errMsg = STATUS_TEXT_ERR_DEVICE;
            }
            else
            {
                ret = STATUS_CODE_ERR_CONNECT;
                errMsg = STATUS_TEXT_ERR_CONNECT;
            }

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
                }
            }
            else
            {
                System.Threading.Thread.Sleep(timeOut);
                readLen = 0;
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

            return readLen;
        }

        public bool IsConnected(TcpClient tcpClient)
        {
            bool ret;

            byte[] dump = new byte[] { 1, 1, 1, 1 };
            try
            {
                tcpClient.GetStream().Write(dump, 0, 4);
                ret = true;
            }
            catch(Exception ex)
            {
                ret = false;
            }

            return ret;
        }

        // Badge Reader status
        public const int STATUS_CODE_OK = 0;
        public const int STATUS_CODE_ERR_CONNECT = 1;
        //public const int STATUS_CODE_ERR_AUTHEN = 2;
        public const int STATUS_CODE_ERR_DEVICE = 3;
        public const string STATUS_TEXT_OK = "OK";
        public const string STATUS_TEXT_ERR_CONNECT = "Connect Failed";
        //public const string STATUS_TEXT_ERR_AUTHEN = "Authen Failed";
        public const string STATUS_TEXT_ERR_DEVICE = "Device Error";
    }
}
