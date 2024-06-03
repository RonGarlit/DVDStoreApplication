namespace DVDStore.Web.MVC.Areas.Store.Common
{
    public class StoreResourceParameters
    {
        private const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public string SearchQuery { get; set; }
        public string Category { get; set; }
        public string Rating { get; set; }
        public string SortOrder { get; set; }
    }
}
