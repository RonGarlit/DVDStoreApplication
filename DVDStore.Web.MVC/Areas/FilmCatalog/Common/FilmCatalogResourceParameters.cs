namespace DVDStore.Web.MVC.Areas.FilmCatalog.Common
{
    public class FilmCatalogResourceParameters
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        // This class includes properties for pagination (PageNumber and PageSize),
        // a search query (SearchQuery), filtering by genre (Genre) and rating (Rating),
        // and sorting (SortOrder).

        public string SearchQuery { get; set; }
        public string Genre { get; set; }
        public string Rating { get; set; }
        public string SortOrder { get; set; } = "Title"; // Default sort order
    }
}