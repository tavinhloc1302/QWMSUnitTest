using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace QWMSServer.DeviceService
{
    public class DeviceService
    {
        // Database
        IDBContext _dbContext;

        // Device Status
        public bool devicesStatus;

        // RFID Reader
        public List<RFIDReader> _rfidReaders;
        IBadgeReaderRepository _badgeReaderRepository;
        List<System.Threading.Thread> _capUIDThreads;

        // Controller

        // Constructor
        public DeviceService()
        {
            _rfidReaders = new List<RFIDReader>();
            _capUIDThreads = new List<System.Threading.Thread>();
        }

        // Update function
        #region
        void UpdateUID(RFIDReader reader, string uid)
        {
            UpdateUID(reader, uid, DateTime.Now);
        }

        void UpdateUID(RFIDReader reader, string uid, DateTime t)
        {
            if (reader != null)
            {
                reader._uid = uid;
                reader._updatedUID = t;
            }
        }

        void UpdateStatus(RFIDReader reader, bool status)
        {
            UpdateStatus(reader, status, DateTime.Now);
        }

        void UpdateStatus(RFIDReader reader, bool status, DateTime t)
        {
            if (reader != null)
            {
                reader._status = status;
                reader._updatedStatus = t;
            }
        }
        #endregion

        // Update Device Info
        #region
        public void UpdateDeviceInfo()
        {
            // RFID Reader
            IEnumerable<BadgeReader> rfidReaders;
            try
            {
                _dbContext = new QWMSDBContext();
                _badgeReaderRepository = new BadgeReaderRepository(_dbContext);
                rfidReaders = _badgeReaderRepository.GetAllAsync().Result;
                if (rfidReaders != null)
                {
                    UpdateRFIDReaderInfo(rfidReaders.ToList());
                }
            }
            catch(Exception ex) { }
            

            // Controller
        }
        #endregion

        // Update RFID Reader Info From database
        #region
        public void UpdateRFIDReaderInfo(List<BadgeReader> newInfo)
        {
            List<RFIDReader> addList;
            List<RFIDReader> removeList;
            RFIDReader tmpRFID;
            BadgeReader tmpBadge;
            addList = new List<RFIDReader>();
            removeList = new List<RFIDReader>();

            lock(_rfidReaders)
            {
                if(newInfo == null || newInfo.Count == 0)
                {
                    // clear all
                    _rfidReaders.Clear();
                }
                else
                {
                    // Update or Add
                    for (int i = 0; i < newInfo.Count; i++)
                    {
                        tmpRFID = _rfidReaders.Find(r => r._code == newInfo[i].Code);
                        if (tmpRFID != null)
                        {
                            // Update
                            int tmpPort;
                            if (newInfo[i].port == null)
                            {
                                tmpPort = 0;
                            }
                            else
                            {
                                tmpPort = (int)newInfo[i].port;
                            }

                            if ((tmpRFID._readerIP != newInfo[i].ipAddress) ||
                                (tmpRFID._readerPort != tmpPort))
                            {
                                // Update Info
                                tmpRFID.closeConnection();
                                tmpRFID.UpdateInfo(newInfo[i].ipAddress, tmpPort);
                            }
                        }
                        else
                        {
                            // Add
                            int tmpPort;
                            if(newInfo[i].port == null)
                            {
                                tmpPort = 0;
                            }
                            else
                            {
                                tmpPort = (int)newInfo[i].port;
                            }
                            addList.Add(new RFIDReader(newInfo[i].Code,
                                                       newInfo[i].ipAddress,
                                                       tmpPort));
                        }
                    }

                    // Remove
                    for (int i = 0; i < _rfidReaders.Count; i++)
                    {
                        tmpBadge = newInfo.Find(n => n.Code ==_rfidReaders[i]._code );
                        if(tmpBadge == null)
                        {
                            removeList.Add(_rfidReaders[i]);
                        }
                    }

                    for(int i=0; i<removeList.Count; i++)
                    {
                        _rfidReaders.Remove(removeList[i]);
                    }
                    _rfidReaders.AddRange(addList);
                }
            }
        }
        #endregion

        // Get Device Status
        #region
        public void GetDeviceStatus()
        {
            // RFID Reader
            RFIDReader[] cpRFIDReaders;
            cpRFIDReaders = null;
            lock(_rfidReaders)
            {
                if (_rfidReaders != null && _rfidReaders.Count > 0)
                {
                    cpRFIDReaders = new RFIDReader[_rfidReaders.Count()];
                    _rfidReaders.CopyTo(cpRFIDReaders);
                }
            }
            if(cpRFIDReaders != null)
            {
                GetRFIDReaderStatus(cpRFIDReaders);
            }

            // Controller
        }
        #endregion

        // Get RFID Status
        #region
        public void GetRFIDReaderStatus(RFIDReader[] readers)
        {
            int stt;
            string errMsg;
            RFIDReader tmp;

            if(readers == null)
            {
                return;
            }

            // Get status
            for(int i =0; i< readers.Count(); i++)
            {
                stt = readers[i].getBadgeStatus(out errMsg);
                if(stt != RFIDReader.STATUS_CODE_OK)
                {
                    readers[i]._status = false;
                }
                else
                {
                    readers[i]._status = true;
                }
            }

            // Update status
            lock(_rfidReaders)
            {
                for (int i = 0; i < readers.Count(); i++)
                {
                    tmp = _rfidReaders.Find(c => c._code == readers[i]._code);
                    UpdateStatus(tmp, readers[i]._status, DateTime.Now);
                }
            }
        }
        #endregion

        // Get UID
        #region
        public void GetUIDFromDev()
        {
            RFIDReader[] cpRFIDReaders;
            RFIDReader tmpReader;
            List<RFIDReader> addReaders;
            List<System.Threading.Thread> removeThreads;

            System.Threading.Thread tmpThread;
            int len;
            cpRFIDReaders = null;

            // Copy
            lock (_rfidReaders)
            {
                if (_rfidReaders != null && _rfidReaders.Count > 0)
                {
                    cpRFIDReaders = new RFIDReader[_rfidReaders.Count()];
                    _rfidReaders.CopyTo(cpRFIDReaders);
                }
            }

            // Check Threads
            if(cpRFIDReaders == null || cpRFIDReaders.Count() == 0)
            {
                foreach(System.Threading.Thread thread in _capUIDThreads )
                {
                    thread.Abort();
                }
                _capUIDThreads.Clear();
            }
            else
            {
                // Add new Thread
                addReaders = new List<RFIDReader>();
                for(int i = 0; i < cpRFIDReaders.Count(); i++)
                {
                    tmpThread = _capUIDThreads.Find(c => c.Name == cpRFIDReaders[i]._code);
                    if(tmpThread == null)
                    {
                        addReaders.Add(cpRFIDReaders[i]);
                    }
                }

                // Remove thread
                removeThreads = new List<System.Threading.Thread>();
                for(int i=0; i<_capUIDThreads.Count(); i++)
                {
                    tmpReader = cpRFIDReaders.ToList().Find(c => c._code == _capUIDThreads[i].Name);
                    if(tmpReader == null)
                    {
                        removeThreads.Add(_capUIDThreads[i]);
                    }
                }

                // Remove action
                len = removeThreads.Count();
                for(int i=0; i<len; i++)
                {
                    removeThreads[i].Abort();
                    _capUIDThreads.Remove(removeThreads[i]);
                }
                // Add action
                len = addReaders.Count();
                for(int i=0; i<len; i++)
                {
                    int index = i;
                    tmpThread = new System.Threading.Thread(() => CapUIDFromDev(addReaders[index]));
                    tmpThread.Name = addReaders[index]._code;
                    tmpThread.Start();
                    _capUIDThreads.Add(tmpThread);
                }
            }
        }
        #endregion

        // Get UID
        #region
        public void CapUIDFromDev(RFIDReader reader)
        {
            byte[] uidByte;
            string uid;
            int len;
            RFIDReader tmp;
            while(true)
            {
                try
                {
                    len = reader.ReadUID(out uidByte, out uid, 500);
                }
                catch(Exception ex)
                {
                    uid = "";
                    len = 0;
                }

                if (len > 0)
                {
                    lock(_rfidReaders)
                    {
                        tmp = _rfidReaders.Find(c => c._code == reader._code);
                        UpdateUID(tmp, uid, DateTime.Now);
                    }
                }
            }
            
            return;
        }
        #endregion

        // Check UID expired
        #region
        public void CheckUIDExpired(int timeOut)
        {
            DateTime now;
            lock(_rfidReaders)
            {
                now = DateTime.Now;
                foreach(RFIDReader r in _rfidReaders)
                {
                    if(now.Subtract(r._updatedUID) > TimeSpan.FromSeconds(timeOut))
                    {
                        UpdateUID(r, "", now);
                    }
                }
            }
        }
        #endregion

        public void DeviceServiceThread()
        {
            // 1. Timer to Update Device info From Database
            System.Timers.Timer getInfoTimer;
            getInfoTimer = new System.Timers.Timer();
            getInfoTimer.Interval = 10000;
            getInfoTimer.Enabled = true;
            getInfoTimer.Elapsed += GetInfoTimer_Elapsed;

            // 2. Timer to Get Status Device
            System.Timers.Timer getStatusTimer;
            getStatusTimer = new System.Timers.Timer();
            getStatusTimer.Interval = 10000;
            getStatusTimer.Enabled = true;
            getStatusTimer.Elapsed += GetStatusTimer_Elapsed;

            // 3. Get RFID UID
            System.Timers.Timer getUIDTimer;
            getUIDTimer = new System.Timers.Timer();
            getUIDTimer.Interval = 300;
            getUIDTimer.Enabled = true;
            getUIDTimer.Elapsed += GetUIDTimer_Elapsed;

            // 4. Timer to check timeout of readed uid
            System.Timers.Timer uidExpiredTimer;
            uidExpiredTimer = new System.Timers.Timer();
            uidExpiredTimer.Interval = 10000;
            uidExpiredTimer.Enabled = true;
            uidExpiredTimer.Elapsed += UidExpiredTimer_Elapsed; ;
        }

        private void UidExpiredTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ((Timer)sender).Enabled = false;
            CheckUIDExpired(120);
            ((Timer)sender).Enabled = true;
        }

        private void GetUIDTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ((Timer)sender).Enabled = false;
            GetUIDFromDev();
            ((Timer)sender).Enabled = true;
        }

        private void GetStatusTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ((Timer)sender).Enabled = false;
            GetDeviceStatus();
            ((Timer)sender).Enabled = true;
        }

        private void GetInfoTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ((Timer)sender).Enabled = false;
            UpdateDeviceInfo();
            ((Timer)sender).Enabled = true;
        }

        // User API
        // Get UID
        #region
        public string GetUID(string readerCode, bool delUID)
        {
            string ret;
            RFIDReader tmp;

            lock(_rfidReaders)
            {
                tmp = _rfidReaders.Find(c => c._code == readerCode);
                if(tmp != null)
                {
                    ret = tmp._uid;
                    if(delUID)
                    {
                        UpdateUID(tmp, "", DateTime.Now);
                    }
                }
                else
                {
                    ret = "";
                }
            }

            return ret;
        }
        #endregion

        // Get Status
        #region
        public bool GetStatus(string readerCode)
        {
            bool ret;
            RFIDReader tmp;

            lock (_rfidReaders)
            {
                tmp = _rfidReaders.Find(c => c._code == readerCode);
                if (tmp != null)
                {
                    ret = tmp._status;
                }
                else
                {
                    ret = false;
                }
            }
            return ret;
        }
        #endregion

        // Get All Status
        #region
        public bool GetAllStatus()
        {
            bool ret;

            ret = true;
            lock (_rfidReaders)
            {
                if(_rfidReaders.Count() == 0)
                {
                    ret = false;
                }
                else
                {
                    foreach(RFIDReader r in _rfidReaders)
                    {
                        if(r._status == false)
                        {
                            ret = false;
                            break;
                        }
                    }
                }
            }
            return ret;
        }
        #endregion

        // Check badge reader connection
        #region
        public bool CheckBadgeReaderConnection(BadgeReader badgeReader)
        {
            int stt;
            bool ret;
            ret = false;

            try
            {
                RFIDReader reader = new RFIDReader(badgeReader.ipAddress, (int)badgeReader.port);
                stt = reader.connectToReader();
                if (stt == RFIDReader.STATUS_CODE_OK)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
                reader.closeConnection();
            }
            catch (Exception ex)
            {
                ret = false;
            }

            return ret;
        }
        #endregion
    }
}
