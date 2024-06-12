
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // salesbyfilmcategory
    public partial class Salesbyfilmcategory
    {
        [MaxLength(25)]
        [StringLength(25)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Category")]
        public string Category { get; set; } // category (length: 25)

        [Display(Name = "Totalsales")]
        public decimal? Totalsales { get; set; } // totalsales

        public Salesbyfilmcategory()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
