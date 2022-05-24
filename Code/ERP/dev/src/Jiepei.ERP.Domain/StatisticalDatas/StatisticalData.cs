using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.StatisticalDatas
{
    public class StatisticalData : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// 询盘数
        /// </summary>
        public int Inquiry { get; private set; }

        /// <summary>
        /// 可报价客户
        /// </summary>
        public int Quotable { get; private set; }

        /// <summary>
        /// 已报价
        /// </summary>
        public int Quoted { get; private set; }

        /// <summary>
        /// 未报价
        /// </summary>
        public int NotQuoted { get; private set; }

        /// <summary>
        /// 有效询盘率
        /// </summary>
        public int ValidInquiryRate { get; private set; }

        /// <summary>
        /// 跟进客户占比
        /// </summary>
        public int FollowUpsRate { get; private set; }

        /// <summary>
        /// 跟进客户
        /// </summary>
        public int FollowUpCustomers { get; private set; }

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
        public int PaymentOrders { get; private set; }

        /// <summary>
        /// 付款成功率
        /// </summary>
        public int PaymentSuccessRate { get; private set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PaymentAmount { get; private set; }

        /// <summary>
        /// 生产总订单
        /// </summary>
        public int TotalProductionNumber { get; private set; }

        /// <summary>
        /// 生产中订单
        /// </summary>
        public int OrdersInProduction { get; private set; }

        /// <summary>
        /// 投产率
        /// </summary>
        public int ProductionRate { get; private set; }

        /// <summary>
        /// 完成订单数
        /// </summary>
        public int CompletedOrders { get; private set; }

        /// <summary>
        /// 订单完工率
        /// </summary>
        public int OrderCompletionRate { get; private set; }

        /// <summary>
        /// 零件总数
        /// </summary>
        public int TotalParts { get; private set; }

        /// <summary>
        /// 完工零件数
        /// </summary>
        public int FinishedParts { get; private set; }

        /// <summary>
        /// 零件完工率
        /// </summary>
        public int PartCompletionRate { get; private set; }

        public StatisticalData(DateTime date,
                               int inquiry,
                               int quotable,
                               int quoted,
                               int notQuoted,
                               int validInquiryRate,
                               int followUpsRate,
                               int followUpCustomers,
                               int churnCustomers,
                               int churnRate,
                               int paymentOrders,
                               int paymentSuccessRate,
                               decimal paymentAmount,
                               int totalProductionNumber,
                               int ordersInProduction,
                               int productionRate,
                               int completedOrders,
                               int orderCompletionRate,
                               int totalParts,
                               int finishedParts,
                               int partCompletionRate)
        {
            Date = date;
            Inquiry = inquiry;
            Quotable = quotable;
            Quoted = quoted;
            NotQuoted = notQuoted;
            ValidInquiryRate = validInquiryRate;
            FollowUpsRate = followUpsRate;
            FollowUpCustomers = followUpCustomers;
            ChurnCustomers = churnCustomers;
            ChurnRate = churnRate;
            PaymentOrders = paymentOrders;
            PaymentSuccessRate = paymentSuccessRate;
            PaymentAmount = paymentAmount;
            TotalProductionNumber = totalProductionNumber;
            OrdersInProduction = ordersInProduction;
            ProductionRate = productionRate;
            CompletedOrders = completedOrders;
            OrderCompletionRate = orderCompletionRate;
            TotalParts = totalParts;
            FinishedParts = finishedParts;
            PartCompletionRate = partCompletionRate;
        }
    }
}
