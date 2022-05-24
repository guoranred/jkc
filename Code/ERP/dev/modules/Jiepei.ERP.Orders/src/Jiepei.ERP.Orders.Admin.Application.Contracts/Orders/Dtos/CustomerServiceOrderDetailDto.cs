using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Admin
{
    public class CustomerServiceOrderDetailDto : CreationAuditedEntityDto<Guid>
    {
        public Guid OrderId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SellingPrice { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal ShipPrice { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 产品费
        /// </summary>
        public decimal ProMoney { get; set; }
        /// <summary>
        /// 税费
        /// </summary>
        public decimal TaxMoney { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal DiscountMoney { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 文件
        /// </summary>
        public string Boms { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }

        /// <summary>
        /// 客户备注
        /// </summary>
        public string CustomerRemark { get; set; }

        /// <summary>
        /// 会员姓名
        /// </summary>
        public string MemberName { get; set; }
        /// <summary>
        /// 会员联系方式
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string ReceiverAddress { get; set; }

        /// <summary>
        /// 收货人联系方式
        /// </summary>
        public string ReceiverTel { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string TrackingNo { get; set; }

        /// <summary>
        /// 客服名称
        /// </summary>
        public string CustomerServiceName { get; set; }



        /// <summary>
        /// 订单名称
        /// </summary>
        public string OrderName { get; set; }


        /// <summary>
        /// 交期天数
        /// </summary>
        public int DeliveryDays { get; set; }


        /// <summary>
        /// 交期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }    
        
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }
    }
}
