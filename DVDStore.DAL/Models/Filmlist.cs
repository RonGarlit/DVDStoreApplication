
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // filmlist
    public partial class Filmlist
    {
        [Display(Name = "Fid")]
        public int? Fid { get; set; } // FID

        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "Title")]
        public string Title { get; set; } // title (length: 255)

        [Display(Name = "Description")]
        public string Description { get; set; } // description

        [MaxLength(25)]
        [StringLength(25)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Category")]
        public string Category { get; set; } // category (length: 25)

        [Display(Name = "Price")]
        public decimal? Price { get; set; } // price

        [Display(Name = "Length")]
        public short? Length { get; set; } // length

        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Rating")]
        public string Rating { get; set; } // rating (length: 10)

        [MaxLength(91)]
        [StringLength(91)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Actors")]
        public string Actors { get; set; } // actors (length: 91)

        public Filmlist()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
