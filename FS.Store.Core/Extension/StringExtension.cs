namespace FS.Store.Core.Extension
{
    public static class StringExtension
    {
        public static bool IsNotNullOrEmpty(this string? val)
            => !string.IsNullOrWhiteSpace(val);

        public static bool IsNullOrEmpty(this string? value)
            => string.IsNullOrWhiteSpace(value);
    }
}
