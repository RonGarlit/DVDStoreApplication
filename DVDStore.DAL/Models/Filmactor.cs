
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // filmactor
    public partial class Filmactor
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Actorid")]
        public int Actorid { get; set; } // actorid (Primary key)

        [Key, Column(Order = 2)]
        [Required]
        [Display(Name = "Filmid")]
        public int Filmid { get; set; } // filmid (Primary key)

        [Required]
        [Display(Name = "Lastupdate")]
        public DateTime Lastupdate { get; set; } // lastupdate

        // Foreign keys

        /// <summary>
        /// Parent Actor pointed by [filmactor].([Actorid]) (fkfilmactoractor)
        /// </summary>
        public Actor Actor { get; set; } // fkfilmactoractor

        /// <summary>
        /// Parent Film pointed by [filmactor].([Filmid]) (fkfilmactorfilm)
        /// </summary>
        public Film Film { get; set; } // fkfilmactorfilm

        public Filmactor()
        {
            Lastupdate = DateTime.Now;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
