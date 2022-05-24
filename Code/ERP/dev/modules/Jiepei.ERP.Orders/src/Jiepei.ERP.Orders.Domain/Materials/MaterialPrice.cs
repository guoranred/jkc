using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.Materials
{
    public class MaterialPrice : AuditedAggregateRoot<Guid>
    {
        protected MaterialPrice() { }

        public MaterialPrice(Guid id, Guid materialId, EnumOrderType orderType, Guid channelId, decimal price, decimal startPrice, decimal discount, bool isSale, int seqNo, string note) : base(id)
        {
            Id = id;
            MaterialId = materialId;
            OrderType = orderType;
            ChannelId = channelId;
            Price = price;
            StartPrice = startPrice;
            Discount = discount;
            IsSale = isSale;
            SeqNo = seqNo;
            Note = note;
        }

        /// <summary>
        /// 材料id
        /// </summary>
        public virtual Guid MaterialId { get; set; }
        /// <summary>
        /// 适用订单类型
        /// </summary>
        public virtual EnumOrderType OrderType { get; set; }
        /// <summary>
        /// 渠道
        /// </summary>
        public virtual Guid ChannelId { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public virtual decimal Price { get; set; }
        /// <summary>
        /// 起步价
        /// </summary>
        public virtual decimal StartPrice { get; set; }
        /// <summary>
        /// 折扣比率
        /// </summary>
        public virtual decimal Discount { get; set; }
        /// <summary>
        /// 是否上架
        /// </summary>
        public virtual bool IsSale { get; set; }
        /// <summary>
        /// 前台排序
        /// </summary>
        public virtual int SeqNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Note { get; set; }
        /// <summary>
        /// 单件起步价
        /// </summary>
        public virtual decimal UnitStartPrice { get; set; }


    }
}
