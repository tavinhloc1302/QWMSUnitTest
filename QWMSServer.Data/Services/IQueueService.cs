using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;

namespace QWMSServer.Data.Services
{
    public interface IQueueService
    {
        Task<ResponseViewModel<GatePassViewModel>> GetAllGatePass();

        Task<ResponseViewModel<GatePassViewModel>> GetGatePassByID(int ID);

        Task<ResponseViewModel<GatePassViewModel>> GetGatePassByCode(string Code);

        Task<ResponseViewModel<GatePassViewModel>> GetGatePassByRFID(string Code);

        Task<ResponseViewModel<GatePassViewModel>> GetGatePassByDriverID(int ID);

        Task<ResponseViewModel<GatePassViewModel>> GetGatePassByPlateNumber(string PlateNumber);

        Task<ResponseViewModel<GatePassViewModel>> UpdateGatePass(GatePassViewModel gatePassViewModel);

        bool AddDriverPicture(string filename, byte[] fileContent);

        Task<ResponseViewModel<GatePassViewModel>> UpdateGatePassWithRFIDCode(GatePassViewModel gatePassViewModel);

        Task<bool> CreateRegisteredQueueItem(int gatePassID, string driverImageName, string employeeRFID, string driverRFID);

        Task<bool> ReOrderQueue();// test

        Task<ResponseViewModel<DOViewModel>> ImportDO(List<DOViewModel> listDO);
    }
}
