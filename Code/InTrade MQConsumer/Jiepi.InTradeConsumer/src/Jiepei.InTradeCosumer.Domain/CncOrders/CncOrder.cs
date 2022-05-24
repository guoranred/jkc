using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Jiepei.InTradeConsumer.Domain.CncOrders
{
    public class CncOrder : Entity<int>
    { 
        public string OrderNo { get; set; }
        public string GroupNo { get; set; }
        public int? MbId { get; set; }
        public int? DeliveryDays { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? ApplicationArea { get; set; }
        public string Remark { get; set; }
        public string CheckedReason { get; set; }
        public int? CheckdId { get; set; }
        public DateTime? CheckedTime { get; set; }
        public int? OfferId { get; set; }
        public DateTime? OfferTime { get; set; }
        public DateTime? OfferModifyTime { get; set; }
        public decimal? Amt { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CustomerRemark { get; set; }

        public void SetStaus(int status) => Status = status;

        public void SetCheckedReason(string checkedReason) => CheckedReason = checkedReason;

        public void Check(int status, string checkedReason)
        {
            SetStaus(status);
            SetCheckedReason(checkedReason);
        }

        public void Offer(int status, decimal sellingMoney)
        {
            SetStaus(status);
            Amt = sellingMoney;
        }
    }
}
