using System;
using System.Collections.Generic;
using System.Text;

namespace Jiepei.ERP.Orders.CncOrders.Dtos
{
    public class CreateCncOrderDto
    {
        public List<CreateCncOrderBomDto> CncOrderBomDtos { get; set; }
    }
}
