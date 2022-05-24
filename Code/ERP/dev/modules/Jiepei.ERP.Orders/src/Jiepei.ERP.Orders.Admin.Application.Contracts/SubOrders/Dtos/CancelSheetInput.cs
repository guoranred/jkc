namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class CancelSheetInput
    {
        public CancelSheetInput()
        {

        }

        public CancelSheetInput(string cancelType, string cancelReason = "")
        {
            this.CancelType = cancelType;
            this.CancelReason = cancelReason;
        }



        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelType { get; set; }

        /// <summary>
        /// 取消说明
        /// </summary>
        public string CancelReason { get; set; }
    }
}
