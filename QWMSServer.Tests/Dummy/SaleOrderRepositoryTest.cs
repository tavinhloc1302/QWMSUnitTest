﻿using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class SaleOrderRepositoryTest : RepositoryBaseTest<SaleOrder>, ISaleOrderRepository
    {
        public override IList<SaleOrder> GetObjectList()
        {
            return new List<SaleOrder>() {
            };
        }

        public override async Task<SaleOrder> GetAsync(Expression<Func<SaleOrder, bool>> where)
        {
            var sampleObject = new SaleOrder()
            {
                ID = 1,
                Code = "1111",
                isDelete = false,
            };

            switch (FLAG_GET_ASYNC)
            {
                case 0:
                    sampleObject = null;
                    break;
                case 1:
                    break;
                case 2:
                    sampleObject.isDelete = true;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return sampleObject;
        }
    }
}
