using System;

namespace Jiepei.ERP.Orders.Materials.Dtos
{
    public class CreateMaterialPriceDto
    {
        /// <summary>
        /// 材料id
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 适用订单类型
        /// </summary>
        public int OrderType { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public Guid ChannelId { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 起步价
        /// </summary>
        public decimal StartPrice { get; set; }

        /// <summary>
        /// 折扣比率
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public bool IsSale { get; set; }

        /// <summary>
        /// 前台排序
        /// </summary>
        public int SeqNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 单件起步价
        /// </summary>
        public decimal UnitStartPrice { get; set; }
    }
}
