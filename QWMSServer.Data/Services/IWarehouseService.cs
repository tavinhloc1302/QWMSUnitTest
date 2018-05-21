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
        Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheckIn(TheoryWeighValueModel theoryWeighValueModel);
        Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheckOut(TheoryWeighValueModel theoryWeighValueModel);
        Task<ResponseViewModel<GenericResponseModel>> UpdateTheoryWeighValue(TheoryWeighValueModel theoryWeighValueModel);
        Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheck(TheoryWeighValueModel theoryWeighValueModel);
        Task<ResponseViewModel<LaneMgntViewModel>> GetLaneForWarehouseManagement(string code);
    }
}
