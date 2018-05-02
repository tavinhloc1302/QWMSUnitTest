﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Model.ViewModels;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Data.Common;
using AutoMapper;

namespace QWMSServer.Data.Services
{
    public class SecurityServices : ISecurityServicecs
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueListRepository _queueListRepository;
        private readonly IGatePassRepository _gatePassRepository;
        private readonly IStateRecordRepository _stateRecordRepository;
        private readonly IRFIDCardRepository _rfidCardRepository;
        private readonly IStateRepository _stateRepository;
        private Random _random = new Random();

        public SecurityServices(IUnitOfWork unitOfWork, IQueueListRepository queueListRepository, 
                                                        IGatePassRepository gatePassRepository,
                                                        IStateRecordRepository stateRecordRepository,
                                                        IRFIDCardRepository rfidCardRepository,
                                                        IStateRepository stateRepository)
        {
            _unitOfWork = unitOfWork;
            _queueListRepository = queueListRepository;
            _gatePassRepository = gatePassRepository;
            _stateRecordRepository = stateRecordRepository;
            _rfidCardRepository = rfidCardRepository;
            _stateRepository = stateRepository;
        }

        public async Task<ResponseViewModel<QueueListViewModel>> GetTrucks(string truckCondition)
        {
            ResponseViewModel<QueueListViewModel> response;
            ResponseConstructor<QueueListViewModel> responseConstructor;
            IEnumerable<QueueList> queryResult;
            IEnumerable<QueueListViewModel> truckOnQueueViewModel;

            responseConstructor = new ResponseConstructor<QueueListViewModel>();
            // Check connection to database
            if (_unitOfWork.Exists() == false)
            {
                response = responseConstructor.ConstructEnumerableData(ResponseCode.ERR_DB_CONNECTION_FAILED, null);
            }
            else
            {
                switch(truckCondition)
                {
                    case TRUCK_CONDITION_ALL:
                        queryResult = await _queueListRepository.GetManyAsync(c =>
                                   c.isDelete == false, QueryIncludes.SECURITY_QUEUE_INCLUDES);
                        truckOnQueueViewModel = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queryResult);

                        response = responseConstructor.ConstructEnumerableData(ResponseCode.SUCCESS, truckOnQueueViewModel);
                        break;
                    case TRUCK_CONDITION_CALLING:
                        queryResult = await _queueListRepository.GetManyAsync(c =>
                                   c.isDelete == false &&
                                   ( c.gatePass.state.code == GatepassState.STATE_CALLING_1 ||
                                     c.gatePass.state.code == GatepassState.STATE_CALLING_2 ||
                                     c.gatePass.state.code == GatepassState.STATE_CALLING_3), QueryIncludes.SECURITY_QUEUE_INCLUDES);
                        truckOnQueueViewModel = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queryResult);

                        response = responseConstructor.ConstructEnumerableData(ResponseCode.SUCCESS, truckOnQueueViewModel);
                        break;

                    case TRUCK_CONDITION_WAITING_CALL:
                        queryResult = await _queueListRepository.GetManyAsync( c => 
                                    c.isDelete == false &&
                                    ( c.gatePass.state.code == GatepassState.STATE_CALLING_1 ||
                                      c.gatePass.state.code == GatepassState.STATE_CALLING_2 ||
                                      c.gatePass.state.code == GatepassState.STATE_CALLING_3 ||
                                      c.gatePass.state.code == GatepassState.STATE_REGISTERED ), QueryIncludes.SECURITY_QUEUE_INCLUDES);
                        truckOnQueueViewModel = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queryResult);

