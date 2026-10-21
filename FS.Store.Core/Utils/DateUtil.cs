namespace FS.Store.Core.Utils
{
    public static class DateUtil
    {
        public static bool Expired(this DateTime? date)
            => date.Value.Date < DateTime.Today;
        public static bool NotExpired(this DateTime? date)
            => date.Value.Date >= DateTime.Today;
    }
}
