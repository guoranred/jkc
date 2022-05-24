namespace Jiepei.ERP.Orders
{
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum EnumOrderStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [EnumDesc("待审核")]
        WaitCheck = 1,

        /// <summary>
        /// 取消
        /// </summary>
        [EnumDesc("取消")]
        Cancel = 2,

        /// <summary>
        /// 审核不通过
        /// </summary>
        [EnumDesc("审核不通过")]
        CheckedNoPass = 4,

        /// <summary>
        /// 审核通过
        /// </summary>
        [EnumDesc("审核通过")]
        CheckedPass = 8,

        /// <summary>
        /// 确认下单
        /// </summary>
        [EnumDesc("确认下单")]
        SureOrder = 16,

        /// <summary>
        /// 生产中
        /// </summary>
        [EnumDesc("生产中")]
        Purchasing = 32,

        /// <summary>
        /// 等待发货
        /// </summary>
        [EnumDesc("等待发货")]
        WaitSend = 64,

        /// <summary>
        /// 已发货
        /// </summary>
        [EnumDesc("已发货")]
        HaveSend = 128,

        /// <summary>
        /// 交易成功
        /// </summary>
        [EnumDesc("交易成功")]
        Finish = 1024
    }
}
