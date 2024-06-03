using System.ComponentModel.DataAnnotations;

namespace DVDStore.Web.MVC.Areas.Store.Models
{
    public class FilmlistViewModel
    {
        [Display(Name = "Film ID")]
        public int? Fid { get; set; }

        [MaxLength(255)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [MaxLength(25)]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Display(Name = "Length")]
        public short? Length { get; set; }

        [MaxLength(10)]
        [Display(Name = "Rating")]
        public string Rating { get; set; }

        [MaxLength(91)]
        [Display(Name = "Actors")]
        public string Actors { get; set; }
    }
}
