using System.Collections.Generic;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Models
{
    public class FilmsPagedModel
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public IEnumerable<DetailsFilmModel> Films { get; set; }
    }
}