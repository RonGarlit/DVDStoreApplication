
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // inventory
    public partial class Inventory
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Inventoryid")]
        public int Inventoryid { get; set; } // inventoryid (Primary key)

        [Required]
        [Display(Name = "Filmid")]
        public int Filmid { get; set; } // filmid

        [Required]
        [Display(Name = "Storeid")]
        public int Storeid { get; set; } // storeid

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Reverse navigation

        /// <summary>
        /// Child Rentals where [rental].[inventoryid] point to this entity (fkrentalinventory)
        /// </summary>
        public ICollection<Rental> Rentals { get; set; } // rental.fkrentalinventory

        // Foreign keys

        /// <summary>
        /// Parent Film pointed by [inventory].([Filmid]) (fkinventoryfilm)
        /// </summary>
        public Film Film { get; set; } // fkinventoryfilm

        /// <summary>
        /// Parent Store pointed by [inventory].([Storeid]) (fkinventorystore)
        /// </summary>
        public Store Store { get; set; } // fkinventorystore

        public Inventory()
        {
            Lastupdate = DateTime.Now;
            Rentals = new List<Rental>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
