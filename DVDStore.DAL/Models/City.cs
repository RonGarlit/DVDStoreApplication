
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // city
    public partial class City
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Cityid")]
        public int Cityid { get; set; } // cityid (Primary key)

        [MaxLength(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "City")]
        public string City_ { get; set; } // city (length: 50)

        [Required]
        [Display(Name = "Countryid")]
        public short Countryid { get; set; } // countryid

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Reverse navigation

        /// <summary>
        /// Child Addresses where [address].[cityid] point to this entity (fkaddresscity)
        /// </summary>
        public ICollection<Address> Addresses { get; set; } // address.fkaddresscity

        // Foreign keys

        /// <summary>
        /// Parent Country pointed by [city].([Countryid]) (fkcitycountry)
        /// </summary>
        public Country Country { get; set; } // fkcitycountry

        public City()
        {
            Lastupdate = DateTime.Now;
            Addresses = new List<Address>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
