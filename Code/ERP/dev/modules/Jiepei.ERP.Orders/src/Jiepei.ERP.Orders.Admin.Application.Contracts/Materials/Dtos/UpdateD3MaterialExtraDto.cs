namespace Jiepei.ERP.Orders.Materials.Dtos
{
    public class UpdateD3MaterialExtraDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string PartCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 密度
        /// </summary>
        public string Density { get; set; }
        /// <summary>
        /// 交期(天)
        /// </summary>
        public int? Delivery { get; set; }
        /// <summary>
        /// 特性
        /// </summary>
        public string Attr { get; set; }
        /// <summary>
        /// 优点
        /// </summary>
        public string Excellence { get; set; }
        /// <summary>
        /// 缺点
        /// </summary>
        public string Short { get; set; }
        /// <summary>
        /// 颜色(对应内贸可选类型) 是否枚举？
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 最小单件重量
        /// </summary>
        public decimal? MinSinWeight { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}
