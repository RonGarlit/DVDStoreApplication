
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // staff
    public partial class Staff
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Staffid")]
        public byte Staffid { get; set; } // staffid (Primary key)

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

        [Required]
        [Display(Name = "Addressid")]
        public int Addressid { get; set; } // addressid

        [MaxLength(2147483647)]
        [Display(Name = "Picture")]
        public byte[] Picture { get; set; } // picture (length: 2147483647)

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; } // email (length: 50)

        [Required]
        [Display(Name = "Storeid")]
        public int Storeid { get; set; } // storeid

        [Required]
        [Display(Name = "Active")]
        public bool Active { get; set; } // active

        [MaxLength(16)]
        [StringLength(16)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Username")]
        public string Username { get; set; } // username (length: 16)

        [MaxLength(40)]
        [StringLength(40)]
        [Display(Name = "Password")]
        public string Password { get; set; } // password (length: 40)

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Reverse navigation

        /// <summary>
        /// Child Payments where [payment].[staffid] point to this entity (fkpaymentstaff)
        /// </summary>
        public ICollection<Payment> Payments { get; set; } // payment.fkpaymentstaff

        /// <summary>
        /// Child Rentals where [rental].[staffid] point to this entity (fkrentalstaff)
        /// </summary>
        public ICollection<Rental> Rentals { get; set; } // rental.fkrentalstaff

        /// <summary>
        /// Child Stores where [store].[managerstaffid] point to this entity (fkstorestaff)
        /// </summary>
        public ICollection<Store> Stores { get; set; } // store.fkstorestaff

        // Foreign keys

        /// <summary>
        /// Parent Address pointed by [staff].([Addressid]) (fkstaffaddress)
        /// </summary>
        public Address Address { get; set; } // fkstaffaddress

        /// <summary>
        /// Parent Store pointed by [staff].([Storeid]) (fkstaffstore)
        /// </summary>
        public Store Store { get; set; } // fkstaffstore

        public Staff()
        {
            Active = true;
            Lastupdate = DateTime.Now;
            Payments = new List<Payment>();
            Rentals = new List<Rental>();
            Stores = new List<Store>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
