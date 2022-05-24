using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Orders.Dtos
{
    public class DeliveryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 收货公司名
        /// </summary>
        public string ReceiverCompany { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string ReceiverAddress { get; set; }

        /// <summary>
        /// 收货人联系方式
        /// </summary>
        public string ReceiverTel { get; set; }

        /// <summary>
        /// 订单联系人
        /// </summary>
        public string OrderContactName { get; set; }

        /// <summary>
        /// 订单联系人手机号
        /// </summary>
        public string OrderContactMobile { get; set; }

        /// <summary>
        /// 订单联系人QQ
        /// </summary>
        public string OrderContactQQ { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string TrackingNo { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public string CourierCompany { get; set; }


        /// <summary>
        /// 省code(数据字典)
        /// </summary>
        ///
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 省名称(数据字典)
        /// </summary>
        ///
        public string ProvinceName { get; set; }


        /// <summary>
        /// 市code(数据字典)
        /// </summary>
        ///
        public string CityCode { get; set; }

        /// <summary>
        /// 市名称(数据字典)
        /// </summary>
        ///
        public string CityName { get; set; }


        /// <summary>
        /// 县code(数据字典)
        /// </summary>
        ///
        public string CountyCode { get; set; }

        /// <summary>
        /// 县名称(数据字典)
        /// </summary>
        ///
        public string CountyName { get; set; }
    }
}
