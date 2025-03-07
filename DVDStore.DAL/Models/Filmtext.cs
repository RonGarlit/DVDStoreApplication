
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // filmtext
    public partial class Filmtext
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Filmid")]
        public short Filmid { get; set; } // filmid (Primary key)

        [MaxLength(255)]
        [StringLength(255)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Title")]
        public string Title { get; set; } // title (length: 255)

        [Display(Name = "Description")]
        public string Description { get; set; } // description

        public Filmtext()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
