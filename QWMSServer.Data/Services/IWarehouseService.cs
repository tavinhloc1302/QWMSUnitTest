using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;

namespace QWMSServer.Data.Services
{
    public interface IWarehouseService
    {
        Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheckIn(QCWeighValueModel theoryWeighValueModel);
        Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheckOut(QCWeighValueModel theoryWeighValueModel);
        Task<ResponseViewModel<GenericResponseModel>> UpdateQCWeighValue(QCWeighValueModel QCWeighValueModel);
        Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheck(QCWeighValueModel theoryWeighValueModel);
        Task<ResponseViewModel<LaneMgntViewModel>> GetLaneForWarehouseManagement(string code);
    }
}
