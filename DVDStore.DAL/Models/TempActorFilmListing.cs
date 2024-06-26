
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // tempActorFilmListing
    public partial class TempActorFilmListing
    {
        [Required]
        [Display(Name = "Actor ID")]
        public int ActorId { get; set; } // ActorId (Primary key)

        [MaxLength(45)]
        [StringLength(45)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "First name")]
        public string FirstName { get; set; } // FirstName (Primary key) (length: 45)

        [MaxLength(45)]
        [StringLength(45)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Last name")]
        public string LastName { get; set; } // LastName (Primary key) (length: 45)

        [Required]
        [Display(Name = "Film ID")]
        public int FilmId { get; set; } // FilmId (Primary key)

        [MaxLength(255)]
        [StringLength(255)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Film title")]
        public string FilmTitle { get; set; } // FilmTitle (Primary key) (length: 255)

        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Rating")]
        public string Rating { get; set; } // Rating (length: 10)

        public TempActorFilmListing()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
