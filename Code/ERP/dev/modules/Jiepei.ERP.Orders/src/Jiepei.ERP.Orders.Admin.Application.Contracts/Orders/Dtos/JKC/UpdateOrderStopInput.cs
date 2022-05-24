namespace Jiepei.ERP.Orders.Admin
{
    public class UpdateOrderStopInput
    {
        public UpdateOrderStopInput()
        {

        }

        public UpdateOrderStopInput(bool isStopExec = false)
        {
            this.IsStopExec = isStopExec;
        }

        /// <summary>
        /// 是否停止执行  确认下单之后,应该是正常执行还是暂停执行(暂停执行则订单生产会暂停)
        /// </summary>
        public bool IsStopExec { get; set; } = false;
    }
}
