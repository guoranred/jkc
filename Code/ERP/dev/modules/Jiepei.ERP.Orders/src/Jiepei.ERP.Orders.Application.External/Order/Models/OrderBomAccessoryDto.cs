namespace Jiepei.ERP.Orders.Application.External.Order.Models
{
    public class OrderBomAccessoryDto
    {
        /// <summary>
        ///辅料数量
        /// </summary> 
        public decimal AccessoryNum { get; set; }

        /// <summary>
        ///辅料高度
        /// </summary> 
        public decimal AccessoryHeight { get; set; }

        /// <summary>
        ///物料名称
        /// </summary> 
        public int AccessoryId { get; set; }

        /// <summary>
        ///物料名称
        /// </summary> 
        public string AccessoryName { get; set; }

        /// <summary>
        ///物料品牌/系列
        /// </summary> 
        public int AccessorySeriesId { get; set; }

        /// <summary>
        ///物料品牌/系列
        /// </summary> 
        public string AccessorySeries { get; set; }

        /// <summary>
        ///物料型号
        /// </summary> 
        public int AccessorySpecId { get; set; }

        /// <summary>
        ///物料型号
        /// </summary> 
        public string AccessorySpec { get; set; }
    }
}
