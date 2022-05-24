using Jiepei.ERP.Shared.Enums.Customers;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Members.Admin
{
    public class UpdateCustomerServiceDto
    {
        /// <summary>
        /// 客服名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 客服手机号
        /// </summary>
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// 客服头像图片地址
        /// </summary>
        public string AvatarImage { get; set; }

        /// <summary>
        /// 客服微信名片图片地址
        /// </summary>
        public string WeChatImage { get; set; }

        /// <summary>
        /// 客服QQ号
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 客服邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 客服类型  0客服  1  业务员 2 渠道业务员
        /// </summary>
        public EnumCustomerServiceType Type { get; set; }

        /// <summary>
        /// 业务员推广码(选填)
        /// </summary>
        public string PromoCode { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// 业务线
        /// </summary>
        public string BusinessLine { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string JobNumber { get; set; }
    }
}
