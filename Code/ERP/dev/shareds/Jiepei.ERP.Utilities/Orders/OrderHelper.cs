using Jiepei.ERP.Orders;
using System;

namespace Jiepei.ERP.Utilities
{
    public static class OrderHelper
    {
        public static string CreateOrderNo()
        {
            var orderNo = OrderCompose();
            return orderNo;
        }
        public static string CreateOrderNo(EnumOrderType orderType)
        {
            var orderNo = OrderCompose() + "-" + GetOrderType(orderType);
            return orderNo;
        }

        public static string GetOrderType(EnumOrderType orderType)
        {
            switch (orderType)
            {
                case EnumOrderType.Mold:
                    return "MJJKC";
                case EnumOrderType.Injection:
                    return "ZSJKC";
                case EnumOrderType.Cnc:
                    return "CNCJKC";
                case EnumOrderType.Print3D:
                    return "3DJKC";
                case EnumOrderType.SheetMetal:
                    return "BJJKC";
                default:
                    return "";
            }
        }

        public static string CreatePayCode()
        {
            return OrderCompose();
        }

        private static string OrderCompose()
        {
            var orderNo = DateTime.Now.ToString("yyyyMMddHHmm") + string.Format("{0:000000}", new Random().Next(1, 999999));
            return orderNo;
        }
    }
}
