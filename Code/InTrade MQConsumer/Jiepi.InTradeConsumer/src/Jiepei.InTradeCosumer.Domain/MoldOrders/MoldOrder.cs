using System;
using Volo.Abp.Domain.Entities;

namespace Jiepei.InTradeConsumer.MoldOrders
{
    public class MoldOrder : Entity<int>
    {
        public string OrderNo { get; set; }
        public string GroupNo { get; set; }
        public int? MbId { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ApplicationArea { get; set; }
        public string Usage { get; set; }
        public string Remark { get; set; }
        public string ProductName { get; set; }
        public string Picture { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int? Material { get; set; }
        public int? Qty { get; set; }
        public string Size { get; set; }
        public decimal? Weight { get; set; }
        public string Color { get; set; }
        public int? Surface { get; set; }
        public int? Status { get; set; }
        public decimal? ProductAfterAmt { get; set; }
        public decimal? ProductAmt { get; set; }
        public string CheckedReason { get; set; }

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
            ProductAfterAmt = sellingMoney;
            ProductAmt = sellingMoney;
        }
        public void SetFileName(string fileName)
        {
            FileName = fileName;
        }
        public void SetFilePath(string filePath)
        {
            FilePath = filePath;
        }
        public void SetPicture(string picture)
        {
            Picture = picture;
        }
    }
}
