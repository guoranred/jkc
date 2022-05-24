
namespace Jiepei.ERP.Members
{
    public class GetCurrentUserCustomerServiceOutputDto
    {
        /// <summary>
        /// 客服名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 客服手机号
        /// </summary>
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
    }
}