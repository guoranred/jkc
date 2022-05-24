namespace Jiepei.ERP.Shared.Enums.Pays
{
    /// <summary>
    /// 订单流水类型
    /// </summary>
    public enum EnumOrderFlowType
    {
        /// <summary>
        ///支付
        /// </summary>
        [EnumDesc("支付")]
        Pay = 1,

        /// <summary>
        /// 取消/退款
        /// </summary>
        [EnumDesc("取消/退款")]
        Refund = 2
    }
}
