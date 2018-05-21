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
        ResponseViewModel<WeightRecordViewModel> AddTruckCamera(string filename, byte[] fileContent); // 
        Task<ResponseViewModel<WeightRecordViewModel>> UpdateWeightValue(WeightDataViewModel weightDataViewModel); // 
        Task<ResponseViewModel<WeightRecordViewModel>> GetWeightValueByGatePassID(int gatePassID); //
        Task<ResponseViewModel<UpdateLaneStatusViewModel>> UpdateLaneStatus(UpdateLaneStatusViewModel updateLaneStatusViewModel);
    }
}
