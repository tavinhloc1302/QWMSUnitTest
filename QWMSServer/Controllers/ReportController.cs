using QWMSServer.Data.Services;
using QWMSServer.Filter;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;
using System.Threading.Tasks;
using System.Web.Http;

namespace QWMSServer.Controllers
{
    [RoutePrefix("Admin")]
    public class ReportController : ApiController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Report/CreateReport", Name = "CreateReport")]
        public async Task<ResponseViewModel<ReportViewModel>> CreateReport([FromBody]SearchCondition searchCondition)
        {
            return await _reportService.CreateReport(searchCondition);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Report/CreateLog", Name = "CreateLog")]
        public async Task<ResponseViewModel<GenericResponseModel>> CreateLog([FromBody]ActivityLog accessLog)
        {
            return await _reportService.CreateLog(accessLog);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Report/GetUserActivityLog", Name = "GetUserActivityLog")]
        public async Task<ResponseViewModel<ActivityLogViewModel>> GetUserActivityLog([FromBody]EmployeeActivityModel employeeActivityModel)
        {
            return await _reportService.GetLastUserActivityLog(employeeActivityModel);
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Report/SearchActivityLog", Name = "SearchActivityLog")]
        public async Task<ResponseViewModel<ActivityLogViewModel>> SearchActivityLog([FromBody]SearchCondition searchCondition)
        {
            return await _reportService.SearchActivityLog(searchCondition);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Report/GetPrintValue/{gatePassCode}", Name = "CreatePrintValue")]
        public async Task<ResponseViewModel<ReportViewModel>> GetPrintValue(string gatePassCode)
        {
            return await _reportService.GetPrintValue(gatePassCode);
        }

        [AuthenticateRequire]
        [HttpGet]
        [Route("Report/GetPrintHeader", Name = "GetPrintHeader")]
        public async Task<ResponseViewModel<PrintHeader>> GetPrintHeader()
        {
            return await _reportService.GetPrintHeader();
        }

        [AuthenticateRequire]
        [HttpPost]
        [Route("Report/AddPrintNo", Name = "AddPrintNo")]
        public async Task<ResponseViewModel<GenericResponseModel>> AddPrintNo([FromBody]PrintNoItem printNoItem)
        {
            return await _reportService.AddPrintNo(printNoItem);
        }
    }
}