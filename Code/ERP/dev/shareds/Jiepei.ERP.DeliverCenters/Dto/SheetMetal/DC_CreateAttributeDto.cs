using System;
using System.Collections.Generic;
using System.Text;

namespace Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal
{
    public  class DC_CreateAttributeDto
    {       
        /// <summary>
        /// 参数名
        /// </summary>
        public virtual string AttributeName { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public virtual string AttributeValue { get; set; }
        /// <summary>
        /// 参数 Id
        /// </summary>
        public virtual string AttributeValueId { get; set; }
    }
}
