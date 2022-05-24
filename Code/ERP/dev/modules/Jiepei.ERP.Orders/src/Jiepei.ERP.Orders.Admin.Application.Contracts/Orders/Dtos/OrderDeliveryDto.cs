using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.BizMO.DeliverCenters.Orders.Orders.Dtos
{
    public class OrderDeliveryDto 
    {
        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// 收货公司名
        /// </summary>
        public string ReceiverCompany { get; set; }
        /// <summary>
        /// 省code
        /// </summary>
        public string ProvinceCode { get; set; }
        /// <summary>
        /// 省Name
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 市code
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 市Name
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 县区Code
        /// </summary>
        public string CountyCode { get; set; }
        /// <summary>
        /// 县区Name
        /// </summary>
        public string CountyName { get; set; }
        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string ReceiverAddress { get; set; }
        /// <summary>
        /// 收货人联系方式
        /// </summary>
        public string ReceiverTel { get; set; }
        ///// <summary>
        ///// 订单联系人
        ///// </summary>
        //public string OrderContactName { get; set; }
        ///// <summary>
        ///// 订单联系人手机号
        ///// </summary>
        //public string OrderContactMobile { get; set; }
        ///// <summary>
        ///// 订单联系人QQ/微信
        ///// </summary>
        //public string OrderContactQQ { get; set; }
    }
}
