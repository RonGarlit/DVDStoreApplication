
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // customer
    public partial class Customer
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Customerid")]
        public int Customerid { get; set; } // customerid (Primary key)

        [Required]
        [Display(Name = "Storeid")]
        public int Storeid { get; set; } // storeid

        [MaxLength(45)]
        [StringLength(45)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; } // firstname (length: 45)

        [MaxLength(45)]
        [StringLength(45)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; } // lastname (length: 45)

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; } // email (length: 50)

        [Required]
        [Display(Name = "Addressid")]
        public int Addressid { get; set; } // addressid

        [MaxLength(1)]
        [StringLength(1)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Active")]
        public string Active { get; set; } // active (length: 1)

        [Required]
        [Display(Name = "Createdate")]
        public DateTime Createdate { get; set; } // createdate

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Reverse navigation

        /// <summary>
        /// Child Payments where [payment].[customerid] point to this entity (fkpaymentcustomer)
        /// </summary>
        public ICollection<Payment> Payments { get; set; } // payment.fkpaymentcustomer

        /// <summary>
        /// Child Rentals where [rental].[customerid] point to this entity (fkrentalcustomer)
        /// </summary>
        public ICollection<Rental> Rentals { get; set; } // rental.fkrentalcustomer

        // Foreign keys

        /// <summary>
        /// Parent Address pointed by [customer].([Addressid]) (fkcustomeraddress)
        /// </summary>
        public Address Address { get; set; } // fkcustomeraddress

        /// <summary>
        /// Parent Store pointed by [customer].([Storeid]) (fkcustomerstore)
        /// </summary>
        public Store Store { get; set; } // fkcustomerstore

        public Customer()
        {
            Active = "Y";
            Createdate = DateTime.Now;
            Lastupdate = DateTime.Now;
            Payments = new List<Payment>();
            Rentals = new List<Rental>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
