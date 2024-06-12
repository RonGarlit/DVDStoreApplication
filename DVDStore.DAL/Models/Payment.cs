
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // payment
    public partial class Payment
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Paymentid")]
        public int Paymentid { get; set; } // paymentid (Primary key)

        [Required]
        [Display(Name = "Customerid")]
        public int Customerid { get; set; } // customerid

        [Required]
        [Display(Name = "Staffid")]
        public byte Staffid { get; set; } // staffid

        [Display(Name = "Rentalid")]
        public int? Rentalid { get; set; } // rentalid

        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; } // amount

        [Required]
        [Display(Name = "Paymentdate")]
        public DateTime Paymentdate { get; set; } // paymentdate

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Foreign keys

        /// <summary>
        /// Parent Customer pointed by [payment].([Customerid]) (fkpaymentcustomer)
        /// </summary>
        public Customer Customer { get; set; } // fkpaymentcustomer

        /// <summary>
        /// Parent Rental pointed by [payment].([Rentalid]) (fkpaymentrental)
        /// </summary>
        public Rental Rental { get; set; } // fkpaymentrental

        /// <summary>
        /// Parent Staff pointed by [payment].([Staffid]) (fkpaymentstaff)
        /// </summary>
        public Staff Staff { get; set; } // fkpaymentstaff

        public Payment()
        {
            Lastupdate = DateTime.Now;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
