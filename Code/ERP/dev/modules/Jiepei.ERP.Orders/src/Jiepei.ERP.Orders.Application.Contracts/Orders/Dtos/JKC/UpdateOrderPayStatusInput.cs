namespace Jiepei.ERP.Orders
{
    public class UpdateOrderPayStatusInput
    {

        public UpdateOrderPayStatusInput(bool isPay = true)
        {
            this.IsPay = isPay;
        }


        /// <summary>
        /// 支付状态
        /// </summary>
        public bool IsPay { get; set; } = true;
    }
}
