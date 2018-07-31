using System;
using System.Collections.Generic;
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
    public class WarehouseService : IWarehouseService
    {
        private readonly IGatePassRepository _gatePassRepository;
        private readonly ILaneRepository _laneRepository;
        private readonly IQueueListRepository _queueListRepository;
        private readonly IOrderMaterialRepository _orderMaterialRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public WarehouseService(IGatePassRepository gatePassRepository, IUnitOfWork unitOfWork, ILaneRepository laneRepository,
                                IQueueListRepository queueListRepository, IAuthService authService, IOrderMaterialRepository orderMaterialRepository, IOrderRepository orderRepository)
        {
            _gatePassRepository = gatePassRepository;
            _unitOfWork = unitOfWork;
            _laneRepository = laneRepository;
            _queueListRepository = queueListRepository;
            _orderMaterialRepository = orderMaterialRepository;
            _orderRepository = orderRepository;
            _authService = authService;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ResponseViewModel<GatePassViewModel>> UpdateQCWeighValue(WarehouseCheckModel warehouseCheckModel)
        {
            ResponseViewModel<GatePassViewModel> response = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var gatePass = await _gatePassRepository.GetAsync(gt => gt.code == warehouseCheckModel.gatePassCode && gt.isDelete == false);
                if (gatePass == null)
                {
                    response = ResponseConstructor<GatePassViewModel>.ConstructBoolRes(ResponseCode.ERR_QUE_NO_GATEPASS_FOUND, false);
                }
                else
                {
                    gatePass.orders.ToList()[0].QCGrossWeight = warehouseCheckModel.QCGrossWeight;
                    _gatePassRepository.Update(gatePass);
                    if (await _unitOfWork.SaveChangesAsync())
                    {
                        response = ResponseConstructor<GatePassViewModel>.ConstructBoolRes(ResponseCode.SUCCESS, true);
                    }
                    else
                    {
                        response = ResponseConstructor<GatePassViewModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                    }
                }
                return response;
            }
            catch (Exception)
            {
                return response = ResponseConstructor<GatePassViewModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
            }
        }



        public async Task<ResponseViewModel<GatePassViewModel>> UpdateTruckOnWarehouseCheckIn(WarehouseCheckModel warehouseCheckModel)
        {
            ResponseViewModel<GatePassViewModel> response = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var gatePass = await _gatePassRepository.GetAsync(gt => gt.code == warehouseCheckModel.gatePassCode && gt.isDelete == false);
                if (gatePass == null)
                {
                    response.responseData = null;
                    response.errorCode = ResponseCode.ERR_QUE_NO_GATEPASS_FOUND;
                    response.booleanResponse = false;
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
                            response.responseData = Mapper.Map<GatePass, GatePassViewModel>(gatePass);
                            response.errorCode = ResponseCode.SUCCESS;
                            response.booleanResponse = true;
                        }
                        else
                        {
                            response.responseData = null;
                            response.errorCode = ResponseCode.ERR_SEC_UNKNOW;
                            response.booleanResponse = false;
                        }
                    }
                    else
                    {
                        response.responseData = null;
                        response.errorCode = ResponseCode.ERR_QUE_GATEPASS_WRONG_STATE;
                        response.booleanResponse = false;
                    }
                }
                return response;
            }
            catch (Exception)
            {
                response.responseData = null;
                response.errorCode = ResponseCode.ERR_SEC_UNKNOW;
                response.booleanResponse = false;
                return response;
            }
        }

        public async Task<ResponseViewModel<GatePassViewModel>> UpdateTruckOnWarehouseCheckOut(WarehouseCheckModel warehouseCheckModel)
        {
            ResponseViewModel<GatePassViewModel> response = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var gatePass = await _gatePassRepository.GetAsync(gt => gt.code == warehouseCheckModel.gatePassCode && gt.isDelete == false);
                if (gatePass == null)
                {
                    response.responseData = null;
                    response.errorCode = ResponseCode.ERR_QUE_NO_GATEPASS_FOUND;
                    response.booleanResponse = false;
                }
                else
                {
                    if (gatePass.stateID == GatepassState.STATE_FINISH_WAREHOUSE_CHECK_IN)
                    {
                        gatePass.stateID = GatepassState.STATE_FINISH_WAREHOUSE_CHECK_OUT;
                        gatePass.QCGrossWeight = warehouseCheckModel.QCGrossWeight;

                        foreach (var item in warehouseCheckModel.orderMaterialViewModels)
                        {
                            var orderMaterial = await _orderMaterialRepository.GetAsync(o => o.ID == item.ID);
                            orderMaterial.QCGrossWeight = item.QCGrossWeight;
                            orderMaterial.QCQuantity = item.QCQuantity;
                            _orderMaterialRepository.Update(orderMaterial);
                        }
                        await _unitOfWork.SaveChangesAsync();
                        foreach (var item in warehouseCheckModel.orderIDs)
                        {
                            var order = await _orderRepository.GetAsync(o => o.ID == item, QueryIncludes.ORDERFULLINCUDES);
                            order.QCGrossWeight = 0;
                            foreach (var material in order.orderMaterials)
                            {
                                order.QCGrossWeight += material.QCGrossWeight;
                            }
                            _orderRepository.Update(order);
                        }
                        await _unitOfWork.SaveChangesAsync();

                        var queue = await _queueListRepository.GetAsync(qu => qu.gatePassID == gatePass.ID);
                        var lane = await _laneRepository.GetAsync(ln => ln.ID == queue.laneID);
                        lane.usingStatus = LaneStatus.FREE;
                        _laneRepository.Update(lane);
                        _gatePassRepository.Update(gatePass);
                        if (await _unitOfWork.SaveChangesAsync())
                        {
                            response.responseData = Mapper.Map<GatePass, GatePassViewModel>(gatePass);
                            response.errorCode = ResponseCode.SUCCESS;
                            response.booleanResponse = true;
                        }
                        else
                        {
                            response.responseData = null;
                            response.errorCode = ResponseCode.ERR_SEC_UNKNOW;
                            response.booleanResponse = false;
                        }
                    }
                    else
                    {
                        response.responseData = null;
                        response.errorCode = ResponseCode.ERR_QUE_GATEPASS_WRONG_STATE;
                        response.booleanResponse = false;
                    }
                }
                return response;
            }
            catch (Exception)
            {
                response.responseData = null;
                response.errorCode = ResponseCode.ERR_SEC_UNKNOW;
                response.booleanResponse = false;
                return response;
            }
        }

        public async Task<ResponseViewModel<GatePassViewModel>> UpdateTruckOnWarehouseCheck(WarehouseCheckModel warehouseCheckModel)
        {
            ResponseViewModel<GatePassViewModel> response = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var gatePass = await _gatePassRepository.GetAsync(gt => gt.code == warehouseCheckModel.gatePassCode && gt.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                if (gatePass == null)
                {
                    response.responseData = null;
                    response.errorCode = ResponseCode.ERR_SEC_UNKNOW;
                    response.booleanResponse = false;
                    return response;
                }
                else
                {
                    switch (gatePass.stateID)
                    {
                        case GatepassState.STATE_FINISH_WEIGHT_IN:
                            return await this.UpdateTruckOnWarehouseCheckIn(warehouseCheckModel);
                        case GatepassState.STATE_FINISH_WAREHOUSE_CHECK_IN:
                            return await this.UpdateTruckOnWarehouseCheckOut(warehouseCheckModel);
                        default:
                            response.responseData = null;
                            response.errorCode = ResponseCode.ERR_SEC_UNKNOW;
                            response.booleanResponse = false;
                            return response;
                    }
                }
            }
            catch (Exception)
            {
                response.responseData = null;
                response.errorCode = ResponseCode.ERR_SEC_UNKNOW;
                response.booleanResponse = false;
                return response;
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
                laneMgntViewModel.loadingBay = lane.loadingBay.nameVi;
                laneMgntViewModel.ID = lane.ID;
                laneMgntViewModel.LaneName = lane.nameVi;
                if (lane.usingStatus == LaneStatus.OCCUPIED)
                    laneMgntViewModel.usingStatus = "Ocuppied";
                else
                    laneMgntViewModel.usingStatus = "Free";
                laneMgntViewModel.status = lane.status;
                laneMgntViewModel.truckType = lane.truckType.description;
                var queue = await _queueListRepository.GetAsync( qu => qu.laneID == lane.ID && qu.gatePass.isDelete == false && qu.gatePass.stateID == GatepassState.STATE_FINISH_WAREHOUSE_CHECK_IN, QueryIncludes.QUEUELISTFULLINCLUDES);
                if(queue != null)
                {
                    var gatePass = await _gatePassRepository.GetAsync( gt => gt.code == queue.gatePass.code, QueryIncludes.GATEPASSFULLINCLUDES); // await _gatePassRepository.GetAsync(gt => gt.queueLists.ToArray()[0].laneID == lane.ID && gt.isDelete == false);// && gt.stateID == GatepassState.STATE_FINISH_WAREHOUSE_CHECK_IN);
                    var start = (DateTime)gatePass.enterTime;
                    var end = (DateTime)gatePass.leaveTime;
                    end = end.AddMonths(1);
                    var now = DateTime.Now.Ticks;
                    var percent = ((now - start.Ticks) / (end.Ticks - start.Ticks)) * 100;
                    if (percent < (1 / 4))
                        laneMgntViewModel.progress = 10;
                    else if (percent > (1 / 4) && percent < (1 / 2))
                        laneMgntViewModel.progress = 50;
                    else if (percent > (1 / 2) && percent < 1)
                        laneMgntViewModel.progress = 75;
                    else
                        laneMgntViewModel.progress = 75;
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
