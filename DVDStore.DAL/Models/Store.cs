
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // store
    public partial class Store
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Storeid")]
        public int Storeid { get; set; } // storeid (Primary key)

        [Required]
        [Display(Name = "Managerstaffid")]
        public byte Managerstaffid { get; set; } // managerstaffid

        [Required]
        [Display(Name = "Addressid")]
        public int Addressid { get; set; } // addressid

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Reverse navigation

        /// <summary>
        /// Child Customers where [customer].[storeid] point to this entity (fkcustomerstore)
        /// </summary>
        public ICollection<Customer> Customers { get; set; } // customer.fkcustomerstore

        /// <summary>
        /// Child Inventories where [inventory].[storeid] point to this entity (fkinventorystore)
        /// </summary>
        public ICollection<Inventory> Inventories { get; set; } // inventory.fkinventorystore

        /// <summary>
        /// Child Staffs where [staff].[storeid] point to this entity (fkstaffstore)
        /// </summary>
        public ICollection<Staff> Staffs { get; set; } // staff.fkstaffstore

        // Foreign keys

        /// <summary>
        /// Parent Address pointed by [store].([Addressid]) (fkstoreaddress)
        /// </summary>
        public Address Address { get; set; } // fkstoreaddress

        /// <summary>
        /// Parent Staff pointed by [store].([Managerstaffid]) (fkstorestaff)
        /// </summary>
        public Staff Staff { get; set; } // fkstorestaff

        public Store()
        {
            Lastupdate = DateTime.Now;
            Customers = new List<Customer>();
            Inventories = new List<Inventory>();
            Staffs = new List<Staff>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
