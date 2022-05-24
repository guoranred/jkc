using Jiepei.ERP.Orders;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.SubOrders
{
    [EventName("Erp.SubOrder.DesignChange")]
    public class SubOrderDesignChangeEto : OrderEventBaseDto
    {
        /// <summary>
        /// 产品图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal? TotalMoney { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal? SellingMoney { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public SubOrderDesignChangeEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="picture">产品图片</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="totalMoney">成本价</param>
        /// <param name="sellingMoney">销售价</param>
        /// <param name="remark">备注</param>
        /// <param name="orderType"></param>
        public SubOrderDesignChangeEto(string orderNo, string picture, string fileName, string filePath, decimal? totalMoney, decimal? sellingMoney, string remark, EnumOrderType orderType)
        {
            OrderNo = orderNo;
            Picture = picture;
            FileName = fileName;
            FilePath = filePath;
            SellingMoney = sellingMoney;
            TotalMoney = totalMoney;
            Remark = remark;
            OrderType = orderType;
        }

    }
}
