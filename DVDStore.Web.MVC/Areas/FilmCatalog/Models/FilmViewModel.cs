using System.ComponentModel.DataAnnotations;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Models
{
    public class FilmViewModel
    {
        [Key]
        [Required]
        [Display(Name = "Film ID")]
        public int Filmid { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [MaxLength(2147483647)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [MaxLength(4)]
        [Display(Name = "Release Year")]
        [RegularExpression(@"\d{4}", ErrorMessage = "Please enter a valid four-digit year.")]
        public string Releaseyear { get; set; }

        [Required]
        [Display(Name = "Language ID")]
        public byte Languageid { get; set; }

        [Display(Name = "Original Language ID")]
        public byte? Originallanguageid { get; set; }

        [Required]
        [Display(Name = "Rental Duration")]
        public byte Rentalduration { get; set; }

        [Required]
        [Display(Name = "Rental Rate")]
        public decimal Rentalrate { get; set; }

        [Display(Name = "Length")]
        public short? Length { get; set; }

        [Required]
        [Display(Name = "Replacement Cost")]
        public decimal Replacementcost { get; set; }

        [MaxLength(10)]
        [Display(Name = "Rating")]
        public string Rating { get; set; }

        [MaxLength(255)]
        [Display(Name = "Special Features")]
        public string Specialfeatures { get; set; }

        [Required]
        [Display(Name = "Last Update")]
        public DateTime Lastupdate { get; set; }
    }
}
