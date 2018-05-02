using QWMSServer.Model.DatabaseModels;
using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Products>> GetAll();

        Task<Products> GetById(int id);

        Task<bool> CreateNewProduct(Products products);

        Task<IEnumerable<LaneViewModel>> Getlanes();
    }
}
