namespace FS.Store.Model.Elements
{
    public class Pagination<T>
    {
        public IEnumerable<T> Data { get; set; } = default!;

        public long TotalItems { get; set; }

        private int qttPerPage;
        public int QttPerPage
        {
            get { return qttPerPage == 0 ? 10 : qttPerPage; }
            set { qttPerPage = value; }
        }

        private int currentPage;
        public int CurrentPage
        {
            get { return GetCurrentPage(); }
            set { currentPage = value; }
        }

        private int GetCurrentPage()
        {
            if (currentPage == 0)
                return 1;

            int qttMaxByPage = (int)Math.Ceiling((double)TotalItems / QttPerPage);
            if (currentPage > qttMaxByPage)
                return qttMaxByPage == 0 ? 1 : qttMaxByPage;

            return currentPage;
        }

        public int FirstPage => 1;
        private int LastPage
        {
            get
            {
                var result = (int)Math.Ceiling((decimal)TotalItems / QttPerPage);
                return result == 0 ? 1 : result;
            }
        }

        public int PreviousPage => currentPage == 1 ? 1 : currentPage - 1;
        public int NextPage => currentPage == LastPage ? LastPage : currentPage + 1;

        public int Skip => (currentPage - 1) * QttPerPage;
    }
}
