using System;

namespace Jiepei.ERP.Orders.Admin.Application.Contracts.Materials.Dtos
{
    public class CreateMaterialSupplierDto
    {
        /// <summary>
        /// 材料id
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 供应商id
        /// </summary>
        public Guid SupplierId { get; set; }

        /// <summary>
        /// 供应商材料id
        /// </summary>
        public string SupplierSpuId { get; set; }
    }
}
