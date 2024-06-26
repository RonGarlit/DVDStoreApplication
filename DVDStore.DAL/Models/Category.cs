
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // category
    public partial class Category
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Categoryid")]
        public byte Categoryid { get; set; } // categoryid (Primary key)

        [MaxLength(25)]
        [StringLength(25)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Name")]
        public string Name { get; set; } // name (length: 25)

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Reverse navigation

        /// <summary>
        /// Child Filmcategories where [filmcategory].[categoryid] point to this entity (fkfilmcategorycategory)
        /// </summary>
        public ICollection<Filmcategory> Filmcategories { get; set; } // filmcategory.fkfilmcategorycategory

        public Category()
        {
            Lastupdate = DateTime.Now;
            Filmcategories = new List<Filmcategory>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
