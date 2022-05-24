namespace Jiepei.ERP.SubOrders
{
    public enum EnumSubOrderFlowType
    {
        [EnumDesc("创建订单")]
        Create = 1,
        [EnumDesc("审核通过")]
        Check = 10,
        [EnumDesc("修改交期")]
        DeliveryDay = 11,
        [EnumDesc("修改产品套数")]
        ProductNum = 12,
        [EnumDesc("审核不通过")]
        CheckNoPass = 15,
        [EnumDesc("报价")]
        Offer = 20,
        [EnumDesc("付款")]
        Payment = 30,
        [EnumDesc("投产")]
        Manufacture = 40,
        [EnumDesc("发货")]
        Deliver = 50,
        [EnumDesc("设计变更")]
        DesignChange = 60,
        [EnumDesc("交期变更")]
        ChangeDeliveryDays = 70,
        [EnumDesc("取消")]
        Cancel = 99,
        [EnumDesc("完成")]
        Complete = 100,

        [EnumDesc("修改状态")]
        UpdateStatus = 101,


    }
}
