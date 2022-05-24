using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.InTradeConsumer.Domain.Shareds
{
    /// <summary>
    /// 订单包状态
    /// </summary>
    public enum EnumOrderMainStatus
    {
        /// <summary>
        /// 等待确认
        /// </summary>
        WaitSure = 1,

        /// <summary>
        /// 确认下单
        /// </summary>
        SureOrder = 2,

        /// <summary>
        /// 订单取消
        /// </summary>
        CancelOrder = 3,

        /// <summary>
        /// 等待发货
        /// </summary>
        WaitSend = 4,

        /// <summary>
        /// 已发货
        /// </summary>
        HaveSend = 5,

        /// <summary>
        /// 交易成功
        /// </summary>
        Finish = 100
    }
}
