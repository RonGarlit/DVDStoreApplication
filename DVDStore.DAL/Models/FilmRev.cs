
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // filmRev
    public partial class FilmRev
    {
        [Required]
        [Display(Name = "Filmid")]
        public int Filmid { get; set; } // filmid (Primary key)

        [MaxLength(255)]
        [StringLength(255)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Title")]
        public string Title { get; set; } // title (Primary key) (length: 255)

        [Display(Name = "Originallanguageid")]
        public byte? Originallanguageid { get; set; } // originallanguageid

        [Required]
        [Display(Name = "Rentalduration")]
        public byte Rentalduration { get; set; } // rentalduration (Primary key)

        [Required]
        [Display(Name = "Rentalrate")]
        public decimal Rentalrate { get; set; } // rentalrate (Primary key)

        [Display(Name = "Length")]
        public short? Length { get; set; } // length

        [Required]
        [Display(Name = "Replacementcost")]
        public decimal Replacementcost { get; set; } // replacementcost (Primary key)

        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Rating")]
        public string Rating { get; set; } // rating (length: 10)

        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "Specialfeatures")]
        public string Specialfeatures { get; set; } // specialfeatures (length: 255)

        [Required]
        [Display(Name = "Categoryid")]
        public byte Categoryid { get; set; } // categoryid (Primary key)

        [MaxLength(25)]
        [StringLength(25)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Name")]
        public string Name { get; set; } // name (Primary key) (length: 25)

        public FilmRev()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
