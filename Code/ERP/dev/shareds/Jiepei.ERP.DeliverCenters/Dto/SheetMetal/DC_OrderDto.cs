using System;

namespace Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal
{
    public class DC_OrderDto 
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// 渠道编号
        /// </summary>
        public string ChannelNo { get; set; }
        /// <summary>
        /// 渠道订单号
        /// </summary>
        public string ChannelOrderNo { get; set; }
        /// <summary>
        /// 组合生产关联订单号
        /// </summary>
        public string BindOrderNo { get; set; }
        /// <summary>
        /// 渠道客户编号
        /// </summary>
        public string ChannelMemberNo { get; set; }
        /// <summary>
        /// 渠道客户等级
        /// </summary>
        public int ChannelMemberLevel { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        //public string Status { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProType { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 订单总价 （不包含优惠券）
        /// </summary>
        public decimal TotalMoney { get; set; }
        /// <summary>
        /// 优惠总金额
        /// </summary>
        public decimal PreferentialMoney { get; set; }
        /// <summary>
        /// 应付金额 = 订单总价 - 优惠总金额
        /// </summary>
        public decimal PayableMoney { get; set; }
        /// <summary>
        /// 待付金额
        /// </summary>
        public decimal PendingMoney { get; set; }
        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal PaidMoney { get; set; }
        /// <summary>
        /// 已付赠送金额(含在已付金额内)
        /// </summary>
        public decimal GiveMoney { get; set; }
        /// <summary>
        /// 是否支付
        /// </summary>
        public int IsPay { get; set; }
        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// 收款类型
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// 结算类型
        /// </summary>
        public string SettleACCTType { get; set; }
        /// <summary>
        /// 是否允许投产
        /// </summary>
        public int IsAllowManufacture { get; set; }
        /// <summary>
        /// 是否允许发货
        /// </summary>
        public int IsAllowDelivery { get; set; }
        /// <summary>
        /// 是否客供
        /// </summary>
        public int IsCustSupply { get; set; }
        /// <summary>
        /// 交期天数
        /// </summary>
        public int DeliveryDays { get; set; }
        /// <summary>
        /// 交期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// 投产时间
        /// </summary>
        public DateTime? ManufactureTime { get; set; }
        /// <summary>
        /// 业务员(工号)
        /// </summary>
        public string Salesman { get; set; }
        /// <summary>
        /// 客服人员(工号)
        /// </summary>
        public string CustomerService { get; set; }
        /// <summary>
        /// 客户备注
        /// </summary>
        public string MemberRemark { get; set; }
    }
}
