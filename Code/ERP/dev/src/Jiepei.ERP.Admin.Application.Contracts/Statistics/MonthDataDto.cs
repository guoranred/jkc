using System.Collections.Generic;

namespace Jiepei.ERP.Admin.Application.Contracts.Statistics
{
    public class MonthDataDto
    {
        /// <summary>
        /// 询盘数
        /// </summary>
        public int Inquiry { get; set; }

        /// <summary>
        /// 已报价
        /// </summary>
        public int Quoted { get; set; }

        /// <summary>
        /// 未报价
        /// </summary>
        public int NotQuoted { get; set; }

        /// <summary>
        /// 付款订单数
        /// </summary>
        public int PaymentOrders { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PaymentAmount { get; set; }

        /****************************/
        /// <summary>
        /// 付款客户占比
        /// </summary>
        public int PaymentCustomerRate { get; set; }

        /// <summary>
        /// 跟进客户占比
        /// </summary>
        public int FollowUpCustomerRate { get; set; }

        /// <summary>
        /// 流失客户占比
        /// </summary>
        public int ChurnCustomerRate { get; set; }

        /****************************/

        /// <summary>
        /// 生产总订单
        /// </summary>
        public int TotalProductionNumber { get; set; }

        /// <summary>
        /// 生产中订单
        /// </summary>
        public int OrdersInProduction { get; set; }

        /// <summary>
        /// 零件总数
        /// </summary>
        public int TotalParts { get; set; }

        /// <summary>
        /// 完成零件数
        /// </summary>
        public int FinishedParts { get; set; }

        public List<DailyDataDto> DailyData { get; set; } = new List<DailyDataDto>();
    }
}
