namespace Jiepei.ERP.Orders.Orders.Dtos
{
    public class OrderDetailDto
    {
        public OrderDto Order { get; set; }
        public DeliveryDto DeliveryInfo { get; set; }

        public CostDto CostInfo { get; set; }
        public object SubOrder { get; set; }
    }
}
