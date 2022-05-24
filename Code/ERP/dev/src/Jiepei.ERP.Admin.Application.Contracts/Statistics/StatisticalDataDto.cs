using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Admin.Application.Contracts.Statistics
{
    public class StatisticalDataDto : CreationAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 询盘数
        /// </summary>
        public int Inquiry { get; set; }

        /// <summary>
        /// 可报价客户
        /// </summary>
        public int Quotable { get; set; }

        /// <summary>
        /// 已报价
        /// </summary>
        public int Quoted { get; set; }

        /// <summary>
        /// 未报价
        /// </summary>
        public int NotQuoted { get; set; }

        /// <summary>
        /// 有效询盘率
        /// </summary>
        public int ValidInquiryRate { get; set; }

        /// <summary>
        /// 跟进客户占比
        /// </summary>
        public int FollowUps { get; set; }

        /// <summary>
        /// 跟进客户
        /// </summary>
        public int FollowUpCustomers { get; set; }

        /// <summary>
        /// 流失客户
        /// </summary>
        public int ChurnCustomers { get; private set; }

        /// <summary>
        /// 客户流失率
        /// </summary>
        public int ChurnRate { get; private set; }

        /// <summary>
        /// 付款订单数
        /// </summary>
        public int PaymentOrders { get; set; }

        /// <summary>
        /// 付款成功率
        /// </summary>
        public int PaymentSuccessRate { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// 生产总订单
        /// </summary>
        public int TotalProductionNumber { get; set; }

        /// <summary>
        /// 生产中订单
        /// </summary>
        public int OrdersInProduction { get; set; }

        /// <summary>
        /// 投产率
        /// </summary>
        public int ProductionRate { get; set; }

        /// <summary>
        /// 完成订单数
        /// </summary>
        public int CompletedOrders { get; set; }

        /// <summary>
        /// 订单完工率
        /// </summary>
        public int OrderCompletionRate { get; set; }

        /// <summary>
        /// 零件总数
        /// </summary>
        public int TotalParts { get; set; }

        /// <summary>
        /// 完工零件数
        /// </summary>
        public int FinishedParts { get; set; }

        /// <summary>
        /// 零件完工率
        /// </summary>
        public int PartCompletionRate { get; set; }
    }
}