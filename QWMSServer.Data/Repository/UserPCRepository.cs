﻿using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Repository
{
    public class UserPCRepository : AsyncRepository<UserPC>, IUserPCRepository
    {
        public UserPCRepository(IDBContext dBContext) : base(dBContext)
        {
        }
    }
}