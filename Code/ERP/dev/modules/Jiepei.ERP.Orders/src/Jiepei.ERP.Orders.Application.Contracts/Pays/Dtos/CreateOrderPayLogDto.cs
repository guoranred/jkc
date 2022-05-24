using System;

namespace Jiepei.ERP.Orders.Pays.Dtos
{
    public class CreateOrderPayLogDto
    {
        /// <summary>
        /// 支付单号
        /// </summary>
        public string PayCode { get; set; }

        /// <summary>
        /// 客户id
        /// </summary>
        public Guid MemberId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayType { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal TotalAmout { get; set; }

        /// <summary>
        /// 是否支付成功
        /// </summary>
        public bool IsPaySuccess { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
