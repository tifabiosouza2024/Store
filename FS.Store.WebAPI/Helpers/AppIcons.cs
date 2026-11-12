namespace FS.Store.WebAPI.Helpers
{
    public static class AppIcons
    {
        private static readonly Dictionary<string, string> SvgIcons = new()
        {
            ["expense"] = "0 0 512 512",
            ["cash-register"] = "0 0 24 24"
        };

        public static bool TryGetViewBox(string name, out string viewBox)
            => SvgIcons.TryGetValue(name, out viewBox);
    }
}
