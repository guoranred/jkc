using System;

namespace Jiepei.ERP.Orders.Admin.Application.Contracts.Materials.Dtos
{
    public class MaterialSupplierDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 材料id
        /// </summary>
        public Guid MaterialId { get; set; }
        /// <summary>
        /// 供应商Id
        /// </summary>
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 供应商SPU
        /// </summary>
        public string SupplierSpuId { get; set; }
    }
}
