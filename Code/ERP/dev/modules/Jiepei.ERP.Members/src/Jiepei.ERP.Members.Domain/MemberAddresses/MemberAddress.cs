using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Members
{
    /// <summary>
    /// 会员收货地址
    /// </summary>
    public class MemberAddress : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public virtual Guid MemberId { get; protected set; }

        /// <summary>
        /// 收货人
        /// </summary>
        /// 
        public virtual string Recipient { get; protected set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CompanyName { get; protected set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        ///
        public virtual string PhoneNumber { get; protected set; }

        /// <summary>
        /// 省code(数据字典)
        /// </summary>
        ///
        public virtual string ProvinceCode { get; protected set; }

        /// <summary>
        /// 省名称(数据字典)
        /// </summary>
        ///
        public virtual string ProvinceName { get; protected set; }


        /// <summary>
        /// 市code(数据字典)
        /// </summary>
        ///
        public virtual string CityCode { get; protected set; }

        /// <summary>
        /// 市名称(数据字典)
        /// </summary>
        ///
        public virtual string CityName { get; protected set; }


        /// <summary>
        /// 县code(数据字典)
        /// </summary>
        ///
        public virtual string CountyCode { get; protected set; }

        /// <summary>
        /// 县名称(数据字典)
        /// </summary>
        ///
        public virtual string CountyName { get; protected set; }


        /// <summary>
        /// 详细地址
        /// </summary>
        ///
        public virtual string DetailAddress { get; protected set; }

        /// <summary>
        /// 是否默认地址
        /// </summary>
        public virtual bool IsDefault { get; protected set; }

        protected MemberAddress() { }

        public MemberAddress(Guid id, Guid memberId,
                             string recipient,
                             string companyName,
                             string phoneNumber,
                             string provinceCode,
                             string provinceName,
                             string cityCode,
                             string cityName,
                             string countyCode,
                             string countyName,
                             string detailAddress,
                             bool isDefault = true)
        {
            Id = id;
            MemberId = memberId;
            Recipient = recipient;
            CompanyName = companyName;
            PhoneNumber = phoneNumber;
            ProvinceCode = provinceCode;
            ProvinceName = provinceName;
            CityCode = cityCode;
            CityName = cityName;
            CountyCode = countyCode;
            CountyName = countyName;
            DetailAddress = detailAddress;
            IsDefault = isDefault;
        }

        /// <summary>
        /// 设置地区信息
        /// </summary>
        public void SetRegion(string recipient,
                              string companyName,
                              string phoneNumber,
                              string provinceCode,
                              string provinceName,
                              string cityCode,
                              string cityName,
                              string countyCode,
                              string countyName,
                              string detailAddress,
                              bool isDefault = true)
        {
            Recipient = recipient;
            CompanyName = companyName;
            PhoneNumber = phoneNumber;
            ProvinceCode = provinceCode;
            ProvinceName = provinceName;
            CityCode = cityCode;
            CityName = cityName;
            CountyCode = countyCode;
            CountyName = countyName;
            DetailAddress = detailAddress;
            IsDefault = isDefault;
        }

        public void RemoveDefault()
        {
            IsDefault = false;
        }
    }
}
