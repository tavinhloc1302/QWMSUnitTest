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

        ResponseViewModel<GenericResponseModel> AddDriverPicture(string filename, byte[] fileContent);

        Task<ResponseViewModel<GatePassViewModel>> UpdateGatePassWithRFIDCode(GatePassViewModel gatePassViewModel);

        Task<ResponseViewModel<GenericResponseModel>> CreateRegisteredQueueItem(int gatePassID, string driverImageName, string employeeRFID, string driverRFID);

        Task<ResponseViewModel<GenericResponseModel>> ReOrderQueue();

        Task<ResponseViewModel<DOViewModel>> ImportDO(List<DOViewModel> listDO);

        Task<ResponseViewModel<POViewModel>> ImportPO(List<POViewModel> listPO);

        Task<ResponseViewModel<OrderViewModel>> GetAllDONotPlaned(string customerCode);

        Task<ResponseViewModel<OrderViewModel>> GetAllPONotPlaned(string vendorCode);

        Task<ResponseViewModel<CreateGatePassViewModel>> CreateGatepassWithDO(CreateGatePassViewModel createGatePassViewModel);

        Task<ResponseViewModel<CreateGatePassViewModel>> CreateGatepassWithPO(CreateGatePassViewModel createGatePassViewModel);

        Task<ResponseViewModel<LoadingBayViewModel>> GetAllLoadingBay();

        Task<ResponseViewModel<LoadingBayViewModel>> GetLoadingBayByTruck(string truckCode);
    }
}
