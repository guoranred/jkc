using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.InTradeConsumer.Domain.Shareds
{
    /// <summary>
    /// 订单详情状态
    /// </summary>
    public enum EnumOrderDetailStatus
    {

        /// <summary>
        /// 待审核
        /// </summary>      
        WaitCheck = 1,

        /// <summary>
        /// 取消
        /// </summary>
        Cancel = 2,

        /// <summary>
        /// 审核不通过
        /// </summary>
        CheckedNoPass = 3,

        /// <summary>
        /// 审核通过
        /// </summary>
        CheckedPass = 4,

        /// <summary>
        /// 确定下单
        /// </summary>
        SureOrder = 5,

        /// <summary>
        /// 生产中
        /// </summary>
        Purchasing = 6,

        /// <summary>
        /// 已退款
        /// </summary>
        HaveRefund = 7,

        /// <summary>
        /// 等待发货
        /// </summary>
        WaitSend = 8,

        /// <summary>
        /// 已发货
        /// </summary>
        HaveSend = 9,

        /// <summary>
        /// 交易成功
        /// </summary>
        Finish = 100
    }
}
