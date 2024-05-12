using Microsoft.AspNetCore.Mvc.Rendering;
using DVDStore.Web.MVC.Areas.Identity.Data;

namespace DVDStore.Web.MVC.Areas.Security.Models
{
    public class DetailsUserModel
    {
        public ApplicationUser? User { get; set; }

        public IList<SelectListItem>? Roles { get; set; }
    }
}
