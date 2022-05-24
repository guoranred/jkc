namespace Jiepei.ERP.Cncs
{
    public enum EnumCncOrderStatus
    {
        /// <summary>
        /// 默认
        /// </summary>    
        [EnumDesc("默认")]
        Default = 0,

        /// <summary>
        /// 待审核
        /// </summary>   
        [EnumDesc("待审核")]
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
        /// 确定下单
        /// </summary> 
        [EnumDesc("确定下单")]
        SureOrder = 40,

        /// <summary>
        /// 工艺设计
        /// </summary>  
        [EnumDesc("工艺设计")]
        Design = 50,

        /// <summary>
        /// 产品生产
        /// </summary>   
        [EnumDesc("产品生产")]
        Production = 55,

        /// <summary>
        /// 检验
        /// </summary>  
        [EnumDesc("检验")]
        Sold = 60,

        /// <summary>
        /// 入库
        /// </summary>  
        [EnumDesc("入库")]
        Entry = 65,

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
