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
        Task<ResponseViewModel<GatePassViewModel>> UpdateTruckOnWarehouseCheckIn(WarehouseCheckModel warehouseCheckModel);
        Task<ResponseViewModel<GatePassViewModel>> UpdateTruckOnWarehouseCheckOut(WarehouseCheckModel warehouseCheckModel);
        Task<ResponseViewModel<GatePassViewModel>> UpdateQCWeighValue(WarehouseCheckModel warehouseCheckModel);
        Task<ResponseViewModel<GatePassViewModel>> UpdateTruckOnWarehouseCheck(WarehouseCheckModel warehouseCheckModel);
        Task<ResponseViewModel<LaneMgntViewModel>> GetLaneForWarehouseManagement(string code);
    }
}
