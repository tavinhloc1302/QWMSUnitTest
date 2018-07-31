using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;

namespace QWMSServer.Data.Services
{
    public interface IWeightService
    {
        Task<ResponseViewModel<QueueListViewModel>> GetQueueList(); // GetTruckGroup
        Task<ResponseViewModel<QueueListViewModel>> CallNextTruck(int truckGroupID); // 
        Task<ResponseViewModel<QueueListViewModel>> CallNextTruckByGatePassID(int gatePassID);
        ResponseViewModel<GenericResponseModel> AddTruckCamera(string filename, byte[] fileContent); // 
        Task<ResponseViewModel<WeightRecordViewModel>> UpdateWeightValue(WeightDataViewModel weightDataViewModel); // 
        Task<ResponseViewModel<WeightRecordViewModel>> GetWeightValueByGatePassID(int gatePassID); //
        Task<ResponseViewModel<UpdateLaneStatusViewModel>> UpdateLaneStatus(UpdateLaneStatusViewModel updateLaneStatusViewModel);
        Task<ResponseViewModel<GatePassViewModel>> GetEmptyGatepass(int employeeID);
        Task<ResponseViewModel<RFIDCardViewModel>> GetRFIDNotUse();
        Task<ResponseViewModel<GatePassViewModel>> UpdateGatepass(GatePassViewModel gatepass);
        Task<ResponseViewModel<GatePassViewModel>> UpdateSealNoNPrintGoodGatepass(GatePassViewModel gatepass);
        Task<ResponseViewModel<WeightRecordViewModel>> GetTruckNetWeightValueByTruckID(int truckID);
        Task<ResponseViewModel<WeighBridge>> GetWB(string WBCode);
        Task<ResponseViewModel<WeighBridge>> UpdateWB(WeighBridge WBView);
        Task<ResponseViewModel<WeighbridgeConfiguration>> GetAllWBConfigs();
        Task<ResponseViewModel<WeighbridgeConfiguration>> UpdateWBConfig(WeighbridgeConfiguration WBConfigView);
        Task<ResponseViewModel<GatePassViewModel>> UpdateGatePassWeightValue(GatePassViewModel gatepass);
    }
}
