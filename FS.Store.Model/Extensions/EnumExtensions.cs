using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FS.Store.Model.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            if (member != null)
            {
                var display = member.GetCustomAttribute<DisplayAttribute>();

                if (display != null && display.GetName() != null)
                    return display.GetName()!;
            }

            return enumValue.ToString();
        }

        public static bool EqualsName<TEnum>(this TEnum value, string? name) where TEnum : struct, Enum
            => value.ToString().Equals(name, StringComparison.OrdinalIgnoreCase);

    }
}
