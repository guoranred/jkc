using Jiepei.ERP.Shared.Enums.Pays;
using System;
using System.Collections.Generic;

namespace Jiepei.ERP.Pays.Dtos
{
    public class CreatePayInputDto
    {
        /// <summary>
        /// 订单ID集合
        /// </summary>
        public List<Guid> OrderIds { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public EnumPayType PayType { get; set; }
    }
}
