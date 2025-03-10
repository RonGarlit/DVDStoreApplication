
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // rental
    public partial class Rental
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Rentalid")]
        public int Rentalid { get; set; } // rentalid (Primary key)

        [Required]
        [Display(Name = "Rentaldate")]
        public DateTime Rentaldate { get; set; } // rentaldate

        [Required]
        [Display(Name = "Inventoryid")]
        public int Inventoryid { get; set; } // inventoryid

        [Required]
        [Display(Name = "Customerid")]
        public int Customerid { get; set; } // customerid

        [Display(Name = "Returndate")]
        public DateTime? Returndate { get; set; } // returndate

        [Required]
        [Display(Name = "Staffid")]
        public byte Staffid { get; set; } // staffid

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Reverse navigation

        /// <summary>
        /// Child Payments where [payment].[rentalid] point to this entity (fkpaymentrental)
        /// </summary>
        public ICollection<Payment> Payments { get; set; } // payment.fkpaymentrental

        // Foreign keys

        /// <summary>
        /// Parent Customer pointed by [rental].([Customerid]) (fkrentalcustomer)
        /// </summary>
        public Customer Customer { get; set; } // fkrentalcustomer

        /// <summary>
        /// Parent Inventory pointed by [rental].([Inventoryid]) (fkrentalinventory)
        /// </summary>
        public Inventory Inventory { get; set; } // fkrentalinventory

        /// <summary>
        /// Parent Staff pointed by [rental].([Staffid]) (fkrentalstaff)
        /// </summary>
        public Staff Staff { get; set; } // fkrentalstaff

        public Rental()
        {
            Lastupdate = DateTime.Now;
            Payments = new List<Payment>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
