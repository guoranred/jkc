namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class SubOrderDetailDto
    {
        public SubOrderDto SubOrder { get; set; }
        public ISubOrderItem SubOrderItem { get; set; }
        public SubOrderFlowDto SubOrderFlow { get; set; }
    }
}
