using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.Materials
{
    public class MaterialSupplier : AuditedAggregateRoot<Guid>
    {

        public MaterialSupplier(Guid id, Guid materialId, Guid supplierId, string supplierName, string supplierSpuId)
        {
            Id = id;
            MaterialId = materialId;
            SupplierId = supplierId;
            SupplierName = supplierName;
            SupplierSpuId = supplierSpuId;
        }

        protected MaterialSupplier() { }
        /// <summary>
        /// 材料id
        /// </summary>
        public virtual Guid MaterialId { get; set; }
        /// <summary>
        /// 供应商Id
        /// </summary>
        public virtual Guid SupplierId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public virtual string SupplierName { get; set; }
        /// <summary>
        /// 供应商SPU
        /// </summary>
        public virtual string SupplierSpuId { get; set; }

    }
}
