using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.Orders
{
    public class OrderDelivery : FullAuditedAggregateRoot<Guid>
    {
        protected OrderDelivery()
        { }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; protected set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public decimal Weight { get; protected set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiverName { get; protected set; }

        /// <summary>
        /// 收货公司名
        /// </summary>
        public string ReceiverCompany { get; protected set; }

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
        /// 收货详细地址
        /// </summary>
        public string ReceiverAddress { get; protected set; }

        /// <summary>
        /// 收货人联系方式
        /// </summary>
        public string ReceiverTel { get; protected set; }

        /// <summary>
        /// 订单联系人
        /// </summary>
        public string OrderContactName { get; protected set; }

        /// <summary>
        /// 订单联系人手机号
        /// </summary>
        public string OrderContactMobile { get; protected set; }

        /// <summary>
        /// 订单联系人QQ
        /// </summary>
        public string OrderContactQQ { get; protected set; }

        public void SetOrderNo(string orderNo)
        {
            OrderNo = orderNo;
        }

        public void SetOrderContactName(string orderContactName)
        {
            OrderContactName = orderContactName;
        }

        public void SetOrderContactMobile(string orderContactMobile)
        {
            OrderContactMobile = orderContactMobile;
        }

        public void SetOrderContactQQ(string orderContactQQ)
        {
            OrderContactQQ = orderContactQQ;
        }

        public void SetWeight(decimal weight)
        {
            Weight = weight;
        }

        public void SetReceiverName(string receiverName)
        {
            ReceiverName = receiverName;
        }

        public void SetReceiverTel(string receiverTel)
        {
            ReceiverTel = receiverTel;
        }

        public void SetReceiverAddress(string receiverAddress)
        {
            ReceiverAddress = receiverAddress;
        }

        public void SetDelivery(string receiverName,
                        string receiverCompany,
                        string provinceCode,
                        string provinceName,
                        string cityCode,
                        string cityName,
                        string countyCode,
                        string countyName,
                        string receiverAddress,
                        string receiverTel
                        //string orderContactName,
                        //string orderContactMobile,
                        //string orderContactQQ
            )
        {
            ReceiverName=receiverName;
            ReceiverCompany=receiverCompany;
            ProvinceCode=provinceCode;
            ProvinceName=provinceName;
            CityCode=cityCode;
            CityName=cityName;
            CountyCode=countyCode;
            CountyName=countyName;
            ReceiverAddress=receiverAddress;
            ReceiverTel=receiverTel;
            //OrderContactName=orderContactName;
            //OrderContactMobile=orderContactMobile;
            //OrderContactQQ=orderContactQQ;

        }
    }
}