
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // language
    public partial class Language
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Languageid")]
        public byte Languageid { get; set; } // languageid (Primary key)

        [MaxLength(20)]
        [StringLength(20)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Name")]
        public string Name { get; set; } // name (length: 20)

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Reverse navigation

        /// <summary>
        /// Child Films where [film].[languageid] point to this entity (fkfilmlanguage)
        /// </summary>
        public ICollection<Film> Films_Languageid { get; set; } // film.fkfilmlanguage

        /// <summary>
        /// Child Films where [film].[originallanguageid] point to this entity (fkfilmlanguageoriginal)
        /// </summary>
        public ICollection<Film> Films_Originallanguageid { get; set; } // film.fkfilmlanguageoriginal

        public Language()
        {
            Lastupdate = DateTime.Now;
            Films_Languageid = new List<Film>();
            Films_Originallanguageid = new List<Film>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
