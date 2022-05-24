namespace Jiepei.ERP.Members
{
    public class UpdateMemberAddressInput
    {
        /// <summary>
        /// 收货人
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 省code(数据字典)
        /// </summary>
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 省名称(数据字典)
        /// </summary>
        public string ProvinceName { get; set; }


        /// <summary>
        /// 市code(数据字典)
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 市名称(数据字典)
        /// </summary>
        public string CityName { get; set; }


        /// <summary>
        /// 县code(数据字典)
        /// </summary>
        public string CountyCode { get; set; }

        /// <summary>
        /// 县名称(数据字典)
        /// </summary>
        public string CountyName { get; set; }


        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailAddress { get; set; }

        /// <summary>
        /// 是否默认地址
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
