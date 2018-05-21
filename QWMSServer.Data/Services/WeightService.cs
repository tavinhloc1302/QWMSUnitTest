using System;
using System.Collections.Generic;
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
        private readonly ILaneRepository _laneRepository;
        private readonly ICommonService _commonService;
		private readonly IAuthService _authService;
        private readonly IQueueService _queueService;

        public WeightService(IUnitOfWork unitOfWork, IGatePassRepository gatePassRepository, IQueueListRepository queueListRepository,
                            IWeightRecordRepository weightRecordRepository, IEmployeeRepository employeeRepository, IWeighBridgeRepository weighBridgeRepository,
                            ILaneRepository laneRepository, ICommonService commonService,
							IAuthService authService, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _gatePassRepository = gatePassRepository;
            _queueListRepository = queueListRepository;
            _weightRecordRepository = weightRecordRepository;
            _employeeRepository = employeeRepository;
            _weighBridgeRepository = weighBridgeRepository;
            _laneRepository = laneRepository;
            _commonService = commonService;
			_authService = authService;
            _queueService = queueService;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        public ResponseViewModel<WeightRecordViewModel> AddTruckCamera(string fileName, byte[] fileContent)
        {
            ResponseViewModel<WeightRecordViewModel> response = new ResponseViewModel<WeightRecordViewModel>();
            string filePath = Constant.DriverCapturePath + fileName;
            // Write to file
            if (fileName == null || fileContent == null)
            {
                response = ResponseConstructor<WeightRecordViewModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
            }
            else
            {
                File.WriteAllBytes(filePath, fileContent);
                response = ResponseConstructor<WeightRecordViewModel>.ConstructBoolRes(ResponseCode.SUCCESS, true);
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
                //if (await _authService.CheckUserPermission(weightDataViewModel.employeeID, weightDataViewModel.employeeRFID, "UpdateWeightValue"))
                //{
                var weighNum = await _weightRecordRepository.GetManyAsync(wt => wt.gatepassID == weightDataViewModel.gatePassID && wt.isDelete == false);
                weightRecord.code = r.Next().ToString();
                weightRecord.gatepassID = weightDataViewModel.gatePassID;
                weightRecord.isDelete = false;
                weightRecord.weighBridgeID = weightDataViewModel.weighBridgeID;
                weightRecord.weighBridge = await _weighBridgeRepository.GetByIdAsync(weightDataViewModel.weighBridgeID);
                weightRecord.weightEmployeeID = weightDataViewModel.employeeID;
                weightRecord.employee = await _employeeRepository.GetByIdAsync(weightDataViewModel.employeeID);
                weightRecord.weightTime = DateTime.Now;
                weightRecord.weightValue = weightDataViewModel.weightValue;
                weightRecord.frontCameraCapturePath = Constant.TruckCapturePath + weightDataViewModel.fontCameraName;
                weightRecord.gearCameraCapturePath = Constant.TruckCapturePath + weightDataViewModel.gearCameraName;
                weightRecord.cabinCameraCapturePath = Constant.TruckCapturePath + weightDataViewModel.cabinCameraName;
                weightRecord.containerCameraCapturePath = Constant.TruckCapturePath + weightDataViewModel.containerCameraName;
                weightRecord.weightNo = weighNum.Count() + 1;
                var gatePass = await _gatePassRepository.GetAsync(gt => gt.ID == weightDataViewModel.gatePassID && gt.isDelete == false);
                if(gatePass.stateID == GatepassState.STATE_FINISH_SECURITY_CHECK_IN)
                    gatePass.stateID = GatepassState.STATE_FINISH_WEIGHT_IN;
                else if(gatePass.stateID == GatepassState.STATE_FINISH_WAREHOUSE_CHECK_OUT)
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
                //}
                //else
                //{
                //    response = ResponseConstructor<WeightRecordViewModel>.ConstructBoolRes(ResponseCode.ERR_WEI_WEIGH_NOT_PERMITTED, false);
                //}
                return response;
            }
            catch (Exception)
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
    }
}
