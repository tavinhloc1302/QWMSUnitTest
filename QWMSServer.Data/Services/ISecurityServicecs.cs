using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Model.ViewModels;

namespace QWMSServer.Data.Services
{
    public interface ISecurityServicecs
    {
        Task<ResponseViewModel<QueueListViewModel>> GetTrucks(string truckCondition);

        Task<ResponseViewModel<GatePassViewModel>> GetGatePassByRFID(string rfidCode);

        Task<ResponseViewModel<GatePassViewModel>> RegisterSecurityCheck(string rfidCode);

        Task<ResponseViewModel<GatePassViewModel>> ConfirmSecurityCheck(GatePassViewModel gatePassView);
    }
}
