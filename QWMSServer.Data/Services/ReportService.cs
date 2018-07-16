using AutoMapper;
using QWMSServer.Data.Common;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGatePassRepository _gatePassRepository;
        private readonly IWeightRecordRepository _weightRecordRepository;
        private readonly IActivityLogRepository _accessLogRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPrintHeaderRepository _printHeaderRepository;

        public ReportService(IUnitOfWork unitOfWork, IGatePassRepository gatePassRepository, IWeightRecordRepository weightRecordRepository,
                            IActivityLogRepository accessLogRepository, IEmployeeRepository employeeRepository, IPrintHeaderRepository printHeaderRepository)
        {
            _unitOfWork = unitOfWork;
            _gatePassRepository = gatePassRepository;
            _weightRecordRepository = weightRecordRepository;
            _accessLogRepository = accessLogRepository;
            _employeeRepository = employeeRepository;
            _printHeaderRepository = printHeaderRepository;
        }

        public async Task<ResponseViewModel<ReportViewModel>> CreateReport(SearchCondition searchCondition)
        {
            ResponseViewModel<ReportViewModel> response = new ResponseViewModel<ReportViewModel>();
            IEnumerable<GatePass> gatePasses = null;
            IEnumerable<WeightRecord> weightRecords = null;
            try
            {
                var reportType = searchCondition.reportType;
                switch (reportType)
                {
                    case ReportType.FIRST_WEIGHT_ONLY: // Report only first weight value - at every weight - include 1st done + 2nd done and 1st done + 2nd not done
                        // Default Search - Search Weight Record FromDate - ToDate
                        weightRecords = await _weightRecordRepository.GetManyAsync(wt => wt.weightTime >= searchCondition.fromDate && wt.weightTime <= searchCondition.toDate && wt.isSuccess == true, QueryIncludes.WEIGHT_RECORD_INCLUDES);
                        weightRecords = this.FilterCondition(weightRecords, searchCondition);
                        break;
                    case ReportType.SECOND_WEIGHT_ONLY: // Report only first weight value - only get the final success weight
                        // Default Search - Search Weight Record FromDate - ToDate
                        weightRecords = await _weightRecordRepository.GetManyAsync(wt => wt.weightTime >= searchCondition.fromDate && wt.weightTime <= searchCondition.toDate && wt.isSuccess == true, QueryIncludes.WEIGHT_RECORD_INCLUDES);
                        weightRecords = this.FilterCondition(weightRecords, searchCondition);
                        break;
                    case ReportType.BOTH_WEIGHT:
                        // Default Search - Search Weight Record FromDate - ToDate - Not include fail weight - Each GP has max 2 rows
                        weightRecords = await _weightRecordRepository.GetManyAsync(wt => wt.weightTime >= searchCondition.fromDate && wt.weightTime <= searchCondition.toDate && wt.isSuccess == true, QueryIncludes.WEIGHT_RECORD_INCLUDES);
                        weightRecords = this.FilterCondition(weightRecords, searchCondition);
                        break;
                    case ReportType.FIRST_UNDONE_SECOND:
                        // Default Search - Search Weight Record FromDate - ToDate & WeightNo = 1 - Only First Weight
                        weightRecords = await _weightRecordRepository.GetManyAsync(wt => wt.weightTime >= searchCondition.fromDate && wt.weightTime <= searchCondition.toDate && wt.isSuccess == true, QueryIncludes.WEIGHT_RECORD_INCLUDES);
                        weightRecords = this.FilterCondition(weightRecords, searchCondition);
                        break;
                    case ReportType.WEIGHT_HISTORY: // Báo cáo xe cân
                        // Default Search - Search Weight Record FromDate - ToDate
                        weightRecords = await _weightRecordRepository.GetManyAsync(gt => gt.weightTime >= searchCondition.fromDate && gt.weightTime <= searchCondition.toDate , QueryIncludes.WEIGHT_RECORD_INCLUDES);
                        if (searchCondition.weightEmployeeID != -1)
                            weightRecords = weightRecords.Where(wt => wt.weightEmployeeID == searchCondition.weightEmployeeID);
                        if (searchCondition.truckID != -1)
                            weightRecords = weightRecords.Where(wt => wt.gatePass.truck.ID == searchCondition.truckID);
                        break;
                    case ReportType.PRINTED_GATEPASS:
                        gatePasses = await _gatePassRepository.GetManyAsync(gt => gt.createDate >= searchCondition.fromDate && gt.createDate <= searchCondition.toDate && gt.printNo > 0, QueryIncludes.GATEPASSFULLINCLUDES);
                        if (searchCondition.printEmployeeID != -1)
                            gatePasses = gatePasses.Where(gt => gt.printEmployeeID == searchCondition.printEmployeeID);
                        if(searchCondition.gatePassCode.Trim() != "")
                            gatePasses = gatePasses.Where(gt => gt.code.Equals( searchCondition.gatePassCode.Trim()));
                        break;
                    case ReportType.WEIGHT_MODIFIED:
                        // Get all weight record which is mofified - from the second weight
                        weightRecords = await _weightRecordRepository.GetManyAsync(wt => wt.weightTime >= searchCondition.fromDate && wt.weightTime <= searchCondition.toDate, QueryIncludes.WEIGHT_RECORD_INCLUDES);
                        if (searchCondition.weightEmployeeID != -1)
                            weightRecords = weightRecords.Where(wt => wt.weightEmployeeID == searchCondition.weightEmployeeID);
                        if (searchCondition.gatePassCode.Trim() != "")
                            weightRecords = weightRecords.Where(wt => wt.gatePass.code.Equals(searchCondition.gatePassCode.Trim()));
                        break;
                    case ReportType.ALL_WEIGHT:
                        // All success weigh of each GatePass
                        weightRecords = await _weightRecordRepository.GetManyAsync(wt => wt.weightTime >= searchCondition.fromDate && wt.weightTime <= searchCondition.toDate && wt.isSuccess == true, QueryIncludes.WEIGHT_RECORD_INCLUDES);
                        weightRecords = this.FilterCondition(weightRecords, searchCondition);
                        break;
                    default:
                        response.errorText = "Không xác định loại báo cáo";
                        break;
                }
                response.responseDatas = this.CreateReportViewModel(weightRecords, gatePasses, searchCondition);
                if (response.responseDatas == null || response.responseDatas.Count() == 0)
                {
                    response.errorText = ResponseText.ERR_SEARCH_FAIL;
                }
                return response;
            }
            catch (Exception e)
            {
                response.errorText = "Lỗi không xác định";
                return response;
            }
        }

        public IEnumerable<WeightRecord> FilterCondition(IEnumerable<WeightRecord> weightRecords, SearchCondition searchCondition)
        {
            if (searchCondition.weighBridgeID != -1)
                weightRecords = weightRecords.Where(wt => wt.weighBridgeID == searchCondition.weighBridgeID);
            if (searchCondition.gatePassCode.Trim() != "")
                weightRecords = weightRecords.Where(wt => wt.gatePass.code.Equals(searchCondition.gatePassCode.Trim()));
            if (searchCondition.materialWeight != -1)
                weightRecords = weightRecords.Where(wt => wt.gatePass.orders.ToList()[0].registGrossWeight == searchCondition.materialWeight);
            if (searchCondition.customerID != -1)
                weightRecords = weightRecords.Where(wt => wt.gatePass.customerID == searchCondition.customerID);
            if (searchCondition.weightType != WeightType.WEIGHT_ALL)
                weightRecords = weightRecords.Where(wt => wt.gatePass.weightType == searchCondition.weightType);
            if (searchCondition.weightEmployeeID != -1)
                weightRecords = weightRecords.Where(wt => wt.weightEmployeeID == searchCondition.weightEmployeeID);
            if (searchCondition.materialID != -1)
                weightRecords = weightRecords.Where(wt => wt.gatePass.materialID == searchCondition.materialID);
            if (searchCondition.plateNumber.Trim() != "")
                weightRecords = weightRecords.Where(wt => wt.gatePass.truck.plateNumber.Equals(searchCondition.plateNumber.Trim()));
            if (searchCondition.driverID != -1)
                weightRecords = weightRecords.Where(wt => wt.gatePass.driver.ID == searchCondition.driverID);

            return weightRecords;
        }

        public IEnumerable<ReportViewModel> CreateReportViewModel(IEnumerable<WeightRecord> weightRecords, IEnumerable<GatePass> gatePasses, SearchCondition searchCondition)
        {
            List<ReportViewModel> reportViewModels = new List<ReportViewModel>();
            
            try
            {
                switch (searchCondition.reportType)
                {
                    case ReportType.FIRST_WEIGHT_ONLY: 
                        List<int> frgatePassIDs = new List<int>();
                        // Get List of gatePass
                        foreach (var weightRecord in weightRecords)
                        {
                            frgatePassIDs.Add(weightRecord.gatepassID);
                        }
                        frgatePassIDs = frgatePassIDs.Distinct().ToList();
                        // Go through list of GatePass to get Weight Value of each item
                        foreach (var gatePassID in frgatePassIDs)
                        {
                            ReportViewModel reportViewModel = new ReportViewModel();
                            var tmpW = weightRecords.Where(wt => wt.gatepassID == gatePassID);
                            tmpW.OrderBy(o => o.weightNo);
                            var firstW = tmpW.First();
                            // gen first weight report
                            reportViewModel.reportType = ReportType.FIRST_WEIGHT_ONLY;
                            reportViewModel.gatePassCode = firstW.gatePass.code;
                            reportViewModel.truckPlateNumber = firstW.gatePass.truck.plateNumber;
                            reportViewModel.customerName = firstW.gatePass.customer.nameVi;
                            reportViewModel.materialName = firstW.gatePass.material.materialNameVi;
                            reportViewModel.firstWeightValue = firstW.weightValue;
                            reportViewModel.firstWeightTime = firstW.weightTime;
                            reportViewModel.firstWeightEmployeeName = firstW.employee.firstName + " " + firstW.employee.lastName;
                            reportViewModel.isOverWeight = (bool)firstW.isOverWeight;
                            reportViewModel.cabinCameraCapturePath = firstW.cabinCameraCapturePath;
                            reportViewModel.containerCameraCapturePath = firstW.containerCameraCapturePath;
                            reportViewModel.frontCameraCapturePath = firstW.frontCameraCapturePath;
                            reportViewModel.gearCameraCapturePath = firstW.gearCameraCapturePath;
                            reportViewModel.weightID = firstW.ID;
                            reportViewModel.truckWeight = firstW.gatePass.truck.truckNetWeight;
                            reportViewModel.driverName = firstW.gatePass.driver.nameVi;
                            reportViewModels.Add(reportViewModel);
                        }
                        break;
                    case ReportType.SECOND_WEIGHT_ONLY:
                        List<int> sgatePassIDs = new List<int>();
                        // Get List of gatePass
                        foreach (var weightRecord in weightRecords)
                        {
                            sgatePassIDs.Add(weightRecord.gatepassID);
                        }
                        sgatePassIDs = sgatePassIDs.Distinct().ToList();
                        // Go through list of GatePass to get Weight Value of each item
                        foreach (var gatePassID in sgatePassIDs)
                        {
                            ReportViewModel reportViewModel = new ReportViewModel();
                            var tmpW = weightRecords.Where(wt => wt.gatepassID == gatePassID);
                            tmpW.OrderBy(o => o.weightNo);
                            if(tmpW.Count() > 1)
                            {
                                var sectW = tmpW.Last();
                                // gen first weight report
                                reportViewModel.reportType = ReportType.SECOND_WEIGHT_ONLY;
                                reportViewModel.gatePassCode = sectW.gatePass.code;
                                reportViewModel.truckPlateNumber = sectW.gatePass.truck.plateNumber;
                                reportViewModel.customerName = sectW.gatePass.customer.nameVi;
                                reportViewModel.materialName = sectW.gatePass.material.materialNameVi;
                                reportViewModel.secondWeightvalue = sectW.weightValue;
                                reportViewModel.secondWeightTime = sectW.weightTime;
                                reportViewModel.secondWeightEmployeeName = sectW.employee.firstName + " " + sectW.employee.lastName;
                                reportViewModel.isOverWeight = (bool)sectW.isOverWeight;
                                reportViewModel.cabinCameraCapturePath = sectW.cabinCameraCapturePath;
                                reportViewModel.containerCameraCapturePath = sectW.containerCameraCapturePath;
                                reportViewModel.frontCameraCapturePath = sectW.frontCameraCapturePath;
                                reportViewModel.gearCameraCapturePath = sectW.gearCameraCapturePath;
                                reportViewModel.weightID = sectW.ID;
                                reportViewModel.truckWeight = sectW.gatePass.truck.truckNetWeight;
                                reportViewModel.driverName = sectW.gatePass.driver.nameVi;
                                reportViewModels.Add(reportViewModel);
                            }
                        }
                        break;
                    case ReportType.BOTH_WEIGHT: // Báo cáo tổng hợp
                        List<int> gatePassIDs = new List<int>();
                        // Get List of gatePass
                        foreach (var weightRecord in weightRecords)
                        {
                            gatePassIDs.Add(weightRecord.gatepassID);
                        }
                        gatePassIDs = gatePassIDs.Distinct().ToList();
                        // Go through list of GatePass to get Weight Value of each item
                        foreach (var gatePassID in gatePassIDs)
                        {
                            ReportViewModel reportViewModel = new ReportViewModel();
                            var tmpW = weightRecords.Where(wt => wt.gatepassID == gatePassID);
                            var firstW = tmpW.First();
                            // gen first weight report
                            reportViewModel.reportType = ReportType.BOTH_WEIGHT;
                            reportViewModel.gatePassCode = firstW.gatePass.code;
                            reportViewModel.weightID = firstW.ID;
                            reportViewModel.truckPlateNumber = firstW.gatePass.truck.plateNumber;
                            reportViewModel.customerName = firstW.gatePass.customer.nameVi;
                            reportViewModel.materialName = firstW.gatePass.material.materialNameVi;
                            reportViewModel.weightType = (int)firstW.gatePass.weightType;
                            reportViewModel.truckWeight = firstW.gatePass.truck.truckNetWeight;
                            reportViewModel.driverName = firstW.gatePass.driver.nameVi;
                            reportViewModel.printNo = firstW.gatePass.printNo == null ? 0 : firstW.gatePass.printNo;
                            reportViewModel.printDate = firstW.gatePass.printDate == null? DateTime.MinValue : (DateTime)firstW.gatePass.printDate;

                            reportViewModel.firstWeightValue = firstW.weightValue;
                            reportViewModel.firstWeightTime = firstW.weightTime;
                            reportViewModel.firstWeightEmployeeName = firstW.employee.firstName + " " + firstW.employee.lastName;
                            reportViewModel.isOverWeight = (bool)firstW.isOverWeight;
                            reportViewModel.cabinCameraCapturePath = firstW.cabinCameraCapturePath;
                            reportViewModel.containerCameraCapturePath = firstW.containerCameraCapturePath;
                            reportViewModel.frontCameraCapturePath = firstW.frontCameraCapturePath;
                            reportViewModel.gearCameraCapturePath = firstW.gearCameraCapturePath;
                            // gen second weight report in case done 2 times weight
                            if (tmpW.Count() > 1)
                            {
                                var secW = tmpW.Last();
                                reportViewModel.secondWeightvalue = secW.weightValue;
                                reportViewModel.secondWeightTime = secW.weightTime;
                                reportViewModel.secondWeightEmployeeName = secW.employee.firstName + " " + firstW.employee.lastName;
                                reportViewModel.isOverWeight = (bool)secW.isOverWeight;
                                reportViewModel.secondCabinCameraCapturePath = secW.cabinCameraCapturePath;
                                reportViewModel.secondContainerCameraCapturePath = secW.containerCameraCapturePath;
                                reportViewModel.secondFrontCameraCapturePath = secW.frontCameraCapturePath;
                                reportViewModel.secondGearCameraCapturePath = secW.gearCameraCapturePath;
                            }
                            reportViewModels.Add(reportViewModel);
                        }
                        break;
                    case ReportType.FIRST_UNDONE_SECOND:
                        List<int> fugatePassIDs = new List<int>();
                        // Get List of gatePass
                        foreach (var weightRecord in weightRecords)
                        {
                            fugatePassIDs.Add(weightRecord.gatepassID);
                        }
                        fugatePassIDs = fugatePassIDs.Distinct().ToList();
                        // Go through list of GatePass to get Weight Value of each item
                        foreach (var gatePassID in fugatePassIDs)
                        {
                            ReportViewModel reportViewModel = new ReportViewModel();
                            var tmpW = weightRecords.Where(wt => wt.gatepassID == gatePassID);
                            tmpW.OrderBy(o => o.weightNo);
                            if(tmpW.Count() == 1)
                            {
                                var firstW = tmpW.First();
                                // gen first weight report
                                reportViewModel.reportType = ReportType.FIRST_UNDONE_SECOND;
                                reportViewModel.gatePassCode = firstW.gatePass.code;
                                reportViewModel.truckPlateNumber = firstW.gatePass.truck.plateNumber;
                                reportViewModel.customerName = firstW.gatePass.customer.nameVi;
                                reportViewModel.materialName = firstW.gatePass.material.materialNameVi;
                                reportViewModel.firstWeightValue = firstW.weightValue;
                                reportViewModel.firstWeightTime = firstW.weightTime;
                                reportViewModel.firstWeightEmployeeName = firstW.employee.firstName + " " + firstW.employee.lastName;
                                reportViewModel.isOverWeight = (bool)firstW.isOverWeight;
                                reportViewModel.cabinCameraCapturePath = firstW.cabinCameraCapturePath;
                                reportViewModel.containerCameraCapturePath = firstW.containerCameraCapturePath;
                                reportViewModel.frontCameraCapturePath = firstW.frontCameraCapturePath;
                                reportViewModel.gearCameraCapturePath = firstW.gearCameraCapturePath;
                                reportViewModel.weightID = firstW.ID;
                                reportViewModel.truckWeight = firstW.gatePass.truck.truckNetWeight;
                                reportViewModel.driverName = firstW.gatePass.driver.nameVi;
                                reportViewModels.Add(reportViewModel);
                            }
                        }
                        break;
                    case ReportType.WEIGHT_HISTORY: // Báo cáo xe cân
                        foreach (var weightRecord in weightRecords)
                        {
                            ReportViewModel reportViewModel = new ReportViewModel();
                            reportViewModel.reportType = ReportType.WEIGHT_HISTORY;
                            reportViewModel.gatePassCode = weightRecord.gatePass.code;
                            reportViewModel.truckPlateNumber = weightRecord.gatePass.truck.plateNumber;
                            reportViewModel.customerName = weightRecord.gatePass.customer.nameVi;
                            reportViewModel.materialName = weightRecord.gatePass.material.materialNameVi;
                            reportViewModel.weightvalue = weightRecord.weightValue;
                            reportViewModel.weightTime = weightRecord.weightTime;
                            reportViewModel.weightEmployeeName = weightRecord.employee.firstName + " " + weightRecord.employee.lastName;
                            reportViewModel.isOverWeight = (bool)weightRecord.isOverWeight;
                            reportViewModel.weightNo = weightRecord.weightNo;
                            reportViewModel.cabinCameraCapturePath = weightRecord.cabinCameraCapturePath;
                            reportViewModel.containerCameraCapturePath = weightRecord.containerCameraCapturePath;
                            reportViewModel.frontCameraCapturePath = weightRecord.frontCameraCapturePath;
                            reportViewModel.gearCameraCapturePath = weightRecord.gearCameraCapturePath;
                            reportViewModel.weightID = weightRecord.ID;
                            reportViewModel.truckWeight = weightRecord.gatePass.truck.truckNetWeight;
                            reportViewModel.driverName = weightRecord.gatePass.driver.nameVi;
                            reportViewModels.Add(reportViewModel);
                        }
                        break;
                    case ReportType.PRINTED_GATEPASS:
                        foreach (var gatePass in gatePasses)
                        {
                            ReportViewModel reportViewModel = new ReportViewModel();
                            reportViewModel.reportType = ReportType.PRINTED_GATEPASS;
                            reportViewModel.gatePassCode = gatePass.code;
                            reportViewModel.printDate = (DateTime)gatePass.printDate;
                            reportViewModel.printNo = (int)gatePass.printNo;
                            reportViewModel.createDate = (DateTime)gatePass.createDate;
                            reportViewModel.printEmployeeName = gatePass.printEmployee.firstName + " " + gatePass.printEmployee.lastName;
                            reportViewModels.Add(reportViewModel);
                        }
                        break;
                    case ReportType.WEIGHT_MODIFIED:
                        foreach (var weightRecord in weightRecords)
                        {
                            ReportViewModel reportViewModel = new ReportViewModel();
                            reportViewModel.reportType = ReportType.WEIGHT_MODIFIED;
                            reportViewModel.gatePassCode = weightRecord.gatePass.code;
                            reportViewModel.truckPlateNumber = weightRecord.gatePass.truck.plateNumber;
                            reportViewModel.customerName = weightRecord.gatePass.customer.nameVi;
                            reportViewModel.materialName = weightRecord.gatePass.material.materialNameVi;
                            reportViewModel.weightvalue = weightRecord.weightValue;
                            reportViewModel.weightTime = weightRecord.weightTime;
                            reportViewModel.weightEmployeeName = weightRecord.employee.firstName + " " + weightRecord.employee.lastName;
                            reportViewModel.isOverWeight = (bool)weightRecord.isOverWeight;
                            reportViewModel.weightNo = weightRecord.weightNo;
                            reportViewModel.cabinCameraCapturePath = weightRecord.cabinCameraCapturePath;
                            reportViewModel.containerCameraCapturePath = weightRecord.containerCameraCapturePath;
                            reportViewModel.frontCameraCapturePath = weightRecord.frontCameraCapturePath;
                            reportViewModel.gearCameraCapturePath = weightRecord.gearCameraCapturePath;
                            reportViewModel.weightID = weightRecord.ID;
                            reportViewModel.createDate = weightRecord.gatePass.createDate;
                            reportViewModel.isSuccess = (bool)weightRecord.isSuccess;
                            reportViewModels.Add(reportViewModel);
                        }
                        break;
                    case ReportType.ALL_WEIGHT:
                        List<int> algatePassIDs = new List<int>();
                        // Get List of gatePass
                        foreach (var weightRecord in weightRecords)
                        {
                            algatePassIDs.Add(weightRecord.gatepassID);
                        }
                        algatePassIDs = algatePassIDs.Distinct().ToList();
                        // Go through list of GatePass to get Weight Value of each item
                        foreach (var gatePassID in algatePassIDs)
                        {
                            ReportViewModel reportViewModel = new ReportViewModel();
                            var tmpW = weightRecords.Where(wt => wt.gatepassID == gatePassID);
                            var firstW = tmpW.First();
                            // gen first weight report
                            reportViewModel.reportType = ReportType.BOTH_WEIGHT;
                            reportViewModel.gatePassCode = firstW.gatePass.code;
                            reportViewModel.weightID = firstW.ID;
                            reportViewModel.truckPlateNumber = firstW.gatePass.truck.plateNumber;
                            reportViewModel.customerName = firstW.gatePass.customer.nameVi;
                            reportViewModel.materialName = firstW.gatePass.material.materialNameVi;
                            reportViewModel.weightType = (int)firstW.gatePass.weightType;
                            reportViewModel.truckWeight = firstW.gatePass.truck.truckNetWeight;
                            reportViewModel.weightvalue = firstW.weightValue;
                            reportViewModel.weightTime = firstW.weightTime;
                            reportViewModel.weightEmployeeName = firstW.employee.firstName + " " + firstW.employee.lastName;
                            reportViewModel.isOverWeight = (bool)firstW.isOverWeight;
                            reportViewModel.cabinCameraCapturePath = firstW.cabinCameraCapturePath;
                            reportViewModel.containerCameraCapturePath = firstW.containerCameraCapturePath;
                            reportViewModel.frontCameraCapturePath = firstW.frontCameraCapturePath;
                            reportViewModel.gearCameraCapturePath = firstW.gearCameraCapturePath;
                            reportViewModel.isSuccess = (bool)firstW.isSuccess;
                            reportViewModel.weightNo = 1;
                            reportViewModel.driverName = firstW.gatePass.driver.nameVi;
                            reportViewModels.Add(reportViewModel);
                            // gen second weight report in case done 2 times weight
                            if (tmpW.Count() > 1)
                            {
                                ReportViewModel secreportViewModel = new ReportViewModel();
                                var secW = tmpW.Last();
                                secreportViewModel.reportType = ReportType.BOTH_WEIGHT;
                                secreportViewModel.gatePassCode = secW.gatePass.code;
                                secreportViewModel.weightID = secW.ID;
                                secreportViewModel.truckPlateNumber = secW.gatePass.truck.plateNumber;
                                secreportViewModel.customerName = secW.gatePass.customer.nameVi;
                                secreportViewModel.materialName = secW.gatePass.material.materialNameVi;
                                secreportViewModel.weightType = (int)secW.gatePass.weightType;
                                secreportViewModel.truckWeight = secW.gatePass.truck.truckNetWeight;
                                secreportViewModel.weightvalue = secW.weightValue;
                                secreportViewModel.weightTime = secW.weightTime;
                                secreportViewModel.weightEmployeeName = secW.employee.firstName + " " + firstW.employee.lastName;
                                secreportViewModel.isOverWeight = (bool)secW.isOverWeight;
                                secreportViewModel.cabinCameraCapturePath = secW.cabinCameraCapturePath;
                                secreportViewModel.containerCameraCapturePath = secW.containerCameraCapturePath;
                                secreportViewModel.frontCameraCapturePath = secW.frontCameraCapturePath;
                                secreportViewModel.gearCameraCapturePath = secW.gearCameraCapturePath;
                                reportViewModel.isSuccess = (bool)secW.isSuccess;
                                secreportViewModel.weightNo = 2;
                                reportViewModels.Add(secreportViewModel);
                            }
                        }
                        break;
                    default:
                        break;
                }
                return reportViewModels;
            }
            catch (Exception e)
            {
                return reportViewModels;
            }
            
        }

        public async Task<ResponseViewModel<ActivityLogViewModel>> GetLastUserActivityLog(EmployeeActivityModel employeeActivityModel)
        {
            ResponseViewModel<ActivityLogViewModel> response = new ResponseViewModel<ActivityLogViewModel>();
            ActivityLogViewModel activityLogViewModel = new ActivityLogViewModel();
            var activities = await _accessLogRepository.GetManyAsync(act => act.employeeID == employeeActivityModel.employeeID && act.logTime >= employeeActivityModel.time, QueryIncludes.ACCESSLOGFULLINCLUDES);
            response.responseData = Mapper.Map<ActivityLog, ActivityLogViewModel>(activities.Last());
            return response;
        }

        public async Task<ResponseViewModel<ActivityLogViewModel>> SearchActivityLog(SearchCondition searchCondition)
        {
            ResponseViewModel<ActivityLogViewModel> response = new ResponseViewModel<ActivityLogViewModel>();
            var activities = await _accessLogRepository.GetManyAsync(ac => true, QueryIncludes.ACCESSLOGFULLINCLUDES);
            if (searchCondition.currentEmployeeID != -1)
                activities = activities.Where(ac => ac.employeeID == searchCondition.currentEmployeeID);
            if (searchCondition.fromDate != null && searchCondition.toDate != null)
                activities = activities.Where(ac => ac.logTime >= searchCondition.fromDate && ac.logTime <= searchCondition.toDate);
            if (searchCondition.currentEmployeeGroupID != -1)
                activities = activities.Where(ac => ac.employee.employeeGroup.ID == searchCondition.currentEmployeeGroupID);
            if (searchCondition.actionScreen.Trim() != "" && searchCondition.actionScreen.Trim() != Constant.ALL)
                activities = activities.Where(ac => ac.screen.Equals(searchCondition.actionScreen.Trim()));
            if (searchCondition.actionType.Trim() != "" && searchCondition.actionType.Trim() != Constant.ALL)
                activities = activities.Where(ac => ac.action.Equals(searchCondition.actionType.Trim()));
            //if (searchCondition.target.Trim() != "")
            //    activities = activities.Where(ac => ac.target.Contains(searchCondition.target.Trim()));
            if (activities.Count() == 0)
            {
                response.errorText = ResponseText.ERR_SEARCH_FAIL;
            }
            response.responseDatas = Mapper.Map<IEnumerable<ActivityLog>, IEnumerable<ActivityLogViewModel>>(activities);
            return response;
        }

        public async Task<ResponseViewModel<GenericResponseModel>> CreateLog(ActivityLog accesLog)
        {
            ResponseViewModel<GenericResponseModel> response = new ResponseViewModel<GenericResponseModel>();
            try
            {
                accesLog.employee = await _employeeRepository.GetAsync(emp => emp.ID == accesLog.employeeID, null);
                accesLog.logTime = DateTime.Now;
                _accessLogRepository.Add(accesLog);
                if (await _unitOfWork.SaveChangesAsync())
                    response.booleanResponse = true;
                else
                    response.booleanResponse = false;
                return response;
            }
            catch (Exception e)
            {
                response.booleanResponse = false;
                return response;
            }

        }

        public async Task<ResponseViewModel<ReportViewModel>> GetPrintValue(string gatePassCode)
        {
            ResponseViewModel<ReportViewModel> response = new ResponseViewModel<ReportViewModel>();
            IEnumerable<GatePass> gatePasses = null;
            IEnumerable<WeightRecord> weightRecords = null;
            SearchCondition searchCondition = new SearchCondition();
            searchCondition.reportType = ReportType.BOTH_WEIGHT;
            weightRecords = await _weightRecordRepository.GetManyAsync(wt => wt.gatePass.code.Equals(gatePassCode), QueryIncludes.WEIGHT_RECORD_INCLUDES);
            response.responseDatas = this.CreateReportViewModel(weightRecords, gatePasses, searchCondition);
            return response;
        }

        public async Task<ResponseViewModel<PrintHeader>> GetPrintHeader()
        {
            ResponseViewModel<PrintHeader> response = new ResponseViewModel<PrintHeader>();
            var result = await _printHeaderRepository.GetAllAsync();
            response.responseData = result.ToList().First();
            return response;
        }

        public async Task<ResponseViewModel<GenericResponseModel>> AddPrintNo(PrintNoItem printNoItem)
        {
            ResponseViewModel<GenericResponseModel> response = new ResponseViewModel<GenericResponseModel>();
            var gatePass = await _gatePassRepository.GetAsync(gt => gt.code.Equals(printNoItem.gatePassCode), null);
            if(gatePass != null)
            {
                if(gatePass.printNo == null)
                    gatePass.printNo = 0;
                gatePass.printNo += 1;
                gatePass.printDate = DateTime.Now;
                gatePass.printEmployeeID = printNoItem.empID;
                _gatePassRepository.Update(gatePass);
                if(await _unitOfWork.SaveChangesAsync())
                    response.booleanResponse = true;
                else
                    response.booleanResponse = false;
            }
            else
            {
                response.booleanResponse = false;
            }

            return response;
        }
    }
}
