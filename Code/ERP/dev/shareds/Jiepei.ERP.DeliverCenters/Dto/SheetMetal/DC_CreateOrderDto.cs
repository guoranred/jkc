using System;
using Jiepei.ERP.DeliverCentersClient.Enums;

namespace Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal
{
    public class DC_CreateOrderDto
    {
        /// <summary>
        /// 订单名称
        /// </summary>
        public string OrderName { get; set; }

        /// <summary>
        /// 渠道编号
        /// </summary>
        public string ChannelNo { get; set; }

        /// <summary>
        /// 渠道订单号
        /// </summary>
        public string ChannelOrderNo { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProType { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public EnumDeliverCenterOrderType OrderType { get; set; }

        /// <summary>
        /// 是否客供
        /// </summary>
        public int IsCustSupply { get; set; }

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

        /// <summary>
        /// 是否淘宝官网
        /// </summary>
        public virtual int IsTaoBao { get;  set; }


        public DC_CreateOrderMemberDemandDto OrderMemberDemand { get; set; }

        public DC_CreateOrderCostDto OrderCost { get; set; }

        public DC_CreateOrderDeliveryDto OrderDelivery { get; set; }

        public DC_CreateOrderProcessDto OrderProcess { get; set; }
    }
}
