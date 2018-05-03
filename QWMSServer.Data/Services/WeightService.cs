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

        public WeightService(IUnitOfWork unitOfWork, IGatePassRepository gatePassRepository, IQueueListRepository queueListRepository,
                            IWeightRecordRepository weightRecordRepository)
        {
            _unitOfWork = unitOfWork;
            _gatePassRepository = gatePassRepository;
            _queueListRepository = queueListRepository;
            _weightRecordRepository = weightRecordRepository;
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

        public async Task<ResponseViewModel<QueueListViewModel>> CallNextTruck(int gatePassID)
        {
            ResponseViewModel<QueueListViewModel> response = new ResponseViewModel<QueueListViewModel>();
            var gatePass = await _gatePassRepository.GetAsync(gt => gt.ID == gatePassID && gt.isDelete == false);
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
                        var currentQueue = await _queueListRepository.GetAsync(qu => qu.gatePassID == gatePassID && qu.isDelete == false);
                        var queueList = await _queueListRepository.GetManyAsync(qu => qu.queueOrder > currentQueue.queueOrder && qu.queueOrder <= (currentQueue.queueOrder+5) && qu.isDelete == false);
                        // If there are less than 5 - move to the last
                        if(queueList.Count() < 5)
                        {
                            // Set new Order
                            currentQueue.queueOrder += queueList.Count();
                            // Shift up 1 item for orther QueueItem
                            foreach (var queueItem in queueList)
                            {
                                queueItem.queueOrder -= 1;
                                _queueListRepository.Update(queueItem);
                            }
                        }else // If more than 5 - move down 5
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
            var queueList = await _queueListRepository.GetManyAsync(qu => qu.isDelete == false, QueryIncludes.QUEUELISTFULLINCLUDES);
            if (queueList == null)
            {
                response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.ERR_QUE_NO_QUEUE_FOUND, null);
            }else
            {
                response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, 
                                                                                            Mapper.Map<IEnumerable<QueueList>, 
                                                                                            IEnumerable<QueueListViewModel>>(queueList));
            }
            return response;
        }

        public async Task<ResponseViewModel<WeightRecordViewModel>> UpdateWeightValue(WeightDataViewModel weightDataViewModel)
        {
            ResponseViewModel<WeightRecordViewModel> response = new ResponseViewModel<WeightRecordViewModel>();
            WeightRecord weightRecord = new WeightRecord();
            Random r = new Random();
            var weighNum = await _weightRecordRepository.GetManyAsync(wt => wt.gatepassID == weightDataViewModel .gatePassID && wt.isDelete == false);
            weightRecord.code = r.ToString();
            weightRecord.gatepassID = weightDataViewModel.gatePassID;
            weightRecord.isDelete = false;
            weightRecord.weighBridgeID = weightDataViewModel.weighBridgeID;
            weightRecord.weightEmployeeID = weightDataViewModel.employeeID;
            weightRecord.weightValue = weightDataViewModel.weightValue;
            weightRecord.frontCameraCapturePath = Constant.TruckCapturePath + weightDataViewModel.fontCameraName;
            weightRecord.gearCameraCapturePath = Constant.TruckCapturePath + weightDataViewModel.gearCameraName;
            weightRecord.cabinCameraCapturePath = Constant.TruckCapturePath + weightDataViewModel.cabinCameraName;
            weightRecord.containerCameraCapturePath = Constant.TruckCapturePath + weightDataViewModel.containerCameraName;
            weightRecord.weightNo = weighNum.Count() + 1;
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

        public async Task<ResponseViewModel<WeightRecordViewModel>> GetWeightValueByGatePassID(int gatePassID)
        {
            ResponseViewModel<WeightRecordViewModel> response = new ResponseViewModel<WeightRecordViewModel>();
            var weighRecord = await _weightRecordRepository.GetManyAsync(wt => wt.gatepassID == gatePassID && wt.isDelete == false);
            if(weighRecord == null)
            {
                response = ResponseConstructor<WeightRecordViewModel>.ConstructEnumerableData(ResponseCode.ERR_QUE_WEIGH_NO_FOUND, Mapper.Map<IEnumerable<WeightRecord>, IEnumerable<WeightRecordViewModel>>(weighRecord));
            }
            else
            {
                response = ResponseConstructor<WeightRecordViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, Mapper.Map<IEnumerable<WeightRecord>, IEnumerable<WeightRecordViewModel>>(weighRecord));
            }
            return response;
        }
    }
}
