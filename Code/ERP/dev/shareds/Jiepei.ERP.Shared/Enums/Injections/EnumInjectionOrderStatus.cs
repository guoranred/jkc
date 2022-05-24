namespace Jiepei.ERP.Injections
{
    public enum EnumInjectionOrderStatus
    {
        /// <summary>
        /// 默认
        /// </summary>  
        [EnumDesc("默认")]
        Default = 0,

        /// <summary>
        /// 审核中
        /// </summary>  
        [EnumDesc("审核中")]
        WaitCheck = 1,

        /// <summary>
        /// 审核不通过
        /// </summary>    
        [EnumDesc("审核不通过")]
        CheckedNoPass = 2,

        /// <summary>
        /// 审核通过
        /// </summary>
        [EnumDesc("审核通过")]
        CheckedPass = 10,

        /// <summary>
        /// 报价完成
        /// </summary>  
        [EnumDesc("报价完成")]
        OfferComplete = 20,

        /// <summary>
        /// 取消
        /// </summary>  
        [EnumDesc("取消")]
        Cancel = 30,

        /// <summary>
        /// 确认下单
        /// </summary> 
        [EnumDesc("确认下单")]
        SureOrder = 40,

        /// <summary>
        /// 生产中
        /// </summary>  
        [EnumDesc("生产中")]
        Purchasing = 70,

        /// <summary>
        /// 等待发货
        /// </summary>  
        [EnumDesc("等待发货")]
        WaitSend = 80,

        /// <summary>
        /// 已发货
        /// </summary>  
        [EnumDesc("已发货")]
        HaveSend = 90,

        /// <summary>
        /// 交易成功
        /// </summary>  
        [EnumDesc("交易成功")]
        Finish = 100
    }
}