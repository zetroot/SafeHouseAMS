using System;
using System.ComponentModel;

namespace SafeHouseAMS.BizLayer
{
    /// <summary>
    /// Методы расширения для enum
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Get description attribute value for enum value
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>value of description attribute</returns>
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            if (field is null) return value.ToString();

            return
                Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute ?
                attribute.Description :
                value.ToString();
        }
    }
}
