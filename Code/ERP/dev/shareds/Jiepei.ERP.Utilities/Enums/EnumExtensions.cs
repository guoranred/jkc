using System;
using System.ComponentModel;
using System.Reflection;

namespace Jiepei.ERP.Utilities
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fd = type.GetField(value.ToString());
            if (fd == null)
                return string.Empty;
            object[] attrs = fd.GetCustomAttributes(typeof(EnumDescAttribute), false);
            string name = string.Empty;
            foreach (EnumDescAttribute attr in attrs)
            {
                name = attr.Desc;
            }
            return name;
        }

        public static string GetDescriptionV2(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fd = type.GetField(value.ToString());
            if (fd == null)
                return string.Empty;
            object[] attrs = fd.GetCustomAttributes(typeof(DescriptionAttribute), false);
            string name = string.Empty;
            foreach (DescriptionAttribute attr in attrs)
            {
                name = attr.Description;
            }
            return name;
        }
    }
}
