using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.Orders
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
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
        /// 代付金额
        /// </summary>
        public virtual decimal PendingMoney { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        public virtual decimal PaidMoney { get; set; }

        /// <summary>
        /// 是否支付
        /// </summary>
        public virtual bool IsPay { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public virtual DateTime? PayTime { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public virtual byte PayMode { get; set; }

        /// <summary>
        /// 组织单元Id
        /// </summary>
        public virtual Guid? OrganizationUnitId { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public virtual EnumOrderStatus Status { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public virtual EnumOrderType OrderType { get; set; }

        /// <summary>
        /// 交期天数
        /// </summary>
        public virtual int DeliveryDays { get; set; }

        /// <summary>
        /// 交期
        /// </summary>
        public virtual DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 客户备注
        /// </summary>
        public virtual string CustomerRemark { get; set; }

        /// <summary>
        /// 客服备注
        /// </summary>
        public virtual string CustomerServiceRemark { get; set; }

        /// <summary>
        /// 客服人员
        /// </summary>
        public virtual Guid? CustomerService { get; set; }

        /// <summary>
        /// 工程师
        /// </summary>
        public virtual Guid? Engineer { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public virtual string TrackingNo { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public virtual string CourierCompany { get; set; }

        /// <summary>
        /// 订单名称
        /// </summary>
        public virtual string OrderName { get; set; }


        protected Order() { }

        public Order(
            Guid id
            , string orderNo
            , EnumOrderStatus status
            , Guid channel
            , string channelOrderNo
            , string channelUserId
            , string customerRemark
            , EnumOrderType orderType
            , string orderName
            //, decimal cost
            //, decimal sellingPrice
            //, decimal pendingMoney
            , int deliveryDays = 0
            , DateTime? deliveryDate = null
            )
        {
            Id = id;
            OrderNo = orderNo;
            Status = status;
            ChannelId = channel;
            ChannelOrderNo = channelOrderNo;
            ChannelUserId = channelUserId;
            CustomerRemark = customerRemark;
            OrderType = orderType;
            DeliveryDays = deliveryDays;
            DeliveryDate = deliveryDate;
            //Cost = cost;
            //SellingPrice = sellingPrice;
            //PendingMoney = pendingMoney;
            OrderName = orderName;
        }

        public void SetOrderNo(string orderNo)
        {
            OrderNo = orderNo;
        }
        public void SetOrderStatus(EnumOrderStatus status)
        {
            Status = status;
        }

        public void SetPaid(DateTime? paidTime, bool isPaid)
        {
            PayTime = paidTime;
            IsPay = isPaid;
        }
        public void SetPendingMoney(decimal money)
        {
            PendingMoney = money;
        }
        public void SetNote(string note)
        {
            CustomerServiceRemark = note;
        }
        public void SetPaidMoney(decimal money)
        {
            PaidMoney = money;
        }

        public void SetDeliveryDays(int deliveryDays)
        {
            DeliveryDays = deliveryDays;
        }
        public void SetDeliveryDate(DateTime deliveryDate)
        {
            DeliveryDate = deliveryDate;
        }
        
        public void Offer(decimal cost, decimal sellingPrice)
        {
            Cost = cost;
            SellingPrice = sellingPrice;
        }

        public void SetSellingPrice(decimal sellingPrice)
        {
            SellingPrice = sellingPrice;
        }

        public void SetTrackingNo(string trackingNo)
        {
            TrackingNo = trackingNo;
        }
        public void SetCourierCompany(string courierCompany)
        {
            CourierCompany = courierCompany;
        }

        public void SetCustomerService(Guid? customerService) {
            CustomerService = customerService;
        }
        public void SetEngineer(Guid? engineer)
        {
            Engineer = engineer;
        }
        
    }
}
