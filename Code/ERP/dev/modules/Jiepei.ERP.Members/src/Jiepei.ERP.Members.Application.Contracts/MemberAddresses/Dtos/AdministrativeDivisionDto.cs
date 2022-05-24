using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Members
{
    public class AdministrativeDivisionDto : EntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}
