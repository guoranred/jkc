using System;

namespace Jiepei.ERP.Orders.Materials.Dtos
{
    public class CalculationResultDto
    {
        public Guid MaterialId { get; set; }
        public decimal Price { get; set; }
        public int Delivery { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal unitPrice { get; set; }

    }
}
