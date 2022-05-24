using System;

namespace Jiepei.ERP.Orders.Application.External.Order.Models
{
    public class OrderGroupDto
    {
        /// <summary>
        /// 订单包编号(生成)
        /// </summary>
        public string GroupNo { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public int? MbId { get; set; }

        /// <summary>
        /// 子订单数量
        /// </summary>
        public int? ProNum { get; set; }

        /// <summary>
        /// 总重量(计算子订单的)
        /// </summary>
        public float? TotalWeight { get; set; }

        /// <summary>
        /// 订单包总金额
        /// </summary>
        public decimal? OrderMoney { get; set; }

        /// <summary>
        /// 订单包总支付金额
        /// </summary>
        public decimal? OrderPayMoney { get; set; }

        /// <summary>
        /// 总产品费
        /// </summary>
        public decimal? ProMoney { get; set; }

        /// <summary>
        /// 总运费
        /// </summary>
        public decimal? ShipMoney { get; set; }

        /// <summary>
        /// 总优惠金额
        /// </summary>
        public decimal? PreferentialMoney { get; set; }

        /// <summary>
        /// 订单包状态
        /// </summary>
        public byte? Status { get; set; }

        /// <summary>
        /// 订单包创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 系统计算交货时间，为所有子订单中最大的交货时间为准
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 实际发货时间
        /// </summary>
        public DateTime? ShipDate { get; set; }

        /// <summary>
        /// 确认收货时间
        /// </summary>
        public DateTime? CompleteDate { get; set; }

        /// <summary>
        /// 客户备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 发货方式
        /// </summary>
        public string ShipType { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        public string ShipNo { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 收货公司名
        /// </summary>
        public string ReceiverCompany { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string ReceiverAddress { get; set; }

        /// <summary>
        /// 收货人联系方式
        /// </summary>
        public string ReceiverTel { get; set; }

        /// <summary>
        /// 收货邮编
        /// </summary>
        public string ReceiverPostCode { get; set; }

        /// <summary>
        /// 收货省Id
        /// </summary>
        public int? ReceiverStateId { get; set; }

        /// <summary>
        /// 收货省名
        /// </summary>
        public string ReceiverState { get; set; }

        /// <summary>
        /// 收货城市名
        /// </summary>
        public string ReceiverCity { get; set; }

        /// <summary>
        /// 订单包类型
        /// </summary>
        public int? OrderType { get; set; }

        public float? ActualFreight { get; set; }

        public decimal? ActualWeight { get; set; }

        /// <summary>
        /// 税点
        /// </summary>
        public decimal? TaxPoint { get; set; }

        /// <summary>
        /// 税费
        /// </summary>
        public decimal? TaxMoney { get; set; }

        /// <summary>
        /// 发票信息
        /// </summary>
        public string InvoiceInfo { get; set; }

        /// <summary>
        /// 是否已开发票
        /// </summary>
        public int? IsMakeInvoiceInfo { get; set; }

        /// <summary>
        /// 发货备注
        /// </summary>
        public string SendRemark { get; set; }

        /// <summary>
        /// 收货地址城市Id
        /// </summary>
        public int? ReceiverCityId { get; set; }

        /// <summary>
        /// 是否是空白盒子
        /// </summary>
        public int IsUseBox { get; set; } = 0;

        /// <summary>
        /// 是否打印发票
        /// </summary>
        public int IsPrintInvoice { get; set; } = 0;

        /// <summary>
        /// 快递面单是否展示捷圆工厂字样
        /// </summary>
        public int IsShowJYCompany { get; set; } = 0;

        /// <summary>
        /// 订单联系人
        /// </summary>
        public string OrderContactName { get; set; }

        /// <summary>
        /// 订单联系人手机
        /// </summary>
        public string OrderContactMobile { get; set; }

        /// <summary>
        /// 联系人QQ
        /// </summary>
        public string OrderContactQQ { get; set; }

        /// <summary>
        /// 订单包业务类型，9 代表钣金
        /// </summary>
        public byte? OrderMainType { get; set; } = 9;

        /// <summary>
        /// 标签类别 0-标签贴里面 1-标签贴外面 2-不贴标签
        /// </summary>
        public int LabelType { get; set; } = 2;

        /// <summary>
        /// 钣金跟单客服
        /// </summary>
        public int? SheetMetalFollowAdminId { get; set; }

        /// <summary>
        /// 渠道来源代码
        /// </summary>
        public string OrderChannel { get; set; }
    }
}
