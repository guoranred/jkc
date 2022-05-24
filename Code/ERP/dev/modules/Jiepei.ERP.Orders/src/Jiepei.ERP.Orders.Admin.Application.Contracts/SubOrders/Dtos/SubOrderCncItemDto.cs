using Jiepei.ERP.Cncs;

namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class SubOrderCncItemDto : ISubOrderItem
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 产品文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 产品文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 产品尺寸
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 材料
        /// </summary>
        public EnumCncMaterial Material { get; set; }

        /// <summary>
        /// 表面处理
        /// </summary>
        public EnumCncSurface Surface { get; set; }

        /// <summary>
        /// 应用领域
        /// </summary>
        public EnumApplicationArea ApplicationArea { get; set; }

        /// <summary>
        /// 预计年使用量
        /// </summary>
        public EnumUsage Usage { get; set; }

        /// <summary>
        /// 材料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 表面处理名称
        /// </summary>
        public string SurfaceName { get; set; }
    }
}
