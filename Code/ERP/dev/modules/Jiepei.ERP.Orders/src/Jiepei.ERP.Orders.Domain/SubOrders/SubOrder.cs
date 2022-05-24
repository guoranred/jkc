using Jiepei.ERP.EventBus.Shared.SubOrders;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.SubOrders;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrder : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 主订单 Id
        /// </summary>
        public virtual Guid OrderId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual string OrderNo { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public virtual Guid ChannelId { get; set; }

        /// <summary>
        /// 渠道订单号
        /// </summary>
        public virtual string ChannelOrderNo { get; set; }

        /// <summary>
        /// 渠道用户Id
        /// </summary>
        public virtual string ChannelUserId { get; set; }

        /// <summary>
        /// 成本
        /// </summary>
        public virtual decimal Cost { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public virtual decimal SellingPrice { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public virtual EnumOrderType OrderType { get; set; }

        /// <summary>
        /// 组织单元Id
        /// </summary>
        public virtual Guid? OrganizationUnitId { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public virtual EnumSubOrderStatus Status { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public virtual DateTime? InboundTime { get; set; }

        /// <summary>
        /// 出库时间
        /// </summary>
        public virtual DateTime? OutboundTime { get; set; }

        protected SubOrder() { }

        public SubOrder(Guid id
            , Guid orderId
            , string orderNo
            , Guid channel
            , string channelOrderNo
            , string channelUserId
            , decimal cost
            , decimal sellingPrice
            , EnumOrderType orderType
            , Guid? organizationUnitId
            , EnumSubOrderStatus status
            , string remark
            ) : base(id)
        {
            Id = id;
            OrderId = orderId;
            OrderNo = orderNo;
            ChannelId = channel;
            ChannelOrderNo = channelOrderNo;
            ChannelUserId = channelUserId;
            OrderType = orderType;
            OrganizationUnitId = organizationUnitId;
            Remark = remark;
            Cost = cost;
            SellingPrice = sellingPrice;
            Status = status;
        }

        public virtual void Complete()
        {
            if (Status != EnumSubOrderStatus.HaveSend)
                throw new UserFriendlyException(message: "当前订单状态不符合完成条件");
            Status = EnumSubOrderStatus.Finish;

            AddLocalEvent(new CompleteOrderEto(OrderId));

            //AddDistributedEvent(new SubOrderCompleteEto(ChannelOrderNo, OrderType));
        }

        public virtual void Check(bool isPassed, string remark)
        {
            if (Status != EnumSubOrderStatus.WaitCheck
                && Status != EnumSubOrderStatus.CheckedNoPass
                && Status != EnumSubOrderStatus.CheckedPass)
            {
                throw new UserFriendlyException(message: "当前订单状态不符合审核条件");
            }

            EnumSubOrderStatus status;
            if (isPassed)
                status = EnumSubOrderStatus.CheckedPass;
            else
                status = EnumSubOrderStatus.CheckedNoPass;
            Status = status;

            AddLocalEvent(new CheckOrderEto(OrderId, isPassed));

            //AddDistributedEvent(new SubOrderCheckEto(ChannelOrderNo, Status, remark, OrderType));
        }

        public virtual void Offer(decimal cost, decimal sellingPrice, decimal shipPrice, decimal discountMoney)
        {
            if (Status != EnumSubOrderStatus.CheckedPass && Status != EnumSubOrderStatus.OfferComplete)
                throw new UserFriendlyException(message: "当前订单状态不符合报价条件");
            Status = EnumSubOrderStatus.OfferComplete;
            Cost = cost;
            //if (OrderType == EnumOrderType.SheetMetal)
                SellingPrice = sellingPrice + shipPrice - discountMoney;
            //else
               // SellingPrice = sellingPrice * 1.08m + shipPrice - discountMoney;

            AddLocalEvent(new OfferOrderEto(OrderId, cost, sellingPrice, shipPrice, discountMoney));

            //  AddDistributedEvent(new SubOrderOfferEto(ChannelOrderNo, SellingPrice, OrderType));
        }

        public virtual void Manufacture()
        {
            if (Status != EnumSubOrderStatus.SureOrder)
                throw new UserFriendlyException(message: "当前订单状态不符合投产条件");
            Status = EnumSubOrderStatus.Purchasing;

            AddLocalEvent(new ManufactureOrderEto(OrderId));

            // AddDistributedEvent(new SubOrderManufactureEto(ChannelOrderNo, OrderType));
        }

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="trackingNo">运单号</param>
        /// <param name="courierCompany">快递公司</param>
        /// <exception cref="UserFriendlyException"></exception>
        public virtual void Deliver(string trackingNo, string courierCompany)
        {
            if (Status != EnumSubOrderStatus.Purchasing)
                throw new UserFriendlyException(message: "当前订单状态不符合发货条件");

            Status = EnumSubOrderStatus.HaveSend;

            AddLocalEvent(new DeliverOrderEto(OrderId, trackingNo, courierCompany));
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="remark"></param>
        public virtual void Cancel(string remark)
        {
            Status = EnumSubOrderStatus.Cancel;

            AddLocalEvent(new CancelOrderEto(OrderId));
        }

        public void SetStatus(EnumSubOrderStatus status)
        {
            Status = status;
        }
        public void SetSellingPrice(decimal sellingPrice)
        {
            SellingPrice = sellingPrice;
        }

        /// <summary>
        /// 设置交期
        /// </summary>
        /// <param name="days"></param>
        /// <exception cref="UserFriendlyException"></exception>
        public virtual void SetDeliveryDays(int days)
        {
            if (Status >= EnumSubOrderStatus.Purchasing || Status == EnumSubOrderStatus.Cancel)
                throw new UserFriendlyException(message: "当前订单状态不符合修改交期条件");

            AddLocalEvent(new SetDeliveryDayEto(OrderId, days));
        }
    }
}
