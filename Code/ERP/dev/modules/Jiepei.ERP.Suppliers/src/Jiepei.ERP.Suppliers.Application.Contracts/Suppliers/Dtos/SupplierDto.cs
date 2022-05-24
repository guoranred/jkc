using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Suppliers.Suppliers.Dtos
{
    public class SupplierDto : EntityDto<Guid>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 系统秘钥
        /// </summary>
        public string SysKey { get; set; }

        /// <summary>
        /// 回调
        /// </summary>
        public string SysCallback { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}
