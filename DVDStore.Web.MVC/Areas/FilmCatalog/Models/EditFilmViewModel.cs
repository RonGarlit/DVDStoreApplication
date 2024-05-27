namespace DVDStore.Web.MVC.Areas.FilmCatalog.Models
{
    public class EditFilmViewModel
    {
        public int FilmId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public decimal RentalRate { get; set; }
        public int Length { get; set; }
        public string Rating { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}