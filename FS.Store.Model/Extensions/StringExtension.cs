using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;

namespace FS.Store.Model.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// valida com IsNullOrWhiteSpace
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? val)
            => !string.IsNullOrWhiteSpace(val);

        /// <summary>
        /// Valida com IsNullOrWhiteSpace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty([NotNullWhen(true)] this string? value)
            => string.IsNullOrWhiteSpace(value);

        public static bool IsNotNullOrEmpty(this StringValues val)
            => !string.IsNullOrWhiteSpace(val.ToString());

        public static bool IsNullOrEmpty(this StringValues val)
            => string.IsNullOrWhiteSpace(val.ToString());

    }
}
