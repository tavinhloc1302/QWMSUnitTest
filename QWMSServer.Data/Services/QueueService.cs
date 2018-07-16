using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QWMSServer.Data.Common;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;

namespace QWMSServer.Data.Services
{
    public class QueueService : IQueueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGatePassRepository _gatePassRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ILaneRepository _laneRepository;
        private readonly ITruckRepository _truckRepository;
        private readonly IQueueListRepository _queueListRepository;
        private readonly IRFIDCardRepository _RFIDCardRepository;
        private readonly IEmployeeRepository _employeepository;
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly IDeliveryOrderRepository _deliveryOrderRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICarrierVendorRepository _carrierVendorRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryOrderTypeRepository _deliveryOrderTypeRepository;
        private readonly ICustomerWarehouseRepository _customerWarehouseRepository;
        private readonly IOrderMaterialRepository _orderMaterialRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IUnitTypeRepository _unitTypeRepository;
        private readonly ILoadingBayRepository _loadingBayRepository;
        private readonly ICommonService _commonService;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IPurchaseOrderTypeRepository _purchaseOrderTypeRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly IOrderTypeRepository _orderTypeRepository;

        public QueueService(IUnitOfWork unitOfWork, IGatePassRepository gatePassRepository, IStateRepository stateRepository,
                            ILaneRepository laneRepository, ITruckRepository truckRepository, IQueueListRepository queueListRepository,
                            IRFIDCardRepository RFIDCardRepository, IEmployeeRepository employeepository,
                            ISaleOrderRepository saleOrderRepository,
                            IDeliveryOrderRepository deliveryOrderRepository,
                            IOrderRepository orderRepository,
                            ICarrierVendorRepository carrierVendorRepository,
                            ICustomerRepository customerRepository,
                            IDeliveryOrderTypeRepository deliveryOrderTypeRepository,
                            ICustomerWarehouseRepository customerWarehouseRepository,
                            IOrderMaterialRepository orderMaterialRepository,
                            IMaterialRepository materialRepository,
                            IDriverRepository driverRepository,
                            IUnitTypeRepository unitTypeRepository,
                            ILoadingBayRepository loadingBayRepository,
                            ICommonService commonService,
                            IPurchaseOrderRepository purchaseOrderRepository,
                            IPurchaseOrderTypeRepository purchaseOrderTypeRepository,
                            IPlantRepository plantRepository,
                            IOrderTypeRepository orderTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _gatePassRepository = gatePassRepository;
            _stateRepository = stateRepository;
            _laneRepository = laneRepository;
            _truckRepository = truckRepository;
            _queueListRepository = queueListRepository;
            _RFIDCardRepository = RFIDCardRepository;
            _employeepository = employeepository;
            _saleOrderRepository = saleOrderRepository;
            _deliveryOrderRepository = deliveryOrderRepository;
            _orderRepository = orderRepository;
            _carrierVendorRepository = carrierVendorRepository;
            _customerRepository = customerRepository;
            _deliveryOrderTypeRepository = deliveryOrderTypeRepository;
            _customerWarehouseRepository = customerWarehouseRepository;
            _orderMaterialRepository = orderMaterialRepository;
            _materialRepository = materialRepository;
            _driverRepository = driverRepository;
            _unitTypeRepository = unitTypeRepository;
            _loadingBayRepository = loadingBayRepository;
            _commonService = commonService;
            _purchaseOrderRepository = purchaseOrderRepository;
            _purchaseOrderTypeRepository = purchaseOrderTypeRepository;
            _plantRepository = plantRepository;
            _orderTypeRepository = orderTypeRepository;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetAllGatePass()
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var result = await _gatePassRepository.GetManyAsync(c => c.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                if (result == null)
                    return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructEnumerableData(ResponseCode.ERR_NO_OBJECT_FOUND, "Không có GatePass trong CSDL", null);
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, Mapper.Map<IEnumerable<GatePass>, IEnumerable<GatePassViewModel>>(result));

            }
            catch (Exception)
            {
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructEnumerableData(ResponseCode.ERR_NO_OBJECT_FOUND, "Không có GatePass trong CSDL", null); ;
            }
        }

        public async Task<ResponseViewModel<GatePassViewModel>> SearchGatePass(string searchText)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var result = await _gatePassRepository.GetManyAsync(c => c.isDelete == false && 
                (c.customer.code.Contains(searchText) || c.customer.nameVi.Contains(searchText) || c.code.Contains(searchText) || c.truck.plateNumber.Contains(searchText) ), 
                QueryIncludes.GATEPASSFULLINCLUDES);
                if (result.Count() == 0)
                    return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructEnumerableData(ResponseCode.ERR_NO_OBJECT_FOUND, ResponseText.ERR_SEARCH_FAIL, null);
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructEnumerableData(ResponseCode.SUCCESS, Mapper.Map<IEnumerable<GatePass>, IEnumerable<GatePassViewModel>>(result));
            }
            catch (Exception)
            {
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructEnumerableData(ResponseCode.ERR_NO_OBJECT_FOUND, ResponseText.ERR_SEARCH_FAIL, null); ;
            }
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByID(int ID)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var result = await _gatePassRepository.GetAsync(g => g.ID == ID && g.stateID != 0 && g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                if (result == null)
                    return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_NO_OBJECT_FOUND, "Không tìm thấy GatePass", null);
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.SUCCESS, Mapper.Map<GatePass, GatePassViewModel>(result));

            }
            catch (Exception)
            {
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_NO_OBJECT_FOUND, "Không tìm thấy GatePass", null);
            }
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByCode(string Code)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var result = await _gatePassRepository.GetAsync(g => g.code.Equals(Code) && g.stateID != 0 && g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                if (result == null)
                    return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_NO_OBJECT_FOUND, "Không tìm thấy GatePass", null);
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.SUCCESS, Mapper.Map<GatePass, GatePassViewModel>(result));
            }
            catch (Exception)
            {
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_NO_OBJECT_FOUND, "Không tìm thấy GatePass", null);
            }
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByRFID(string Code)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var result = await _gatePassRepository.GetAsync(g => g.RFIDCard.code.Equals(Code) && g.stateID != 0 && g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                if (result == null)
                    return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_NO_OBJECT_FOUND, ResponseText.ERR_SEC_NOT_FOUND_GATEPASS_VI, null);
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.SUCCESS, Mapper.Map<GatePass, GatePassViewModel>(result));
            }
            catch (Exception e)
            {
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_NO_OBJECT_FOUND, ResponseText.ERR_SEC_NOT_FOUND_GATEPASS_VI, null);
            }
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByPlateNumber(string PlateNumber)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var result = await _gatePassRepository.GetAsync(g => g.truck.plateNumber.Contains(PlateNumber) && g.stateID != 0 && g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                if (result == null)
                    return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_NO_OBJECT_FOUND, "Không tìm thấy GatePass", null);
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.SUCCESS, Mapper.Map<GatePass, GatePassViewModel>(result));
            }
            catch (Exception)
            {
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_NO_OBJECT_FOUND, "Không tìm thấy GatePass", null);
            }
        }

        public async Task<ResponseViewModel<GatePassViewModel>> UpdateGatePass(GatePassViewModel gatePassViewModel)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            try
            {
                if (gatePassViewModel != null)
                {
                    GatePass gatePassClient = Mapper.Map<GatePassViewModel, GatePass>(gatePassViewModel);
                    if (gatePassClient != null)
                    {
                        var gatePass = await _gatePassRepository.GetAsync(g => g.ID == gatePassViewModel.ID && g.stateID != 0 && g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                        // Update new driver
                        gatePass.driverID = gatePassClient.driver.ID;
                        // Update new truck
                        gatePass.truckID = gatePassClient.truck.ID;
                        // Update orders
                        foreach (var order in gatePass.orders)
                        {
                            order.gatePassID = null;// remove order of this gatePass
                            _orderRepository.Update(order);
                        }
                        if(await this.SaveChangesAsync()) 
                        {
                            foreach (var clientOrder in gatePassClient.orders)
                            {
                                var order = await _orderRepository.GetAsync(o => o.ID == clientOrder.ID);
                                order.gatePassID = gatePass.ID; // add current gatePass ID
                                _orderRepository.Update(order);
                            }
                            _gatePassRepository.Update(gatePass); // save new order & gatePass
                            if (await this.SaveChangesAsync()) 
                            {
                                gatePass = await _gatePassRepository.GetAsync(g => g.ID == gatePassViewModel.ID && g.stateID != 0 && g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                                gatePass.truckGroupID = findTruckGroup(gatePass);
                                _gatePassRepository.Update(gatePass);
                                if (await this.SaveChangesAsync())
                                {
                                    responseViewModel = await this.GetGatePassByCode(gatePass.code);
                                }
                                else
                                {
                                    responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_DB_FAIL_TO_SAVE, "Lưu đối tượng thất bại", null);
                                }
                            }
                        }

                    }
                    else
                    {
                        responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_UNKNOW, "Mapper bị lỗi", null);
                    }
                }
                else
                {
                    responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_UNKNOW, "Input rỗng", null);
                }
                return responseViewModel;
            }
            catch (Exception)
            {
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_UNKNOW, "Input rỗng", null);
            }
        }

        public ResponseViewModel<GenericResponseModel> AddDriverPicture(string fileName, byte[] fileContent)
        {
            ResponseViewModel<GenericResponseModel> response = new ResponseViewModel<GenericResponseModel>();
            string filePath = Constant.DriverCapturePath + fileName;
            // Write to file
            if (fileName == null || fileContent == null)
            {
                response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
            }
            else
            {
                File.WriteAllBytes(filePath, fileContent);
                response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.SUCCESS, true);
            }
            return response;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> UpdateGatePassWithRFIDCode(GatePassViewModel gatePassViewModel)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            try
            {
                if (gatePassViewModel != null)
                {
                    GatePass gatePassClient = Mapper.Map<GatePassViewModel, GatePass>(gatePassViewModel);
                    if (gatePassClient != null)
                    {
                        var result = await _gatePassRepository.GetAsync(g => g.ID == gatePassViewModel.ID && g.stateID != 0 && g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                        result.RFIDCardID = gatePassViewModel.RFIDCardID;
                        _gatePassRepository.Update(result);
                        if (await this.SaveChangesAsync())
                        {
                            responseViewModel = await this.GetGatePassByCode(gatePassViewModel.code);
                        }
                        else
                        {
                            responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_DB_FAIL_TO_SAVE, "Lưu đối tượng thất bại", null);
                        }
                    }
                    else
                    {
                        responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_UNKNOW, "Mapper bị lỗi", null);
                    }
                }
                else
                {
                    responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_UNKNOW, "Input rỗng", null);
                }
                return responseViewModel;
            }
            catch (Exception)
            {
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_SEC_UNKNOW, "Input rỗng", null); ;
            }
        }

        public async Task<ResponseViewModel<GenericResponseModel>> CreateRegisteredQueueItem(int gatePassID, string driverImageName, string employeeRFID, string driverRFID)
        {
            ResponseViewModel<GenericResponseModel> response = new ResponseViewModel<GenericResponseModel>();
            try
            {
                if (driverImageName == null || employeeRFID == null || driverRFID == null)
                    return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                Random r = new Random();
                /* Get GatePass by ID */
                GatePass gatePass = await _gatePassRepository.GetAsync(gt => gt.ID == gatePassID, QueryIncludes.GATEPASSFULLINCLUDES);
                RFIDCard card = await _RFIDCardRepository.GetAsync(ca => ca.code.Equals(driverRFID) && ca.isDelete == false);
                if (gatePass == null || card == null)
                    return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                /* Create Queue Element */
                if (gatePass.orders.ToArray()[0].orderTypeID != OrderTypeConst.INTERNALORDER) // Internal truck doesn't need to be queued just change state
                {
                    QueueList queue = new QueueList();
                    /* Code autogen */
                    queue.code = r.Next().ToString();
                    queue.estimateTime = gatePass.truck.KPI;
                    queue.truckID = gatePass.truckID;
                    //queue.truckGroupID = this.findTruckGroup(gatePass);
                    queue.gatePassID = gatePass.ID;
                    queue.gatePass = gatePass;
                    queue.queueTime = DateTime.Now;
                    // Assign lane
                    queue.laneID = await this.assignLane((int)gatePass.loadingBayID, (int)gatePass.truckID);
                    // Create order
                    queue.queueOrder = await _queueListRepository.CountAsync(qu => qu.gatePass.truckGroupID == queue.gatePass.truckGroupID) + 1;
                    queue.queueNumber = await _queueListRepository.CountAsync(qu => qu.gatePass.truckGroupID == queue.gatePass.truckGroupID) + 1;
                    // Receive picture
                    string filePath = Constant.DriverCapturePath + driverImageName;
                    gatePass.driverCamCapturePath = filePath;
                    gatePass.RFIDCardID = card.ID;
                    gatePass.stateID = GatepassState.STATE_REGISTERED;
                    // Save queue to db
                    _queueListRepository.Add(queue);
                }
                else
                {
                    string filePath = Constant.DriverCapturePath + driverImageName;
                    gatePass.driverCamCapturePath = filePath;
                    gatePass.RFIDCardID = card.ID;
                    gatePass.stateID = GatepassState.STATE_IN_SECURITY_CHECK_IN;
                }
                // Save db
                _gatePassRepository.Update(gatePass);
                if (await _unitOfWork.SaveChangesAsync())
                    return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.SUCCESS, true);
                else
                    return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
            }
            catch (Exception e)
            {
                return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
            }
        }

        public int findTruckGroup(GatePass gatePass)
        {
            if(gatePass.orders.First().orderTypeID == Constant.DELIVERYORDER)
            {
                if(gatePass.truckTyeID == Constant.PUMP)
                {
                    return Constant.TRUCKGROUP2X;
                }else
                {
                    return Constant.TRUCKGROUP1X;
                }
            }else if(gatePass.orders.First().orderTypeID == Constant.PURCHASEORDER)
            {
                return Constant.TRUCKGROUP3X;
            }
            else
            {
                return Constant.TRUCKGROUP3X;
            }
        }

        public async Task<int> assignLane(int loadingBayID, int truckID)
        {
            List<Lane> lanes = new List<Lane>();
            var lanesInLoadingBay = await _laneRepository.GetManyAsync(ln => ln.loadingBayID == loadingBayID && ln.isDelete == false && ln.status == 1);
            var truck = await _truckRepository.GetAsync(tr => tr.ID == truckID && tr.isDelete == false);
            foreach (var lane in lanesInLoadingBay)
            {
                if (lane.loadingTypeID == truck.loadingTypeID && lane.truckTypeID == truck.truckTypeID)
                    lanes.Add(lane);
            }

            Dictionary<int, int> lane_kpi = new Dictionary<int, int>();
            foreach (var lane in lanes)
            {
                int totalKPI = 0;
                var queueList = await _queueListRepository.GetManyAsync(qu => qu.laneID == lane.ID && qu.isDelete == false, QueryIncludes.QUEUELISTFULLINCLUDES);
                foreach (var queue in queueList)
                {
                    totalKPI += queue.truck.KPI;
                }
                lane_kpi.Add(lane.ID, totalKPI);
            }
            var laneID = lane_kpi.OrderBy(b => b.Value).FirstOrDefault().Key;
            var rlane = await _laneRepository.GetByIdAsync(laneID);
            return rlane.ID;
        }

        public async Task<ResponseViewModel<GenericResponseModel>> ReOrderQueue()
        {
            ResponseViewModel<GenericResponseModel> response = new ResponseViewModel<GenericResponseModel>();
            try
            {
                List<int> bakLaneID = new List<int>();
                var queueList = await _queueListRepository.GetManyAsync(qu => qu.isDelete == false, QueryIncludes.QUEUELISTFULLINCLUDES);
                foreach (var queue in queueList)
                {
                    bakLaneID.Add((int)queue.laneID);
                    queue.laneID = Constant.NULLLANE;
                    _queueListRepository.Update(queue);
                }
                if (await _unitOfWork.SaveChangesAsync())
                {
                    foreach (var queue in queueList)
                    {
                        queue.laneID = await assignLane((int)queue.gatePass.loadingBayID, (int)queue.truckID);
                        _queueListRepository.Update(queue);
                        if (!await _unitOfWork.SaveChangesAsync())
                        {
                            for (int i = 0; i < queueList.Count(); i++)
                            {
                                queueList.ToList()[i].laneID = bakLaneID[i];
                                _queueListRepository.Update(queueList.ToList()[i]);
                            }
                            await _unitOfWork.SaveChangesAsync();
                            return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                        }
                    }
                    return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.SUCCESS, true);
                }
                else
                {
                    return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
                }
            }
            catch (Exception)
            {
                return response = ResponseConstructor<GenericResponseModel>.ConstructBoolRes(ResponseCode.ERR_SEC_UNKNOW, false);
            }
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByDriverID(int ID)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var result = await _gatePassRepository.GetAsync(g => g.driverID == ID && g.stateID != 0 && g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                if (result == null)
                    return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_NO_OBJECT_FOUND, "Không tìm thấy GatePass", null);
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.SUCCESS, Mapper.Map<GatePass, GatePassViewModel>(result));
            }
            catch (Exception)
            {
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_NO_OBJECT_FOUND, "Không tìm thấy GatePass", null);
            }
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByDriverIDNo(string driverIDNo)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var result = await _gatePassRepository.GetAsync(g => g.driver.IDNo.Equals(driverIDNo) && g.stateID != 0 && g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                if (result == null)
                    return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_NO_OBJECT_FOUND, "Không tìm thấy GatePass", null);
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.SUCCESS, Mapper.Map<GatePass, GatePassViewModel>(result));
            }
            catch (Exception)
            {
                return responseViewModel = ResponseConstructor<GatePassViewModel>.ConstructData(ResponseCode.ERR_NO_OBJECT_FOUND, "Không tìm thấy GatePass", null);
            }
        }

        public async Task<ResponseViewModel<DOViewModel>> ImportDO(List<DOViewModel> listDO)
        {
            ResponseViewModel<DOViewModel> responseViewModel = new ResponseViewModel<DOViewModel>();
            try
            {
                for (int i = 0; i < listDO.Count; i++)
                {
                    //Check SO# in DB SaleOrder
                    string valueSOCode = listDO.ElementAt(i).sOCode;
                    var checkSOCode_SaleOrder = await _saleOrderRepository.GetAsync(c => c.Code.Equals(valueSOCode), null);
                    if (checkSOCode_SaleOrder == null)
                    {
                        // If SO# don't have in DB then create new record with SO#
                        try
                        {
                            SaleOrder saleOrder = new SaleOrder();
                            saleOrder.Code = valueSOCode;
                            saleOrder.isDelete = false;
                            _saleOrderRepository.Add(saleOrder);
                            await this.SaveChangesAsync();
                        }
                        catch
                        {
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Tạo SO#: " + valueSOCode + " thất bại";
                            return responseViewModel;
                        }
                    }

                    //Check DO# in DB DeliveryOrder
                    string valueDOCode = listDO.ElementAt(i).dOCode;
                    var checkDOCode_DeliveryOrder = await _deliveryOrderRepository.GetAsync(c => c.code.Equals(valueDOCode), null);
                    if (checkDOCode_DeliveryOrder == null)
                    {
                        //DO# in DeliveryOrder and Order have to R1-1
                        //Check DO# in Order table
                        var checkDOCode_Order = await _orderRepository.GetAsync(c => c.code.Equals(valueDOCode), null);
                        if (checkDOCode_Order != null)
                        {
                            // Data is not sync
                            // Return false
                            //return 0;
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Dữ liệu tại dòng thứ " + (i + 1).ToString() + " không đúng";
                            return responseViewModel;
                        }

                        // If DO# don't have in DB then create new record with DO#
                        // Create a new DO record in DeliveryOrder, a new Order record in Orders table
                        // Create a new DO record in DeliveryOrder
                        string format = "MM/dd/yyyy";
                        CultureInfo provider = CultureInfo.InvariantCulture;

                        DeliveryOrder deliveryOrder = new DeliveryOrder();
                        //deliveryOrder.doNumber = listDO.ElementAt(i).dOCode;
                        deliveryOrder.code = listDO.ElementAt(i).dOCode;
                        deliveryOrder.createDate = DateTime.ParseExact(listDO.ElementAt(i).dayCreate, format, provider);
                        deliveryOrder.saleOrder.Code = listDO.ElementAt(i).sOCode;
                        deliveryOrder.remark = listDO.ElementAt(i).remark;
                        deliveryOrder.sloc = listDO.ElementAt(i).sLoc;
                        deliveryOrder.isDelete = false;

                        var carrierCode = listDO.ElementAt(i).carrierCode;
                        var customerCode = listDO.ElementAt(i).customerCode;
                        var customerWarehouseName = listDO.ElementAt(i).warehouseName;
                        try
                        {
                            deliveryOrder.carrierVendor = await _carrierVendorRepository.GetAsync(ca => ca.code.Equals(carrierCode));
                            deliveryOrder.carrierVendorID = deliveryOrder.carrierVendor.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Không có carrier#: " + carrierCode + " trong cơ sở dữ liệu";
                            return responseViewModel;
                        }

                        try
                        {
                            deliveryOrder.customer = await _customerRepository.GetAsync(ca => ca.code.Equals(customerCode));
                            deliveryOrder.customerID = deliveryOrder.customer.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Không có customer#: " + customerCode + " trong cơ sở dữ liệu";
                            return responseViewModel;
                        }
                        try
                        {
                            deliveryOrder.deliveryOrderType = await _deliveryOrderTypeRepository.GetAsync(ca => ca.code.Equals("DO"));
                            deliveryOrder.doTypeID = deliveryOrder.deliveryOrderType.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Không có DOType#: DO trong cơ sở dữ liệu";
                            return responseViewModel;
                        }

                        try
                        {
                            deliveryOrder.customerWarehouse = await _customerWarehouseRepository.GetAsync(ca => ca.warehouseName.Equals(customerWarehouseName));
                            deliveryOrder.customerWarehouseID = deliveryOrder.customerWarehouse.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Không có Nhà kho khách hàng: " + customerWarehouseName + " trong cơ sở dữ liệu";
                            return responseViewModel;
                        }

                        _deliveryOrderRepository.Add(deliveryOrder);
                        await this.SaveChangesAsync();

                        // Create a new Order record in Orders table
                        Order order = new Order();
                        order.code = listDO.ElementAt(i).dOCode;
                        try
                        {
                            order.deliveryOrder = await _deliveryOrderRepository.GetAsync(ca => ca.code.Equals(deliveryOrder.code));
                            order.doID = order.deliveryOrder.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Dữ liệu tại dòng thứ " + (i + 1).ToString() + " không đúng";
                            return responseViewModel;
                        }
                        order.registGrossWeight = 0;
                        order.isDelete = false;
                        order.orderTypeID = Constant.DELIVERYORDER;

                        string valuePlantCode = listDO.ElementAt(i).plant;
                        try
                        {
                            var getPlantID = await _plantRepository.GetAsync(ca => ca.code.Equals(valuePlantCode), null);
                            order.plant = getPlantID;
                            order.plantID = order.plant.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Không có Plant#: " + valuePlantCode + " trong cơ sở dữ liệu";
                            return responseViewModel;
                        }

                        _orderRepository.Add(order);

                        await this.SaveChangesAsync();
                        //return 1;
                    }
                    else
                    {
                        //Check SO# in DO
                        if (checkDOCode_DeliveryOrder.saleOrder.Code != valueSOCode)
                        {
                            // Data is not sync
                            // Return false
                            //return 0;
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Dữ liệu tại dòng thứ " + (i + 1).ToString() + " không đúng";
                            return responseViewModel;
                        }

                        //DO# in DeliveryOrder and Order have to R1-1
                        //Check DO# in Order table
                        var checkDOCode_Order = await _orderRepository.GetAsync(c => c.code.Equals(valueDOCode), null);
                        if (checkDOCode_Order == null)
                        {
                            // Data is not sync
                            // Return false
                            //return 0;
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Dữ liệu tại dòng thứ " + (i + 1).ToString() + " không đúng";
                            return responseViewModel;
                        }
                    }

                    // Check DOItem# with Oders.code have in OrderMaterials or not?
                    // Check Oders.code in OrderMaterials
                    // Get orderID by  valueDOCode
                    // Get OrderID from DOCode
                    var getOrderFromDOCode = await _orderRepository.GetAsync(c => c.code.Equals(valueDOCode), null);
                    var checkOderCode_OrderMaterial = await _orderMaterialRepository.GetManyAsync(c => c.orderID == getOrderFromDOCode.ID, null);
                    if (checkOderCode_OrderMaterial.Count() > 0)
                    {
                        // If have, compare DOItem have in checkOderCode_OrderMaterial. list or not

                        for (int j = 0; i < checkOderCode_OrderMaterial.Count(); j++)
                        {
                            if ((listDO.ElementAt(i).dOCode + "_" + listDO.ElementAt(i).dOItemCode) == checkOderCode_OrderMaterial.ElementAt(j).code)
                            {
                                responseViewModel.errorCode = i + 1;
                                responseViewModel.errorText = "DOItem#: " + listDO.ElementAt(i).dOItemCode + " tại dòng:" + (i + 1).ToString() + " đã có trong hệ thống";
                                //return 0;
                                return responseViewModel;
                            }
                        }
                    }
                    //------
                    //Create new OrderMaterial record
                    //OrderMaterial code = DO#+"_"+DOItem#
                    OrderMaterial orderMaterial = new OrderMaterial();
                    orderMaterial.code = listDO.ElementAt(i).dOCode + "_" + listDO.ElementAt(i).dOItemCode;
                    orderMaterial.order = getOrderFromDOCode;
                    orderMaterial.orderID = getOrderFromDOCode.ID;
                    //orderMaterial.reht = 0;
                    orderMaterial.registQuantity = listDO.ElementAt(i).quanlity;
                    orderMaterial.isDelete = false;
                    var materialCode_material = listDO.ElementAt(i).materialCode;
                    try
                    {
                        orderMaterial.material = await _materialRepository.GetAsync(c => c.code.Equals(materialCode_material), null);
                        orderMaterial.materialID = orderMaterial.material.ID;
                    }
                    catch
                    {
                        responseViewModel.errorCode = i + 1;
                        responseViewModel.errorText = "Không có Material#: " + materialCode_material + " trong cơ sở dữ liệu";
                        return responseViewModel;
                    }

                    _orderMaterialRepository.Add(orderMaterial);
                    await this.SaveChangesAsync();
                }
                //return 1;
                responseViewModel.errorCode = 0;
                return responseViewModel;
            }
            catch
            {
                responseViewModel.errorCode = -1;
                responseViewModel.errorText = "Lỗi chưa xác định";
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<POViewModel>> ImportPO(List<POViewModel> listPO)
        {
            ResponseViewModel<POViewModel> responseViewModel = new ResponseViewModel<POViewModel>();
            try
            {
                for (int i = 0; i < listPO.Count; i++)
                {
                    //Check PO# in DB PurchaseOrder
                    string valuePOCode = listPO.ElementAt(i).pOCode;
                    var checkPOCode_PurchaseOrder = await _purchaseOrderRepository.GetAsync(c => c.code.Equals(valuePOCode), null);
                    if (checkPOCode_PurchaseOrder == null)
                    {
                        //PO# in PurchaseOrder and Order have to R1-1
                        //Check PO# in Order table
                        var checkPOCode_Order = await _orderRepository.GetAsync(c => c.code.Equals(valuePOCode), null);
                        if (checkPOCode_Order != null)
                        {
                            // Data is not sync
                            // Return false
                            //return 0;
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Dữ liệu tại dòng thứ " + (i + 1).ToString() + " không đúng";
                            return responseViewModel;
                        }

                        // If PO# don't have in DB then create new record with PO#
                        // Create a new PO record in PurchaseOrder, a new Order record in Orders table
                        // Create a new PO record in PurchaseOrder
                        PurchaseOrder purchaseOrder = new PurchaseOrder();
                        purchaseOrder.code = listPO.ElementAt(i).pOCode;
                        purchaseOrder.poNumber = listPO.ElementAt(i).pOCode;

                        try
                        {
                            purchaseOrder.purchaseOrderType = await _purchaseOrderTypeRepository.GetAsync(ca => ca.Code.Equals("PO"));
                            purchaseOrder.poTypeID = purchaseOrder.purchaseOrderType.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Không có POType#: PO trong cơ sở dữ liệu";
                            return responseViewModel;
                        }

                        purchaseOrder.sloc = listPO.ElementAt(i).sLoc;
                        purchaseOrder.remark = listPO.ElementAt(i).remark;
                        purchaseOrder.invoiceNumber = listPO.ElementAt(i).billCode;
                        purchaseOrder.isDelete = false;

                        var vendorCode = listPO.ElementAt(i).vendorCode;
                        //purchaseOrder.carrierVendor = await _carrierVendorRepository.GetAsync(ca => ca.code.Equals(vendorCode));
                        try
                        {
                            purchaseOrder.carrierVendor = await _carrierVendorRepository.GetAsync(ca => ca.code.Equals(vendorCode), null);
                            purchaseOrder.carrierVendorID = purchaseOrder.carrierVendor.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Không có Vendor#: " + vendorCode + " trong cơ sở dữ liệu";
                            return responseViewModel;
                        }
                        purchaseOrder.createDate = DateTime.Now;

                        _purchaseOrderRepository.Add(purchaseOrder);
                        await this.SaveChangesAsync();

                        // Create a new Order record in Orders table
                        Order order = new Order();
                        var getPOId = await _purchaseOrderRepository.GetAsync(ca => ca.code.Equals(valuePOCode) && ca.isDelete == false, null);
                        order.purchaseOrder = getPOId;
                        order.poID = order.purchaseOrder.ID;
                        order.code = order.purchaseOrder.code;
                        order.orderTypeID = Constant.PURCHASEORDER;
                        order.isDelete = false;
                        string valuePlantCode = listPO.ElementAt(i).plant;
                        try
                        {
                            var getPlantID = await _plantRepository.GetAsync(ca => ca.code.Equals(valuePlantCode), null);
                            order.plant = getPlantID;
                            order.plantID = order.plant.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Không có Plant#: " + valuePlantCode + " trong cơ sở dữ liệu";
                            return responseViewModel;
                        }

                        //order.grossWeight = 0;

                        _orderRepository.Add(order);
                        await this.SaveChangesAsync();
                    }
                    else
                    {
                        //PO# in DeliveryOrder and Order have to R1-1
                        //Check DO# in Order table
                        var checkPOCode_Order = await _orderRepository.GetAsync(c => c.code.Equals(valuePOCode), null);
                        if (checkPOCode_Order == null)
                        {
                            // Data is not sync
                            // Return false
                            //return 0;
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Dữ liệu tại dòng thứ " + (i + 1).ToString() + " không đúng";
                            return responseViewModel;
                        }
                    }

                    // Check POItem# with Oders.code have in OrderMaterials or not?
                    // Check Oders.code in OrderMaterials
                    // get orderID by valuePOCode
                    var getOrderID = await _orderRepository.GetAsync(c => c.code.Equals(valuePOCode), null);
                    var checkOderCode_OrderMaterial = await _orderMaterialRepository.GetManyAsync(c => c.orderID == getOrderID.ID, null);
                    if (checkOderCode_OrderMaterial.Count() > 0)
                    {
                        // If have, compare DOItem have in checkOderCode_OrderMaterial. list or not
                        //
                        for (int j = 0; i < checkOderCode_OrderMaterial.Count(); j++)
                        {
                            if ((listPO.ElementAt(i).pOCode + "_" + listPO.ElementAt(i).pOItemCode) == checkOderCode_OrderMaterial.ElementAt(j).code)
                            {
                                responseViewModel.errorCode = i + 1;
                                responseViewModel.errorText = "Dữ liệu tại dòng thứ " + (i + 1).ToString() + " đã có trong hệ thống";
                                //return 0;
                                return responseViewModel;
                            }

                        }
                    }
                    //------
                    //Create new OrderMaterial record
                    //OrderMaterial code = DO#+"_"+DOItem#
                    OrderMaterial orderMaterial = new OrderMaterial();
                    orderMaterial.code = listPO.ElementAt(i).pOCode + "_" + listPO.ElementAt(i).pOItemCode;
                    //orderMaterial.grossWeight = 0;
                    orderMaterial.registQuantity = listPO.ElementAt(i).quanlity;
                    //Get orderID by list.ElementAt(i).pOCode
                    //orderMaterial.orderID = list.ElementAt(i).pOCode;
                    var poCode_order = listPO.ElementAt(i).pOCode;
                    orderMaterial.order = getOrderID;
                    orderMaterial.orderID = orderMaterial.order.ID;

                    var materialCode_material = listPO.ElementAt(i).materialCode;
                    try
                    {
                        orderMaterial.material = await _materialRepository.GetAsync(c => c.code.Equals(materialCode_material), null);
                        orderMaterial.materialID = orderMaterial.material.ID;
                    }
                    catch
                    {
                        responseViewModel.errorCode = i + 1;
                        responseViewModel.errorText = "Không có Material#: " + materialCode_material + " trong cơ sở dữ liệu";
                        return responseViewModel;
                    }
                    

                    _orderMaterialRepository.Add(orderMaterial);
                    await this.SaveChangesAsync();
                }
                //return 1;
                responseViewModel.errorCode = 0;
                return responseViewModel;
            }
            catch
            {
                responseViewModel.errorCode = -1;
                responseViewModel.errorText = "Lỗi chưa xác định";
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<OrderViewModel>> GetAllDONotPlaned(string customerCode)
        {
            ResponseViewModel<OrderViewModel> responseViewModel = new ResponseViewModel<OrderViewModel>();
            try
            {
                //Get All Order record with GatepassID == null and doID != null
                var result = await _orderRepository.GetManyAsync(g => g.gatePassID == null && g.doID != null && g.isDelete == false, QueryIncludes.ORDERFULLINCUDES);
                if (result == null)
                {
                    responseViewModel.errorCode = -1;
                    responseViewModel.errorText = "Không tìm thấy DO chưa xếp";
                    return responseViewModel;
                }
                else
                {
                    responseViewModel.errorCode = 0;
                    List<OrderViewModel> listTemp = new List<OrderViewModel>();
                    for (int i = 0; i < result.Count(); i++)
                    {
                        OrderViewModel tempO = Mapper.Map<Order, OrderViewModel>(result.ElementAt(i));
                        //Get OrderMaterial each DO
                        var listOrderMaterial = await _orderMaterialRepository.GetManyAsync(g => g.orderID == tempO.ID, null);
                        if (listOrderMaterial.Count() != 0)
                        {
                            for (int j = 0; j < listOrderMaterial.Count(); j++)
                            {
                                OrderMaterialViewModel tempOM = Mapper.Map<OrderMaterial, OrderMaterialViewModel>(listOrderMaterial.ElementAt(j));
                                tempOM.order = null;
                                var material = await _materialRepository.GetAsync(g => g.ID == tempOM.materialID, null);
                                if (material != null)
                                {
                                    MaterialViewModel tempM = Mapper.Map<Material, MaterialViewModel>(material);
                                    var unit = await _unitTypeRepository.GetAsync(g => g.ID == material.unitID, null);
                                    if (unit != null)
                                    {
                                        UnitTypeViewModel tempU = Mapper.Map<UnitType, UnitTypeViewModel>(unit);
                                        tempM.unit = tempU;
                                        tempOM.material = tempM;
                                        tempO.orderMaterials.Add(tempOM);
                                    }
                                }
                            }
                        }

                        //Get DO each Order
                        var dO = await _deliveryOrderRepository.GetAsync(g => g.ID == tempO.doID, null);
                        if (dO != null)
                        {
                            DeliveryOrderViewModel tempDO = Mapper.Map<DeliveryOrder, DeliveryOrderViewModel>(dO);
                            tempO.deliveryOrder = tempDO;
                            //Get Customer each Order
                            var customer = await _customerRepository.GetAsync(g => g.code.Equals(customerCode) && g.isDelete == false, null);
                            if (customer != null)
                            {
                                CustomerViewModel tempCustomer = Mapper.Map<Customer, CustomerViewModel>(customer);
                                //Only Get DO from customer.ID == customerID
                                if (customer.ID != dO.customerID)
                                {
                                    continue;
                                }
                                else
                                {
                                    tempDO.customer = tempCustomer;
                                    listTemp.Add(tempO);
                                }  
                            }
                        }
                    }

                    if (listTemp == null || listTemp.Count <= 0)
                    {
                        responseViewModel.errorCode = -1;
                        responseViewModel.errorText = "Không tìm thấy DO chưa xếp của khách hàng được chọn";
                        return responseViewModel;
                    }

                    responseViewModel.errorCode = 0;
                    responseViewModel.responseDatas = (IEnumerable<OrderViewModel>)listTemp;
                    return responseViewModel;
                }
            }
            catch
            {
                responseViewModel.errorCode = -1;
                responseViewModel.errorText = "Lỗi chưa xác định";
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<OrderViewModel>> GetAllPONotPlaned(string vendorCode)
        {
            ResponseViewModel<OrderViewModel> responseViewModel = new ResponseViewModel<OrderViewModel>();
            try
            {
                //Get All Order record with GatepassID == null and poID != null
                var result = await _orderRepository.GetManyAsync(g => g.gatePassID == null && g.poID != null && g.isDelete == false, null);
                if (result == null)
                {
                    responseViewModel.errorCode = -1;
                    responseViewModel.errorText = "Không tìm thấy PO chưa xếp";
                    return responseViewModel;
                }
                else
                {
                    responseViewModel.errorCode = 0;
                    List<OrderViewModel> listTemp = new List<OrderViewModel>();
                    for (int i = 0; i < result.Count(); i++)
                    {
                        OrderViewModel tempO = Mapper.Map<Order, OrderViewModel>(result.ElementAt(i));
                        //Get OrderMaterial each PO
                        var listOrderMaterial = await _orderMaterialRepository.GetManyAsync(g => g.orderID == tempO.ID, null);
                        if (listOrderMaterial.Count() != 0)
                        {
                            for (int j = 0; j < listOrderMaterial.Count(); j++)
                            {
                                OrderMaterialViewModel tempOM = Mapper.Map<OrderMaterial, OrderMaterialViewModel>(listOrderMaterial.ElementAt(j));
                                tempOM.order = null;
                                var material = await _materialRepository.GetAsync(g => g.ID == tempOM.materialID, null);
                                if (material != null)
                                {
                                    MaterialViewModel tempM = Mapper.Map<Material, MaterialViewModel>(material);
                                    var unit = await _unitTypeRepository.GetAsync(g => g.ID == material.unitID, null);
                                    if (unit != null)
                                    {
                                        UnitTypeViewModel tempU = Mapper.Map<UnitType, UnitTypeViewModel>(unit);
                                        tempM.unit = tempU;
                                        tempOM.material = tempM;
                                        tempO.orderMaterials.Add(tempOM);
                                    }
                                }
                            }
                        }

                        //Get DO each Order
                        var pO = await _purchaseOrderRepository.GetAsync(g => g.ID == tempO.poID, null);
                        if (pO != null)
                        {
                            PurchaseOrderViewModel tempPO = Mapper.Map<PurchaseOrder, PurchaseOrderViewModel>(pO);
                            tempO.purchaseOrder = tempPO;
                            //Get Vendor each Order
                            var vendor = await _carrierVendorRepository.GetAsync(g => g.code.Equals(vendorCode) && g.isDelete == false, null);
                            if (vendor != null)
                            {
                                CarrierVendorViewModel tempVendor = Mapper.Map<CarrierVendor, CarrierVendorViewModel>(vendor);
                                //Only Get PO from vendor.ID == carrierVendorID
                                if (vendor.ID != pO.carrierVendorID)
                                {
                                    continue;
                                }
                                else
                                {
                                    tempPO.carrierVendor = tempVendor;
                                    listTemp.Add(tempO);
                                }
                            }
                        }
                    }

                    if (listTemp == null || listTemp.Count <= 0)
                    {
                        responseViewModel.errorCode = -1;
                        responseViewModel.errorText = "Không tìm thấy DO chưa xếp của khách hàng được chọn";
                        return responseViewModel;
                    }

                    responseViewModel.errorCode = 0;
                    responseViewModel.responseDatas = (IEnumerable<OrderViewModel>)listTemp;
                    return responseViewModel;
                }
            }
            catch
            {
                responseViewModel.errorCode = -1;
                responseViewModel.errorText = "Lỗi chưa xác định";
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<CreateGatePassViewModel>> CreateGatepassWithDO(CreateGatePassViewModel createGatePassViewModel)
        {
            ResponseViewModel<CreateGatePassViewModel> responseViewModel = new ResponseViewModel<CreateGatePassViewModel>();
            try
            {
                //Search GatePass_Code in DB 
                var result = await _gatePassRepository.GetAsync(g => g.code.Equals(createGatePassViewModel.gatePassViewModel.code) && g.isDelete == false, null);
                if (result != null)
                {
                    //If have return false
                    responseViewModel.errorCode = -1;
                    responseViewModel.errorText = "Mã Gatepass đã có trong cơ sở dữ liệu";
                    return responseViewModel;
                }
                else
                {
                    //If don't have, create GatePass
                    GatePass gatePass = new GatePass();
                    gatePass.code = createGatePassViewModel.gatePassViewModel.code;
                    gatePass.createDate = (DateTime)createGatePassViewModel.gatePassViewModel.createDate;
                    gatePass.isDelete = false;
                    gatePass.loadingBayID = createGatePassViewModel.gatePassViewModel.loadingBayID;
                    gatePass.enterTime = DateTime.Now;
                    gatePass.leaveTime = DateTime.Now;
                    //gatePass.ID = 1;
                    if (createGatePassViewModel.gatePassViewModel.driver != null)
                    {
                        try
                        {
                            //var driverCode = createGatePassViewModel.gatePassViewModel.code;
                            //var driver = await _driverRepository.GetAsync(g => g.code.Equals(driverCode) && g.isDelete == false, null);
                            gatePass.driverID = createGatePassViewModel.gatePassViewModel.driver.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = -1;
                            responseViewModel.errorText = "Không có tài xế được chọn trong cơ sở dữ liệu";
                            return responseViewModel;
                        }
                    }
                    if (createGatePassViewModel.gatePassViewModel.truck != null)
                    {
                        try
                        {
                            //gatePass.truck = await _truckRepository.GetAsync(g => ((g.ID == createGatePassViewModel.gatePassViewModel.truck.ID) && (g.isDelete == false)), null);
                            gatePass.truckID = createGatePassViewModel.gatePassViewModel.truck.ID;
                            gatePass.stateID = 2;
                            gatePass.truckTyeID = createGatePassViewModel.gatePassViewModel.truck.truckTypeID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = -1;
                            responseViewModel.errorText = "Không có xe được chọn trong cơ sở dữ liệu";
                            return responseViewModel;
                        }
                    }
                    else
                    {
                        //responseViewModel.errorCode = -1;
                        //responseViewModel.errorText = "Cần chọn xe để tạo Gatepass";
                        //return responseViewModel;
                        gatePass.stateID = 1;
                    }

                    _gatePassRepository.Add(gatePass);
                    await this.SaveChangesAsync();
                }

                // Add GatePass_ID into listOrder
                //Get GatePassID with createGatePassViewModel.gatePassViewModel.code
                var gatepass = await _gatePassRepository.GetAsync(g => g.code.Equals(createGatePassViewModel.gatePassViewModel.code) && g.isDelete == false, null);
                for (int i = 0; i < createGatePassViewModel.listOrderViewModel.Count; i++)
                {
                    var oderCode = createGatePassViewModel.listOrderViewModel.ElementAt(i).code;
                    var order = await _orderRepository.GetAsync(g => g.code.Equals(oderCode) && g.isDelete == false, null);
                    order.gatePassID = gatepass.ID;
                    _orderRepository.Update(order);
                    await this.SaveChangesAsync();
                }

                //Update TruckGroupID for fatePass
                var updateGatePass = await _gatePassRepository.GetAsync(g => g.code.Equals(createGatePassViewModel.gatePassViewModel.code) && g.isDelete == false, QueryIncludes.QUEUE_GATEPASS_ORDER_INCLUDES);
                var getTruckGroupID = findTruckGroup(updateGatePass);
                updateGatePass.truckGroupID = getTruckGroupID;
                _gatePassRepository.Update(updateGatePass);

                await this.SaveChangesAsync();
                return responseViewModel;
            }
            catch (Exception e)
            {
                throw e;
                //responseViewModel.errorCode = -1;
                //responseViewModel.errorText = "Lỗi chưa xác định";
                //return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<CreateGatePassViewModel>> CreateGatepassWithPO(CreateGatePassViewModel createGatePassViewModel)
        {
            ResponseViewModel<CreateGatePassViewModel> responseViewModel = new ResponseViewModel<CreateGatePassViewModel>();
            try
            {
                //Search GatePass_Code in DB 
                var result = await _gatePassRepository.GetAsync(g => g.code.Equals(createGatePassViewModel.gatePassViewModel.code) && g.isDelete == false, null);
                if (result != null)
                {
                    //If have return false
                    responseViewModel.errorCode = -1;
                    responseViewModel.errorText = "Mã Gatepass đã có trong cơ sở dữ liệu";
                    return responseViewModel;
                }
                else
                {
                    //If don't have, create GatePass
                    GatePass gatePass = new GatePass();
                    gatePass.code = createGatePassViewModel.gatePassViewModel.code;
                    gatePass.createDate = (DateTime)createGatePassViewModel.gatePassViewModel.createDate;
                    gatePass.isDelete = false;
                    gatePass.loadingBayID = createGatePassViewModel.gatePassViewModel.loadingBayID;
                    gatePass.enterTime = DateTime.Now;
                    gatePass.leaveTime = DateTime.Now;
                    //gatePass.ID = 1;
                    if (createGatePassViewModel.gatePassViewModel.driver != null)
                    {
                        try
                        {
                            //var driverCode = createGatePassViewModel.gatePassViewModel.code;
                            //var driver = await _driverRepository.GetAsync(g => g.code.Equals(driverCode) && g.isDelete == false, null);
                            gatePass.driverID = createGatePassViewModel.gatePassViewModel.driver.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = -1;
                            responseViewModel.errorText = "Không có tài xế được chọn trong cơ sở dữ liệu";
                            return responseViewModel;
                        }
                    }
                    if (createGatePassViewModel.gatePassViewModel.truck != null)
                    {
                        try
                        {
                            //gatePass.truck = await _truckRepository.GetAsync(g => ((g.ID == createGatePassViewModel.gatePassViewModel.truck.ID) && (g.isDelete == false)), null);
                            gatePass.truckID = createGatePassViewModel.gatePassViewModel.truck.ID;
                            gatePass.stateID = 2;
                            gatePass.truckTyeID = createGatePassViewModel.gatePassViewModel.truck.truckTypeID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = -1;
                            responseViewModel.errorText = "Không có xe được chọn trong cơ sở dữ liệu";
                            return responseViewModel;
                        }
                    }
                    else
                    {
                        //responseViewModel.errorCode = -1;
                        //responseViewModel.errorText = "Cần chọn xe để tạo Gatepass";
                        //return responseViewModel;
                        gatePass.stateID = 1;
                    }

                    _gatePassRepository.Add(gatePass);
                    await this.SaveChangesAsync();
                }

                // Add GatePass_ID into listOrder
                //Get GatePassID with createGatePassViewModel.gatePassViewModel.code
                var gatepass = await _gatePassRepository.GetAsync(g => g.code.Equals(createGatePassViewModel.gatePassViewModel.code) && g.isDelete == false, null);
                for (int i = 0; i < createGatePassViewModel.listOrderViewModel.Count; i++)
                {
                    var oderCode = createGatePassViewModel.listOrderViewModel.ElementAt(i).code;
                    var order = await _orderRepository.GetAsync(g => g.code.Equals(oderCode) && g.isDelete == false, null);
                    order.gatePassID = gatepass.ID;
                    _orderRepository.Update(order);
                    await this.SaveChangesAsync();
                }

                //Update TruckGroupID for fatePass
                var updateGatePass = await _gatePassRepository.GetAsync(g => g.code.Equals(createGatePassViewModel.gatePassViewModel.code) && g.isDelete == false, QueryIncludes.QUEUE_GATEPASS_ORDER_INCLUDES);
                var getTruckGroupID = findTruckGroup(updateGatePass);
                updateGatePass.truckGroupID = getTruckGroupID;
                _gatePassRepository.Update(updateGatePass);

                await this.SaveChangesAsync();
                return responseViewModel;
            }
            catch (Exception e)
            {
                throw e;
                //responseViewModel.errorCode = -1;
                //responseViewModel.errorText = "Lỗi chưa xác định";
                //return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<CreateGatePassViewModel>> CreateGatepassSP(CreateGatePassViewModel createGatePassViewModel)
        {
            ResponseViewModel<CreateGatePassViewModel> responseViewModel = new ResponseViewModel<CreateGatePassViewModel>();
            try
            {
                //Search GatePass_Code in DB 
                var result = await _gatePassRepository.GetAsync(g => g.code.Equals(createGatePassViewModel.gatePassViewModel.code) && g.isDelete == false, null);
                if (result != null)
                {
                    //If have return false
                    responseViewModel.errorCode = -1;
                    responseViewModel.errorText = "Mã Gatepass đã có trong cơ sở dữ liệu";
                    return responseViewModel;
                }
                else
                {
                    //If don't have, create GatePass
                    GatePass gatePass = new GatePass();
                    gatePass.code = createGatePassViewModel.gatePassViewModel.code;
                    gatePass.createDate = (DateTime)createGatePassViewModel.gatePassViewModel.createDate;
                    gatePass.isDelete = false;
                    gatePass.loadingBayID = createGatePassViewModel.gatePassViewModel.loadingBayID;
                    gatePass.enterTime = DateTime.Now;
                    gatePass.leaveTime = DateTime.Now;
                    //gatePass.ID = 1;
                    if (createGatePassViewModel.gatePassViewModel.driver != null)
                    {
                        try
                        {
                            //var driverCode = createGatePassViewModel.gatePassViewModel.code;
                            //var driver = await _driverRepository.GetAsync(g => g.code.Equals(driverCode) && g.isDelete == false, null);
                            gatePass.driverID = createGatePassViewModel.gatePassViewModel.driver.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = -1;
                            responseViewModel.errorText = "Không có tài xế được chọn trong cơ sở dữ liệu";
                            return responseViewModel;
                        }
                    }

                    if (createGatePassViewModel.gatePassViewModel.truck != null)
                    {
                        try
                        {
                            //gatePass.truck = await _truckRepository.GetAsync(g => ((g.ID == createGatePassViewModel.gatePassViewModel.truck.ID) && (g.isDelete == false)), null);
                            gatePass.truckID = createGatePassViewModel.gatePassViewModel.truck.ID;
                            gatePass.truckTyeID = createGatePassViewModel.gatePassViewModel.truck.truckTypeID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = -1;
                            responseViewModel.errorText = "Không có xe được chọn trong cơ sở dữ liệu";
                            return responseViewModel;
                        }
                    }
                    if ((createGatePassViewModel.gatePassViewModel.truck == null)
                        || (createGatePassViewModel.gatePassViewModel.driver == null))
                    {
                        gatePass.stateID = 1;
                    }
                    else
                    {
                        gatePass.stateID = 2;
                    }

                    _gatePassRepository.Add(gatePass);
                    await this.SaveChangesAsync();
                }

                // Add GatePass_ID into listOrder
                //Get GatePassID with createGatePassViewModel.gatePassViewModel.code
                var gatepass = await _gatePassRepository.GetAsync(g => g.code.Equals(createGatePassViewModel.gatePassViewModel.code) && g.isDelete == false, null);
                Order order = new Order();
                order.code = gatepass.code + "_SP";
                if (createGatePassViewModel.gatePassViewModel.truck.truckType.description.Equals(Constant.TRASH_CAR))
                {
                    //Get ID of truckType "Xe rác"
                    var orderTemp = await _orderTypeRepository.GetAsync(g => g.description.Equals(Constant.TRASH_CAR) && g.isDelete == false, null);
                    order.orderTypeID = orderTemp.ID;
                }  
                else if (createGatePassViewModel.gatePassViewModel.truck.truckType.description.Equals(Constant.TRUCK_INTERNAL))
                {
                    ////Get ID of truckType "Xe nội bộ"
                    var orderTemp = await _orderTypeRepository.GetAsync(g => g.description.Equals(Constant.TRUCK_INTERNAL) && g.isDelete == false, null);
                    order.orderTypeID = orderTemp.ID;
                }
                else
                {
                    responseViewModel.errorCode = -1;
                    responseViewModel.errorText = "Không phải loại xe đặc biệt";
                    return responseViewModel;
                }
                order.gatePassID = gatepass.ID;
                _orderRepository.Add(order);
                await this.SaveChangesAsync();

                //Update TruckGroupID for gatePass
                var updateGatePass = await _gatePassRepository.GetAsync(g => g.code.Equals(createGatePassViewModel.gatePassViewModel.code) && g.isDelete == false, QueryIncludes.QUEUE_GATEPASS_ORDER_INCLUDES);
                var getTruckGroupID = findTruckGroup(updateGatePass);
                //truckGroup 3xxx
                updateGatePass.truckGroupID = Constant.TRUCKGROUP3X;
                _gatePassRepository.Update(updateGatePass);

                await this.SaveChangesAsync();
                return responseViewModel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ResponseViewModel<LoadingBayViewModel>> GetAllLoadingBay()
        {
            ResponseViewModel<LoadingBayViewModel> responseViewModel = new ResponseViewModel<LoadingBayViewModel>();
            try
            {
                var result = await _loadingBayRepository.GetManyAsync(g => g.isDelete == false, null);
                if (result == null)
                {
                    responseViewModel.errorCode = -1;
                    responseViewModel.errorText = "Không tìm thấy LoadingBay";
                    return responseViewModel;
                }
                else
                {
                    List<LoadingBayViewModel> listTemp = new List<LoadingBayViewModel>();
                    for (int i = 0; i < result.Count(); i++)
                    {
                        LoadingBayViewModel tempLD = Mapper.Map<LoadingBay, LoadingBayViewModel>(result.ElementAt(i));
                        listTemp.Add(tempLD);
                    }
                    responseViewModel.errorCode = 0;
                    responseViewModel.responseDatas = (IEnumerable<LoadingBayViewModel>)listTemp;
                    return responseViewModel;
                }
            }
            catch
            {
                responseViewModel.errorCode = -1;
                responseViewModel.errorText = "Lỗi chưa xác định";
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<LoadingBayViewModel>> GetLoadingBayByTruck(string truckCode)
        {
            ResponseViewModel<LoadingBayViewModel> responseViewModel = new ResponseViewModel<LoadingBayViewModel>();
            try
            {
                //return Value
                List<LoadingBayViewModel> returnValue = new List<LoadingBayViewModel>();
                //Get truck by code
                var truck = await _truckRepository.GetAsync(g => g.code.Equals(truckCode) && g.isDelete == false, null);
                //Get All Lane
                var allLane = await _laneRepository.GetManyAsync(g => g.isDelete == false, null);

                //Check input truck != null, allLane.Count() > 0
                if ((truck == null) || (allLane.Count() == 0))
                {
                    responseViewModel.errorCode = 1;
                    responseViewModel.errorText = "Lỗi input";
                    return responseViewModel;
                }

                //Search in allLane have
                for (int i = 0; i < allLane.Count(); i++)
                {
                    bool isHave = false; // flag Check LoadingBayID

                    //Check LoadingBayID in current lane with LoadingBayID in return value
                    if (returnValue.Count > 0)
                    {
                        for (int j = 0; j < returnValue.Count; j++)
                        {
                            if (returnValue.ElementAt(j).ID == allLane.ElementAt(i).loadingBayID)
                            {
                                isHave = true;
                                break;
                            }
                        }
                    }

                    if (!isHave)
                    {
                        // Check truckTypeID, loadingTypeID in truck and current lane
                        if ((truck.truckTypeID == allLane.ElementAt(i).truckTypeID) &&
                                (truck.loadingTypeID == allLane.ElementAt(i).loadingTypeID))
                        {
                            //Add loadingBay into returnValue
                            var loadingBayID = allLane.ElementAt(i).loadingBayID;
                            var loadingBay = await _loadingBayRepository.GetAsync(g => g.ID == loadingBayID && g.isDelete == false, null);
                            LoadingBayViewModel tempLD = Mapper.Map<LoadingBay, LoadingBayViewModel>(loadingBay);
                            returnValue.Add(tempLD);
                        }
                    }
                }

                responseViewModel.errorCode = 0;
                responseViewModel.responseDatas = (IEnumerable<LoadingBayViewModel>)returnValue;
                return responseViewModel;
            }
            catch
            {
                responseViewModel.errorCode = -1;
                responseViewModel.errorText = "Lỗi chưa xác định";
                return responseViewModel;
            }
        }

        public async Task<ResponseViewModel<GatePassViewModel>> DeleteGatePass(int ID)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            try
            {
                var gatePass = await _gatePassRepository.GetAsync(gt => gt.ID == ID);
                gatePass.isDelete = true;
                _gatePassRepository.Update(gatePass);
                if (await this.SaveChangesAsync())
                {
                    responseViewModel.booleanResponse = true;
                    responseViewModel.errorCode = 0;
                    responseViewModel.errorText = ResponseText.DELETE_GATEPASS_SUCCESS;
                    return await this.GetAllGatePass();
                }
                else
                {
                    responseViewModel.booleanResponse = false;
                    responseViewModel.errorCode = -1;
                    responseViewModel.errorText = ResponseText.DELETE_GATEPASS_FAIL;
                }
                return responseViewModel;
            }
            catch (Exception e)
            {
                responseViewModel.booleanResponse = false;
                responseViewModel.errorCode = -1;
                responseViewModel.errorText = e.ToString();
                return responseViewModel;
            }
        }
    }
}
