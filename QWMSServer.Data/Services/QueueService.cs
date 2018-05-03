using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
                            IMaterialRepository materialRepository)
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
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetAllGatePass()
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            var result = await _gatePassRepository.GetManyAsync(c => true, QueryIncludes.GATEPASSFULLINCLUDES);
            if (result == null)
                responseViewModel.errorText = "No GatePass Found";
            responseViewModel.responseDatas = Mapper.Map<IEnumerable<GatePass>, IEnumerable<GatePassViewModel>>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByID(int ID)
        {
            var result = await _gatePassRepository.GetAsync(g => g.ID == ID || g.stateID != 0 || g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            if (result == null)
                responseViewModel.errorText = "No GatePass Found";
            responseViewModel.responseData = Mapper.Map<GatePass, GatePassViewModel>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByCode(string Code)
        {
            var result = await _gatePassRepository.GetAsync(g => g.code.Equals(Code) || g.stateID != 0 || g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            if (result == null)
                responseViewModel.errorText = "No GatePass Found";
            responseViewModel.responseData = Mapper.Map<GatePass, GatePassViewModel>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByRFID(string Code)
        {
            var result = await _gatePassRepository.GetAsync(g => g.RFIDCard.code.Equals(Code) || g.stateID != 0 || g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            if (result == null)
                responseViewModel.errorText = "No GatePass Found";
            responseViewModel.responseData = Mapper.Map<GatePass, GatePassViewModel>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByPlateNumber(string PlateNumber)
        {
            var result = await _gatePassRepository.GetAsync(g => g.truck.plateNumber.Equals(PlateNumber) || g.stateID != 0 || g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            if (result == null)
                responseViewModel.errorText = "No GatePass Found";
            responseViewModel.responseData = Mapper.Map<GatePass, GatePassViewModel>(result);
            return responseViewModel;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> UpdateGatePass(GatePassViewModel gatePassViewModel)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            if (gatePassViewModel != null)
            {
                GatePass gatePassClient = Mapper.Map<GatePassViewModel, GatePass>(gatePassViewModel);
                if (gatePassClient != null)
                {
                    var result = await _gatePassRepository.GetAsync(g => g.ID == gatePassViewModel.ID || g.stateID != 0 || g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                    result.driverID = gatePassClient.driver.ID;
                    // State Update Sequence
                    // result.stateID = xxxx;
                    _gatePassRepository.Update(result);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetGatePassByCode(gatePassViewModel.code);
                    }
                    else
                    {
                        responseViewModel.errorText = "Save Fail";
                    }
                }
                else
                {
                    responseViewModel.errorText = "Cannot map";
                }
            }
            else
            {
                responseViewModel.errorText = "Input is null";
            }
            return responseViewModel;
        }

        public bool AddDriverPicture(string fileName, byte[] fileContent)
        {
            string filePath = Constant.DriverCapturePath + fileName;
            // Write to file
            if (fileName == null || fileContent == null)
                return false;
            File.WriteAllBytes(filePath, fileContent);
            return true;
        }

        public async Task<ResponseViewModel<GatePassViewModel>> UpdateGatePassWithRFIDCode(GatePassViewModel gatePassViewModel)
        {
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            if (gatePassViewModel != null)
            {
                GatePass gatePassClient = Mapper.Map<GatePassViewModel, GatePass>(gatePassViewModel);
                if (gatePassClient != null)
                {
                    var result = await _gatePassRepository.GetAsync(g => g.ID == gatePassViewModel.ID || g.stateID != 0 || g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
                    result.RFIDCardID = gatePassViewModel.RFIDCardID;
                    _gatePassRepository.Update(result);
                    if (await this.SaveChangesAsync())
                    {
                        responseViewModel = await this.GetGatePassByCode(gatePassViewModel.code);
                    }
                    else
                    {
                        responseViewModel.errorText = "Save Fail";
                    }
                }
                else
                {
                    responseViewModel.errorText = "Cannot map";
                }
            }
            else
            {
                responseViewModel.errorText = "Input is null";
            }
            return responseViewModel;
        }

        public async Task<bool> CreateRegisteredQueueItem(int gatePassID, string driverImageName, string employeeRFID, string driverRFID)
        {
            try
            {
                if (driverImageName == null || employeeRFID == null || driverRFID == null)
                    return false;
                Random r = new Random();
                /* Get GatePass by ID */
                GatePass gatePass = await _gatePassRepository.GetAsync(gt => gt.ID == gatePassID, QueryIncludes.GATEPASSFULLINCLUDES);
                RFIDCard card = await _RFIDCardRepository.GetAsync(ca => ca.code.Equals(driverRFID) && ca.isDelete == false);
                Employee employee = await _employeepository.GetAsync(emp => emp.rfidCard.code.Equals(employeeRFID) && emp.isDelete == false, QueryIncludes.EMPLOYEEFULLINCLUDES);
                if (gatePass == null || card == null || employee == null)
                    return false;
                bool found = false;
                foreach (var group in employee.groupMaps)
                {
                    foreach (var function in group.employeeGroup.functionMaps)
                    {
                        if (function.systemFunction.API.Equals("CreateRegisteredQueueItem"))
                            found = true;
                    }
                }
                if (found == false)
                    return false;
                /* Create Queue Element */
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
                // Receive picture
                string filePath = Constant.DriverCapturePath + driverImageName;
                gatePass.driverCamCapturePath = filePath;
                gatePass.RFIDCardID = card.ID;
                // Save db
                _queueListRepository.Add(queue);
                _gatePassRepository.Update(gatePass);
                if (await _unitOfWork.SaveChangesAsync())
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
                return false;
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
            var lanesInLoadingBay = await _laneRepository.GetManyAsync(ln => ln.loadingBayID == loadingBayID && ln.isDelete == false);
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

        public async Task<bool> ReOrderQueue()
        {
            List<int> bakLaneID = new List<int>();
            var queueList = await _queueListRepository.GetManyAsync(qu => qu.isDelete == false, QueryIncludes.QUEUELISTFULLINCLUDES);
            foreach (var queue in queueList)
            {
                bakLaneID.Add((int)queue.laneID);
                queue.laneID = Constant.NULLLANE;
                _queueListRepository.Update(queue);
            }
            if(await _unitOfWork.SaveChangesAsync())
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
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<bool> AddGatePassToQueue(GatePassViewModel gatePassViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel<GatePassViewModel>> GetGatePassByDriverID(int ID)
        {
            var result = await _gatePassRepository.GetAsync(g => g.driverID == ID || g.stateID != 0 || g.isDelete == false, QueryIncludes.GATEPASSFULLINCLUDES);
            ResponseViewModel<GatePassViewModel> responseViewModel = new ResponseViewModel<GatePassViewModel>();
            if (result == null)
                responseViewModel.errorText = "No GatePass Found";
            responseViewModel.responseData = Mapper.Map<GatePass, GatePassViewModel>(result);
            return responseViewModel;
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
                        deliveryOrder.doNumber = listDO.ElementAt(i).dOCode;
                        deliveryOrder.code = listDO.ElementAt(i).dOCode;
                        deliveryOrder.createDate = DateTime.ParseExact(listDO.ElementAt(i).dayCreate, format, provider);
                        deliveryOrder.soNumber = listDO.ElementAt(i).sOCode;
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
                            order.deliveryOrder = await _deliveryOrderRepository.GetAsync(ca => ca.doNumber.Equals(deliveryOrder.doNumber)); ;
                            order.doID = order.deliveryOrder.ID;
                        }
                        catch
                        {
                            responseViewModel.errorCode = i + 1;
                            responseViewModel.errorText = "Dữ liệu tại dòng thứ " + (i + 1).ToString() + " không đúng";
                            return responseViewModel;
                        }
                        order.grossWeight = 0;
                        order.isDelete = false;
                        _orderRepository.Add(order);

                        await this.SaveChangesAsync();
                        //return 1;
                    }
                    else
                    {
                        //Check SO# in DO
                        if (checkDOCode_DeliveryOrder.soNumber != valueSOCode)
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
                    orderMaterial.grossWeight = 0;
                    orderMaterial.quantity = listDO.ElementAt(i).quanlity;
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
    }
}
