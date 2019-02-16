using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day24
{
    public static class Extensions
    {
        public static string Text(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = fi.GetCustomAttribute<DescriptionAttribute>();

            // Return attribute description or call basic ToString method
            if (attribute != null && !String.IsNullOrWhiteSpace(attribute.Description))
                return attribute.Description;
            else
                return value.ToString();
        }
    }
}
