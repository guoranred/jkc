using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Suppliers.Suppliers
{
    [Table("Supplier")]
    public class Supplier : AuditedAggregateRoot<Guid>
    {
        protected Supplier() { }
        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 系统秘钥
        /// </summary>
        public virtual string SysKey { get; set; }

        /// <summary>
        /// 回调
        /// </summary>
        public virtual string SysCallback { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsEnable { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Note { get; set; }
    }
}
