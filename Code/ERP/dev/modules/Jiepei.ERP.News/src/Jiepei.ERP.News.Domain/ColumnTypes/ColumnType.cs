
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.News
{
    /// <summary>
    /// 项目类型
    /// </summary>
    public class ColumnType : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public virtual Guid? ChannelId { get; set; }

        /// <summary>
        /// 父id
        /// </summary>
        public virtual Guid? Pid { get; set; }

        /// <summary>
        /// 栏目编号
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 栏目别名
        /// </summary>
        public virtual string Alias { get; set; }

        /// <summary>
        /// 栏目类型
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// logo图片
        /// </summary>
        public virtual string LogoImage { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 栏目所属  FAQ|新闻|公告
        /// </summary>
        public EnumColumnOwnership ColumnOwnership { get; set; }

        public ColumnType() { }

        public ColumnType(Guid? channelId, Guid id, Guid? pid, string code, string name, string alias, string type, string logoImage, string remark, EnumColumnOwnership columnOwnership)
        {
            ChannelId = channelId;
            Id = id;
            Pid = pid;
            Code = code;
            Name = name;
            Alias = alias;
            Type = type;
            LogoImage = logoImage;
            Remark = remark;
            ColumnOwnership = columnOwnership;
        }

        /// <summary>
        /// 设置栏目类型
        /// </summary>
        /// <param name="Pid"></param>
        /// <param name="Name"></param>
        /// <param name="Alias"></param>
        /// <param name="Type"></param>
        /// <param name="LogoImage"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        internal ColumnType SetColumnType(Guid? Pid, string Name, string Alias, string Type, string LogoImage, string Remark, EnumColumnOwnership ColumnOwnership)
        {
            this.Pid = Pid;
            this.Name = Name;
            this.Alias = Alias;
            this.Type = Type;
            this.LogoImage = LogoImage;
            this.Remark = Remark;
            this.ColumnOwnership = ColumnOwnership;
            return this;
        }
    }
}
