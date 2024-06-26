
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // country
    public partial class Country
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Countryid")]
        public short Countryid { get; set; } // countryid (Primary key)

        [MaxLength(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Country")]
        public string Country_ { get; set; } // country (length: 50)

        [Display(Name = "Lastupdate")]
        public DateTime? Lastupdate { get; set; } // lastupdate

        // Reverse navigation

        /// <summary>
        /// Child Cities where [city].[countryid] point to this entity (fkcitycountry)
        /// </summary>
        public ICollection<City> Cities { get; set; } // city.fkcitycountry

        public Country()
        {
            Lastupdate = DateTime.Now;
            Cities = new List<City>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
