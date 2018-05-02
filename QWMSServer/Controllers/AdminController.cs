using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace QWMSServer.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminController : ApiController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /* Customer API */
        [HttpGet]
        [Route("Customer/GetAll", Name ="CustomerGetAll")]
        public async Task<ResponseViewModel<CustomerViewModel>> GetAllCustomer()
        {
            return await _adminService.GetAllCustomer();
        }

        [HttpGet]
        [Route("Customer/SearchByCode/code={code}", Name ="CustomerSearchByCode")]
        public async Task<ResponseViewModel<CustomerViewModel>> SearchCustomer(string code)
        {
            return await _adminService.SearchCustomer(code);
        }

        [HttpPost]
        [Route("Customer/Add", Name = "CustomerAddNew")]
        public async Task<ResponseViewModel<CustomerViewModel>> CreateNewCustomer([FromBody]CustomerViewModel customerView)
        {
            return await _adminService.CreateNewCustomer(customerView);
        }

        [HttpPost]
        [Route("Customer/Update", Name = "CustomerUpdate")]
        public async Task<ResponseViewModel<CustomerViewModel>> UpdateCustomer([FromBody]CustomerViewModel customerView)
        {
            return await _adminService.UpdateCustomer(customerView);
        }

        [HttpPost]
        [Route("Customer/Delete", Name = "CustomerDelte")]
        public async Task<ResponseViewModel<CustomerViewModel>> DeleteCustomer([FromBody]CustomerViewModel customerView)
        {
            return await _adminService.DeleteCustomer(customerView);
        }


        /* Driver API */
        [HttpGet]
        [Route("Driver/GetAll", Name = "DriverGetAll")]
        public async Task<ResponseViewModel<DriverViewModel>> GetAllDriver()
        {
            return await _adminService.GetAllDriver();
        }

        [HttpGet]
        [Route("Driver/SearchByCode/code={code}", Name = "DriverSearchByCode")]
        public async Task<ResponseViewModel<DriverViewModel>> SearchDriver(string code)
        {
            return await _adminService.SearchDriver(code);
        }

        [HttpPost]
        [Route("Driver/Add", Name = "DriverAddNew")]
        public async Task<ResponseViewModel<DriverViewModel>> CreateNewDriver([FromBody]DriverViewModel driverView)
        {
            return await _adminService.CreateNewDriver(driverView);
        }

        [HttpPost]
        [Route("Driver/Update", Name = "DriverUpdate")]
        public async Task<ResponseViewModel<DriverViewModel>> UpdateDriver([FromBody]DriverViewModel driverView)
        {
            return await _adminService.UpdateDriver(driverView);
        }

        [HttpPost]
        [Route("Driver/Delete", Name = "DriverDelte")]
        public async Task<ResponseViewModel<DriverViewModel>> DeleteDriver([FromBody]DriverViewModel driverView)
        {
            return await _adminService.DeleteDriver(driverView);
        }

        /* Carrier API */
        [HttpGet]
        [Route("Carrier/GetAll", Name = "CarrierGetAll")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> GetAllCarrier()
        {
            return await _adminService.GetAllCarrier();
        }

        [HttpGet]
        [Route("Carrier/SearchByCode/code={code}", Name = "CarrierSearchByCode")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> SearchCarrier(string code)
        {
            return await _adminService.SearchCarrier(code);
        }

        [HttpPost]
        [Route("Carrier/Add", Name = "CarrierAddNew")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> CreateNewCarrier([FromBody]CarrierVendorViewModel carrierView)
        {
            return await _adminService.CreateNewCarrier(carrierView);
        }

        [HttpPost]
        [Route("Carrier/Update", Name = "CarrierUpdate")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> UpdateCarrier([FromBody]CarrierVendorViewModel carrierView)
        {
            return await _adminService.UpdateCarrier(carrierView);
        }

        [HttpPost]
        [Route("Carrier/Delete", Name = "CarrierDelte")]
        public async Task<ResponseViewModel<CarrierVendorViewModel>> DeleteCarrier([FromBody]CarrierVendorViewModel carrierView)
        {
            return await _adminService.DeleteCarrier(carrierView);
        }

        [HttpGet]
        [Route("User/GetPermission/{id}", Name = "GetPermission")]
        public async Task<List<SystemFunctionViewModel>> GetPermission(int id)
        {
            return await _adminService.GetUserPermission(id);
        }
        /* The following code show how to map between UI & Return Permission */
        List<SystemFunctionViewModel> functions = new List<SystemFunctionViewModel>();

    }
}
