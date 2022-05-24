using System;
using Volo.Abp.Domain.Entities.Auditing;


namespace Jiepei.ERP.Orders.Materials
{
    public class D3Material : AuditedAggregateRoot<Guid>
    {
        protected D3Material() { }
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public virtual string PartCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public virtual string Category { get; set; }
        /// <summary>
        /// 密度
        /// </summary>
        public virtual string Density { get; set; }
        /// <summary>
        /// 交期(天)
        /// </summary>
        public virtual int? Delivery { get; set; }
        /// <summary>
        /// 特性
        /// </summary>
        public virtual string Attr { get; set; }
        /// <summary>
        /// 优点
        /// </summary>
        public virtual string Excellence { get; set; }
        /// <summary>
        /// 缺点
        /// </summary>
        public virtual string Short { get; set; }
        /// <summary>
        /// 颜色(对应内贸可选类型) 是否枚举？
        /// </summary>
        public virtual string Color { get; set; }
        /// <summary>
        /// 最小单件重量
        /// </summary>
        public virtual decimal? MinSinWeight { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Note { get; set; }


        public D3Material(
            string code
            , string partCode
            , string name
            , string category
            , string density
            , string attr
            , string excellence
            , string shorts
            , string color
            , decimal? minSinWeight
            , string note)
        {
            Code = code;
            PartCode = partCode;
            Name = name;
            Category = category;
            Density = density;
            Attr = attr;
            Excellence = excellence;
            Short = shorts;
            Color = color;
            MinSinWeight = minSinWeight;
            Note = note;
        }
    }

}
