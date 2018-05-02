using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;

namespace QWMSServer.Data.Services
{
    public interface IAdminService
    {
        /* Customer management block */
        Task<ResponseViewModel<CustomerViewModel>> GetAllCustomer();

        Task<ResponseViewModel<CustomerViewModel>> GetCustomerByCode(string code);

        Task<ResponseViewModel<CustomerViewModel>> SearchCustomer(string code);

        Task<ResponseViewModel<CustomerViewModel>> CreateNewCustomer(CustomerViewModel customerView);

        Task<ResponseViewModel<CustomerViewModel>> UpdateCustomer(CustomerViewModel customerView);

        Task<ResponseViewModel<CustomerViewModel>> DeleteCustomer(CustomerViewModel customerView);

        /* Driver management block */
        Task<ResponseViewModel<DriverViewModel>> GetAllDriver();

        Task<ResponseViewModel<DriverViewModel>> GetDriverByCode(string code);

        Task<ResponseViewModel<DriverViewModel>> SearchDriver(string code);

        Task<ResponseViewModel<DriverViewModel>> CreateNewDriver(DriverViewModel driverView);

        Task<ResponseViewModel<DriverViewModel>> UpdateDriver(DriverViewModel driverView);

        Task<ResponseViewModel<DriverViewModel>> DeleteDriver(DriverViewModel driverView);

        /* Carrier management block */
        Task<ResponseViewModel<CarrierVendorViewModel>> GetAllCarrier();

        Task<ResponseViewModel<CarrierVendorViewModel>> SearchCarrier(string code);

        Task<ResponseViewModel<CarrierVendorViewModel>> GetCarrierByCode(string code);

        Task<ResponseViewModel<CarrierVendorViewModel>> CreateNewCarrier(CarrierVendorViewModel carrierView);

        Task<ResponseViewModel<CarrierVendorViewModel>> UpdateCarrier(CarrierVendorViewModel carrierView);

        Task<ResponseViewModel<CarrierVendorViewModel>> DeleteCarrier(CarrierVendorViewModel carrierView);

        Task<List<SystemFunctionViewModel>> GetUserPermission(int userID);
    }
}
