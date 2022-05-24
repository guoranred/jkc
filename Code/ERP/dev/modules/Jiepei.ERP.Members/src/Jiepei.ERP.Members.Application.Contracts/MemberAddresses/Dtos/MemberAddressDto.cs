using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Members
{
    /// <summary>
    /// 收货地址
    /// </summary>
    public class MemberAddressDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 收货人
        /// </summary>
        ///
        public virtual string Recipient { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CompanyName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        ///
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 省code(数据字典)
        /// </summary>
        ///
        public virtual string ProvinceCode { get; set; }

        /// <summary>
        /// 省名称(数据字典)
        /// </summary>
        ///
        public virtual string ProvinceName { get; set; }

        /// <summary>
        /// 市code(数据字典)
        /// </summary>
        ///
        public virtual string CityCode { get; set; }

        /// <summary>
        /// 市名称(数据字典)
        /// </summary>
        ///
        public virtual string CityName { get; set; }

        /// <summary>
        /// 县code(数据字典)
        /// </summary>
        ///
        public virtual string CountyCode { get; set; }

        /// <summary>
        /// 县名称(数据字典)
        /// </summary>
        ///
        public virtual string CountyName { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        ///
        public virtual string DetailAddress { get; set; }

        /// <summary>
        /// 是否默认地址
        /// </summary>
        public virtual bool IsDefault { get; set; }
    }
}
