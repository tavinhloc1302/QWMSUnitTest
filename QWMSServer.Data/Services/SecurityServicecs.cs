using System;
using System.Collections.Generic;
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
    public class SecurityServicecs : ISecurityServicecs
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueListRepository _queueListRepository;

        public SecurityServicecs(IUnitOfWork unitOfWork, IQueueListRepository queueListRepository)
        {
            _unitOfWork = unitOfWork;
            _queueListRepository = queueListRepository;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ResponseViewModel<QueueListViewModel>> GetQueueList()
        {
            ResponseViewModel<QueueListViewModel> response = new ResponseViewModel<QueueListViewModel>();
            var queueList = await _queueListRepository.GetManyAsync( qe=>qe.isDelete==false, QueryIncludes.QUEUELISTFULLINCLUDES);
            if (queueList == null)
                response.errorText = "No record found";
            response.responseDatas = Mapper.Map<IEnumerable<QueueList>, IEnumerable<QueueListViewModel>>(queueList);
            return response;
        }
    }
}
