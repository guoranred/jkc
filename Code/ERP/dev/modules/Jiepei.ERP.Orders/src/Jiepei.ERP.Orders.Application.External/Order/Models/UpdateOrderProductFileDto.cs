using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.Application.External.Order.Models
{
    public class UpdateOrderProductFileDto
    {
        public UpdateOrderProductFileDto()
        {

        }

        public UpdateOrderProductFileDto(string orderNo, string fileName, string filePath)
        {
            this.OrderNo = orderNo;
            this.FileName = fileName;
            this.FilePath = filePath;
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        [Required]
        public string OrderNo { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
    }
}
