using System;

namespace Jiepei.ERP.Orders.Materials.Dtos
{
    public class Calculation3DPriceDto
    {
        /// <summary>
        /// 渠道id
        /// </summary>
        public Guid ChannelId { get; set; }
        /// <summary>
        /// 材料id
        /// </summary>
        public Guid MaterialId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 后处理（表面处理）
        /// </summary>
        public string HandleMethod { get; set; }
        /// <summary>
        /// 后处理详情（表面处理）
        /// </summary>
        public string HandleMethodDesc { get; set; }
        //  public Dictionary<string,string> HandleMethodDesc { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Volume { get; set; }
    }
}
