namespace FS.Store.Model.Elements
{
    public class Pagination<T>
    {
        public IEnumerable<T?> Data { get; set; } = [];

        public long TotalItems { get; set; }

        private int qttPerPage = 10;

        public int QttPerPage
        {
            get => qttPerPage;
            set => qttPerPage = value > 0 ? value : 10;
        }

        private int currentPage = 1;

        public int CurrentPage
        {
            get
            {
                var page = Math.Max(1, currentPage);
                return Math.Min(page, LastPage);
            }
            set => currentPage = value;
        }

        public int FirstPage => 1;

        public int LastPage => Math.Max(1, (int)Math.Ceiling((double)TotalItems / QttPerPage));

        public int PreviousPage => Math.Max(FirstPage, CurrentPage - 1);

        public int NextPage => Math.Min(LastPage, CurrentPage + 1);

        public int Skip => (CurrentPage - 1) * QttPerPage;

        public long FirstItem => TotalItems == 0 ? 0 : Skip + 1;

        public long LastItem => Math.Min((long)Skip + QttPerPage, TotalItems);

        public bool HasPreviousPage => CurrentPage > FirstPage;

        public bool HasNextPage => CurrentPage < LastPage;

        public IEnumerable<int> PreviousPages(int quantity = 2)
        {
            return Enumerable.Range(
                Math.Max(FirstPage, CurrentPage - quantity),
                Math.Min(quantity, CurrentPage - FirstPage)
            );
        }

        public IEnumerable<int> NextPages(int quantity = 2)
        {
            int start = CurrentPage + 1;
            int count = Math.Min(quantity, LastPage - CurrentPage);

            return Enumerable.Range(start, count);
        }
    }
}
