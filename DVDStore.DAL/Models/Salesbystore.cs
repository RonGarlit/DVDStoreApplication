
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // salesbystore
    public partial class Salesbystore
    {
        [Required]
        [Display(Name = "Storeid")]
        public int Storeid { get; set; } // storeid

        [MaxLength(101)]
        [StringLength(101)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Store")]
        public string Store { get; set; } // store (length: 101)

        [MaxLength(91)]
        [StringLength(91)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Manager")]
        public string Manager { get; set; } // manager (length: 91)

        [Display(Name = "Totalsales")]
        public decimal? Totalsales { get; set; } // totalsales

        public Salesbystore()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
