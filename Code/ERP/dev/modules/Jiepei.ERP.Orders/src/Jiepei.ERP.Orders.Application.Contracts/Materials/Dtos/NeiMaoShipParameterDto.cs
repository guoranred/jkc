namespace Jiepei.ERP.Orders.Materials.Dtos
{
    public class NeiMaoShipParameterDto
    {
        public NeiMaoShipParameterDto(string shipUrl, string appsecret, long timestamp, string shipTpe, decimal weight, decimal tjWeight, string provinceName, int provinceId, int grouptype, int promoney, int mbId)
        {
            ShipUrl = shipUrl;
            Appsecret = appsecret;
            Timestamp = timestamp;
            ShipTpe = shipTpe;
            Weight = weight;
            TjWeight = tjWeight;
            ProvinceName = provinceName;
            ProvinceId = provinceId;
            Grouptype = grouptype;
            Promoney = promoney;
            MbId = mbId;
        }

        //内贸运费计价地址
        public string ShipUrl { get; set; }
        /// <summary>
        /// 认证码
        /// </summary>
        public string Appsecret { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
        /// <summary>
        /// 快递类型
        /// </summary>
        public string ShipTpe { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 特价重量 
        /// </summary>
        public decimal TjWeight { get; set; }
        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 省Id
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 订单类型，只有5 元器件商城订单有效 5.1 纯捷配仓 5.2 有三方仓单仓不合并 5.3 有三方仓单仓合并 5.4 有三方多仓合并 5.5 有三方仓多仓不合并  
        /// </summary>
        public int Grouptype { get; set; }
        /// <summary>
        /// 订单产品费，只有类型5元器件商城有效  
        /// </summary>
        public int Promoney { get; set; }
        /// <summary>
        /// 会员id
        /// </summary>
        public int MbId { get; set; }
    }
}