                        response = responseConstructor.ConstructEnumerableData(ResponseCode.SUCCESS, truckOnQueueViewModel);
                        break;
                    case TRUCK_CONDITION_1XXX_WAITING_CALL:
                        queryResult = await _queueListRepository.GetManyAsync(c =>
                                   c.isDelete == false &&
                                   ( c.gatePass.state.code == GatepassState.STATE_CALLING_1 ||
                                     c.gatePass.state.code == GatepassState.STATE_CALLING_2 ||
                                     c.gatePass.state.code == GatepassState.STATE_CALLING_3 ||
                                     c.gatePass.state.code == GatepassState.STATE_REGISTERED ) &&
                                   c.gatePass.truckGroup.Code == TruckGroups.GROUP_1XXX, QueryIncludes.SECURITY_QUEUE_INCLUDES);
                        truckOnQueueViewModel = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queryResult);

                        response = responseConstructor.ConstructEnumerableData(ResponseCode.SUCCESS, truckOnQueueViewModel);
                        break;
                    case TRUCK_CONDITION_2XXX_WAITING_CALL:
                        queryResult = await _queueListRepository.GetManyAsync(c =>
                                   c.isDelete == false &&
                                   ( c.gatePass.state.code == GatepassState.STATE_CALLING_1 ||
                                     c.gatePass.state.code == GatepassState.STATE_CALLING_2 ||
                                     c.gatePass.state.code == GatepassState.STATE_CALLING_3 ||
                                     c.gatePass.state.code == GatepassState.STATE_REGISTERED ) &&
                                   c.gatePass.truckGroup.Code == TruckGroups.GROUP_2XXX, QueryIncludes.SECURITY_QUEUE_INCLUDES);
                        truckOnQueueViewModel = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queryResult);

                        response = responseConstructor.ConstructEnumerableData(ResponseCode.SUCCESS, truckOnQueueViewModel);
                        break;
                    case TRUCK_CONDITION_3XXX_WAITING_CALL:
                        queryResult = await _queueListRepository.GetManyAsync(c =>
                                   c.isDelete == false &&
                                   ( c.gatePass.state.code == GatepassState.STATE_CALLING_1 ||
                                     c.gatePass.state.code == GatepassState.STATE_CALLING_2 ||
                                     c.gatePass.state.code == GatepassState.STATE_CALLING_3 ||
                                     c.gatePass.state.code == GatepassState.STATE_REGISTERED ) &&
                                   c.gatePass.truckGroup.Code == TruckGroups.GROUP_3XXX, QueryIncludes.SECURITY_QUEUE_INCLUDES);
                        truckOnQueueViewModel = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queryResult);

                        response = responseConstructor.ConstructEnumerableData(ResponseCode.SUCCESS, truckOnQueueViewModel);
                        break;
                    default:
                        response = responseConstructor.ConstructEnumerableData(ResponseCode.ERR_SEC_NOT_SUPPORT_CONDITION, null);
                        break;
                }
            }

            return response;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByRFID(string rfidCode)
        {
            ResponseViewModel<GatePassViewModel> response;
            ResponseConstructor<GatePassViewModel> responseConstructor;
            GatePass queryGatePassResult;
            GatePassViewModel gatePassViewModel;
            RFIDCard queryRFIDResult;
            responseConstructor = new ResponseConstructor<GatePassViewModel>();
            if (_unitOfWork.Exists() == false)
            {
                response = responseConstructor.ConstructData(ResponseCode.ERR_DB_CONNECTION_FAILED, null);
            }
            else
            {
                // Find RFID ID
                queryRFIDResult = await _rfidCardRepository.GetAsync(r => r.isDelete == false && r.code == rfidCode);
                if(queryRFIDResult == null)
                {
                    // Not found RFID tag on Database
                    response = responseConstructor.ConstructData(ResponseCode.ERR_SEC_NOT_FOUND_RFID, null);
                }
                else
                {
                    // Query Gatepass by RFID tag
                    queryGatePassResult = await _gatePassRepository.GetAsync(
                                            g => g.isDelete == false &&
                                            g.RFIDCardID == queryRFIDResult.ID, QueryIncludes.SECURITY_GATEPASS_INCLUDES);

                    if (queryGatePassResult == null)
                    {
                        // Not Found Gatepass matched with RFID No
                        response = responseConstructor.ConstructData(ResponseCode.ERR_SEC_NOT_FOUND_GATEPASS, null);
                    }
                    else
                    {
                        // Found the GatePass
                        gatePassViewModel = Mapper.Map<GatePass, GatePassViewModel>(queryGatePassResult);
                        response = responseConstructor.ConstructData(ResponseCode.SUCCESS, gatePassViewModel);
                    }
                }
            }

            return response;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> RegisterSecurityCheck(string rfidCode)
        {
            ResponseViewModel<GatePassViewModel> response;
            ResponseConstructor<GatePassViewModel> responseConstructor;
            GatePass queryGatePassResult;
            GatePassViewModel GatePassViewModel;
            RFIDCard queryRfidResult;

            responseConstructor = new ResponseConstructor<GatePassViewModel>();
            int tmpResponseCode;

            if (_unitOfWork.Exists() == false)
            {
                // Cound Not Connect to Database
                response = responseConstructor.ConstructData(ResponseCode.ERR_DB_CONNECTION_FAILED, null);
            }
            else
            {
                // Find RFID
                queryRfidResult = await _rfidCardRepository.GetAsync(r => r.isDelete == false && r.code == rfidCode);
                if(queryRfidResult == null)
                {
                    // Not found RFID card ID
                    response = responseConstructor.ConstructData(ResponseCode.ERR_SEC_NOT_FOUND_RFID, null);
                }
                else
                {
                    // Find GatePass
                    queryGatePassResult = await _gatePassRepository.GetAsync(
                                        g => g.isDelete == false && 
                                        g.RFIDCardID == queryRfidResult.ID , QueryIncludes.SECURITY_GATEPASS_INCLUDES);
                    if (queryGatePassResult == null)
                    {
                        // Not Found Gatepass matched with RFID No
                        response = responseConstructor.ConstructData(ResponseCode.ERR_SEC_NOT_FOUND_GATEPASS, null);
                    }
                    else if (queryGatePassResult.state == null)
                    {
                        // GatePass lacked "stateID" property
                        response = responseConstructor.ConstructData(ResponseCode.ERR_SEC_GATEPASS_LACK_STATEID, null);
                    }
                    else
                    {
                        // Check Truck state and Update its
                        switch (queryGatePassResult.state.code)
                        {
                            case GatepassState.STATE_CALLING_1:
                            case GatepassState.STATE_CALLING_2:
                            case GatepassState.STATE_CALLING_3:
                                tmpResponseCode = ResponseCode.SUCCESS;
                                break;
                            case GatepassState.STATE_WEIGHT_OUT:
                                tmpResponseCode = ResponseCode.SUCCESS;
                                break;
                            default:
                                tmpResponseCode = ResponseCode.ERR_SEC_NOT_PERMITTED_REG;
                                break;
                        }

                        GatePassViewModel = Mapper.Map<GatePass, GatePassViewModel>(queryGatePassResult);

                        // Return gatepass/truck
                        response = responseConstructor.ConstructData(tmpResponseCode, GatePassViewModel);
                    }
                }
            }

            return response;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> ConfirmSecurityCheck(GatePassViewModel gatePassView)
        {
            ResponseViewModel<GatePassViewModel> response;
            ResponseConstructor<GatePassViewModel> responseContructor;
            GatePass queryGatePassResult;
            GatePassViewModel gatePassViewModel;
            State queryStateResult;
            int tmpResponseCode;
            responseContructor = new ResponseConstructor<GatePassViewModel>();
            
            if (_unitOfWork.Exists() == false)
            {
                // Cound Not Connect to Database
                response = responseContructor.ConstructData(ResponseCode.ERR_DB_CONNECTION_FAILED, null);
            }
            else if(gatePassView == null)
            {
                // Wrong request format
                response = responseContructor.ConstructData(ResponseCode.ERR_SEC_WRONG_BODY_REQUEST_FORMAT, null);
            }
            else
            {
                // Get Queue by gatePassCode from database
                queryGatePassResult = await _gatePassRepository.GetAsync(q => 
                                      q.isDelete == false &&
                                      q.code == gatePassView.code, QueryIncludes.SECURITY_GATEPASS_INCLUDES);
                // @TODO: Get permission of confirm RFID
                //
                bool isConfirmPermited = true;

                if (queryGatePassResult == null)
                {
                    // Not found GatePass
                    response = responseContructor.ConstructData(ResponseCode.ERR_SEC_NOT_FOUND_GATEPASS, null);
                }
                else if (queryGatePassResult.state == null)
                {
                    // GatePass lacked "stateID" property
                    response = responseContructor.ConstructData(ResponseCode.ERR_SEC_GATEPASS_LACK_STATEID, null);
                }
                else
                {
                    // Found the GatePass and State
                    if (isConfirmPermited == false)
                    {
                        // Not Permit
                        tmpResponseCode = ResponseCode.ERR_SEC_WRONG_CONFIRMED_RFID;
                    }
                    else
                    {
                        // Check requested updated state
                        switch (queryGatePassResult.state.code)
                        {
                            case GatepassState.STATE_CALLING_1:
                            case GatepassState.STATE_CALLING_2:
                            case GatepassState.STATE_CALLING_3:
                                // Get state ID of "Finish security check-in"
                                queryStateResult = await _stateRepository.GetAsync(s => s.code == GatepassState.STATE_SECURITY_CHECK_IN);
                                // Updat gatepass/state
                                queryGatePassResult.stateID = queryStateResult.ID;
                                queryGatePassResult.enterTime = DateTime.Now;
                                tmpResponseCode = ResponseCode.SUCCESS;
                                break;
                            case GatepassState.STATE_INTERNAL_WAREHOUSE_CHECK_OUT:
                                // Get state ID of "Finish security check-out"
                                queryStateResult = await _stateRepository.GetAsync(s => s.code == GatepassState.STATE_SECURITY_CHECK_OUT);
                                // Updat gatepass/state
                                queryGatePassResult.stateID = queryStateResult.ID;
                                queryGatePassResult.enterTime = DateTime.Now;
                                tmpResponseCode = ResponseCode.SUCCESS;
                                break;
                            default:
                                // NOT SUPPORT STATE
                                tmpResponseCode = ResponseCode.ERR_SEC_NOT_PERMIT_PASS_SECURITY_GATE;
                                break;
                        }
                    }

                    // Save on database
                    _unitOfWork.SaveChanges();

                    // @TODO: Update state record

                    // Re-query after changing
                    queryGatePassResult = await _gatePassRepository.GetAsync(q => q.code == gatePassView.code, QueryIncludes.SECURITY_GATEPASS_INCLUDES);
                    gatePassViewModel = Mapper.Map<GatePass, GatePassViewModel>(queryGatePassResult);
                    
                    // Return gatepass/truck
                    response = responseContructor.ConstructData(tmpResponseCode, gatePassViewModel);
                }
            }
            return response;
        }

        // Define constant
        public const string TRUCK_CONDITION_CALLING = "Calling";
        public const string TRUCK_CONDITION_WAITING_CALL = "WaittingCall";
        public const string TRUCK_CONDITION_1XXX_WAITING_CALL = "1xxxWaittingCall";
        public const string TRUCK_CONDITION_2XXX_WAITING_CALL = "2xxxWaittingCall";
        public const string TRUCK_CONDITION_3XXX_WAITING_CALL = "3xxxWaittingCall";
        public const string TRUCK_CONDITION_ALL = "All";

        public const int CONFIRM_TYPPE_OK = 0;
        public const int CONFIRM_TYPE_REJECT = 1;
    }
}
