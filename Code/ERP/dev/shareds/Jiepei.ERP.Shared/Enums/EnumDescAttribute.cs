using System;

namespace Jiepei.ERP
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class EnumDescAttribute : Attribute
    {
        public string Desc { get; set; }

        public EnumDescAttribute(string desc)
        {
            this.Desc = desc;
        }
    }
}
