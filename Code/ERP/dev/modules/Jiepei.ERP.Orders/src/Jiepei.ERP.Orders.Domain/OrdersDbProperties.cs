﻿namespace Jiepei.ERP.Orders
{
    public static class OrdersDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Orders";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Default";
    }
}
