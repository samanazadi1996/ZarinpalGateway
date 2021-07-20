using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ZarinpalGateway.Utilitis
{
    public static class EnumExtensions
    {
        public static string ToDisplay(this Enum value)
        {
            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty("Name").GetValue(attribute, null);
            return propValue.ToString();
        }
    }
}
