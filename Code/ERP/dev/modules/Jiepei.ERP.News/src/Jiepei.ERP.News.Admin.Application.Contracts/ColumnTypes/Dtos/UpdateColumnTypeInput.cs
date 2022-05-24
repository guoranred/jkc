using System;

namespace Jiepei.ERP.News.Admin
{
    /// <summary>
    /// 栏目类型修改Dto
    /// </summary>
    [Serializable]
    public class UpdateColumnTypeInput
    {
        /// <summary>
        /// 父id
        /// </summary>
        public Guid? Pid { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 栏目别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 栏目类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// logo图片
        /// </summary>
        public string LogoImage { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 栏目所属  FAQ|新闻|公告
        /// </summary>
        public EnumColumnOwnership ColumnOwnership { get; set; }
    }
}
