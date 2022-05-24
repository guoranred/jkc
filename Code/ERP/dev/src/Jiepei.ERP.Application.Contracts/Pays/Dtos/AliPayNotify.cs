using Microsoft.AspNetCore.Mvc;
using System;

namespace Jiepei.ERP.Pays.Dtos
{
    public class AliPayNotify
    {
        /// <summary>
        /// 通知时间
        /// </summary>      
        [FromForm(Name = "notify_time")]
        public DateTime NotifyTime { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        [FromForm(Name = "notify_type")]
        public string NotifyType { get; set; }

        /// <summary>
        /// 通知校验id
        /// </summary>
        [FromForm(Name = "notify_id")]
        public string NotifyId { get; set; }

        /// <summary>
        /// 支付宝分配给开发者的应用Id
        /// </summary>
        [FromForm(Name = "app_id")]
        public string AppId { get; set; }

        /// <summary>
        /// 编码格式，如utf-8、gbk、gb2312等
        /// </summary>
        [FromForm(Name = "charset")]
        public string Charset { get; set; }

        /// <summary>
        /// 调用的接口版本
        /// </summary>
        [FromForm(Name = "version")]
        public string Version { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        [FromForm(Name = "sign_type")]
        public string SignType { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [FromForm(Name = "sign")]
        public string Sign { get; set; }

        /// <summary>
        /// 支付宝交易号
        /// </summary>
        [FromForm(Name = "trade_no")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [FromForm(Name = "out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 商户业务号，主要是退款通知中返回退款申请的流水号
        /// </summary>
        [FromForm(Name = "out_biz_no")]
        public string OutBizNo { get; set; }

        /// <summary>
        /// 买家支付宝用户号
        /// </summary>
        [FromForm(Name = "buyer_id")]
        public string BuyerId { get; set; }

        /// <summary>
        /// 买家支付宝账号
        /// </summary>
        [FromForm(Name = "buyer_logon_id")]
        public string BuyerLogonId { get; set; }

        /// <summary>
        /// 卖家支付宝用户号
        /// </summary>
        [FromForm(Name = "seller_id")]
        public string SellerId { get; set; }

        /// <summary>
        /// 卖家支付宝账号
        /// </summary>
        [FromForm(Name = "seller_email")]
        public string SellerEmail { get; set; }

        /// <summary>
        /// 交易状态 WAIT_BUYER_PAY 交易创建，等待买家付款， TRADE_CLOSED 未付款交易超时关闭，或支付完成后全额退款， TRADE_SUCCESS 交易支付成功， TRADE_FINISHED 交易结束，不可退款
        /// </summary>
        [FromForm(Name = "trade_status")]
        public string TradeStatus { get; set; }

        /// <summary>
        /// 订单金额(元)
        /// </summary>
        [FromForm(Name = "total_amount")]
        public decimal? TotalAmount { get; set; }

        /// <summary>
        /// 实收金额 商家在交易中实际收到的款项，单位为元
        /// </summary>
        [FromForm(Name = "receipt_amount")]
        public decimal? ReceiptAmount { get; set; }

        /// <summary>
        /// 开票金额 用户在交易中支付的可开发票的金额
        /// </summary>
        [FromForm(Name = "invoice_amount")]
        public decimal? InvoiceAmount { get; set; }

        /// <summary>
        /// 付款金额 用户在交易中支付的金额
        /// </summary>
        [FromForm(Name = "buyer_pay_amount")]
        public decimal? BuyerPayAmount { get; set; }

        /// <summary>
        /// 集分宝金额 使用集分宝支付的金额
        /// </summary>
        [FromForm(Name = "point_amount")]
        public decimal? PointAmount { get; set; }

        /// <summary>
        /// 总退款金额
        /// </summary>
        [FromForm(Name = "refund_fee")]
        public decimal? RefundFee { get; set; }

        /// <summary>
        /// 订单标题
        /// </summary>
        [FromForm(Name = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        [FromForm(Name = "body")]
        public string Body { get; set; }

        /// <summary>
        /// 交易创建时间 该笔交易创建的时间。格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        [FromForm(Name = "gmt_create")]
        public DateTime? GmtCreate { get; set; }

        /// <summary>
        /// 交易付款时间 该笔交易的买家付款时间。格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        [FromForm(Name = "gmt_payment")]
        public DateTime? GmtPayment { get; set; }

        /// <summary>
        /// 交易退款时间 该笔交易的退款时间。格式为yyyy-MM-dd HH:mm:ss.S
        /// </summary>
        [FromForm(Name = "gmt_refund")]
        public DateTime? GmtRefund { get; set; }

        /// <summary>
        /// 交易结束时间 该笔交易结束时间。格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        [FromForm(Name = "gmt_close")]
        public DateTime? GmtClose
        {
            get; set;
        }

        /// <summary>
        /// 支付金额信息 支付成功的各个渠道金额信息  [{“amount”:“15.00”,“fundChannel”:“ALIPAYACCOUNT”}]
        /// </summary>
        [FromForm(Name = "fund_bill_list")]
        public string FundBillList
        {
            get; set;
        }

        /// <summary>
        /// 回传参数，如果请求时传递了该参数，则返回给商户时会在异步通知时将该参数原样返回。
        /// </summary>
        [FromForm(Name = "passback_params")]
        public string PassbackParams
        {
            get; set;
        }

        /// <summary>
        /// 优惠券信息 本交易支付时所使用的所有优惠券信息 
        /// </summary>
        [FromForm(Name = "voucher_detail_list")]
        public string VoucherDetailList
        {
            get; set;
        }
    }
}
