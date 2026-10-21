namespace FS.Store.Core.Extension
{
    public static class StringExtension
    {
        public static bool IsNotNullAndNotEmpty(this string? val)
        {
            return !string.IsNullOrEmpty(val);
        }

        public static bool IsNullOrEmpty(this string? value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
