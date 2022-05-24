using System.Collections.Generic;

namespace Jiepei.ERP.Orders.Application.External.Order.Models
{
    /// <summary>
    /// 创建订单
    /// </summary>
    public class CreateOrderDto
    {
        public CreateOrderDto()
        {
            OrderBomMaterialDTOs = new List<OrderBomMaterialDto>();
        }

        public CreateOrderDto(OrderInfoDto orderInfoDto)
        {
            OrderInfoDTO = orderInfoDto;
            OrderBomMaterialDTOs = new List<OrderBomMaterialDto>();
        }

        public CreateOrderDto(OrderInfoDto orderInfoDto, List<OrderBomMaterialDto> orderBomMaterialDtos)
        {
            OrderInfoDTO = orderInfoDto;

            OrderBomMaterialDTOs = orderBomMaterialDtos;
        }

        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderInfoDto OrderInfoDTO { get; set; }

        /// <summary>
        /// 订单零件信息
        /// </summary>
        public List<OrderBomMaterialDto> OrderBomMaterialDTOs { get; set; }
    }
}