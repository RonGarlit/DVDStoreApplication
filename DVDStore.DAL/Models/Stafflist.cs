
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // stafflist
    public partial class Stafflist
    {
        [Required]
        [Display(Name = "Id")]
        public byte Id { get; set; } // ID

        [MaxLength(91)]
        [StringLength(91)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Name")]
        public string Name { get; set; } // name (length: 91)

        [MaxLength(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Address")]
        public string Address { get; set; } // address (length: 50)

        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Zipcode")]
        public string Zipcode { get; set; } // zipcode (length: 10)

        [MaxLength(20)]
        [StringLength(20)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Phone")]
        public string Phone { get; set; } // phone (length: 20)

        [MaxLength(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "City")]
        public string City { get; set; } // city (length: 50)

        [MaxLength(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Country")]
        public string Country { get; set; } // country (length: 50)

        [Required]
        [Display(Name = "Sid")]
        public int Sid { get; set; } // SID

        public Stafflist()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
