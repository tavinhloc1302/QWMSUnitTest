using QWMSServer.Data.Common;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;
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
        IBadgeReaderRepository _badgeReaderRepository;
        IEmployeeRepository _employeeRepository;
        IControllerRepository _controllerRepository;
        IWeighBridgeRepository _weightBridgeRepository;
        ICameraRepository _cameraRepository;
        IGatePassRepository _gatePassRepository;
        IUserRepository _userRepository;
        IConstrainRepository _constrainRepository;

        // Device Status
        public bool devicesStatus;

        // RFID Reader
        public List<BadgeReaderDriver> _rfidReaders;
        List<System.Threading.Thread> _getBadgeStateThreads;
        List<System.Timers.Timer> _getRFIDReaderStateTimers;

        // Controller
        public List<ControllerDriver> _controllers;
        public List<SensorDriver> _sensors;
        public List<BarrierDriver> _barriers;
        public List<HazardLightDriver> _hazardLights;
        public List<System.Timers.Timer> _getControllerStateTimer;

        // Weightbridge / Camera
        public List<WeighBridgeDriver> _weighbridges;
        public List<CameraDriver> _cameras;

        // Constructor
        public DeviceService()
        {
            _rfidReaders = new List<BadgeReaderDriver>();
            _getRFIDReaderStateTimers = new List<Timer>();
            _getBadgeStateThreads = new List<System.Threading.Thread>();
            _controllers = new List<ControllerDriver>();
            _sensors = new List<SensorDriver>();
            _barriers = new List<BarrierDriver>();
            _hazardLights = new List<HazardLightDriver>();
            _getControllerStateTimer = new List<Timer>();
            _weighbridges = new List<WeighBridgeDriver>();
            _cameras = new List<CameraDriver>();
        }

        // Update Device Info
        #region
        public void UpdateDeviceInfo()
        {
            // RFID Reader
            IEnumerable<BadgeReader> rfidReaders;
            // Controller
            IEnumerable<Controller> controllerView;
            List<Sensor> sensorView;
            List<Barrier> barrierView;
            List<HazardLight> hazardLightView;
            // Weighbridge/Camera
            IEnumerable<WeighBridge> weighbridgeView;
            IEnumerable<Camera> cameraView;

            try
            {
                _dbContext = new QWMSDBContext();
                _badgeReaderRepository = new BadgeReaderRepository(_dbContext);
                rfidReaders = _badgeReaderRepository.GetManyAsync(c => c.isDelete == false).Result;
                if (rfidReaders != null && rfidReaders.Count() > 0)
                {
                    UpdateRFIDReaderInfo(rfidReaders.ToList());
                }
                _dbContext.Dispose();
            }
            catch (Exception ex) { }

            // Controller / Senser / Barrier / HazardLight
            try
            {
                _dbContext = new QWMSDBContext();
                _controllerRepository = new ControllerRepository(_dbContext);
                controllerView = _controllerRepository.GetManyAsync(c => c.isDelete == false,
                                                                     new List<String> { "userPC", "valueSensors", "statusSensors", "openBarriers", "closeBarriers", "hazardLights" }).Result;
                if (controllerView != null && controllerView.Count() > 0)
                {
                    // Controller
                    UpdateControllerInfo(controllerView.ToList());

                    // Sensor
                    sensorView = new List<Sensor>();
                    foreach (Controller c in controllerView)
                    {
                        if (c.valueSensors != null && c.valueSensors.Count() > 0)
                        {
                            foreach(Sensor ss in c.valueSensors)
                            {
                                var tmp = sensorView.Find(s => s.Code == ss.Code);
                                if(tmp == null && ss.isDelete == false)
                                {
                                    sensorView.Add(ss);
                                }
                            }
                        }

                        if (c.statusSensors != null && c.statusSensors.Count() > 0)
                        {
                            foreach (Sensor ss in c.statusSensors)
                            {
                                var tmp = sensorView.Find(s => s.Code == ss.Code);
                                if (tmp == null && ss.isDelete == false)
                                {
                                    sensorView.Add(ss);
                                }
                            }
                        }
                    }
                    UpdateSensorInfo(sensorView);

                    // Barrier
                    barrierView = new List<Barrier>();
                    foreach (Controller c in controllerView)
                    {
                        if (c.openBarriers != null && c.openBarriers.Count() > 0)
                        {
                            foreach (Barrier br in c.openBarriers)
                            {
                                var findResult = barrierView.Find(b => b.Code == br.Code);
                                if (findResult == null && br.isDelete == false)
                                {
                                    barrierView.Add(br);
                                }
                            }
                        }
                        if (c.closeBarriers != null && c.closeBarriers.Count() > 0)
                        {
                            foreach (Barrier br in c.closeBarriers)
                            {
                                var findResult = barrierView.Find(b => b.Code == br.Code);
                                if (findResult == null && br.isDelete == false)
                                {
                                    barrierView.Add(br);
                                }
                            }
                        }
                    }
                    UpdateBarrierInfo(barrierView);

                    // Hazard Light
                    hazardLightView = new List<HazardLight>();
                    foreach (Controller c in controllerView)
                    {
                        if (c.hazardLights != null && c.hazardLights.Count() > 0)
                        {
                            var tmp = c.hazardLights.Where(h => h.isDelete == false);
                            hazardLightView.AddRange(tmp);
                        }
                    }
                    UpdateHazardLightInfo(hazardLightView);
                }
                _dbContext.Dispose();
            }
            catch (Exception ex) { }

            // Weighbridge
            try
            {
                _dbContext = new QWMSDBContext();
                _weightBridgeRepository = new WeighBridgeRepository(_dbContext);
                weighbridgeView = _weightBridgeRepository.GetManyAsync(w => (w.isDelete == false) &&
                                                                     (w.UserPCID != null)).Result;
                if (weighbridgeView != null && weighbridgeView.Count() > 0)
                {
                    UpdateWeighbridgeInfo(weighbridgeView.ToList());
                }
                _dbContext.Dispose();
            }
            catch(Exception ex) { }

            // Camera
            try
            {
                _dbContext = new QWMSDBContext();
                _cameraRepository = new CameraRepository(_dbContext);
                cameraView = _cameraRepository.GetManyAsync(c => (c.isDelete == false) &&
                                                                     (c.UserPCID != null)).Result;
                if (cameraView != null && cameraView.Count() > 0)
                {
                    UpdateCameraInfo(cameraView.ToList());
                }
                _dbContext.Dispose();
            }
            catch (Exception ex) { }
        }
        #endregion

        // Update RFID Reader Info From database
        #region
        public void UpdateRFIDReaderInfo(List<BadgeReader> newInfo)
        {
            BadgeReaderDriver tmpRFID;

            lock(_rfidReaders)
            {
                if (newInfo != null && newInfo.Count > 0)
                {
                    if (_rfidReaders.Count == 0)
                    {
                        // Add (First time)
                        foreach (BadgeReader br in newInfo)
                        {
                            int tmpPort;
                            if (br.port == null)
                            {
                                tmpPort = 0;
                            }
                            else
                            {
                                tmpPort = (int)br.port;
                            }
                            _rfidReaders.Add(new BadgeReaderDriver(br.Code,
                                                       br.ipAddress,
                                                       tmpPort));
                        }
                    }
                    else
                    {
                        // Update
                        foreach (BadgeReader br in newInfo)
                        {
                            tmpRFID = _rfidReaders.Find(r => r._code == br.Code);
                            if (tmpRFID != null)
                            {
                                // Update
                                int tmpPort;
                                if (br.port == null)
                                {
                                    tmpPort = 0;
                                }
                                else
                                {
                                    tmpPort = (int)br.port;
                                }

                                if ((tmpRFID._readerIP != br.ipAddress) ||
                                    (tmpRFID._readerPort != tmpPort))
                                {
                                    // Update Info
                                    tmpRFID.closeConnection();
                                    tmpRFID.UpdateInfo(br.ipAddress, tmpPort);
                                    tmpRFID.connectToReader();
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region
        public void UpdateControllerInfo(List<Controller> newInfo)
        {
            ControllerDriver tmpCtrl;

            lock (_controllers)
            {
                if (newInfo != null && newInfo.Count > 0)
                {
                    if (_controllers.Count == 0)
                    {
                        // Add (First time)
                        foreach (Controller ctrl in newInfo)
                        {
                            _controllers.Add(new ControllerDriver(ctrl.Code, 
                                "http", ctrl.ipAdrress, ctrl.port,
                                ctrl.getPath, ctrl.setPath, ctrl.setParam, ctrl.user, ctrl.password));
                        }
                    }
                    else
                    {
                        // Update
                        foreach (Controller ctrl in newInfo)
                        {
                            tmpCtrl = _controllers.Find(c => c._code == ctrl.Code);
                            if (tmpCtrl != null)
                            {
                                // Update
                                tmpCtrl.UpdateInfo(ctrl.Code,
                                "http", ctrl.ipAdrress, ctrl.port,
                                ctrl.getPath, ctrl.setPath, ctrl.setParam, ctrl.user, ctrl.password);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region
        public void UpdateSensorInfo(List<Sensor> newInfo)
        {
            SensorDriver tmpSensor;
            ControllerDriver valueCtrl, statusCtrl;

            lock (_sensors)
            {
                if (newInfo != null && newInfo.Count > 0)
                {
                    if (_sensors.Count == 0)
                    {
                        // Add (First time)
                        foreach (Sensor ss in newInfo)
                        {
                            valueCtrl = _controllers.Where(c => c._code == ss.valueController.Code).First();
                            statusCtrl = _controllers.Where(c => c._code == ss.statusController.Code).First();
                            _sensors.Add(new SensorDriver(ss.Code,
                                valueCtrl, ss.valueControllerPort, statusCtrl, ss.statusControllerPort));
                        }
                    }
                    else
                    {
                        // Update
                        foreach (Sensor ss in newInfo)
                        {
                            tmpSensor = _sensors.Find(s => s._code == ss.Code);
                            if (tmpSensor != null)
                            {
                                // Update
                                valueCtrl = _controllers.Where(c => c._code == ss.valueController.Code).First();
                                statusCtrl = _controllers.Where(c => c._code == ss.statusController.Code).First();
                                tmpSensor.UpdateInfo(ss.Code,
                                    valueCtrl, ss.valueControllerPort, statusCtrl, ss.statusControllerPort);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region
        public void UpdateBarrierInfo(List<Barrier> newInfo)
        {
            BarrierDriver tmpBarrier;
            ControllerDriver openCtrl, closeCtrl;

            lock (_barriers)
            {
                if (newInfo != null && newInfo.Count > 0)
                {
                    if (_barriers.Count == 0)
                    {
                        // Add (First time)
                        foreach (Barrier br in newInfo)
                        {
                            openCtrl = _controllers.Where(c => c._code == br.openController.Code).First();
                            closeCtrl = _controllers.Where(c => c._code == br.closeController.Code).First();
                            _barriers.Add(new BarrierDriver(br.Code,
                                openCtrl, br.openPort, closeCtrl, br.closePort));
                        }
                    }
                    else
                    {
                        // Update
                        foreach (Barrier br in newInfo)
                        {
                            tmpBarrier = _barriers.Find(b => b._code == br.Code);
                            if (tmpBarrier != null)
                            {
                                // Update
                                openCtrl = _controllers.Where(c => c._code == br.openController.Code).First();
                                closeCtrl = _controllers.Where(c => c._code == br.closeController.Code).First();
                                tmpBarrier.UpdateInfo(br.Code,
                                    openCtrl, br.openPort, closeCtrl, br.closePort);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region
        public void UpdateHazardLightInfo(List<HazardLight> newInfo)
        {
            HazardLightDriver tmpHzLight;
            ControllerDriver ctrl;

            lock (_hazardLights)
            {
                if (newInfo != null && newInfo.Count > 0)
                {
                    if (_hazardLights.Count == 0)
                    {
                        // Add (First time)
                        foreach (HazardLight hz in newInfo)
                        {
                            ctrl = _controllers.Where(c => c._code == hz.controller.Code).First();
                            _hazardLights.Add(new HazardLightDriver(hz.Code,
                                ctrl, hz.port));
                        }
                    }
                    else
                    {
                        // Update
                        foreach (HazardLight hz in newInfo)
                        {
                            tmpHzLight = _hazardLights.Find(s => s._code == hz.Code);
                            if (tmpHzLight != null)
                            {
                                // Update
                                ctrl = _controllers.Where(c => c._code == hz.controller.Code).First();
                                tmpHzLight.UpdateInfo(hz.Code,
                                    ctrl, hz.port);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region
        public void UpdateWeighbridgeInfo(List<WeighBridge> newInfo)
        {
            WeighBridgeDriver tmpWB;

            lock (_weighbridges)
            {
                if (newInfo != null && newInfo.Count > 0)
                {
                    if (_weighbridges.Count == 0)
                    {
                        // Add (First time)
                        foreach (WeighBridge wb in newInfo)
                        {
                            _weighbridges.Add(new WeighBridgeDriver(wb.Code));
                        }
                    }
                    else
                    {
                        // Update
                        foreach (WeighBridge wb in newInfo)
                        {
                            tmpWB = _weighbridges.Find(w => w._code == wb.Code);
                            if (tmpWB != null)
                            {
                                // Update
                                tmpWB.UpdateInfo(wb.Code);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region
        public void UpdateCameraInfo(List<Camera> newInfo)
        {
            CameraDriver tmpCam;

            lock (_cameras)
            {
                if (newInfo != null && newInfo.Count > 0)
                {
                    if (_cameras.Count == 0)
                    {
                        // Add (First time)
                        foreach (Camera cam in newInfo)
                        {
                            _cameras.Add(new CameraDriver(cam.Code));
                        }
                    }
                    else
                    {
                        // Update
                        foreach (Camera cam in newInfo)
                        {
                            tmpCam = _cameras.Find(c => c._code == cam.Code);
                            if (tmpCam != null)
                            {
                                // Update
                                tmpCam.UpdateInfo(cam.Code);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        // Update Device state
        #region
        public void GetDeviceState()
        {
            Timer tmpTimer;
            System.Threading.Thread tmpThread;

            if (_rfidReaders != null && _rfidReaders.Count > 0)
            {
                // Add (first time)
                if (_getBadgeStateThreads.Count() == 0)
                {
                    foreach (BadgeReaderDriver rd in _rfidReaders)
                    {
                        tmpThread = new System.Threading.Thread(() => GetBadgeStateThreadFunc(rd));
                        tmpThread.Name = rd._code;
                        tmpThread.Start();
                        _getBadgeStateThreads.Add(tmpThread);
                    }
                }
            }

            // Controller
            if (_controllers != null && _controllers.Count > 0)
            {
                if (_getControllerStateTimer.Count == 0)
                {
                    foreach (ControllerDriver ctrl in _controllers)
                    {
                        tmpTimer = new Timer();
                        tmpTimer.Interval = 300;
                        tmpTimer.Elapsed += (sender, e) => GetControllerState_Elapsed(sender, e, ctrl);
                        _getControllerStateTimer.Add(tmpTimer);
                        tmpTimer.Enabled = true;
                    }
                }
            }
        }
        #endregion

        // Get controller status and port value
        #region
        public void GetControllerState_Elapsed(object sender, ElapsedEventArgs e, ControllerDriver ctrl)
        {
            ((Timer)sender).Enabled = false;

            if(ctrl != null)
            {
                ctrl.GetStatusSync(out string errmsg);
            }

            ((Timer)sender).Enabled = true;
        }
        #endregion

        // Get UID
        #region
        public void GetBadgeStateThreadFunc(BadgeReaderDriver reader)
        {
            int stt;

            while (true)
            {
                try
                {
                    stt = reader.UpdateState(out byte[] uidByte, out string uid, 2000);
                }
                catch (Exception ex)
                {
                    stt = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                }

                if (stt != DeviceStatus.STATUS_CODE_OK)
                {
                    System.Threading.Thread.Sleep(2000);
                }
            }

            return;
        }
        #endregion

        // Check expired - uid, weight status, camera status
        #region
        public void CheckUIDExpired(int timeOutSec)
        {
            DateTime now;
            foreach(BadgeReaderDriver r in _rfidReaders)
            {
                lock(r)
                {
                    now = DateTime.Now;
                    if (now.Subtract(r._updatedUID) > TimeSpan.FromSeconds(timeOutSec))
                    {
                        r.ClearUID();
                    }
                }
            }
        }
        #endregion

        #region
        public void CheckWeighbridgeStatusExpired(int timeOutSec)
        {
            DateTime now;
            foreach (WeighBridgeDriver wb in _weighbridges)
            {
                lock (wb)
                {
                    now = DateTime.Now;
                    if (now.Subtract(wb._upDateTime) > TimeSpan.FromSeconds(timeOutSec))
                    {
                        wb.ClearStatus();
                    }
                }
            }
        }
        #endregion

        #region
        public void CheckCameraStatusExpired(int timeOutSec)
        {
            DateTime now;
            foreach (CameraDriver cam in _cameras)
            {
                lock (cam)
                {
                    now = DateTime.Now;
                    if (now.Subtract(cam._upDateTime) > TimeSpan.FromSeconds(timeOutSec))
                    {
                        cam.ClearStatus();
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
            getInfoTimer.Interval = 2000;
            getInfoTimer.Enabled = true;
            getInfoTimer.Elapsed += GetInfoTimer_Elapsed;

            // 2. Timer to Update Device state
            System.Timers.Timer getStateTimer;
            getStateTimer = new System.Timers.Timer();
            getStateTimer.Interval = 2000;
            getStateTimer.Enabled = true;
            getStateTimer.Elapsed += GetStateTimer_Elapsed;

            // 3. Timer to check expired - uid, weight status, camera status
            System.Timers.Timer uidExpiredTimer;
            uidExpiredTimer = new System.Timers.Timer();
            uidExpiredTimer.Interval = 500;
            uidExpiredTimer.Enabled = true;
            uidExpiredTimer.Elapsed += UidExpiredTimer_Elapsed;
        }

        private void UidExpiredTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ((Timer)sender).Enabled = false;
            CheckUIDExpired(60);
            CheckWeighbridgeStatusExpired(60);
            CheckCameraStatusExpired(60);
            ((Timer)sender).Enabled = true;
        }

        private void GetStateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ((Timer)sender).Enabled = false;
            GetDeviceState();
            ((Timer)sender).Enabled = true;
        }

        private void GetInfoTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ((Timer)sender).Enabled = false;
            UpdateDeviceInfo();
            ((Timer)sender).Enabled = true;
        }

        // User API
        // Is employee UID
        public async Task<bool> IsEmployeeUID(string uid)
        {
            bool ret;
            if(uid == null || uid == "")
            {
                ret = false;
            }
            else
            {
                Employee qr;
                try
                {
                    _dbContext = new QWMSDBContext();
                    _employeeRepository = new EmployeeRepository(_dbContext);
                    qr = await _employeeRepository.GetAsync(e => e.rfidCard.code == uid &&
                                                                e.isDelete == false, QueryIncludes.EMPLOYEEINCLUDES);
                }
                catch (Exception ex)
                {
                    qr = null;
                }
                if (qr != null)
                {
                    ret  = true;
                }
                else
                {
                    ret  = false;
                }
                if(_dbContext != null)
                {
                    try
                    {
                        _dbContext.Dispose();
                    }
                    catch (Exception ex) { }
                }
            }
            return ret;
        }

        public async Task<bool> IsDriverUID(string uid)
        {
            bool ret;
            if (uid == null || uid == "")
            {
                ret = false;
            }
            else
            {
                GatePass qr;
                try
                {
                    _dbContext = new QWMSDBContext();
                    _gatePassRepository = new GatePassRepository(_dbContext);
                    qr = await _gatePassRepository.GetAsync(e => e.RFIDCard.code == uid &&
                                                                 e.isDelete == false,
                                                    QueryIncludes.GATEPASSFULLINCLUDES);
                }
                catch (Exception ex)
                {
                    qr = null;
                }
                if (qr != null)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
                if (_dbContext != null)
                {
                    try
                    {
                        _dbContext.Dispose();
                    }
                    catch (Exception ex) { }
                }
            }
            return ret;
        }

        public async Task<bool> IsBarrierSecurityUID(string uid)
        {
            bool ret;
            if (uid == null || uid == "")
            {
                ret = false;
            }
            else
            {
                User qr;
                try
                {
                    _dbContext = new QWMSDBContext();
                    _userRepository = new UserRepository(_dbContext);
                    qr = await _userRepository.GetAsync(e => e.employee.rfidCard.code == uid &&
                                                             e.isDelete == false,
                                                             QueryIncludes.USERFULLINCLUDES);
                    var permiss = qr.employee.employeeGroup.functionMaps.Where(f => f.systemFunction.Code == "QF_41500");
                    if (permiss != null && permiss.Count() > 0)
                    {
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
                
                if (_dbContext != null)
                {
                    try
                    {
                        _dbContext.Dispose();
                    }
                    catch (Exception ex) { }
                }
            }
            return ret;
        }
        
        // Get Badge Reader State
        public async Task<BadgeReader> GetBadgeReaderState(BadgeReader readerView)
        {
            BadgeReaderDriver tmp;
            if(readerView != null)
            {
                // Clear
                readerView.uid = "";
                readerView.isEmployeeUID = false;
                readerView.isDriverUID = false;
                readerView.isBarrierSecurityUID = false;
                readerView.status = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                readerView.updateStatusTime = readerView.updateUIDTime = DateTime.Now;

                if(_rfidReaders != null && _rfidReaders.Count() > 0 &&
                    readerView.Code != null && readerView.Code != "")
                {
                    tmp = _rfidReaders.Find(r => r._code == readerView.Code);
                    if(tmp != null)
                    {
                        lock (tmp)
                        {
                            readerView.uid = tmp._uid;
                            readerView.status = tmp._status;
                            readerView.updateUIDTime = tmp._updatedUID;
                            readerView.updateStatusTime = tmp._updatedStatus;
                            tmp.ClearUID();
                        }

                        // Check Employee UID
                        readerView.isEmployeeUID = await IsEmployeeUID(readerView.uid);
                        readerView.isDriverUID = await IsDriverUID(readerView.uid);
                        readerView.isBarrierSecurityUID = await IsBarrierSecurityUID(readerView.uid);
                    }
                }
            }
            return readerView;
        }

        // Get Many Badge Reader State
        public async Task<List<BadgeReader>> GetManyBadgeReaderState(List<BadgeReader> readerViews)
        {
            if(readerViews != null && readerViews.Count() > 0)
            {
                foreach(BadgeReader r in readerViews)
                {
                    await GetBadgeReaderState(r);
                }
            }
            return readerViews;
        }

        // Get Badge Reader Status
        #region
        public async Task<BadgeReader> GetBadgeReaderStatus(BadgeReader readerView)
        {
            BadgeReaderDriver tmp;
            if (readerView != null)
            {
                // Clear
                readerView.uid = "";
                readerView.isEmployeeUID = false;
                readerView.status = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                readerView.updateStatusTime = readerView.updateUIDTime = DateTime.Now;

                if (_rfidReaders != null && _rfidReaders.Count() > 0 &&
                    readerView.Code != null && readerView.Code != "")
                {
                    tmp = _rfidReaders.Find(r => r._code == readerView.Code);
                    if (tmp != null)
                    {
                        lock (tmp)
                        {
                            readerView.uid = tmp._uid;
                            readerView.status = tmp._status;
                            readerView.updateUIDTime = tmp._updatedUID;
                            readerView.updateStatusTime = tmp._updatedStatus;
                        }
                    }
                }
            }
            return readerView;
        }
        #endregion

        // Get Many Badge Reader Status
        #region
        public async Task<List<BadgeReader>> GetManyBadgeReaderStatus(List<BadgeReader> readerViews)
        {
            if (readerViews != null && readerViews.Count() > 0)
            {
                foreach (BadgeReader r in readerViews)
                {
                    await GetBadgeReaderStatus(r);
                }
            }
            return readerViews;
        }
        #endregion

        // Check badge reader connection
        #region
        public int CheckBadgeReaderConnection(BadgeReader badgeReader)
        {
            int stt;

            try
            {
                BadgeReaderDriver reader = new BadgeReaderDriver(badgeReader.ipAddress, (int)badgeReader.port);
                stt = reader.connectToReader();
                reader.closeConnection();
            }
            catch (Exception ex)
            {
                stt = DeviceStatus.STATUS_CODE_ERR_CONNECT;
            }

            return stt;
        }
        #endregion

        // Get Controller State
        #region
        public async Task<Controller>  GetControllerState(Controller controllerView)
        {
            ControllerDriver tmp;
            if(controllerView != null)
            {
                // Clear
                controllerView.status = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                controllerView.updateTime = DateTime.Now;
                
                if(_controllers != null && _controllers.Count() > 0 &&
                    controllerView.Code != null && controllerView.Code != "")
                {
                    tmp = _controllers.Find(c => c._code == controllerView.Code);
                    if(tmp != null)
                    {
                        controllerView.status = tmp.GetStatusLocal(out string errMsg);
                        controllerView.updateTime = tmp.GetUpdateTime();
                    }
                }
            }
            return controllerView;
        }
        #endregion

        // Get Many Controller Status
        #region
        public async Task<List<Controller>> GetManyControllerState(List<Controller> controllerViews)
        {
            if (controllerViews != null && controllerViews.Count() > 0)
            {
                foreach (Controller ctr in controllerViews)
                {
                    await GetControllerState(ctr);
                }
            }
            return controllerViews;
        }
        #endregion

        // Get Sensor State
        #region
        public async Task<Sensor> GetSensorState(Sensor sensorView)
        {
            SensorDriver tmp;
            if (sensorView != null)
            {
                // Clear
                sensorView.status = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                sensorView.portValue = SensorDriver.NO_OBJECT_PRESENCE;

                if (_sensors != null && _sensors.Count() > 0 &&
                    sensorView.Code != null && sensorView.Code != "")
                {
                    tmp = _sensors.Find(s => s._code == sensorView.Code);
                    if (tmp != null)
                    {
                        sensorView.status = tmp.GetStatusLocal(out string errMsg);
                        sensorView.portValue = tmp.GetValueLocal();
                    }
                }
            }
            return sensorView;
        }
        #endregion

        // Get Many Sensor Status
        #region
        public async Task<List<Sensor>> GetManySensorState(List<Sensor> sensorViews)
        {
            if (sensorViews != null && sensorViews.Count() > 0)
            {
                foreach (Sensor ss in sensorViews)
                {
                    await GetSensorState(ss);
                }
            }
            return sensorViews;
        }
        #endregion

        // Get Barrier State
        #region
        public async Task<Barrier> GetBarrierState(Barrier barrierView)
        {
            BarrierDriver tmp;
            if (barrierView != null)
            {
                // Clear
                barrierView.status = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                barrierView.openPortValue = BarrierDriver.STATUS_PORT_LOW;
                barrierView.closePortValue = BarrierDriver.STATUS_PORT_LOW;

                if (_barriers != null && _barriers.Count() > 0 &&
                    barrierView.Code != null && barrierView.Code != "")
                {
                    tmp = _barriers.Find(b => b._code == barrierView.Code);
                    if (tmp != null)
                    {
                        barrierView.status = tmp.GetStatusLocal(out string errMsg);
                        barrierView.openPortValue = tmp.GetOpenPortLocal();
                        barrierView.closePortValue = tmp.GetClosePortLocal();
                    }
                }
            }
            return barrierView;
        }
        #endregion

        // Get Many Barrier State
        #region
        public async Task<List<Barrier>> GetManyBarrierState(List<Barrier> barrierViews)
        {
            if (barrierViews != null && barrierViews.Count() > 0)
            {
                foreach (Barrier br in barrierViews)
                {
                    await GetBarrierState(br);
                }
            }
            return barrierViews;
        }
        #endregion

        // Get Hazard Light State
        #region
        public async Task<HazardLight> GetHazardLightState(HazardLight hazardLightView)
        {
            HazardLightDriver tmp;
            if (hazardLightView != null)
            {
                // Clear
                hazardLightView.status = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                hazardLightView.portValue = HazardLightDriver.STATUS_PORT_LOW;

                if (_hazardLights != null && _hazardLights.Count() > 0 &&
                    hazardLightView.Code != null && hazardLightView.Code != "")
                {
                    tmp = _hazardLights.Find(h => h._code == hazardLightView.Code);
                    if (tmp != null)
                    {
                        hazardLightView.status = tmp.GetStatusLocal(out string errMsg);
                        hazardLightView.portValue = tmp.GetPortLocal();
                    }
                }
            }
            return hazardLightView;
        }
        #endregion

        // Get Many Hazard Light State
        #region
        public async Task<List<HazardLight>> GetManyHazardLightState(List<HazardLight> hazardLightViews)
        {
            if (hazardLightViews != null && hazardLightViews.Count() > 0)
            {
                foreach (HazardLight hz in hazardLightViews)
                {
                    await GetHazardLightState(hz);
                }
            }
            return hazardLightViews;
        }
        #endregion

        // Get Weighbridge State
        #region
        public async Task<WeighBridge> GetWeighbridgeState(WeighBridge weighbridgeView)
        {
            WeighBridgeDriver tmp;
            if (weighbridgeView != null)
            {
                // Clear
                weighbridgeView.status = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                weighbridgeView.weight = 0;
                weighbridgeView.updateTime = DateTime.Now;

                if (_weighbridges != null && _weighbridges.Count() > 0 &&
                    weighbridgeView.Code != null && weighbridgeView.Code != "")
                {
                    tmp = _weighbridges.Find(w => w._code == weighbridgeView.Code);
                    if (tmp != null)
                    {
                        weighbridgeView.status = tmp._status;
                        weighbridgeView.updateTime = tmp._upDateTime;
                        weighbridgeView.weight = tmp._weight;
                    }
                }
            }
            return weighbridgeView;
        }
        #endregion

        // Get Many Weighbridge State
        #region
        public async Task<List<WeighBridge>> GetManyWeighbridgeState(List<WeighBridge> weighbridgeViews)
        {
            if (weighbridgeViews != null && weighbridgeViews.Count() > 0)
            {
                foreach (WeighBridge wb in weighbridgeViews)
                {
                    await GetWeighbridgeState(wb);
                }
            }
            return weighbridgeViews;
        }
        #endregion

        // Get Camera State
        #region
        public async Task<Camera> GetCameraState(Camera cameraView)
        {
            CameraDriver tmp;
            if (cameraView != null)
            {
                // Clear
                cameraView.status = DeviceStatus.STATUS_CODE_ERR_DEVICE;
                cameraView.updateTime = DateTime.Now;

                if (_cameras != null && _cameras.Count() > 0 &&
                    cameraView.Code != null && cameraView.Code != "")
                {
                    tmp = _cameras.Find(c => c._code == cameraView.Code);
                    if (tmp != null)
                    {
                        cameraView.status = tmp._status;
                        cameraView.updateTime = tmp._upDateTime;
                    }
                }
            }
            return cameraView;
        }
        #endregion

        // Get Many Camera State
        #region
        public async Task<List<Camera>> GetManyCameraState(List<Camera> cameraViews)
        {
            if (cameraViews != null && cameraViews.Count() > 0)
            {
                foreach (Camera cam in cameraViews)
                {
                    await GetCameraState(cam);
                }
            }
            return cameraViews;
        }
        #endregion

        // Open/Close Barrier
        #region
        public async Task<bool> OpenBarrier(string code)
        {
            bool ret;

            // Init
            ret = false;
            if (_barriers != null && _barriers.Count() > 0 &&
                code != null && code != "")
            {
                var brr = _barriers.Find(b => b._code == code);
                if (brr != null)
                {
                    // set Open port
                    brr.SetOpenPort(BarrierDriver.STATUS_PORT_HIGHT);

                    // un-set Close port
                    brr.SetClosePort(BarrierDriver.STATUS_PORT_LOW);

                    // set
                    ret = true;
                }
            }
            return ret;
        }

        // Open Barrier
        public async Task<bool> CloseBarrier(string code)
        {
            bool ret;

            // Init
            ret = false;
            if (_barriers != null && _barriers.Count() > 0 &&
                code != null && code != "")
            {
                var brr = _barriers.Find(b => b._code == code);
                if (brr != null)
                {
                    // un-set Open port
                    brr.SetOpenPort(BarrierDriver.STATUS_PORT_LOW);

                    // set Close port
                    brr.SetClosePort(BarrierDriver.STATUS_PORT_HIGHT);

                    // set
                    ret = true;
                }
            }
            return ret;
        }
        #endregion

        // Open/Close Hazard light
        #region
        public async Task<bool> OpenHazardLight(string code)
        {
            bool ret;

            // Init
            ret = false;
            if (_hazardLights != null && _hazardLights.Count() > 0 &&
                code != null && code != "")
            {
                var hz = _hazardLights.Find(h => h._code == code);
                if(hz != null)
                {
                    hz.SetPort(HazardLightDriver.STATUS_PORT_HIGHT);
                    ret = true;
                }
            }
            return ret;
        }

        public async Task<bool> CloseHazardLight(string code)
        {
            bool ret;

            // Init
            ret = false;
            if (_hazardLights != null && _hazardLights.Count() > 0 &&
                code != null && code != "")
            {
                var hz = _hazardLights.Find(h => h._code == code);
                if (hz != null)
                {
                    hz.SetPort(HazardLightDriver.STATUS_PORT_LOW);
                    ret = true;
                }
            }
            return ret;
        }
        #endregion

        // Update Camera States
        #region
        public async Task<bool> UpdateCameraState(List<Camera> cameraViews)
        {
            bool ret;
            CameraDriver tmp;
            DateTime now;

            if(cameraViews != null && cameraViews.Count() > 0 &&
                _cameras != null && _cameras.Count() > 0)
            {
                ret = true;
                now = DateTime.Now;
                foreach(Camera cam in cameraViews)
                {
                    tmp = _cameras.Find(c => c._code == cam.Code);
                    if(tmp != null)
                    {
                        tmp._status = cam.status;
                        tmp._upDateTime = now;
                    }
                    else
                    {
                        ret = false;
                    }
                }
            }
            else
            {
                ret = false;
            }

            return ret;
        }
        #endregion

        // Update Weighbridge States
        #region
        public async Task<bool> UpdateWeighbridgeState(List<WeighBridge> weighbridgeViews)
        {
            bool ret;
            WeighBridgeDriver tmp;
            DateTime now;

            if (weighbridgeViews != null && weighbridgeViews.Count() > 0 &&
                _weighbridges != null && _weighbridges.Count() > 0)
            {
                ret = true;
                now = DateTime.Now;
                foreach (WeighBridge wb in weighbridgeViews)
                {
                    tmp = _weighbridges.Find(w => w._code == wb.Code);
                    if (tmp != null)
                    {
                        tmp._status = wb.status;
                        tmp._weight = wb.weight;
                        tmp._upDateTime = now;
                    }
                    else
                    {
                        ret = false;
                    }
                }
            }
            else
            {
                ret = false;
            }

            return ret;
        }
        #endregion
    }
}