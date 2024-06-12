
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // filmcategory
    public partial class Filmcategory
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Filmid")]
        public int Filmid { get; set; } // filmid (Primary key)

        [Key, Column(Order = 2)]
        [Required]
        [Display(Name = "Categoryid")]
        public byte Categoryid { get; set; } // categoryid (Primary key)

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Foreign keys

        /// <summary>
        /// Parent Category pointed by [filmcategory].([Categoryid]) (fkfilmcategorycategory)
        /// </summary>
        public Category Category { get; set; } // fkfilmcategorycategory

        /// <summary>
        /// Parent Film pointed by [filmcategory].([Filmid]) (fkfilmcategoryfilm)
        /// </summary>
        public Film Film { get; set; } // fkfilmcategoryfilm

        public Filmcategory()
        {
            Lastupdate = DateTime.Now;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
