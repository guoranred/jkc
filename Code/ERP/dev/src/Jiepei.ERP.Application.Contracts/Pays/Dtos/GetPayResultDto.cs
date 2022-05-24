using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Pays.Dtos
{
    public class GetPayResultDto
    {
        /// <summary>
        /// 支付单号
        /// </summary>
        /// 
        [Required(AllowEmptyStrings = false, ErrorMessage = "参数错误")]
        public string PayNo { get; set; }
    }
}
