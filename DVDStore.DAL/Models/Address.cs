
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // address
    public partial class Address
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Addressid")]
        public int Addressid { get; set; } // addressid (Primary key)

        [MaxLength(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Address")]
        public string Address_ { get; set; } // address (length: 50)

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Address 2")]
        public string Address2 { get; set; } // address2 (length: 50)

        [MaxLength(20)]
        [StringLength(20)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "District")]
        public string District { get; set; } // district (length: 20)

        [Required]
        [Display(Name = "Cityid")]
        public int Cityid { get; set; } // cityid

        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Postalcode")]
        public string Postalcode { get; set; } // postalcode (length: 10)

        [MaxLength(20)]
        [StringLength(20)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Phone")]
        public string Phone { get; set; } // phone (length: 20)

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Reverse navigation

        /// <summary>
        /// Child Customers where [customer].[addressid] point to this entity (fkcustomeraddress)
        /// </summary>
        public ICollection<Customer> Customers { get; set; } // customer.fkcustomeraddress

        /// <summary>
        /// Child Staffs where [staff].[addressid] point to this entity (fkstaffaddress)
        /// </summary>
        public ICollection<Staff> Staffs { get; set; } // staff.fkstaffaddress

        /// <summary>
        /// Child Stores where [store].[addressid] point to this entity (fkstoreaddress)
        /// </summary>
        public ICollection<Store> Stores { get; set; } // store.fkstoreaddress

        // Foreign keys

        /// <summary>
        /// Parent City pointed by [address].([Cityid]) (fkaddresscity)
        /// </summary>
        public City City { get; set; } // fkaddresscity

        public Address()
        {
            Lastupdate = DateTime.Now;
            Customers = new List<Customer>();
            Staffs = new List<Staff>();
            Stores = new List<Store>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
