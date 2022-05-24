namespace Jiepei.ERP.Orders
{
    /// <summary>
    /// 远程接口调用本地分发给对应的执行方法需要此枚举
    /// </summary>
    public enum EnumApiSheetMetalMethod
    {
        /// <summary>
        /// 修改订单包价格（客户销售价）
        /// </summary>
        UpdateOrderMainPrice = 0,

        /// <summary>
        /// 修改用户收货地址
        /// </summary>
        UpdateOrderMainReceiver = 1,

        /// <summary>
        /// 修改发货要求
        /// </summary>
        UpdateOrderMainBox = 2,

        /// <summary>
        /// 修改交付日期
        /// </summary>
        UpdateOrderDetailDeliveryDay = 3,

        /// <summary>
        /// 修改发货记录
        /// </summary>
        DeliverProducts = 4,

        /// <summary>
        /// 修改订单状态
        /// </summary>
        UpdateOrderDetailStatus = 5,

        /// <summary>
        /// 修改订单价格
        /// </summary>
        UpdateOrderDetailTotalMoney = 6,

        /// <summary>
        /// 修改订单产品套数
        /// </summary>
        UpdateOrderBasic = 7
    }
}
