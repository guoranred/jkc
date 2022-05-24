using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class DesignChangeInput
    {
        /// <summary>
        /// 产品图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }


        /// <summary>
        /// 产品价
        /// </summary>
        public decimal ProMoney { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string remark { get; set; }
    }
}
