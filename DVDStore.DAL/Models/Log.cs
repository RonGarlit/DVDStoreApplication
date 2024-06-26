
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // logs
    public partial class Log
    {
        [Key, Column(Order = 1)]
        [Required]
        [Display(Name = "Logid")]
        public int Logid { get; set; } // logid (Primary key)

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Level")]
        public string Level { get; set; } // level

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Callsite")]
        public string Callsite { get; set; } // callsite

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Type")]
        public string Type { get; set; } // type

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Message")]
        public string Message { get; set; } // message

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Stacktrace")]
        public string Stacktrace { get; set; } // stacktrace

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Innerexception")]
        public string Innerexception { get; set; } // innerexception

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Additionalinfo")]
        public string Additionalinfo { get; set; } // additionalinfo

        [Required]
        [Display(Name = "Loggedondate")]
        public DateTime Loggedondate { get; set; } // loggedondate

        public Log()
        {
            Loggedondate = DateTime.Now;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
