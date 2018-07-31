﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class OrderMaterialViewModel
    {
        public int ID { get; set; }

        public string code { get; set; }

        public int? orderID { get; set; }

        public int? materialID { get; set; }

        public int? QCQuantity { get; set; }

        public float? QCGrossWeight { get; set; }

        public int? registQuantity { get; set; }

        public float? registGrossWeight { get; set; }

        public float? registNetWeight { get; set; }

        public bool isDelete { get; set; }

        public OrderViewModel order { get; set; }

        public MaterialViewModel material { get; set; }
    }
}
