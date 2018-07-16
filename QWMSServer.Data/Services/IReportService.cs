using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Services
{
    public interface IReportService
    {

        Task<ResponseViewModel<ReportViewModel>> CreateReport(SearchCondition searchCondition);
        Task<ResponseViewModel<ActivityLogViewModel>> GetLastUserActivityLog(EmployeeActivityModel employeeActivityModel);
        Task<ResponseViewModel<ActivityLogViewModel>> SearchActivityLog(SearchCondition searchCondition);
        Task<ResponseViewModel<GenericResponseModel>> CreateLog(ActivityLog accesLog);
        Task<ResponseViewModel<ReportViewModel>> GetPrintValue(string gatePassCode);
        Task<ResponseViewModel<PrintHeader>> GetPrintHeader();
        Task<ResponseViewModel<GenericResponseModel>> AddPrintNo(PrintNoItem printNoItem);
    }
}
