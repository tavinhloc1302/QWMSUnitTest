using QWMSServer.Data.Services;
using QWMSServer.Filter;
using QWMSServer.Model.ViewModels;
using System.Threading.Tasks;
using System.Web.Http;

namespace QWMSServer.Controllers
{
    [RoutePrefix("Warehouse")]
    public class WarehouseController : ApiController
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("UpdateTruckOnWarehouseCheckIn", Name = "UpdateTruckOnWarehouseCheckIn")]
        public async Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheckIn(QCWeighValueModel QCWeighValueModel)
        {
            return await _warehouseService.UpdateTruckOnWarehouseCheckIn(QCWeighValueModel);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("UpdateTruckOnWarehouseCheckOut", Name = "UpdateTruckOnWarehouseCheckOut")]
        public async Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheckOut(QCWeighValueModel QCWeighValueModel)
        {
            return await _warehouseService.UpdateTruckOnWarehouseCheckOut(QCWeighValueModel);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("UpdateTheoryWeighValue", Name = "UpdateTheoryWeighValue")]
        public async Task<ResponseViewModel<GenericResponseModel>> UpdateQCWeighValue(QCWeighValueModel QCWeighValueModel)
        {
            return await _warehouseService.UpdateQCWeighValue(QCWeighValueModel);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("UpdateTruckOnWarehouseCheck", Name = "UpdateTruckOnWarehouseCheck")]
        public async Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheck(QCWeighValueModel theoryWeighValueModel)
        {
            return await _warehouseService.UpdateTruckOnWarehouseCheck(theoryWeighValueModel);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("GetLaneForWarehouseManagement/{code}", Name = "GetLaneForWarehouseManagement")]
        public async Task<ResponseViewModel<LaneMgntViewModel>> GetLaneForWarehouseManagement(string code)
        {
            return await _warehouseService.GetLaneForWarehouseManagement(code);
        }
    }
}