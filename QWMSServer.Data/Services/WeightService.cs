using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using QWMSServer.Data.Common;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;

namespace QWMSServer.Data.Services
{
    public class WeightService : IWeightService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGatePassRepository _gatePassRepository;
        private readonly IQueueListRepository _queueListRepository;
        private readonly IWeightRecordRepository _weightRecordRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWeighBridgeRepository _weighBridgeRepository;
        private readonly IWeighbridgeConfigurationRepository _weighbridgeConfigurationRepository;
        private readonly ILaneRepository _laneRepository;
        private readonly ICommonService _commonService;
		private readonly IAuthService _authService;
        private readonly IQueueService _queueService;
        private readonly IRFIDCardRepository _rFIDCardRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IReportService _reportService;

        public WeightService(IUnitOfWork unitOfWork, IGatePassRepository gatePassRepository, IQueueListRepository queueListRepository,
                            IWeightRecordRepository weightRecordRepository, IEmployeeRepository employeeRepository, 
                            IWeighBridgeRepository weighBridgeRepository, IWeighbridgeConfigurationRepository weighbridgeConfigurationRepository,
        ILaneRepository laneRepository, ICommonService commonService,
							IAuthService authService, IQueueService queueService,
                            IRFIDCardRepository rFIDCardRepository, IReportService reportService, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _gatePassRepository = gatePassRepository;
            _queueListRepository = queueListRepository;
            _weightRecordRepository = weightRecordRepository;
            _employeeRepository = employeeRepository;
            _weighBridgeRepository = weighBridgeRepository;
            _weighbridgeConfigurationRepository = weighbridgeConfigurationRepository;
            _laneRepository = laneRepository;
            _commonService = commonService;
			_authService = authService;
            _queueService = queueService;
            _rFIDCardRepository = rFIDCardRepository;
            _reportService = reportService;
            _orderRepository = orderRepository;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        public ResponseViewModel<GenericResponseModel> AddTruckCamera(string fileName, byte[] fileContent)
        {
            ResponseViewModel<GenericResponseModel> response = new ResponseViewModel<GenericResponseModel>();
            string filePath = Constant.TruckCapturePath + fileName;
            // Write to file
            if (fileName == null || fileContent == null)
            {
                response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
            }
            else
            {
                Directory.CreateDirectory(Constant.TruckCapturePath);
                File.WriteAllBytes(filePath, fileContent);
                response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.SUCCESS, true);
            }
            return response;
        }

