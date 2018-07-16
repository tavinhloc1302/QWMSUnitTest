using QWMSServer.Data.Common;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Services
{
    public class CommonService : ICommonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;

        public CommonService(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CheckUserPermission(int empID, string empRFIDCode, string APIName)
        {
            var employee = await _employeeRepository.GetAsync(emp => emp.rfidCard.code.Equals(empRFIDCode) && emp.isDelete == false, QueryIncludes.EMPLOYEEFULLINCLUDES);
            if (employee == null)
                return false;
            if (employee.ID != empID)
                return false;
            bool found = false;
            foreach (var function in employee.employeeGroup.functionMaps)
            {
                if (function.systemFunction.API.Equals(APIName))
                    found = true;
            }
            if (found == false)
                return false;
            return true;
        }
    }
}
