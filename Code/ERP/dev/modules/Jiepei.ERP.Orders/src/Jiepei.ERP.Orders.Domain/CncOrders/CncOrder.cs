using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Jiepei.ERP.Orders.CncOrders
{
    public class CncOrder : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public CncOrder() { }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 主订单号
        /// </summary>
        public string MainOrderNo { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 特殊备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 客服备注
        /// </summary>
        public string CustomerRemark { get; set; }

        public Guid? TenantId { get; set; }

        public void SetStatus(int status)
        {
            Status = status;
        }
        public void SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
        }

        public void SetOrderNo(string orderNo)
        {
            OrderNo = orderNo;
        }

        public void SetMainNo(string mainOrderNo)
        {
            MainOrderNo = mainOrderNo;
        }

    }
}
