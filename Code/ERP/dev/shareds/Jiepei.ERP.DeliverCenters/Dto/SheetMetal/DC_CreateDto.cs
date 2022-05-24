using System;
using System.Collections.Generic;
using System.Text;

namespace Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal
{
    public class DC_CreateDto
    {
        public DC_CreateOrderDto Order { get; set; }
        public DC_CreateProductDto Items { get; set; }
    }
}
