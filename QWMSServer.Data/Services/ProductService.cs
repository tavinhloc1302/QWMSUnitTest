using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using AutoMapper;
using QWMSServer.Model.ViewModels;
using QWMSServer.Data.Common;

namespace QWMSServer.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productRepository;
        private readonly ILaneRepository _laneRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork, IProductsRepository productsRepository, ILaneRepository laneRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productsRepository;
            _laneRepository = laneRepository;
        }
        public async Task<bool> CreateNewProduct(Products product)
        {
            _productRepository.Add(product);
            return await this.SaveChangesAsync();
        }

        public async Task<IEnumerable<Products>> GetAll()
        {
            var result = await _productRepository.GetAllAsync();
            return result;
        }

        public async Task<Products> GetById(int id)
        {
            var result = await _productRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<IEnumerable<LaneViewModel>> Getlanes()
        {
            IEnumerable<Lane> lanes = await _laneRepository.GetManyAsync( l => l.isDelete == false, QueryIncludes.LANEFULLINCLUDES );
            LaneViewModel view = null;
            IEnumerable<LaneViewModel> models = new List<LaneViewModel>();
            //foreach (var lane in lanes)
            //{
            //    view = new LaneViewModel();
            //    view.name = lane.nameVi;
            //    view.code = lane.code;
            //    view.loadingBayCode = lane.loadingBayCode;
            //    view.loadingBayName = lane.loadingBay.nameVi;
            //    view.truckTypeDesciption = lane.truckType.desciption;
            //    view.loadingTypeDescription = lane.loadingType.description;
            //    view.maxCapactity = lane.maxCapactity;
            //    view.minCapacity = lane.minCapacity;
            //    models.Add(view);
            //}
            models = Mapper.Map<IEnumerable<Lane>, IEnumerable<LaneViewModel>>(lanes);
            return models;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

    }
}
