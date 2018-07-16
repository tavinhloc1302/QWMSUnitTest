using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Data.Common;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Model.ViewModels;

namespace QWMSServer.Data.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IGatePassRepository _gatePassRepository;
        private readonly ILaneRepository _laneRepository;
        private readonly IQueueListRepository _queueListRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public WarehouseService(IGatePassRepository gatePassRepository, IUnitOfWork unitOfWork, ILaneRepository laneRepository,
                                IQueueListRepository queueListRepository, IAuthService authService)
        {
            _gatePassRepository = gatePassRepository;
            _unitOfWork = unitOfWork;
            _laneRepository = laneRepository;
            _queueListRepository = queueListRepository;
            _authService = authService;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ResponseViewModel<GenericResponseModel>> UpdateQCWeighValue(QCWeighValueModel QCWeighValueModel)
        {
            ResponseViewModel<GenericResponseModel> response = new ResponseViewModel<GenericResponseModel>();
            try
            {
                var gatePass = await _gatePassRepository.GetAsync(gt => gt.ID == QCWeighValueModel.gatePassID && gt.isDelete == false);
                if (gatePass == null)
                {
                    response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_QUE_NO_GATEPASS_FOUND, false);
                }
                else
                {
                    gatePass.orders.ToList()[0].QCWeightValue = QCWeighValueModel.QCWeighValue;
                    _gatePassRepository.Update(gatePass);
                    if (await _unitOfWork.SaveChangesAsync())
                    {
                        response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.SUCCESS, true);
                    }
                    else
                    {
                        response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                    }
                }
                return response;
            }
            catch (Exception)
            {
                return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
            }
        }



        public async Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheckIn(QCWeighValueModel QCWeighValueModel)
        {
            ResponseViewModel<GenericResponseModel> response = new ResponseViewModel<GenericResponseModel>();
            try
            {
                var gatePass = await _gatePassRepository.GetAsync(gt => gt.ID == QCWeighValueModel.gatePassID && gt.isDelete == false);
                if (gatePass == null)
                {
                    response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_QUE_NO_GATEPASS_FOUND, false);
                }
                else
                {
                    if (gatePass.stateID == GatepassState.STATE_FINISH_WEIGHT_IN)
                    {
                        gatePass.stateID = GatepassState.STATE_FINISH_WAREHOUSE_CHECK_IN;
                        var queue = await _queueListRepository.GetAsync(qu => qu.gatePassID == gatePass.ID);
                        var lane = await _laneRepository.GetAsync(ln => ln.ID == queue.laneID);
                        lane.usingStatus = LaneStatus.OCCUPIED;
                        _laneRepository.Update(lane);
                        _gatePassRepository.Update(gatePass);
                        if (await _unitOfWork.SaveChangesAsync())
                        {
                            response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.SUCCESS, true);
                        }
                        else
                        {
                            response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                        }
                    }
                    else
                    {
                        response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_QUE_GATEPASS_WRONG_STATE, false);
                    }
                }
            return response;
            }
            catch (Exception)
            {
                return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
            }
        }

        public async Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheckOut(QCWeighValueModel QCWeighValueModel)
        {
            ResponseViewModel<GenericResponseModel> response = new ResponseViewModel<GenericResponseModel>();
            try
            {
                var gatePass = await _gatePassRepository.GetAsync(gt => gt.ID == QCWeighValueModel.gatePassID && gt.isDelete == false);
                if (gatePass == null)
                {
                    response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_QUE_NO_GATEPASS_FOUND, false);
                }
                else
                {
                    if (gatePass.stateID == GatepassState.STATE_FINISH_WAREHOUSE_CHECK_IN)
                    {
                        gatePass.stateID = GatepassState.STATE_FINISH_WAREHOUSE_CHECK_OUT;
                        var queue = await _queueListRepository.GetAsync(qu => qu.gatePassID == gatePass.ID);
                        var lane = await _laneRepository.GetAsync(ln => ln.ID == queue.laneID);
                        lane.usingStatus = LaneStatus.FREE;
                        _laneRepository.Update(lane);
                        _gatePassRepository.Update(gatePass);
                        if (await _unitOfWork.SaveChangesAsync())
                        {
                            response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.SUCCESS, true);
                        }
                        else
                        {
                            response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                        }
                    }
                    else
                    {
                        response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_QUE_GATEPASS_WRONG_STATE, false);
                    }
                }
                return response;
            }
            catch (Exception)
            {
                return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
            }
        }

        public async Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheck(QCWeighValueModel QCWeighValueModel)
        {
            ResponseViewModel<GenericResponseModel> response = new ResponseViewModel<GenericResponseModel>();
            try
            {
                var gatePass = await _gatePassRepository.GetAsync(gt => gt.ID == QCWeighValueModel.gatePassID && gt.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                if (gatePass == null)
                {
                    return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                }
                else
                {
                    switch (gatePass.stateID)
                    {
                        case GatepassState.STATE_FINISH_WEIGHT_IN:
                            return await this.UpdateTruckOnWarehouseCheckIn(QCWeighValueModel);
                        case GatepassState.STATE_FINISH_WAREHOUSE_CHECK_IN:
                            return await this.UpdateTruckOnWarehouseCheckOut(QCWeighValueModel);
                        default:
                            return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                    }
                }
            }
            catch (Exception)
            {
                return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
            }

        }

        public async Task<ResponseViewModel<LaneMgntViewModel>> GetLaneForWarehouseManagement(string code)
        {
            ResponseViewModel<LaneMgntViewModel> responseViewModel = new ResponseViewModel<LaneMgntViewModel>();
            List<LaneMgntViewModel> laneMgntViewModels = new List<LaneMgntViewModel>();
            var lanes = await _laneRepository.GetManyAsync(ln => ln.loadingBay.code.Equals(code) && ln.isDelete == false, QueryIncludes.LANEFULLINCLUDES);
            foreach (var lane in lanes)
            {
                LaneMgntViewModel laneMgntViewModel = new LaneMgntViewModel();
                laneMgntViewModel.ID = lane.ID;
                laneMgntViewModel.LaneName = lane.nameVi;
                if (lane.usingStatus == LaneStatus.OCCUPIED)
                    laneMgntViewModel.usingStatus = "Ocuppied";
                else
                    laneMgntViewModel.usingStatus = "Free";
                laneMgntViewModel.status = lane.status;
                laneMgntViewModel.truckType = lane.truckType.description;
                var queue = await _queueListRepository.GetAsync( qu => qu.laneID == lane.ID && qu.gatePass.isDelete == false && qu.gatePass.stateID == GatepassState.STATE_IN_WAREHOUSE_CHECK_IN, QueryIncludes.QUEUELISTFULLINCLUDES);
                if(queue != null)
                {
                    var gatePass = await _gatePassRepository.GetAsync( gt => gt.ID == queue.gatePass.ID, QueryIncludes.GATEPASSFULLINCLUDES); // await _gatePassRepository.GetAsync(gt => gt.queueLists.ToArray()[0].laneID == lane.ID && gt.isDelete == false);// && gt.stateID == GatepassState.STATE_FINISH_WAREHOUSE_CHECK_IN);
                    var start = (DateTime)gatePass.enterTime;
                    var end = (DateTime)gatePass.leaveTime;
                    end = end.AddMonths(1);
                    var now = DateTime.Now.Ticks;
                    var percent = ((now - start.Ticks) / (end.Ticks - start.Ticks)) * 100;
                    if (percent < (1 / 4))
                        laneMgntViewModel.progress = "0 %";
                    else if (percent > (1 / 4) && percent < (1 / 2))
                        laneMgntViewModel.progress = "50 %";
                    else if (percent > (1 / 2) && percent < 1)
                        laneMgntViewModel.progress = "75 %";
                    else
                        laneMgntViewModel.progress = "75 %";
                    laneMgntViewModel.KPI = gatePass.truck.KPI;
                    laneMgntViewModel.inTime = (DateTime)gatePass.enterTime;
                    laneMgntViewModel.outTime = (DateTime)gatePass.leaveTime;
                    laneMgntViewModel.plateNumber = gatePass.truck.plateNumber;
                }
                laneMgntViewModels.Add(laneMgntViewModel);
            }
            responseViewModel = ResponseConstructor<LaneMgntViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, laneMgntViewModels);
            return responseViewModel;
        }
    }
}
