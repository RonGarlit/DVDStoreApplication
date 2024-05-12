using Microsoft.AspNetCore.Identity;

namespace DVDStore.Web.MVC.Areas.Security.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}