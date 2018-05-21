using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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

        [HttpPost]
        [Route("UpdateTruckOnWarehouseCheckIn", Name = "UpdateTruckOnWarehouseCheckIn")]
        public async Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheckIn(TheoryWeighValueModel theoryWeighValueModel)
        {
            return await _warehouseService.UpdateTruckOnWarehouseCheckIn(theoryWeighValueModel);
        }

        [HttpPost]
        [Route("UpdateTruckOnWarehouseCheckOut", Name = "UpdateTruckOnWarehouseCheckOut")]
        public async Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheckOut(TheoryWeighValueModel theoryWeighValueModel)
        {
            return await _warehouseService.UpdateTruckOnWarehouseCheckOut(theoryWeighValueModel);
        }

        [HttpPost]
        [Route("UpdateTheoryWeighValue", Name = "UpdateTheoryWeighValue")]
        public async Task<ResponseViewModel<GenericResponseModel>> UpdateTheoryWeighValue(TheoryWeighValueModel theoryWeighValueModel)
        {
            return await _warehouseService.UpdateTheoryWeighValue(theoryWeighValueModel);
        }

        [HttpPost]
        [Route("UpdateTruckOnWarehouseCheck", Name = "UpdateTruckOnWarehouseCheck")]
        public async Task<ResponseViewModel<GenericResponseModel>> UpdateTruckOnWarehouseCheck(TheoryWeighValueModel theoryWeighValueModel)
        {
            return await _warehouseService.UpdateTruckOnWarehouseCheck(theoryWeighValueModel);
        }

        [HttpGet]
        [Route("GetLaneForWarehouseManagement/{code}", Name = "GetLaneForWarehouseManagement")]
        public async Task<ResponseViewModel<LaneMgntViewModel>> GetLaneForWarehouseManagement(string code)
        {
            return await _warehouseService.GetLaneForWarehouseManagement(code);
        }
    }
}