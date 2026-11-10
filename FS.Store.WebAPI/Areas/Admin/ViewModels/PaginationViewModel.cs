namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class PaginationViewModel
    {
        public long TotalItems { get; set; }

        public int QttPerPage { get; set; } = 10;

        public int CurrentPage { get; set; } = 1;

        public int LastPage => Math.Max(1, (int)Math.Ceiling((double)TotalItems / Math.Max(1, QttPerPage)));

        public int FirstPage => 1;

        public int PreviousPage => Math.Max(FirstPage, CurrentPage - 1);

        public int NextPage => Math.Min(LastPage, CurrentPage + 1);

        public long FirstItem => TotalItems == 0 ? 0 : ((long)(CurrentPage - 1) * QttPerPage) + 1;

        public long LastItem => Math.Min((long)CurrentPage * QttPerPage, TotalItems);

        public bool HasPreviousPage => CurrentPage > FirstPage;

        public bool HasNextPage => CurrentPage < LastPage;

        public IEnumerable<int> PreviousPages(int quantity = 2)
        {
            return Enumerable.Range(Math.Max(FirstPage, CurrentPage - quantity), Math.Min(quantity, CurrentPage - FirstPage));
        }

        public IEnumerable<int> NextPages(int quantity = 2)
        {
            return Enumerable.Range(CurrentPage + 1, Math.Min(quantity, LastPage - CurrentPage));
        }
    }
}
