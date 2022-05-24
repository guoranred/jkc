namespace Jiepei.ERP.Pays.Dtos
{
    public class CreatePayOutputDto
    {
        /// <summary>
        /// 支付二维码base64
        /// </summary>
        public string QRCodeBase64 { get; set; }

        /// <summary>
        /// 支付单号
        /// </summary>
        public string PayNo { get; set; }
    }
}
