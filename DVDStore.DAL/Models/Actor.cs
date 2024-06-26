
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // ****************************************************************************************************
    // DVDStore DAL Code
    // ****************************************************************************************************

    // actor
    public partial class Actor
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Actorid")]
        public int Actorid { get; set; } // actorid (Primary key)

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
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Reverse navigation

        /// <summary>
        /// Child Filmactors where [filmactor].[actorid] point to this entity (fkfilmactoractor)
        /// </summary>
        public ICollection<Filmactor> Filmactors { get; set; } // filmactor.fkfilmactoractor

        public Actor()
        {
            Lastupdate = DateTime.Now;
            Filmactors = new List<Filmactor>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
