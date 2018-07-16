using System;
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
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;

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
            IEnumerable<QueueList> queryResult;
            IEnumerable<QueueListViewModel> truckOnQueueViewModel;

            // Check connection to database
            if (_unitOfWork.Exists() == false)
            {
                response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.ERR_DB_CONNECTION_FAILED, null);
            }
            else
            {
                switch(truckCondition)
                {
                    case TRUCK_CONDITION_ALL:
                        queryResult = (await _queueListRepository.GetManyAsync(c => c.isDelete == false, 
                                                QueryIncludes.SECURITY_QUEUE_INCLUDES)
                                      ).ToArray().OrderBy(q => q.queueOrder).ToList();

                        truckOnQueueViewModel = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queryResult);
                        response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, truckOnQueueViewModel);
                        break;
                    case TRUCK_CONDITION_CALLING:
                        queryResult = (await _queueListRepository.GetManyAsync(c =>
                                                c.isDelete == false &&
                                                (   c.gatePass.state.ID == GatepassState.STATE_CALLING_1 ||
                                                    c.gatePass.state.ID == GatepassState.STATE_CALLING_2 ||
                                                    c.gatePass.state.ID == GatepassState.STATE_CALLING_3
                                                ),
                                                QueryIncludes.SECURITY_QUEUE_INCLUDES)
                                      ).ToArray().OrderBy(q => q.queueOrder).ToList();

                        truckOnQueueViewModel = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queryResult);
                        response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, truckOnQueueViewModel);
                        break;

                    case TRUCK_CONDITION_WAITING_CALL:
                        queryResult = (await _queueListRepository.GetManyAsync( c => 
                                                c.isDelete == false &&
                                                (
                                                    c.gatePass.state.ID == GatepassState.STATE_REGISTERED 
                                                ), QueryIncludes.SECURITY_QUEUE_INCLUDES)
                                        ).ToArray().OrderBy(q => q.queueOrder).ToList();

                        truckOnQueueViewModel = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queryResult);
                        response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, truckOnQueueViewModel);
                        break;
                    case TRUCK_CONDITION_1XXX_WAITING_CALL:
                        queryResult = ( await _queueListRepository.GetManyAsync(c =>
                                                c.isDelete == false &&
                                                (
                                                    c.gatePass.state.ID == GatepassState.STATE_REGISTERED
                                                ) &&
                                                c.gatePass.truckGroup.Code == TruckGroups.GROUP_1XXX, 
                                                QueryIncludes.SECURITY_QUEUE_INCLUDES)
                                        ).ToArray().OrderBy(q => q.queueOrder).ToList();

                        truckOnQueueViewModel = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queryResult);
                        response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, truckOnQueueViewModel);
                        break;
                    case TRUCK_CONDITION_2XXX_WAITING_CALL:
                        queryResult = (await _queueListRepository.GetManyAsync(c =>
                                                c.isDelete == false &&
                                                (
                                                    c.gatePass.state.ID == GatepassState.STATE_REGISTERED 
                                                ) &&
                                                c.gatePass.truckGroup.Code == TruckGroups.GROUP_2XXX, 
                                                QueryIncludes.SECURITY_QUEUE_INCLUDES)
                                        ).ToArray().OrderBy(q => q.queueOrder).ToList();

                        truckOnQueueViewModel = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queryResult);
                        response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, truckOnQueueViewModel);
                        break;
                    case TRUCK_CONDITION_3XXX_WAITING_CALL:
                        queryResult = ( await _queueListRepository.GetManyAsync(c =>
                                                c.isDelete == false &&
                                                (
                                                    c.gatePass.state.ID == GatepassState.STATE_REGISTERED
                                                ) &&
                                                c.gatePass.truckGroup.Code == TruckGroups.GROUP_3XXX, 
                                                QueryIncludes.SECURITY_QUEUE_INCLUDES)
                                        ).ToArray().OrderBy(q => q.queueOrder).ToList();

                        truckOnQueueViewModel = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queryResult);
                        response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, truckOnQueueViewModel);
                        break;
                    default:
                        response = ResponseConstructor<QueueListViewModel>.ConstructEnumerableData(ResponseCode.ERR_SEC_NOT_SUPPORT_CONDITION, null);
                        break;
                }
            }

            return response;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByRFID(string rfidCode)
        {
            ResponseViewModel<GatePassViewModel> response;
            GatePass queryGatePassResult;
            GatePassViewModel gatePassViewModel;
            RFIDCard queryRFIDResult;
            if (_unitOfWork.Exists() == false)
            {
                response = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_DB_CONNECTION_FAILED, null);
            }
            else
            {
                // Find RFID ID
                queryRFIDResult = await _rfidCardRepository.GetAsync(r => r.isDelete == false && r.code == rfidCode);
                if(queryRFIDResult == null)
                {
                    // Not found RFID tag on Database
                    response = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_NOT_FOUND_RFID, null);
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
                        response = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_NOT_FOUND_GATEPASS, null);
                    }
                    else
                    {
                        // Found the GatePass
                        gatePassViewModel = Mapper.Map<GatePass, GatePassViewModel>(queryGatePassResult);
                        response = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.SUCCESS, gatePassViewModel);
                    }
                }
            }

            return response;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> RegisterSecurityCheck(string rfidCode)
        {
            ResponseViewModel<GatePassViewModel> response;
            GatePass queryGatePassResult;
            GatePassViewModel GatePassViewModel;
            RFIDCard queryRfidResult;

            int tmpResponseCode;

            if (_unitOfWork.Exists() == false)
            {
                // Cound Not Connect to Database
                response = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_DB_CONNECTION_FAILED, null);
            }
            else
            {
                // Find RFID
                queryRfidResult = await _rfidCardRepository.GetAsync(r => r.isDelete == false && r.code == rfidCode);
                if(queryRfidResult == null)
                {
                    // Not found RFID card ID
                    response = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_NOT_FOUND_RFID, null);
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
                        response = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_NOT_FOUND_GATEPASS, null);
                    }
                    else if (queryGatePassResult.state == null)
                    {
                        // GatePass lacked "stateID" property
                        response = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_GATEPASS_LACK_STATEID, null);
                    }
                    else
                    {
                        // Check Truck state and Update its
                        switch (queryGatePassResult.state.ID)
                        {
                            case GatepassState.STATE_CALLING_1:
                            case GatepassState.STATE_CALLING_2:
                            case GatepassState.STATE_CALLING_3:
                                tmpResponseCode = ResponseCode.SUCCESS;
                                break;
                            case GatepassState.STATE_FINISH_WEIGHT_OUT: //STATE_WEIGHT_OUT:
                                tmpResponseCode = ResponseCode.SUCCESS;
                                break;
                            default:
                                tmpResponseCode = ResponseCode.ERR_SEC_NOT_PERMITTED_REG;
                                break;
                        }

                        GatePassViewModel = Mapper.Map<GatePass, GatePassViewModel>(queryGatePassResult);

                        // Return gatepass/truck
                        response = ResponseConstructor<GatePassViewModel>.ConstructData(tmpResponseCode, GatePassViewModel);
                    }
                }
            }

            return response;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> ConfirmSecurityCheck(SecurityUpdateStateViewModel updateStateView)
        {
            ResponseViewModel<GatePassViewModel> response;
            GatePass queryGatePassResult;
            GatePassViewModel gatePassViewModel;
            State queryStateResult;
            int tmpResponseCode;
            
            if (_unitOfWork.Exists() == false)
            {
                // Cound Not Connect to Database
                response = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_DB_CONNECTION_FAILED, null);
            }
            else if(updateStateView == null)
            {
                // Wrong request format
                response = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_WRONG_BODY_REQUEST_FORMAT, null);
            }
            else
            {
                // Get Queue by gatePassCode from database
                queryGatePassResult = await _gatePassRepository.GetAsync(q => 
                                      q.isDelete == false &&
                                      q.code == updateStateView.gatePassCode, QueryIncludes.SECURITY_GATEPASS_INCLUDES);
                // @TODO: Get permission of confirm RFID
                //
                bool isConfirmPermited = true;

                if (queryGatePassResult == null)
                {
                    // Not found GatePass
                    response = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_NOT_FOUND_GATEPASS, null);
                }
                else if (queryGatePassResult.state == null)
                {
                    // GatePass lacked "stateID" property
                    response = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_GATEPASS_LACK_STATEID, null);
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
                        switch (queryGatePassResult.state.ID)
                        {
                            case GatepassState.STATE_CALLING_1:
                            case GatepassState.STATE_CALLING_2:
                            case GatepassState.STATE_CALLING_3:
                            case GatepassState.STATE_IN_SECURITY_CHECK_IN:
                                // Get state ID of "Finish security check-in"
                                queryStateResult = await _stateRepository.GetAsync(s => s.ID == GatepassState.STATE_FINISH_SECURITY_CHECK_IN); // STATE_SECURITY_CHECK_IN);
                                // Updat gatepass/state
                                queryGatePassResult.stateID = queryStateResult.ID;
                                queryGatePassResult.enterTime = DateTime.Now;
                                tmpResponseCode = ResponseCode.SUCCESS;
                                break;
                            case GatepassState.STATE_FINISH_WEIGHT_OUT:
                                // Get state ID of "Finish security check-out"
                                queryStateResult = await _stateRepository.GetAsync(s => s.ID == GatepassState.STATE_FINISH_SECURITY_CHECK_OUT); // STATE_SECURITY_CHECK_OUT);
                                // Updat gatepass/state
                                queryGatePassResult.stateID = queryStateResult.ID;
                                queryGatePassResult.leaveTime = DateTime.Now;
                                queryGatePassResult.isDelete = true;
                                queryGatePassResult.queueLists.First().isDelete = true;
                                if(queryGatePassResult.orders != null && queryGatePassResult.orders.Count > 0)
                                {
                                    foreach (Order order in queryGatePassResult.orders)
                                    {
                                        order.isDelete = true;
                                    }
                                }
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
                    queryGatePassResult = await _gatePassRepository.GetAsync(q => q.code == updateStateView.gatePassCode, QueryIncludes.SECURITY_GATEPASS_INCLUDES);
                    gatePassViewModel = Mapper.Map<GatePass, GatePassViewModel>(queryGatePassResult);
                    
                    // Return gatepass/truck
                    response = ResponseConstructor<GatePassViewModel>.ConstructData(tmpResponseCode, gatePassViewModel);
                }
            }
            return response;
        }

        public MultipartFormDataContent GetDriverImage(string filePath)
        {
            byte[] fileContent;
            MultipartFormDataContent MIMEContent;
            StreamContent streamContent;
            FileInfo fileInfo;

            MIMEContent = null;

            try
            {
                fileInfo = new FileInfo(filePath);
                fileContent = File.ReadAllBytes(filePath);
                
                // Create content
                MIMEContent = new MultipartFormDataContent();
                streamContent = new StreamContent(new MemoryStream(fileContent));
                streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                MIMEContent.Add(streamContent,fileInfo.Name,fileInfo.FullName);
            }
            catch(Exception) { }

            return MIMEContent;
        }

        public async Task<ResponseViewModel<QueueInfo>> GetQueueInfo()
        {
            ResponseViewModel<QueueInfo> queueInfo = new ResponseViewModel<QueueInfo>();
            try
            {
                var gatePasses = await _gatePassRepository.GetAllAsync();
                queueInfo.responseData = new QueueInfo();
                queueInfo.responseData.truckInPlant = gatePasses.Where(gt => gt.stateID > GatepassState.STATE_FINISH_SECURITY_CHECK_IN).Count();
                queueInfo.responseData.truckOutPlant = gatePasses.Where(gt => gt.stateID <= GatepassState.STATE_FINISH_SECURITY_CHECK_IN).Count();
                return queueInfo;
            }
            catch (Exception e)
            {
                return queueInfo;
            }
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
