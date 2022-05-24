namespace Jiepei.ERP.Orders
{
    /// <summary>
    /// 根据远程接口的返回值修改状态时需要此枚举来做转换
    /// 转换为OrderStatusEnum
    /// </summary>
    public enum EnumApiOrderStatus
    {
        已取消 = -1,
        待审核 = 1,
        审核不通过 = 2,
        审核通过 = 10,
        待分配工程 = 20,
        待设计 = 25,
        工程文件制作 = 28,
        生产中 = 30,
        已发货 = 40,
        交易完成 = 100,
    }
}
