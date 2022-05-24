using System;
using Volo.Abp.Domain.Entities;

namespace Jiepei.InTradeConsumer.OrderDetails
{
    public class OrderDetail : Entity<int>
    {
        public int MainId { get; set; }
        public string ProName { get; set; }
        public int Status { get; set; }
        public decimal? PayMoney { get; set; }
        public decimal? CostMoney { get; set; }
        public decimal? TotalMoney { get; set; }
        public bool? IsSendToCustomer { get; protected set; }
        public string SendExpName { get; protected set; }
        public string SendExpNo { get; protected set; }
        public DateTime? SendTime { get; protected set; }
        public DateTime? SureConfirmTime { get; protected set; }
        public DateTime? FinishTime { get; protected set; }

        public void SetStatus(int status)
        {
            Status = status;
        }
        public void SetIsSendToCustomer(bool isSendToCustomer)
        {
            IsSendToCustomer = isSendToCustomer;
        }

        public void SetFinishTime(DateTime? time)
        {
            FinishTime = time;
        }

        public void SetSureConfirmTime(DateTime? time)
        {
            SureConfirmTime = time;
        }

        public void SetSendExpName(string sendExpName)
        {
            SendExpName = sendExpName;
        }

        public void SetSendExpNo(string sendExpNo)
        {
            SendExpNo = sendExpNo;
        }

        public void SetSendTime(DateTime? time)
        {
            SendTime = time;
        }


        public void SetTotalMoney(decimal? sellingMoney)
        {
            //PayMoney = sellingMoney;
            TotalMoney = sellingMoney;
            CostMoney = sellingMoney;
        }

        public void SetPayMoney(decimal? payMoney)
        {
            PayMoney = payMoney;
        }
    }
}
