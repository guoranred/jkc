using Jiepei.ERP.Members.Enums;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Members
{
    public class AdministrativeDivision : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 父id
        /// </summary>
        public virtual Guid? Pid { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 地区级别
        /// </summary>
        public virtual AdministrativeDivisionLevel Level { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 地区英文名称
        /// </summary>
        public virtual string EnglishName { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 关联内贸Id
        /// </summary>
        public virtual int RelationNmId { get; set; }

        protected AdministrativeDivision() { }

        public AdministrativeDivision(Guid id, Guid? pid, string code, string name, string englishName, AdministrativeDivisionLevel level, int sort) : base(id)
        {
            Id = id;
            Code = Check.NotNull(code, nameof(code));
            Name = name;
            EnglishName = englishName;
            Pid = pid;
            Level = level;
            Sort = sort;
        }
    }
}
