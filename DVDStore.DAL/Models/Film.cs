
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // film
    public partial class Film
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Filmid")]
        public int Filmid { get; set; } // filmid (Primary key)

        [MaxLength(255)]
        [StringLength(255)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Title")]
        public string Title { get; set; } // title (length: 255)

        [Display(Name = "Description")]
        public string Description { get; set; } // description

        [MaxLength(4)]
        [StringLength(4)]
        [Display(Name = "Releaseyear")]
        public string Releaseyear { get; set; } // releaseyear (length: 4)

        [Required]
        [Display(Name = "Languageid")]
        public byte Languageid { get; set; } // languageid

        [Display(Name = "Originallanguageid")]
        public byte? Originallanguageid { get; set; } // originallanguageid

        [Required]
        [Display(Name = "Rentalduration")]
        public byte Rentalduration { get; set; } // rentalduration

        [Required]
        [Display(Name = "Rentalrate")]
        public decimal Rentalrate { get; set; } // rentalrate

        [Display(Name = "Length")]
        public short? Length { get; set; } // length

        [Required]
        [Display(Name = "Replacementcost")]
        public decimal Replacementcost { get; set; } // replacementcost

        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Rating")]
        public string Rating { get; set; } // rating (length: 10)

        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "Specialfeatures")]
        public string Specialfeatures { get; set; } // specialfeatures (length: 255)

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Reverse navigation

        /// <summary>
        /// Child Filmactors where [filmactor].[filmid] point to this entity (fkfilmactorfilm)
        /// </summary>
        public ICollection<Filmactor> Filmactors { get; set; } // filmactor.fkfilmactorfilm

        /// <summary>
        /// Child Filmcategories where [filmcategory].[filmid] point to this entity (fkfilmcategoryfilm)
        /// </summary>
        public ICollection<Filmcategory> Filmcategories { get; set; } // filmcategory.fkfilmcategoryfilm

        /// <summary>
        /// Child Inventories where [inventory].[filmid] point to this entity (fkinventoryfilm)
        /// </summary>
        public ICollection<Inventory> Inventories { get; set; } // inventory.fkinventoryfilm

        // Foreign keys

        /// <summary>
        /// Parent Language pointed by [film].([Languageid]) (fkfilmlanguage)
        /// </summary>
        public Language Language_Languageid { get; set; } // fkfilmlanguage

        /// <summary>
        /// Parent Language pointed by [film].([Originallanguageid]) (fkfilmlanguageoriginal)
        /// </summary>
        public Language Originallanguage { get; set; } // fkfilmlanguageoriginal

        public Film()
        {
            Rentalduration = 3;
            Rentalrate = 4.99m;
            Replacementcost = 19.99m;
            Rating = "G";
            Lastupdate = DateTime.Now;
            Filmactors = new List<Filmactor>();
            Filmcategories = new List<Filmcategory>();
            Inventories = new List<Inventory>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
