using System;
using Volo.Abp.Domain.Entities;

namespace Jiepei.InTradeConsumer.Domain.OrderMains
{
    public class OrderMain : Entity<int>
    {
        public string GroupNo { get; set; }
        public byte Status { get; set; }
        public decimal? TotalMoney { get; set; }
        public decimal? OrderMoney { get; set; }
        public decimal? ProMoney { get; set; }
        public decimal? OrderPayMoney { get; set; }

        public string ShipNo { get; protected set; }
        public DateTime? ShipDate { get; protected set; }
        public void SetStatus(byte status)
        {
            Status = status;
        }
        public void SetSendExpNo(string shipNo)
        {
            ShipNo = shipNo;
        }
        public void SetShipDate(DateTime? time)
        {
            ShipDate = time;
        }

        public void SetTotalMoney(decimal sellingMoney)
        {
            TotalMoney = sellingMoney;
            OrderMoney = sellingMoney;
            ProMoney = sellingMoney;
        }

        public void SetPayMoney(decimal orderPayMoney)
        {
            OrderPayMoney = orderPayMoney;
        }
    }
}