        public async Task<ResponseViewModel<QueueListViewModel>> CallNextTruck(int truckGroupID)
        {
            ResponseViewModel<QueueListViewModel> response = new ResponseViewModel<QueueListViewModel>();
            var queues = await _queueListRepository.GetManyAsync( qu => qu.gatePass.truckGroupID == truckGroupID && qu.isDelete == false, QueryIncludes.QUEUELISTFULLINCLUDES);
            var firstqueue = queues.OrderBy(q => q.queueNumber).First();
            var gatePass = await _gatePassRepository.GetAsync(gt => gt.ID == firstqueue.gatePassID && gt.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
            if(gatePass == null)
            {
                response = ResponseConstructor<QueueListViewModel>.ConstructBoolRes(ResponseCode.ERR_QUE_NO_QUEUE_FOUND, false);
            }
            else
            {
                // Change GatePass state
                switch (gatePass.stateID)
                {
                    case GatepassState.STATE_REGISTERED:
                        gatePass.stateID = GatepassState.STATE_CALLING_1;
                        break;
                    case GatepassState.STATE_CALLING_1:
                        gatePass.stateID = GatepassState.STATE_CALLING_2;
                        break;
                    case GatepassState.STATE_CALLING_2:
                        gatePass.stateID = GatepassState.STATE_CALLING_3;
                        break;
                    case GatepassState.STATE_CALLING_3:
                        // Update state & Update queueList
                        gatePass.stateID = GatepassState.STATE_REGISTERED;
                        var currentQueue = await _queueListRepository.GetAsync(qu => qu.gatePassID == gatePass.ID && qu.isDelete == false);
                        var queueList = await _queueListRepository.GetManyAsync(qu => qu.queueNumber > currentQueue.queueNumber && qu.queueNumber <= (currentQueue.queueNumber + 5) && qu.gatePass.truckGroup.ID == gatePass.truckGroup.ID && qu.isDelete == false);
                        // If there are less than 5 - move to the last
                        if(queueList.Count() < 5)
                        {
                            // Set new Order
                            currentQueue.queueNumber += queueList.Count();
                            // Shift up 1 item for orther QueueItem
                            foreach (var queueItem in queueList)
                            {
                                queueItem.queueNumber -= 1;
                                _queueListRepository.Update(queueItem);
                            }
                        }else // If more than 5 - move down 5
                        {
                            // Set new order
                            currentQueue.queueNumber += 5;
                            // Shift up 1 item for orther QueueItem
                            foreach (var queueItem in queueList)
                            {
                                queueItem.queueNumber -= 1;
                                _queueListRepository.Update(queueItem);
                            }
                        }
                        break;
                    default:
                        return response = ResponseConstructor<QueueListViewModel>.ConstructBoolRes(ResponseCode.ERR_QUE_NO_QUEUE_FOUND, false);
                }
                _gatePassRepository.Update(gatePass);
                if(await _unitOfWork.SaveChangesAsync())
                {
                    response = ResponseConstructor<QueueListViewModel>.ConstructBoolRes(ResponseCode.SUCCESS, true);
                }
                else
                {
                    response = ResponseConstructor<QueueListViewModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                }
            }
            return response;
        }

        public async Task<ResponseViewModel<QueueListViewModel>> CallNextTruckByGatePassID(int gatePassID)
        {
            ResponseViewModel<QueueListViewModel> response = new ResponseViewModel<QueueListViewModel>();
            var gatePass = await _gatePassRepository.GetAsync(gt => gt.ID == gatePassID && gt.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
            if (gatePass == null)
            {
                response = ResponseConstructor<QueueListViewModel>.ConstructBoolRes(ResponseCode.ERR_QUE_NO_QUEUE_FOUND, false);
            }
            else
            {
                // Change GatePass state
                switch (gatePass.stateID)
                {
                    case GatepassState.STATE_REGISTERED:
                        gatePass.stateID = GatepassState.STATE_CALLING_1;
                        break;
                    case GatepassState.STATE_CALLING_1:
                        gatePass.stateID = GatepassState.STATE_CALLING_2;
                        break;
                    case GatepassState.STATE_CALLING_2:
                        gatePass.stateID = GatepassState.STATE_CALLING_3;
                        break;
                    case GatepassState.STATE_CALLING_3:
                        // Update state & Update queueList
                        gatePass.stateID = GatepassState.STATE_REGISTERED;
                        var currentQueue = await _queueListRepository.GetAsync(qu => qu.gatePassID == gatePassID && qu.isDelete == false);
                        var queueList = await _queueListRepository.GetManyAsync(qu => qu.queueOrder > currentQueue.queueOrder && qu.queueOrder <= (currentQueue.queueOrder + 5) && qu.gatePass.truckGroup.ID == gatePass.truckGroup.ID && qu.isDelete == false);
                        // If there are less than 5 - move to the last
                        if (queueList.Count() < 5)
                        {
                            // Set new Order
                            currentQueue.queueOrder += queueList.Count();
                            // Shift up 1 item for orther QueueItem
                            foreach (var queueItem in queueList)
                            {
                                queueItem.queueOrder -= 1;
                                _queueListRepository.Update(queueItem);
                            }
                        }
                        else // If more than 5 - move down 5
                        {
                            // Set new order
                            currentQueue.queueOrder += 5;
                            // Shift up 1 item for orther QueueItem
                            foreach (var queueItem in queueList)
                            {
                                queueItem.queueOrder -= 1;
                                _queueListRepository.Update(queueItem);
                            }
                        }
                        break;
                    default:
                        response = ResponseConstructor<QueueListViewModel>.ConstructBoolRes(ResponseCode.ERR_QUE_NO_QUEUE_FOUND, false);
                        break;
                }
                _gatePassRepository.Update(gatePass);
                if (await _unitOfWork.SaveChangesAsync())
                {
                    response = ResponseConstructor<QueueListViewModel>.ConstructBoolRes(ResponseCode.SUCCESS, true);
                }
                else
                {
                    response = ResponseConstructor<QueueListViewModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                }
            }
            return response;
        }

        public async Task<ResponseViewModel<QueueListViewModel>> GetQueueList()
        {
            ResponseViewModel<QueueListViewModel> response = new ResponseViewModel<QueueListViewModel>();
            try
            {
                var queueList = await _queueListRepository.GetManyAsync(qu => qu.isDelete == false, QueryIncludes.QUEUELISTFULLINCLUDES);
                if (queueList == null)
                {
                    response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.ERR_QUE_NO_QUEUE_FOUND, null);
                }
                else
                {
                    response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS,
                                                                                                Mapper.Map<IEnumerable<QueueList>,
                                                                                                IEnumerable<QueueListViewModel>>(queueList));
                }
                return response;
            }
            catch (Exception)
            {
                return response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.ERR_QUE_NO_QUEUE_FOUND, null);
            }
        }

        public async Task<ResponseViewModel<WeightRecordViewModel>> UpdateWeightValue(WeightDataViewModel weightDataViewModel)
        {
            ResponseViewModel<WeightRecordViewModel> response = new ResponseViewModel<WeightRecordViewModel>();
            try
            {
                WeightRecord weightRecord = new WeightRecord();
                Random r = new Random();
                var weighNum = await _weightRecordRepository.GetManyAsync(wt => wt.gatepassID == weightDataViewModel.gatePassID && wt.isDelete == false);
                weightRecord.code = null;
                weightRecord.gatepassID = weightDataViewModel.gatePassID;
                weightRecord.isDelete = false;
                weightRecord.weighBridgeID = weightDataViewModel.weighBridgeID;
                weightRecord.weighBridge = await _weighBridgeRepository.GetByIdAsync(weightDataViewModel.weighBridgeID);
                weightRecord.weightEmployeeID = weightDataViewModel.employeeID;
                weightRecord.employee = await _employeeRepository.GetByIdAsync(weightDataViewModel.employeeID);
                weightRecord.weightTime = DateTime.Now;
                weightRecord.weightValue = weightDataViewModel.weightValue;
                weightRecord.frontCameraCapturePath = Constant.TruckCapturePath + weightDataViewModel.frontCameraName;
                weightRecord.gearCameraCapturePath = Constant.TruckCapturePath + weightDataViewModel.gearCameraName;
                weightRecord.cabinCameraCapturePath = Constant.TruckCapturePath + weightDataViewModel.cabinCameraName;
                weightRecord.containerCameraCapturePath = Constant.TruckCapturePath + weightDataViewModel.containerCameraName;
                weightRecord.weightNo = weighNum.Count() + 1;
                weightRecord.isSuccess = weightDataViewModel.isSuccess;
                weightRecord.isOverWeight = weightDataViewModel.isOverWeight;
                weightRecord.PCIP = weightDataViewModel.PCIP;
                var gatePass = await _gatePassRepository.GetAsync(gt => gt.ID == weightDataViewModel.gatePassID && gt.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                if (gatePass.stateID == GatepassState.STATE_FINISH_SECURITY_CHECK_IN)
                    gatePass.stateID = GatepassState.STATE_FINISH_WEIGHT_IN;
                else if (gatePass.stateID == GatepassState.STATE_FINISH_WAREHOUSE_CHECK_OUT)
                    gatePass.stateID = GatepassState.STATE_FINISH_WEIGHT_OUT;
                _gatePassRepository.Update(gatePass);
                _weightRecordRepository.Add(weightRecord);
                if (await _unitOfWork.SaveChangesAsync())
                {
                    response = ResponseConstructor<WeightRecordViewModel>.ConstructBoolRes(ResponseCode.SUCCESS, true);
                }
                else
                {
                    response = ResponseConstructor<WeightRecordViewModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                }
                return response;
            }
            catch (Exception e )
            {
                return response = ResponseConstructor<WeightRecordViewModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
            }
        }

        public async Task<ResponseViewModel<WeightRecordViewModel>> GetWeightValueByGatePassID(int gatePassID)
        {
            ResponseViewModel<WeightRecordViewModel> response = new ResponseViewModel<WeightRecordViewModel>();
            try
            {
                var weighRecord = await _weightRecordRepository.GetManyAsync(wt => wt.gatepassID == gatePassID && wt.isDelete == false);
                if (weighRecord == null)
                {
                    response = ResponseConstructor<WeightRecordViewModel>.ConstructEnumerableData(ResponseCode.ERR_QUE_WEIGH_NO_FOUND, Mapper.Map<IEnumerable<WeightRecord>, IEnumerable<WeightRecordViewModel>>(weighRecord));
                }
                else
                {
                    response = ResponseConstructor<WeightRecordViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, Mapper.Map<IEnumerable<WeightRecord>, IEnumerable<WeightRecordViewModel>>(weighRecord));
                }
                return response;
            }
            catch (Exception)
            {
                return response = ResponseConstructor<WeightRecordViewModel>.ConstructEnumerableData(ResponseCode.ERR_SEC_UNKNOW, null);
            }
        }

        public async Task<ResponseViewModel<WeightRecordViewModel>> GetTruckNetWeightValueByTruckID(int truckID)
        {
            ResponseViewModel<WeightRecordViewModel> response = new ResponseViewModel<WeightRecordViewModel>();
            try
            {
                var _weighRecords = await _weightRecordRepository.GetManyAsync(wt => wt.gatePass.truckID == truckID && wt.isSuccess == true, QueryIncludes.WEIGHT_RECORD_INCLUDES);
                List<int> gatePassIDs = new List<int>();
                List<WeightRecord> weightRecords = new List<WeightRecord>();
                foreach (var item in _weighRecords)
                {
                    gatePassIDs.Add(item.gatepassID);
                }
                gatePassIDs = gatePassIDs.Distinct().ToList();
                foreach (var item in gatePassIDs)
                {
                    var wbrs = _weighRecords.Where( wt => wt.gatepassID == item);
                    if (wbrs.Count() > 1)
                    {
                        switch (wbrs.ToList()[0].gatePass.weightType)
                        {
                            case WeightType.WEIGHT_IN:
                                weightRecords.Add(wbrs.Last());
                                break;
                            case WeightType.WEIGHT_OUT:
                                weightRecords.Add(wbrs.First());
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (weightRecords == null)
                {
                    response = ResponseConstructor<WeightRecordViewModel>.ConstructEnumerableData(ResponseCode.ERR_QUE_WEIGH_NO_FOUND, Mapper.Map<IEnumerable<WeightRecord>, IEnumerable<WeightRecordViewModel>>(weightRecords));
                }
                else
                {
                    response = ResponseConstructor<WeightRecordViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, Mapper.Map<IEnumerable<WeightRecord>, IEnumerable<WeightRecordViewModel>>(weightRecords));
                }
                return response;
            }
            catch (Exception)
            {
                return response = ResponseConstructor<WeightRecordViewModel>.ConstructEnumerableData(ResponseCode.ERR_SEC_UNKNOW, null);
            }
        }

        public async Task<ResponseViewModel<UpdateLaneStatusViewModel>> UpdateLaneStatus(UpdateLaneStatusViewModel updateLaneStatusViewModel)
        {
            ResponseViewModel<UpdateLaneStatusViewModel> response = new ResponseViewModel<UpdateLaneStatusViewModel>();
            //Get lane from laneName
            var laneRecord = await _laneRepository.GetAsync(l => l.nameVi.Equals(updateLaneStatusViewModel.laneName), null);
            if (laneRecord != null)
            {
                //Check current status
                if(laneRecord.status == 0) // If lane block
                {
                    laneRecord.status = 1;
                    laneRecord.usingStatus = 0;
                }
                else
                {
                    if (laneRecord.usingStatus == 0) // If lane free
                    {
                        laneRecord.status = 0;
                        laneRecord.usingStatus = 0;
                    }
                    else // If lane using
                    {
                        response.errorCode = -1;
                    }
                }
            }

            _laneRepository.Update(laneRecord);
            await _unitOfWork.SaveChangesAsync();
            await _queueService.ReOrderQueue();
            return response;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetEmptyGatepass(int employeeID)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            //Search empty gatepass
            var result = await _gatePassRepository.GetAsync(g => (g.driverID == null) 
                                                                    && (g.truckID == null) 
                                                                    && (g.customerID == null) 
                                                                    && (g.weightType == null)
                                                                    && (g.warehouseID == null)
                                                                    && (g.materialID == null)
                                                                    && g.isDelete == true, null);
            if (result != null)
            {
                //Return empty gatepass
                var emptyGatepassView = Mapper.Map<GatePass, GatePassViewModel>(result);
                responseViewModel.responseData = emptyGatepassView;
                return responseViewModel;
            }
            //If don't have empty gatepass
            //Create empty gatepass
            GatePass emptyGatepass = new GatePass();
            emptyGatepass.code = "GP." + DateTime.Now.ToString("yyMMddHHmmss", CultureInfo.InvariantCulture);
            emptyGatepass.createDate = DateTime.Now;
            emptyGatepass.enterTime = DateTime.Now;
            emptyGatepass.leaveTime = DateTime.Now;
            emptyGatepass.isDelete = true;
            var emp = _employeeRepository.GetByIdAsync(employeeID);
            emptyGatepass.employeeID = employeeID;

            _gatePassRepository.Add(emptyGatepass);
            await this.SaveChangesAsync();

            result = await _gatePassRepository.GetAsync(g => (g.driverID == null)
                                                                    && (g.truckID == null)
                                                                    && (g.customerID == null)
                                                                    && (g.weightType == null)
                                                                    && (g.warehouseID == null)
                                                                    && (g.materialID == null)
                                                                    && g.isDelete == true, null);
            if (result != null)
            {
                //Return empty gatepass
                var emptyGatepassView = Mapper.Map<GatePass, GatePassViewModel>(result);
                responseViewModel.responseData = emptyGatepassView;
                return responseViewModel;
            }
            else
            {
                //Return fail, can get empty gatepass
                responseViewModel.errorCode = ResponseCode.ERR_WEI_NOT_FOUND_EMPTY_GATEPASS;
                responseViewModel.errorText = ResponseText.ERR_WEI_NOT_FOUND_EMPTY_GATEPASS_VI;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<RFIDCardViewModel>> GetRFIDNotUse()
        {
            ResponseViewModel<RFIDCardViewModel> responseViewModel = new ResponseViewModel<RFIDCardViewModel>();
            var result = await _rFIDCardRepository.GetManyAsync(g => (g.status == 0) && (g.isDelete == false), null);
            var rFIDView = Mapper.Map<IEnumerable<RFIDCard>, IEnumerable<RFIDCardViewModel>>(result);
            responseViewModel.responseDatas = rFIDView;
            return responseViewModel;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> UpdateGatepass(GatePassViewModel gatepass)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            try
            {
                GatePass result = await _gatePassRepository.GetAsync(g => g.code.Equals(gatepass.code), QueryIncludes.GATEPASSFULLINCLUDES);
                if (result.weightRecords.Count() > 0)
                {
                    responseViewModel.booleanResponse = false;
                    responseViewModel.errorText = ResponseText.ERR_WEIGHTED_GATEPASS;
                    return responseViewModel;
                }
                //Update gatepass infor
                if (gatepass.driver != null)
                    result.driverID = gatepass.driver.ID;

                if (gatepass.truck != null)
                    result.truckID = gatepass.truck.ID;

                if (gatepass.RFIDCard != null)
                {
                    if ((result.RFIDCardID > 0) && (result.RFIDCardID != gatepass.RFIDCard.ID))
                    {
                        //Change status for old RFIDCard
                        var oldRFID = await _rFIDCardRepository.GetAsync(g => g.ID == result.RFIDCardID, null);
                        if (oldRFID != null)
                        {
                            oldRFID.status = 0;
                            _rFIDCardRepository.Update(oldRFID);
                            await this.SaveChangesAsync();
                        }

                    }
                    //Change status for new RFID Card
                    result.RFIDCardID = gatepass.RFIDCard.ID;
                    var newRFID = await _rFIDCardRepository.GetAsync(g => g.ID == result.RFIDCardID, null);
                    if (newRFID != null)
                    {
                        newRFID.status = 1;
                        _rFIDCardRepository.Update(newRFID);
                        await this.SaveChangesAsync();
                    }
                }

                if (gatepass.weightType != 0)
                    result.weightType = gatepass.weightType;

                if (gatepass.warehouse != null)
                    result.warehouseID = gatepass.warehouse.ID;

                if (gatepass.loadingBay != null)
                    result.loadingBayID = gatepass.loadingBay.ID;

                result.isDelete = false;
                result.tareWeightValue = gatepass.tareWeightValue;
                result.netWeightValue = gatepass.netWeightValue;
                result.registGrossWeight = gatepass.registGrossWeight;

                foreach (var order in result.orders.ToList())
                {
                    var torder = await _orderRepository.GetAsync(o => o.ID == order.ID);
                    //torder.isDelete = true;
                    torder.gatePassID = null;
                    torder.gatePass = null;
                    _orderRepository.Update(torder);
                }

                foreach (var order in gatepass.orders.ToList())
                {
                    if(gatepass.weightType == WeightType.WEIGHT_IN)
                    {
                        if(order.orderTypeID == Constant.PURCHASEORDER)
                        {
                            var torder = await _orderRepository.GetAsync(o => o.ID == order.ID, QueryIncludes.ORDERSHORTINCUDES);
                            torder.isDelete = false;
                            torder.gatePassID = gatepass.ID;
                            _orderRepository.Update(torder);
                            result.carrierVendorID = torder.purchaseOrder.carrierVendorID;
                        }
                    }else if(gatepass.weightType == WeightType.WEIGHT_OUT)
                    {
                        if (order.orderTypeID == Constant.DELIVERYORDER)
                        {
                            var torder = await _orderRepository.GetAsync(o => o.ID == order.ID, QueryIncludes.ORDERSHORTINCUDES);
                            torder.isDelete = false;
                            torder.gatePassID = gatepass.ID;
                            _orderRepository.Update(torder);
                            result.customerID = torder.deliveryOrder.customerID;
                        }
                    }
                    else if (gatepass.weightType == WeightType.WEIGHT_INTERNAL)
                    {
                        if (order.orderTypeID == Constant.INTERNALORDER)
                        {
                            var torder = await _orderRepository.GetAsync(o => o.ID == order.ID);
                            torder.isDelete = false;
                            torder.gatePassID = gatepass.ID;
                            _orderRepository.Update(torder);
                        }
                    }
                }
                await this.SaveChangesAsync();

                _gatePassRepository.Update(result);
                await this.SaveChangesAsync();
                responseViewModel.booleanResponse = true;
                return responseViewModel;
            }
            catch (Exception ex)
            {
                responseViewModel.booleanResponse = false;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<WeighBridge>> GetWB(string WBCode)
        {
            try
            {
                var result = await _weighBridgeRepository.GetAsync(c => c.isDelete == false &&
                                                                       c.Code == WBCode, QueryIncludes.WBINCLUDES);
                ResponseViewModel<WeighBridge> responseViewModel = new ResponseViewModel<WeighBridge>();
                responseViewModel.responseData = Mapper.Map<WeighBridge, WeighBridge>(result);
                return responseViewModel;
            }
            catch (Exception ex)
            {
                ResponseViewModel<WeighBridge> responseViewModel = new ResponseViewModel<WeighBridge>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<WeighBridge>> UpdateWB(WeighBridge WBView)
        {
            try
            {
                var result = await _weighBridgeRepository.GetAsync(c => c.isDelete == false &&
                                                                       c.Code == WBView.Code, QueryIncludes.WBINCLUDES);
                // Update WB
                result.WBConfigCode = WBView.WBConfigCode;

                _weighBridgeRepository.Update(result);
                await this.SaveChangesAsync();
                ResponseViewModel<WeighBridge> responseViewModel = new ResponseViewModel<WeighBridge>();
                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<WeighBridge> responseViewModel = new ResponseViewModel<WeighBridge>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<WeighbridgeConfiguration>> GetAllWBConfigs()
        {
            try
            {
                var result = await _weighbridgeConfigurationRepository.GetManyAsync(c => c.isDelete == false);
                ResponseViewModel<WeighbridgeConfiguration> responseViewModel = new ResponseViewModel<WeighbridgeConfiguration>();
                responseViewModel.responseDatas = result;
                return responseViewModel;
            }
            catch (Exception ex)
            {
                ResponseViewModel<WeighbridgeConfiguration> responseViewModel = new ResponseViewModel<WeighbridgeConfiguration>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<WeighbridgeConfiguration>> UpdateWBConfig(WeighbridgeConfiguration WBConfigView)
        {
            try
            {
                var result = await _weighbridgeConfigurationRepository.GetAsync(c => c.isDelete == false &&
                                                                       c.Name == WBConfigView.Name);

                ResponseViewModel<WeighbridgeConfiguration> responseViewModel = new ResponseViewModel<WeighbridgeConfiguration>();
                if (result == null)
                {
                    // Add new
                    var resultAll = await _weighbridgeConfigurationRepository.GetManyAsync(c => c.isDelete == false);
                    if(resultAll != null && resultAll.Count() > 22)
                    {
                        // Not add
                        responseViewModel.errorText = ResponseText.ERR_CONFIG_WB_TIMES_OVER_20;
                    }
                    else if(WBConfigView.Name == null || WBConfigView.Name == "")
                    {
                        // Not add
                        responseViewModel.errorText = ResponseText.ERR_CONFIG_WB_NAME_NULL;
                    }
                    else
                    {
                        // Add new
                        Random rnd = new Random();
                        WBConfigView.isDelete = false;
                        WBConfigView.Code = "WBC" + rnd.Next(1000, 9999);
                        WBConfigView.DefaultConfig = false;
                        _weighbridgeConfigurationRepository.Add(WBConfigView);
                        await this.SaveChangesAsync();

                        result = await _weighbridgeConfigurationRepository.GetAsync(c => c.isDelete == false &&
                                                                       c.Name == WBConfigView.Name);
                        responseViewModel.responseData = result;
                    }
                }
                else
                {
                    if(result.DefaultConfig == true)
                    {
                        // Not Update
                        responseViewModel.errorText = ResponseText.ERR_CONFIG_WB_DONT_CHANGE_DEFAULT;
                    }
                    else
                    {
                        // Update WB
                        if (WBConfigView.ComPort != null)
                        {
                            result.ComPort = WBConfigView.ComPort;
                        }
                        result.BaundRate = WBConfigView.BaundRate;
                        result.DataBits = WBConfigView.DataBits;
                        if (WBConfigView.Parity != null)
                        {
                            result.Parity = WBConfigView.Parity;
                        }
                        if (WBConfigView.StopBits != null)
                        {
                            result.StopBits = WBConfigView.StopBits;
                        }
                        if (WBConfigView.OutputMode != null)
                        {
                            result.OutputMode = WBConfigView.OutputMode;
                        }
                        result.CheckSum = WBConfigView.CheckSum;
                        result.startChar = WBConfigView.startChar;
                        result.NumPrefixChar = WBConfigView.NumPrefixChar;
                        result.DataLength = WBConfigView.DataLength;
                        result.NumWeightChar = WBConfigView.NumWeightChar;
                        result.MinusChar = WBConfigView.MinusChar;
                        result.StableTime = WBConfigView.StableTime;
                        result.OscillatingWeight = WBConfigView.OscillatingWeight;
                        result.MinWeight = WBConfigView.MinWeight;
                        result.MaxWeight = WBConfigView.MaxWeight;
                        _weighbridgeConfigurationRepository.Update(result);
                        await this.SaveChangesAsync();

                        result = await _weighbridgeConfigurationRepository.GetAsync(c => c.isDelete == false &&
                                                                       c.Name == WBConfigView.Name);
                        responseViewModel.responseData = result;
                    }
                }

                return responseViewModel;
            }
            catch
            {
                ResponseViewModel<WeighbridgeConfiguration> responseViewModel = new ResponseViewModel<WeighbridgeConfiguration>();
                responseViewModel.errorText = Common.ResponseText.ERR_EMPTY_DATABASE;
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<GatePassViewModel>> UpdateSealNoNPrintGoodGatepass(GatePassViewModel gatepass)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            GatePass result = await _gatePassRepository.GetAsync(g => g.code.Equals(gatepass.code), QueryIncludes.GATEPASSFULLINCLUDES);
            //if (result.weightRecords.Count() > 0)
            //{
            //    responseViewModel.errorCode = ResponseCode.ERR_WEIGHTED_GATEPASS;
            //    responseViewModel.errorText = ResponseText.ERR_WEIGHTED_GATEPASS;
            //    return responseViewModel;
            //}
            //Update gatepass infor
            result.printGoods = gatepass.printGoods;
            result.sealNo = gatepass.sealNo;

            _gatePassRepository.Update(result);
            await this.SaveChangesAsync();
            responseViewModel.errorCode = ResponseCode.SUCCESS;
            return responseViewModel;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> UpdateGatePassWeightValue(GatePassViewModel gatepass)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            GatePass result = await _gatePassRepository.GetAsync(g => g.code.Equals(gatepass.code), QueryIncludes.GATEPASSFULLINCLUDES);
            //if (result.weightRecords.Count() > 0)
            //{
            //    responseViewModel.errorCode = ResponseCode.ERR_WEIGHTED_GATEPASS;
            //    responseViewModel.errorText = ResponseText.ERR_WEIGHTED_GATEPASS;
            //    return responseViewModel;
            //}
            //Update gatepass infor
            result.tareWeightValue = gatepass.tareWeightValue;
            result.registNetWeight = gatepass.registNetWeight;
            result.registGrossWeight = gatepass.registGrossWeight;

            _gatePassRepository.Update(result);
            await this.SaveChangesAsync();
            responseViewModel.errorCode = ResponseCode.SUCCESS;
            return responseViewModel;
        }
    }
}
