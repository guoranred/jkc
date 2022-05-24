namespace Jiepei.ERP.Admin.Application.Contracts.Statistics
{
    public class DailyDataDto
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 询盘数
        /// </summary>
        public int Inquiry { get; set; }

        /// <summary>
        /// 已报价
        /// </summary>
        public int Quoted { get; set; }

        /// <summary>
        /// 付款订单数
        /// </summary>
        public int PaymentOrders { get; set; }

        /// <summary>
        /// 投产率
        /// </summary>
        public int ProductionRate { get; set; }

        /// <summary>
        /// 订单完工率
        /// </summary>
        public int OrderCompletionRate { get; set; }

        /// <summary>
        /// 零件完工率
        /// </summary>
        public int PartCompletionRate { get; set; }
    }
}
